using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Quartz;
using Formula;
using Workflow.Logic.Domain;
using Config;

namespace WorkflowScheduleJob
{
    [Description("任务超时通知")]
    public class TimeoutNoticeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var flowEntities = FormulaHelper.GetEntities<WorkflowEntities>();

            #region 设定发通知时间

            var taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && c.TimeoutNotice == null).ToList();
            foreach (var taskExec in taskExecList)
            {
                int timeout = -1;
                var step = taskExec.S_WF_InsTask.S_WF_InsDefStep;
                if (step.TimeoutNotice != null)
                    timeout = (int)step.TimeoutNotice;
                if (timeout <= 0)
                {
                    taskExec.TimeoutNotice = DateTime.MaxValue;
                    taskExec.TimeoutNoticeResult = "无设定";
                    continue;
                }
                var calendarService = FormulaHelper.GetService<ICalendarService>();
                taskExec.TimeoutNotice = calendarService.GetTimeoutTime((DateTime)taskExec.CreateTime, timeout);
            }
            flowEntities.SaveChanges();

            #endregion

            #region 超时任务发通知

            taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && string.IsNullOrEmpty(c.TimeoutNoticeResult) && DateTime.Now > c.TimeoutNotice).ToList();

            foreach (var taskExec in taskExecList)
            {
                string msg = string.Format("任务超时:{0}", taskExec.S_WF_InsTask.TaskName);
                try
                {
                    string url = taskExec.S_WF_InsFlow.S_WF_InsDefFlow.FormUrl;
                    if (url.Contains("?"))
                        url += "&";
                    else
                        url += "?";
                    url += "TaskExecID=" + taskExec.ID + "&ID=" + taskExec.S_WF_InsFlow.FormInstanceID;

                    var msgService = FormulaHelper.GetService<IMessageService>();
                    msgService.SendMsg(msg, msg, url, "", taskExec.ExecUserID, taskExec.ExecUserName, null, MsgReceiverType.UserType, MsgType.Normal, false, false);
                    taskExec.TimeoutNoticeResult = "Success";
                    flowEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.WorkFlow);
                    string sql = "update S_WF_InsTaskExec set TimeoutNoticeResult='{0}' where ID='{1}'";
                    string str = e.Message ?? "";
                    if (str.Length > 500)
                        str = str.Substring(0, 500);
                    sql = string.Format(sql, str, taskExec.ID);
                    sqlHelper.ExecuteNonQuery(sql); //记录错误
                }
            }

            #endregion
        }
    }
}
