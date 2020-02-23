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
    [Description("任务超时过期")]
    public class TimeoutDeadlineJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var flowEntities = FormulaHelper.GetEntities<WorkflowEntities>();

            #region 设定过期时间

            var taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && string.IsNullOrEmpty(c.TimeoutDeadlineResult)).ToList();
            foreach (var taskExec in taskExecList)
            {
                int timeout = -1;
                var step = taskExec.S_WF_InsTask.S_WF_InsDefStep;
                if (step.TimeoutDeadline != null)
                    timeout = (int)step.TimeoutDeadline;
                if (timeout <= 0)
                {
                    taskExec.TimeoutDeadline = DateTime.MaxValue;
                    taskExec.TimeoutDeadlineResult = "无设定";
                    continue;
                }
                var calendarService = FormulaHelper.GetService<ICalendarService>();
                taskExec.TimeoutDeadline = calendarService.GetTimeoutTime((DateTime)taskExec.CreateTime, timeout);
            }
            flowEntities.SaveChanges();

            #endregion

            #region 设置任务暂停
            taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && string.IsNullOrEmpty(c.TimeoutDeadlineResult) && DateTime.Now > c.TimeoutDeadline).ToList();
            foreach (var taskExec in taskExecList)
            {
                taskExec.S_WF_InsTask.Status = Workflow.Logic.FlowTaskStatus.Stop.ToString();
                taskExec.ExecTime = DateTime.Now;

                var defFlow = taskExec.S_WF_InsFlow.S_WF_InsDefFlow;
                SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(defFlow.ConnName);
                var sql = string.Format("update {0} set FlowPhase = '超时过期' where ID = '{1}'", defFlow.TableName, taskExec.S_WF_InsFlow.FormInstanceID);
                sqlHelper.ExecuteNonQuery(sql);
            }
            flowEntities.SaveChanges();
            #endregion
        }
    }
}
