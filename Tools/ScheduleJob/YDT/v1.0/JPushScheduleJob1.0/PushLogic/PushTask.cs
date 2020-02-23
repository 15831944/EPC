using System;
using System.Collections.Generic;
using Quartz;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using Common.Logging;
using JPush.Api;
using JPush.Api.Common;
using JPush.Api.Common.Resp;
using JPush.Api.Push.Mode;

namespace PushLogic
{
    [Description("推送消息至移动设备")]
    public class JPushTask : IJob
    {
        private static string app_key = ConfigurationManager.AppSettings["APIKey"];
        private static string master_secret = ConfigurationManager.AppSettings["SecretKey"];
        private static string tag = ConfigurationManager.AppSettings["Tag"];
        private static ILog log = LogManager.GetCurrentClassLogger();
        private static bool isProduction = Boolean.Parse(ConfigurationManager.AppSettings["ApnsProduction"]);
        private string bindCode = ConfigurationManager.AppSettings["BindCode"].ToString();
        private static string CompanyID = string.Empty;

        private int intervalMinutes = 0;
        private int intervalMinutesConstant = 30;
        public static int intervalMinutesThreshold = 300;
        public void Execute(IJobExecutionContext context)
        {
            JPushClient client = new JPushClient(app_key, master_secret);
            //晚上九点至早上七点不推送
            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour <= 7) return;

            PushTaskRepository repo = new PushTaskRepository();
            var taskId = "";
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
            {
                taskId = jobDataTaskId.ToString();
            }
            else
            {
                repo.AddTask(); //检测是否有新数据需要推送
            }

            //获取公司ID
            if (string.IsNullOrEmpty(CompanyID))
            {
                CompanyID = PushTaskRepository.GetCompanyID();
            }

            var task = repo.GetTask(taskId);

            int intervalMinutesTemp = 0;
            while (task != null)
            {
                if (!task.DoneLog.Trim().Equals("") 
                    && int.TryParse(task.DoneLog.Trim().Split(new char[]{'-'})[0], out intervalMinutesTemp))
                {
                    if (intervalMinutesTemp >= intervalMinutesThreshold)
                    {
                        intervalMinutes = intervalMinutesThreshold + intervalMinutesConstant;
                    }

                    intervalMinutes = intervalMinutesTemp;
                }
                else
                {
                    intervalMinutes = 0;
                }
                
                if (intervalMinutes <= intervalMinutesThreshold
                    && (task.BeginTime == null || DateTime.Now.AddMinutes(-intervalMinutes) >= task.BeginTime))
                {
                    try
                    {
                        intervalMinutes = intervalMinutes + intervalMinutesConstant;

                        repo.BeginTask(task.ID);

                        //附加信息
                        var dict = new Dictionary<string, string>();
                        dict.Add("PushType", task.SourceType);
                        dict.Add("PushID", task.ID);

                        PushPayload payload = new PushPayload();

                        if (task.PushType == PushType.Broadcast.ToString())
                        {
                            payload = PushMessage.PushObject_AndroidAndIos_Tag(task.ShortContent, task.Title,
                                new[] { bindCode }, isProduction, dict);
                        }
                        else if (task.PushType == PushType.Personal.ToString() && !string.IsNullOrEmpty(task.UserID))
                        {
                            payload = PushMessage.PushObject_AndroidAndIos_Tag(task.ShortContent, task.Title, PushTaskRepository.GetItems(task.UserID, true), isProduction, dict);
                        }
                        else if (task.PushType == PushType.Group.ToString() && !string.IsNullOrEmpty(task.DeptID))
                        {
                            string[] tags = task.DeptID.Contains(CompanyID) ? new[] { bindCode } : PushTaskRepository.GetItems(task.DeptID, false);
                            for (int i = 0; i < Math.Ceiling(tags.Length * 1.0 / 19); i++)
                            {
                                var temp = tags.Skip(i * 19).Take(19).ToArray();
                                payload = PushMessage.PushObject_AndroidAndIos_Tag(task.ShortContent, task.Title, temp, isProduction, dict);
                            }
                        }
                        var result = client.SendPush(payload);

                        repo.EndTask(task.ID);
                    }
                    catch (APIRequestException e)
                    {
                        log.Error("推送失败，详细信息如下： ");
                        log.Error("HTTP状态: " + e.Status);
                        log.Error("失败Code: " + e.ErrorCode);
                        log.Error("出错信息: " + e.ErrorMessage);

                        repo.Log(task.ID, e.ErrorMessage, intervalMinutes);
                    }
                    catch (APIConnectionException e)
                    {
                        repo.Log(task.ID, e.Message, intervalMinutes);
                    }
                }
                else
                {
                    repo.Log(task.ID, "", intervalMinutes);
                }

                task = repo.GetTask(); //获取下一个推送任务
            }
        }
    }
}