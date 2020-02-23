using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Configuration;
using System.ComponentModel;
using System.Collections;
using System.Data;

namespace JPushTask
{
    [Description("推送消息至移动设备")]
    public class PushTask : IJob
    {
        //发送手机通知的url
        static string MobileMsgsUrl = System.Configuration.ConfigurationManager.AppSettings["MobileMsgsUrl"];
        //待办任务类别
        static string UndoTaskType = "task";
        //未读消息类别
        static string UnrecMsgType = "msg";
        //未过期未删除提醒
        static string UnexpiredAlarmType = "alarm";

        private string GetUserCode(string userID)
        {
            if (string.IsNullOrEmpty(userID))
                return null;
            if (!PushTaskRepository.UserDic.ContainsKey(userID))
            {
                PushTaskRepository._userDic = null;
            }
            return PushTaskRepository.UserDic[userID].Code;
        }

        //取待办总数（从缓存）
        private int GetBadgeCountByUserID(string userID)
        {
            if (string.IsNullOrEmpty(userID))
                return 0;

            if (!PushTaskRepository.UserDic.ContainsKey(userID))
            {
                PushTaskRepository._userDic = null;
            }
            var user = PushTaskRepository.UserDic[userID];
            return user.UndoTaskCount + user.UnrecMsgCount + user.UnexpiredAlarmCount;
        }

        public void Execute(IJobExecutionContext context)
        {
            Log4NetConfig.Configure();
            var taskId = "";
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
                taskId = jobDataTaskId.ToString();

            PushTaskRepository repo = new PushTaskRepository();
            try
            {
                //1.重新检测生成推送队列
                repo.AddTask();

                //2.循环队列推送
                PushTaskDTO task = repo.GetTask(taskId);
                while (task != null)
                {
                    try
                    {
                        taskId = task.ID;
                        task.BeginTime = DateTime.Now;
                        //推送附带的总角标数（待办任务+待办消息）
                        string badgeCount = GetBadgeCountByUserID(task.UserID).ToString();
                        //接收人的账户名（唯一索引）
                        string userCode = GetUserCode(task.UserID);
                        string result = "ok";

                        if (task.SourceType == "Task")
                        {
                            #region Task
                            //找到任务对应的菜单
                            var menuItem = PushTaskRepository.MenuItems.FirstOrDefault(c => c["Code"].ToString().Contains(task.FlowCode) && c["Type"].ToString() == "2");
                            if (menuItem != null)
                            {
                                var name = menuItem["Name"].ToString();
                                var senderTaskUserId = task.SendUserID.Split(',').FirstOrDefault();
                                var senderCode = GetUserCode(senderTaskUserId);

                                HttpHelper httpHelper = new HttpHelper();
                                Hashtable pars = new Hashtable();
                                pars.Add("FromUser", senderCode);
                                pars.Add("ToUser", userCode);
                                pars.Add("Content", task.ShortContent);
                                pars.Add("ID", task.SourceID);
                                pars.Add("Pid", task.FormInstanceID);
                                pars.Add("Type", PushTask.UndoTaskType);
                                pars.Add("Name", name);
                                pars.Add("Title", "审批任务(" + task.ShortContent + ")");
                                pars.Add("Description", task.ShortContent);
                                pars.Add("Badge", badgeCount);

                                if (string.IsNullOrEmpty(task.HuaWeiToken))
                                    result = httpHelper.GetHtml(MobileMsgsUrl, pars, true);
                                else
                                {
                                    HuaWeiPushNcMsg.sendPushMessage(task.HuaWeiToken, pars, 1);
                                    result = "ok";
                                }
                            }
                            else
                                result = "没有找到对应的对应入口菜单！发送失败！";
                            #endregion
                        }
                        else if (task.SourceType == "Msg")
                        {
                            #region Msg
                            HttpHelper httpHelper = new HttpHelper();
                            Hashtable pars = new Hashtable();
                            pars.Add("FromUser", "");
                            pars.Add("ToUser", userCode);
                            pars.Add("Content", task.ShortContent);
                            pars.Add("ID", task.SourceID);
                            pars.Add("Pid", "");
                            pars.Add("Type", PushTask.UnrecMsgType);
                            pars.Add("Name", "消息");
                            pars.Add("Title", "新消息(" + task.Title + ")");
                            pars.Add("Description", task.ShortContent);
                            pars.Add("Badge", badgeCount);

                            if (string.IsNullOrEmpty(task.HuaWeiToken))
                                result = httpHelper.GetHtml(MobileMsgsUrl, pars, true);
                            else
                            {
                                HuaWeiPushNcMsg.sendPushMessage(task.HuaWeiToken, pars, 1);
                                result = "ok";
                            }

                            #endregion
                        }
                        else if (task.SourceType == "Alarm")
                        {
                            #region Alarm
                            HttpHelper httpHelper = new HttpHelper();
                            Hashtable pars = new Hashtable();
                            pars.Add("FromUser", "");
                            pars.Add("ToUser", userCode);
                            pars.Add("Content", task.ShortContent);
                            pars.Add("ID", task.SourceID);
                            pars.Add("Pid", "");
                            pars.Add("Type", PushTask.UnexpiredAlarmType);
                            pars.Add("Name", "提醒");
                            pars.Add("Title", "事项提醒(" + task.Title + ")");
                            pars.Add("Description", task.ShortContent);
                            pars.Add("Badge", badgeCount);

                            if (string.IsNullOrEmpty(task.HuaWeiToken))
                                result = httpHelper.GetHtml(MobileMsgsUrl, pars, true);
                            else
                            {
                                HuaWeiPushNcMsg.sendPushMessage(task.HuaWeiToken, pars, 1);
                                result = "ok";
                            }

                            #endregion
                        }
                        task.State = "Success";
                        result = result.Trim('\'', '"', ' ');
                        if (result != "ok")
                            throw new Exception(result);
                    }
                    catch (Exception e)
                    {
                        task.State = "Error";
                        task.DoneLog = e.Message;
                        repo.EndTask(task);
                    }
                    finally
                    {
                        //获取下一个推送任务
                        task = repo.GetTask();
                    }
                }
            }
            catch (Exception ex)
            {
                var errorTask = repo.GetTask(taskId);
                if (errorTask != null)
                {
                    errorTask.State = "Error";
                    errorTask.DoneLog = ex.Message;
                    repo.EndTask(errorTask);
                }
                LogWriter.Error(ex, ex.Message);
            }
            finally
            {
                var endTask = repo.GetTask(taskId);
                if (endTask != null)
                    PushTaskRepository.LastSendTime = endTask.SendTime;
            }
        }
    }
}
