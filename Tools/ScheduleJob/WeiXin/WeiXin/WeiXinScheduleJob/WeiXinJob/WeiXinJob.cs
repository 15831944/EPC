using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.EnterpriseServices;
using System.Configuration;
using Quartz;

namespace WeiXinScheduleJob
{
    [Description("企业微信同步任务")]
    public class WeiXinJob : IJob
    {
        private bool CompareTime(string msgsSendTime)
        {
            var time = msgsSendTime.Split(',');
            var sysTime = System.DateTime.Now.ToString("HH:mm");
            return time.Where(c => c == sysTime).Count() > 0;
        } 

        public void Execute(IJobExecutionContext context)
        {
            WeiXinTaskRepository repo = new WeiXinTaskRepository();
            
            try
            {
                bool isOnlyPush = Convert.ToBoolean(AppSettingService.IsOnlyPush);
                bool sYNFlag = Convert.ToBoolean(AppSettingService.SYNFlag);
                var userTable = repo.GetUser();
                var wxUsers = repo.GetWXUser(); //获取微信成员
                if (sYNFlag)
                {
                    bool sYNDept = Convert.ToBoolean(AppSettingService.SYNDept);
                    bool sYNUser = Convert.ToBoolean(AppSettingService.SYNUser);
                    var table = repo.GetDepartment(); //获取部门
                    if (sYNDept)
                    {
                        repo.SyncOrg(table); //同步组织
                    }
                    if (sYNUser)
                    {
                        var updateSql = repo.SyscUser(table, userTable, wxUsers); //同步人员
                        if (!string.IsNullOrEmpty(updateSql))
                        {
                            var db = SqlHelper.Create("Base");
                            db.ExecuteNonQuery(updateSql);
                        }
                    }                   
                }
                if (isOnlyPush)
                {
                    string msgsSendTime = AppSettingService.MsgsSendTime;
                    //如果不配置时按自动作业运行，否则按时间推送
                    if (string.IsNullOrEmpty(msgsSendTime) || CompareTime(msgsSendTime))
                    {
                        repo.SendUndoTasks(userTable, wxUsers); //流程推送
                        repo.SendMsgs(userTable, wxUsers); //消息推送
                    }

                    //创建项目群组
                    bool isCreateProjectGroup = Convert.ToBoolean(AppSettingService.IsCreateProjectGroup);
                    if (isCreateProjectGroup)
                    {
                        repo.CreateProjectGroup(userTable, wxUsers);
                    }
                }
         
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
