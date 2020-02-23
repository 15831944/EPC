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
    [Description("任务超时警告")]
    public class TimeoutAlarmJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var flowEntities = FormulaHelper.GetEntities<WorkflowEntities>();

            #region 设定发警告时间

            var taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && c.TimeoutAlarm == null).ToList();
            foreach (var taskExec in taskExecList)
            {
                int timeout = -1;
                var step = taskExec.S_WF_InsTask.S_WF_InsDefStep;
                if (step.TimeoutAlarm != null)
                    timeout = (int)step.TimeoutAlarm;
                if (timeout <= 0)
                {
                    taskExec.TimeoutAlarm = DateTime.MaxValue;
                    taskExec.TimeoutAlarmResult = "无设定";
                    continue;
                }
                var calendarService = FormulaHelper.GetService<ICalendarService>();
                taskExec.TimeoutAlarm = calendarService.GetTimeoutTime((DateTime)taskExec.CreateTime, timeout);
            }
            flowEntities.SaveChanges();

            #endregion

            #region 超时任务发通知

            taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && string.IsNullOrEmpty(c.TimeoutAlarmResult) && DateTime.Now > c.TimeoutAlarm).ToList();

            string deadLine = System.Configuration.ConfigurationManager.AppSettings["TimeOutAlarmDeadline"];
            var deadLineTime = double.Parse(string.IsNullOrEmpty(deadLine) ? "24" : deadLine);

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

                    var alarmService = FormulaHelper.GetService<IAlarmService>();
                    alarmService.SendAlarm(msg, msg, url, taskExec.ExecUserName, taskExec.ExecUserID, DateTime.Now.AddHours(deadLineTime), "", null);
                    taskExec.TimeoutAlarmResult = "Success";
                    flowEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.WorkFlow);
                    string sql = "update S_WF_InsTaskExec set TimeoutAlarmResult='{0}' where ID='{1}'";
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
