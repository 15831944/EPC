using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using Config;
using System.Data;
using RTXSAPILib;
namespace RTX_SendMsgAndTaskTool
{
    class SendMsgAndTaskService
    {
        #region 共有变量

        RTXSAPIRootObj RootObj = new RTXSAPIRootObj();
        public static SQLHelper baseSqlHelper;
        public static SQLHelper workFlowHelper;
        public static string strdelayTime;
        public static string webAddress;
        RTXSAPIUserManager userManager = null;
        string logSQL = string.Empty;

        #endregion

        #region 向RTX发送消息

        public void StartSendNotify()
        {
            userManager = RootObj.UserManager;
            //UserService userService = new UserService();
            var userService = FormulaHelper.GetService<IUserService>();
            string msgSQL = @" select * from S_E_RTXSynchNoticeAndTask where State='Wait'
                               union select * from {0}.dbo.S_E_RTXSynchNoticeAndTask where State='Wait'";
            msgSQL = string.Format(msgSQL, baseSqlHelper.DbName);
            DataTable msgDt = workFlowHelper.ExecuteDataTable(msgSQL);
            var msgRows = msgDt.Rows;
            //将本次同步的消息置为"Processing"
            var Ids = string.Empty;
            foreach (DataRow msgRow in msgRows)
                Ids += msgRow["ID"].ToString() + ",";
            if (!string.IsNullOrEmpty(Ids))
                Ids = Ids.TrimEnd(',');
            string updateSQL = @" update S_E_RTXSynchNoticeAndTask set State = 'Processing' where ID in ('" + Ids.Replace(",", "','") + "')";
            workFlowHelper.ExecuteNonQuery(updateSQL);
            baseSqlHelper.ExecuteNonQuery(updateSQL);

            //循环将BE用户信息同步至RTX
            foreach (DataRow msgRow in msgRows)
            {
                //主键ID
                var id = msgRow["ID"].ToString();
                //消息ID
                var taskOrMsgID = msgRow["TaskExecIDOrMsgID"].ToString();

                //标题
                var title = msgRow["Title"] == null ? "" : msgRow["Title"].ToString();
                //内容
                var content = msgRow["Content"] == null ? title : msgRow["Content"].ToString();
                //LinkURL
                var linkURL = msgRow["LinkURL"] == null ? "" : msgRow["LinkURL"].ToString();
                //OwnerUserID
                var userID = msgRow["OwnerUserID"].ToString();
                //系统名称
                UserInfo userInfo = userService.GetUserInfoByID(userID);
                var systemName = userInfo.Code;
                systemName = systemName.TrimEnd(' ').TrimStart(' ');

                if (!userManager.IsUserExist(systemName))   //判断用户是否存在于RTX
                {
                    //用户不存在记录异常信息
                    updateSQL = @" update S_E_RTXSynchNoticeAndTask set State = 'Fail' where ID='" + id + "'  ";
                    workFlowHelper.ExecuteNonQuery(updateSQL);
                    logSQL = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                    logSQL = string.Format(logSQL, FormulaHelper.CreateGuid(), "WorkFlow", "", "Added", "给用户" + systemName + "发送提醒失败(提醒ID:" + id + ")"
                         , "失败时间：" + DateTime.Now, "失败详细信息：用户" + systemName + "不存在", "", "", DateTime.Now, "", "");
                    baseSqlHelper.ExecuteNonQuery(logSQL);
                    continue;

                }
                //创建日期
                var strCreateDate = msgRow["CreateDate"].ToString();
                DateTime createdate = Convert.ToDateTime(strCreateDate);
                //流程ID
                var insFlowID = msgRow["InsFlowID"] == null ? "" : msgRow["InsFlowID"].ToString();
                //FormID
                var formID = msgRow["FormID"] == null ? "" : msgRow["FormID"].ToString();
                //数据类型
                var dataType = msgRow["DataType"].ToString();

                if (dataType == "TASK") //任务
                {
                    //如果提醒信息未任务，则添加判断任务是否已经执行过的逻辑。
                    string taskSql = @" select ExecTime from S_WF_InsTaskExec where ID = '" + taskOrMsgID + "' and ExecTime is null";
                    DataTable taskDt = workFlowHelper.ExecuteDataTable(taskSql);
                    if (taskDt == null || taskDt.Rows.Count == 0)
                    {
                        updateSQL = @" update S_E_RTXSynchNoticeAndTask set State = 'Fail',ErrorMsg='任务已经被执行过了' where ID='" + id + "'  ";
                        workFlowHelper.ExecuteNonQuery(updateSQL);
                        logSQL = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                        logSQL = string.Format(logSQL, FormulaHelper.CreateGuid(), "WorkFlow", "", "Added", "给用户" + systemName + "发送任务(ID:" + id + ")失败"
                             , "失败时间：" + DateTime.Now, "失败详细信息：任务已经被执行过了。", "", "", DateTime.Now, "", "");
                        baseSqlHelper.ExecuteNonQuery(logSQL);
                        continue;
                    }

                    if (linkURL.IndexOf("?") >= 0)
                        linkURL += "&ID=" + formID + "&TaskExecID=" + taskOrMsgID + "&TaskStatus=Processing";
                    else
                        linkURL += "?ID=" + formID + "&TaskExecID=" + taskOrMsgID + "&TaskStatus=Processing";
                }

                //添加WebAddress
                linkURL = webAddress + linkURL;
                //增加模拟登录
                if (!string.IsNullOrEmpty(linkURL))
                {
                    if (linkURL.IndexOf("?") >= 0)
                        linkURL += "&SystemName=" + systemName;
                    else
                        linkURL += "?SystemName=" + systemName;
                }
                //发送者
                var sendUserIDs = msgRow["SendUserIDs"] == null ? "" : msgRow["SendUserIDs"].ToString();
                var senderUserNames = msgRow["SendUserNames"] == null ? "" : msgRow["SendUserNames"].ToString();

                //处理标题和内容
                title = title.Replace("[", "");
                title = title.Replace("]", "");
                content = content.Replace("[", "");
                content = content.Replace("]", "");
                if (title.Length > 14)
                {
                    title = title.Substring(0, 13) + "......";
                }
                if (content.Length > 34)
                {
                    content = content.Substring(0, 33) + "......";
                }

                //消息主体
                //string txtMsgContent = " 标  题：" + title + "\r\n概  要：" + content + "\r\n发送者：" + senderUserNames
                //       + "\r\n\r\n[查看详细|" + linkURL + "] ";
                string txtMsgContent = " 标  题：" + title + "\r\n概  要：" + content + "\r\n发送者：" + senderUserNames
                       + "\r\n\r\n ";
                int servertime = Convert.ToInt32(strdelayTime);
                servertime = servertime * 1000;

                try
                {
                    if (dataType == "MSG")
                        RootObj.SendNotify(systemName, "消息提醒", servertime, txtMsgContent);
                    else
                        RootObj.SendNotify(systemName, "任务提醒", servertime, txtMsgContent);

                }
                catch (Exception exp)
                {
                    updateSQL = @" update S_E_RTXSynchNoticeAndTask set State = 'Fail' where ID='" + id + "'  ";
                    workFlowHelper.ExecuteNonQuery(updateSQL);
                    logSQL = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                    logSQL = string.Format(logSQL, FormulaHelper.CreateGuid(), "WorkFlow", "", "Added", "给用户" + systemName + "发送提醒失败(提醒ID(" + id + "))"
                         , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message.ToString(), "", "", DateTime.Now, "", "");
                    baseSqlHelper.ExecuteNonQuery(logSQL);
                    continue;
                }
                updateSQL = @" update S_E_RTXSynchNoticeAndTask set State = 'Finish',ExecTime='" + DateTime.Now + "' where ID='" + id + "'  ";
                workFlowHelper.ExecuteNonQuery(updateSQL);

            }

        }

        #endregion

    }
}
