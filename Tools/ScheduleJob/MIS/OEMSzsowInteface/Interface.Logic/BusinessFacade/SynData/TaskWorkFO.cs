using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class TaskWorkFO : SynDataFO
    {
        public void CreateTaskWorkQueue()
        {
            var sourceSql = @"select tw.*,wbs.Name as SubProjectWBSName from S_W_TaskWork tw 
left join S_I_ProjectInfo p on p.ID = tw.ProjectInfoID 
left join S_W_WBS wbs with(nolock) on  CHARINDEX(wbs.ID,tw.WBSFullID,1)>0 and wbs.WBSType='SubProject'
where 1=1";
            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                sourceSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_W_TaskWork>("select * from S_W_TaskWork");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and tw.ModifyDate >='" + startDate + "' and tw.ModifyDate <='" + endDate + "'";
            }

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "Task/Delete";
            var alldtSql = @"select m.WBSID from S_W_TaskWork m left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1 ";
            if (!string.IsNullOrEmpty(synProjectMode))
                alldtSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(alldtSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["WBSID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_W_TaskWork", item.ID,""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            #region Edit

            var saveUrl = this.BaseServerUrl + "project/edittaskpackage";
            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);
                //设置参数

                var param = new TaskWorkRequestData();
                param.projectId = dic.GetValue("ProjectInfoID");
                param.id = dic.GetValue("WBSID");
                param.name = dic.GetValue("Name");
                param.code = dic.GetValue("Code");
                if (!string.IsNullOrEmpty(param.code))
                    param.name += "（" + param.code + "）";
                param.userId = dic.GetValue("DesignerUserID").Split(',')[0];
                if (string.IsNullOrEmpty(param.userId))
                    param.userId = dic.GetValue("CreateUserID");
                var synUser = GlobalData.UserList.FirstOrDefault(a => a.ID == param.userId);
                if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                    param.userId = synUser.SynID;
                param.type = 1;
                param.autoPublish = true;
                var wbstype = dic.GetValue("WBSType");
                param.subProjectName = dic.GetValue("SubProjectWBSName");
                param.phaseCode = row["PhaseValue"].ToString();
                param.specialtyCode = row["MajorValue"].ToString();
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_W_TaskWork", param.id, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<TaskWorkRequestData>(param).Replace("'", "''"), saveUrl);
                saveSb.AppendLine(queueSql);

            }

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
            #endregion

        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty;
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                sql = @"if exists(select 1 from S_W_TaskWork where ID='{0}')
                                update S_W_TaskWork set Code = '{1}', Name='{2}', SynTime='{5}',SynData='{6}' where ID='{0}'
                            else
	                            insert into S_W_TaskWork(ID,Code,Name,State,SynID,SynTime,SynData,ProjectInfoID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

                var synData = JsonHelper.ToObject<TaskWorkRequestData>(queueData.RequestData);
                sql = string.Format(sql, queueData.RelateID, synData.code.Replace("'", "''"), synData.name.Replace("'", "''"), DataState.Normal.ToString(), ""
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"), synData.projectId);

            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_W_TaskWork set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
