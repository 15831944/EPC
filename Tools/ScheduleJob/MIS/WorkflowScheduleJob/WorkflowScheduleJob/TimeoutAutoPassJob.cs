using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Quartz;
using Formula;
using Workflow.Logic.Domain;
using Config;
using Workflow.Logic.BusinessFacade;
using Common.WebAPIKit;

namespace WorkflowScheduleJob
{
    [Description("任务超时自动通过")]
    public class TimeoutAutoPassJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var flowEntities = FormulaHelper.GetEntities<WorkflowEntities>();

            #region 设定自动发送时间

            var taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && c.TimeoutAutoPass == null).ToList();
            foreach (var taskExec in taskExecList)
            {
                int timeout = 0;
                var step = taskExec.S_WF_InsTask.S_WF_InsDefStep;
                if (step.TimeoutAutoPass != null)
                    timeout = (int)step.TimeoutAutoPass;
                //TimeoutAutoPass 为负数时 轮巡直接自动通过
                if (timeout == 0)
                {
                    taskExec.TimeoutAutoPass = DateTime.MaxValue;
                    taskExec.TimeoutAutoPassResult = "无设定";
                    continue;
                }
                var calendarService = FormulaHelper.GetService<ICalendarService>();
                taskExec.TimeoutAutoPass = calendarService.GetTimeoutTime((DateTime)taskExec.CreateTime, timeout);
            }
            flowEntities.SaveChanges();

            #endregion

            #region 超时自动执行任务

            taskExecList = flowEntities.S_WF_InsTaskExec.Where(c => c.ExecTime == null && string.IsNullOrEmpty(c.TimeoutAutoPassResult) && DateTime.Now > c.TimeoutAutoPass).ToList();
            string FlowApiUrl = System.Configuration.ConfigurationManager.AppSettings["FlowApiUrl"];
            if (FlowApiUrl.EndsWith("/"))
                FlowApiUrl = FlowApiUrl.TrimEnd('/');

            foreach (var taskExec in taskExecList)
            {
                try
                {
                    var agentUser = FormulaHelper.GetUserInfoByID(taskExec.ExecUserID);
                    FlowFO flowFO = new FlowFO();
                    //flowFO.AutoExecTask(taskExec.ID, "自动通过");

                    var routingList = flowFO.AutoExecGetRoutingList(taskExec.ID);
                    if (routingList.Count == 0)
                        continue;
                    var routing = routingList[0];
                    if (routing.Type == Workflow.Logic.RoutingType.Branch.ToString())
                        continue;

                    var formInstanceID = taskExec.S_WF_InsFlow.FormInstanceID;
                    var param = flowFO.GetRoutingParams(routing, taskExec, formInstanceID);
                    string nextUserIDs = param.userIDs;

                    var uri = string.Format("{0}/FlowTaskAPI/SubmitForm?id={1}&FormInstanceID={2}&TaskExecID={3}&NextExecUserIDs={4}&Comment={5}&ExecUserID={6}&UserAccount={7}&IsMobileRequest=6",
                        FlowApiUrl, routingList[0].ID, formInstanceID, taskExec.ID, nextUserIDs, "自动通过", taskExec.TaskUserID, agentUser.Code);
                    var str = WebApiClientHelper.DoJsonRequest(uri, EnuHttpMethod.Post, new { FormDic = "{ID:\"" + formInstanceID + "\"}", FlowCode = "" });

                    //5.3平台FlowTaskAPI
                    //var uri = string.Format("{0}/FlowTaskAPI/0/Submit?id={1}&FormInstanceID={2}&TaskExecID={3}&NextExecUserIDs={4}&Comment={5}&ExecUserID={6}&UserAccount={7}",
                    //    FlowApiUrl, routingList[0].ID, formInstanceID, taskExec.ID, nextUserIDs, "自动通过", taskExec.TaskUserID, agentUser.Code);
                    //var str = WebApiClientHelper.DoJsonRequest(uri, EnuHttpMethod.Get);

                    if (str.ToLower() == "true")
                    {
                        taskExec.TimeoutAutoPassResult = "Success";
                        flowEntities.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("FlowTaskAPI报错");
                    }
                }
                catch (Exception e)
                {
                    SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.WorkFlow);
                    string sql = "update S_WF_InsTaskExec set TimeoutAutoPassResult='{0}' where ID='{1}'";
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
