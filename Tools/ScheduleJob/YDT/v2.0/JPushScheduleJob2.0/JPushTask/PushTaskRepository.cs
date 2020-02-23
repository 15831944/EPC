using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JPushTask
{
    public enum PushType
    {
        /// <summary>
        /// 广播
        /// </summary>
        Broadcast = 0,
        /// <summary>
        /// 个人
        /// </summary>
        Personal = 1,
        /// <summary>
        /// 群组
        /// </summary>
        Group = 2,
    }

    public struct UserDTO
    {
        public string ID;
        public string Name;
        public string Code;
        public int UndoTaskCount;
        public int UnrecMsgCount;
        public int UnexpiredAlarmCount;
        public string HuaWeiToken;
    }


    public class PushTaskRepository
    {
        public static List<PushTaskDTO> TaskList = new List<PushTaskDTO>();

        public static DateTime? _lastSendTime = null;
        public static DateTime LastSendTime
        {
            get
            {
                if (_lastSendTime == null)
                {
                    DateTime _dt = default(DateTime);
                    string _date = ConfigurationManager.AppSettings["LastSendTime"] ?? "";
                    if (!string.IsNullOrEmpty(_date))
                        DateTime.TryParse(_date, out _dt);
                    _lastSendTime = _dt;
                }
                return (DateTime)_lastSendTime;
            }
            set
            {
                _lastSendTime = value;
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings["LastSendTime"] != null)
                    config.AppSettings.Settings["LastSendTime"].Value = value.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                else
                    config.AppSettings.Settings.Add("LastSendTime", value.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        //缓存用户映射表
        public static Dictionary<string, UserDTO> _userDic;
        public static Dictionary<string, UserDTO> UserDic
        {
            get
            {
                if (_userDic == null)
                {
                    var dbBase = SqlHelper.Create("Base");
                    var dbTerminal = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Terminal"].ToString());
                    var sql = string.Format(@"
select S_A_User.ID,S_A_User.Code,S_A_User.Name,S_S_UserClientInfo.HuaWeiToken
from S_A_User left join {0}..S_S_UserClientInfo on S_A_User.ID = S_S_UserClientInfo.UserID", dbTerminal.Database);
                    DataTable dt = dbBase.ExecuteDataTable(sql);
                    _userDic = dt.Rows.Cast<DataRow>().ToDictionary(
                        x => x["ID"].ToString(), x => new UserDTO
                        {
                            ID = x["ID"].ToString(),
                            Name = x["Name"].ToString(),
                            Code = x["Code"].ToString(),
                            UndoTaskCount = 0,
                            UnrecMsgCount = 0,
                            UnexpiredAlarmCount = 0,
                            HuaWeiToken = x["HuaWeiToken"].ToString()
                        });
                }
                return _userDic;
            }
        }

        //菜单入口种类
        private static EnumerableRowCollection<DataRow> _mItems;
        public static EnumerableRowCollection<DataRow> MenuItems
        {
            get
            {
                if (_mItems == null)
                {
                    var dbTerminal = SqlHelper.Create("Terminal");
                    _mItems = dbTerminal.ExecuteDataTable("select * from S_S_EntryMenu").AsEnumerable();
                }
                return _mItems;
            }
        }

        public PushTaskDTO GetTask(string Id = "")
        {
            PushTaskDTO task = null;
            if (string.IsNullOrWhiteSpace(Id))
            {
                task = PushTaskRepository.TaskList.OrderBy(a => a.SendTime).FirstOrDefault(a => a.State == "New");
            }
            else
            {
                task = PushTaskRepository.TaskList.FirstOrDefault(a => a.ID == Id);
            }
            return task;
        }
        public void EndTask(PushTaskDTO task)
        {
            var db = SqlHelper.Create("Base");
            var sql = string.Format(@"
insert into S_D_PushTask (ID,SourceID,FormInstanceID,Title,ShortContent,
SendUserID,SendUserName,SendTime,SourceType,
UserID,UserName,PushType,BeginTime,EndTime,
State,DoneLog) VALUES ('{0}','{1}','{2}','{3}','{4}',
'{5}','{6}','{7}','{8}',
'{9}','{10}','{11}','{12}','{13}',
'{14}','{15}')
", task.ID, task.SourceID, task.FormInstanceID, task.Title, task.ShortContent,
 task.SendUserID, task.SendUserName, task.SendTime, task.SourceType,
 task.UserID, task.UserName, task.PushType, task.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
 task.State, task.DoneLog);
            db.ExecuteNonQuery(sql);
        }
        public void AddTask()
        {
            if (MenuItems.Any(c => c["Type"].ToString() == "2"))
                addFlowTaskPush();
            if (MenuItems.Any(c => c["Type"].ToString() == "9"))
                addMsgPush();
            if (MenuItems.Any(c => c["Type"].ToString() == "10"))
                addAlarmPush();
        }

        /// <summary>
        /// 最近两天的新闻,通知，且只推送一次.
        /// </summary>
        private void addNewsPush()
        {
            //最近2天，且之推送一次
            var cycleDay = 30;
            var dbBase = SqlHelper.Create("Base");
            dbBase.ExecuteNonQuery("DELETE FROM S_D_PushTask WHERE State='end' AND DateDiff(day,BeginTime,getDate())>" + cycleDay);

            string sqlNews = string.Format(@"
SELECT Info.ID,CatalogName AS Title,Title AS Content FROM 
(SELECT ID,CatalogId,Title FROM S_I_PublicInformation
WHERE DateDiff(day,CreateTime,getDate())<={0}) Info
INNER JOIN S_I_PublicInformCatalog AS cat on info.CatalogId = cat.ID", cycleDay);
            DataTable dtNews = dbBase.ExecuteDataTable(sqlNews);

            foreach (DataRow row in dtNews.Rows)
            {
                //周期内只推一次
                var sqlExist = string.Format("SELECT * FROM S_D_PushTask WHERE SourceID='{0}' AND  State='end' AND DateDiff(day,BeginTime,getDate())<={1}",
                    row["ID"].ToString(), cycleDay);
                var exist = dbBase.ExecuteDataTable(sqlExist).Rows.Count;
                if (exist > 0) continue;

                var sql = string.Format("INSERT INTO S_D_PushTask(ID, SourceID, Title, ShortContent, SourceType, UserID, UserName, PushType, State, ClientID, AppID, ChannelID) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", Tools.GuidTool.CreateGuid(),
                   row["ID"].ToString(), row["Title"].ToString(), row["Content"].ToString(), "News"
                   , "", "", PushType.Group, "New", "", "", "");
                dbBase.ExecuteNonQuery(sql);
            }

        }
        /// <summary>
        /// 任务队列
        /// </summary>
        private void addFlowTaskPush()
        {
            SqlHelper dbBase = SqlHelper.Create("Base");
            SqlHelper dbFlow = SqlHelper.Create("Workflow");
            //只推送移动通配置的任务
            var flowCodes = string.Join(",", PushTaskRepository.MenuItems.Where(p => p["Type"].ToString() == "2").Select(c => c["Code"].ToString()));
            //待推送的任务
            string sqlTask = string.Format(@"select S_WF_InsTaskExec.InsTaskID
,S_WF_InsTaskExec.ID as TaskExecID
,S_WF_InsTask.SendTaskUserIDs
,S_WF_InsTask.SendTaskUserNames
,S_WF_InsDefFlow.Code as FlowCode
,S_WF_InsDefFlow.Name as FlowName
,S_WF_InsFlow.FormInstanceID
,S_WF_InsTaskExec.CreateTime as CreateTime
,TaskName
,S_WF_InsTaskExec.TaskUserID
,S_WF_InsTaskExec.TaskUserName
from S_WF_InsTaskExec
join S_WF_InsTask on ExecTime is null and S_WF_InsTask.Type in('Normal','Inital') and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='') and S_WF_InsTask.ID=InsTaskID
join S_WF_InsFlow on S_WF_InsFlow.Status='Processing' and S_WF_InsFlow.ID=S_WF_InsTask.InsFlowID  
join S_WF_InsDefFlow on InsDefFlowID=S_WF_InsDefFlow.ID and S_WF_InsDefFlow.Code in('{0}')
join S_WF_InsDefStep on InsDefStepID = S_WF_InsDefStep.ID
join S_WF_DefStep on S_WF_InsDefStep.DefStepID=S_WF_DefStep.ID and S_WF_DefStep.Type='Normal' and S_WF_DefStep.AllowToMobile ='1'
order by S_WF_InsTask.CreateTime desc", flowCodes.Replace(",", "','"));
            var dtTask = dbFlow.ExecuteDataTable(sqlTask).AsEnumerable();

            foreach (DataRow row in dtTask)
            {
                string userID = row["TaskUserID"].ToString();
                string taskID = row["TaskExecID"].ToString();

                //1.更新每个人的待办任务条数
                var taskCount = dtTask.Count(p => p["TaskUserID"].ToString() == userID);
                if (!PushTaskRepository.UserDic.ContainsKey(userID))
                    continue;
                var user = PushTaskRepository.UserDic[userID];
                user.UndoTaskCount = taskCount;
                PushTaskRepository.UserDic[userID] = user;

                //2.往推送队列里塞值
                try
                {
                    //小于上次推送时间的跳过
                    var sendDate = (DateTime)row["CreateTime"];
                    if (sendDate > PushTaskRepository.LastSendTime &&
                        !PushTaskRepository.TaskList.Exists(a => a.SourceID == taskID))
                    {
                        PushTaskRepository.TaskList.Add(new PushTaskDTO
                        {
                            ID = Tools.GuidTool.CreateGuid(),
                            SourceID = taskID,
                            Title = "任务提醒",
                            ShortContent = row["TaskName"].ToString().Replace("'", "").Replace("\"", ""),
                            SourceType = "Task",
                            UserID = row["TaskUserID"].ToString(),
                            UserName = row["TaskUserName"].ToString(),
                            PushType = PushType.Personal.ToString(),
                            State = "New",
                            SendUserID = row["SendTaskUserIDs"].ToString(),
                            SendUserName = row["SendTaskUserNames"].ToString(),
                            SendTime = sendDate,
                            HuaWeiToken = user.HuaWeiToken,
                            FlowCode = row["FlowCode"].ToString(),
                            FlowName = row["FlowName"].ToString(),
                            FormInstanceID = row["FormInstanceID"].ToString()
                        });
                    }
                }
                catch { continue; }
            }
        }

        /// <summary>
        /// 消息队列
        /// </summary>
        private void addMsgPush()
        {
            var dbBase = SqlHelper.Create("Base");
            string sqlNews = @"
select S_S_MsgReceiver.UserID,S_S_MsgReceiver.UserName,S_S_MsgReceiver.FirstViewTime,S_S_MsgBody.* from S_S_MsgBody join S_S_MsgReceiver on 
S_S_MsgReceiver.MsgBodyID=S_S_MsgBody.ID and S_S_MsgReceiver.IsDeleted='0' and S_S_MsgReceiver.FirstViewTime is null ";
            var dtMsg = dbBase.ExecuteDataTable(sqlNews).AsEnumerable();

            foreach (DataRow row in dtMsg)
            {
                //垃圾数据过滤
                if (row["UserID"] == null || string.IsNullOrWhiteSpace(row["UserID"].ToString()) || row["SendTime"] == null)
                    continue;

                string userID = row["UserID"].ToString();
                string msgBodyID = row["ID"].ToString();

                //1.更新每个人的未读条数
                var msgCount = dtMsg.Count(p => p["UserID"].ToString() == userID);
                if (!PushTaskRepository.UserDic.ContainsKey(userID))
                    continue;
                var user = PushTaskRepository.UserDic[userID];
                user.UnrecMsgCount = msgCount;
                PushTaskRepository.UserDic[userID] = user;

                //2.往推送队列里塞值
                try
                {
                    //小于上次推送时间的跳过
                    var sendDate = (DateTime)row["SendTime"];
                    if (sendDate > PushTaskRepository.LastSendTime &&
                        !PushTaskRepository.TaskList.Exists(a => a.SourceID == msgBodyID))
                    {
                        PushTaskRepository.TaskList.Add(new PushTaskDTO
                        {
                            ID = Tools.GuidTool.CreateGuid(),
                            SourceID = msgBodyID,
                            Title = row["Title"].ToString().Replace("'", "").Replace("\"", ""),
                            ShortContent = row["Content"].ToString().Replace("'", "").Replace("\"", ""),
                            SourceType = "Msg",
                            UserID = userID,
                            UserName = row["UserName"].ToString(),
                            PushType = PushType.Personal.ToString(),
                            State = "New",
                            SendUserID = row["SenderID"].ToString(),
                            SendUserName = row["SenderName"].ToString(),
                            SendTime = sendDate,
                            HuaWeiToken = user.HuaWeiToken
                        });
                    }
                }
                catch { continue; }
            }
        }

        private void addAlarmPush()
        {
            var dbBase = SqlHelper.Create("Base");
            string sqlNews = @"select * from S_S_Alarm where (IsDelete='0' or IsDelete is null) and DeadlineTime>=getdate()";
            var dtAlarm = dbBase.ExecuteDataTable(sqlNews).AsEnumerable();

            foreach (DataRow row in dtAlarm)
            {
                //垃圾数据过滤
                if (row["OwnerID"] == null || string.IsNullOrWhiteSpace(row["OwnerID"].ToString()) || row["DeadlineTime"] == null)
                    continue;

                string userID = row["OwnerID"].ToString();
                string alarmID = row["ID"].ToString();

                //1.更新每个人的未过期条数
                var alarmCount = dtAlarm.Count(p => p["OwnerID"].ToString() == userID);
                if (!PushTaskRepository.UserDic.ContainsKey(userID))
                    continue;
                var user = PushTaskRepository.UserDic[userID];
                user.UnexpiredAlarmCount = alarmCount;
                PushTaskRepository.UserDic[userID] = user;

                //2.过滤
                try
                {
                    //小于上次推送时间的跳过
                    var sendDate = (DateTime)row["SendTime"];
                    if (sendDate > PushTaskRepository.LastSendTime &&
                        !PushTaskRepository.TaskList.Exists(a => a.SourceID == alarmID))
                    {
                        PushTaskRepository.TaskList.Add(new PushTaskDTO
                        {
                            ID = Tools.GuidTool.CreateGuid(),
                            SourceID = alarmID,
                            Title = row["AlarmTitle"].ToString().Replace("'", "").Replace("\"", ""),
                            ShortContent = row["AlarmContent"].ToString().Replace("'", "").Replace("\"", ""),
                            SourceType = "Alarm",
                            UserID = userID,
                            UserName = row["OwnerName"].ToString(),
                            PushType = PushType.Personal.ToString(),
                            State = "New",
                            SendUserID = row["SenderID"].ToString(),
                            SendUserName = row["SenderName"].ToString(),
                            SendTime = sendDate,
                            HuaWeiToken = user.HuaWeiToken
                        });
                    }
                }
                catch { continue; }
            }
        }
    }

    public class PushTaskDTO
    {
        public string ID { get; set; }
        public string SourceID { get; set; }
        public string SendUserID { get; set; }
        public string SendUserName { get; set; }
        public DateTime SendTime { get; set; }
        public string SourceType { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PushType { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string State { get; set; }
        public string DoneLog { get; set; }
        public string AppID { get; set; }
        public string ClientID { get; set; }
        public string ChannelID { get; set; }
        public string DeviceOS { get; set; }
        public string HuaWeiToken { get; set; }

        public string FlowCode { get; set; }
        public string FlowName { get; set; }
        public string FormInstanceID { get; set; }
    }
}
