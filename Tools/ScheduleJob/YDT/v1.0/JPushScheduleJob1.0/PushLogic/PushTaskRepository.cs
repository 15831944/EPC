using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Validation;
using System.Runtime.InteropServices;
using Common.Logging;
using PushLogic.Domain;

namespace PushLogic
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

    public enum TaskState
    {
        /// <summary>
        /// 新建
        /// </summary>
        New,
        /// <summary>
        /// 开始
        /// </summary>
        Start,
        /// <summary>
        /// 结束
        /// </summary>
        End,
        /// <summary>
        /// 出错
        /// </summary>
        Error
    }

    public class PushTaskRepository
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        private int cycleDay = Int32.Parse(ConfigurationManager.AppSettings["ExpireDay"]);
        private static string bindCode = ConfigurationManager.AppSettings["BindCode"].ToString();

        /// <summary>
        /// 分解IDs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="b">true:user,flase:dept</param>
        /// <returns></returns>
        public static string[] GetItems(string items,bool b)
        {
            if (b)
            {
                items = items.Replace("-", "");
                if (items.IndexOf(',') <= 0)
                {
                    return new[] { items + bindCode };
                }
                else
                {
                    var temp = items.Split(',');
                    for (int index = 0; index < temp.Length; index++)
                    {
                        temp[index] = temp[index] + bindCode;
                    }
                    return temp;
                }
            }
            else
            {
                var temp=items.Split(',');
                var list = new List<string>();
                foreach (var item in temp)
                {
                    var db = new BaseContext();
                    var users = db.S_A__OrgUser.Where(p => p.OrgID == item);
                    if (users.Any())
                    {
                        foreach (var user in users.Select(p=>p.UserID))
                        {
                            list.Add(user.Replace("-", "") + bindCode);
                        }
                    }
                }
                return list.ToArray();
            }
        }

        public static string GetCompanyID()
        {
            var db = new BaseContext();
            var org = db.S_A_Org.FirstOrDefault(p => p.ParentID == null || p.FullID.Length < 40);
            if (org != null)
                return org.ID;
            else
            {
                return string.Empty;
            }
        }

        public string GetContent(string content,string title)
        {
            if (string.IsNullOrEmpty(content)){
                return title.Length>20?title.Substring(0, 20) + "...":title;
            }else{
                return content.Length > 20 ? content.Substring(0, 20) + "..." : content;
            }
        }

        /// <summary>
        /// 查询任务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public S_D_PushTask GetTask(string id = "")
        {
            var db = new BaseContext();
            var state = TaskState.New.ToString();
            return string.IsNullOrWhiteSpace(id) ? db.S_D_PushTask.OrderByDescending(p=>p.CreateTime).FirstOrDefault(p => p.State == state) : db.S_D_PushTask.Find(id);
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="id"></param>
        public void BeginTask(string id)
        {
            var db = new BaseContext();
            var task = db.S_D_PushTask.Find(id);
            //task.BeginTime = task.BeginTime?? DateTime.Now;
            task.BeginTime = DateTime.Now;
            task.State = TaskState.Start.ToString();
            db.SaveChanges();
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        /// <param name="id"></param>
        public void EndTask(string id)
        {
            var db = new BaseContext();
            var task = db.S_D_PushTask.Find(id);
            task.EndTime =task.EndTime?? DateTime.Now;
            task.State = TaskState.End.ToString();
            db.SaveChanges();
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        public void AddTask()
        {
            DeleteExpirePush();
            AddNewsPush(); //分类新闻
            AddImageNewsPush();//图片新闻
            AddFlowTaskPush(); //任务
            AddAlarmPush(); //提醒
            AddMsgPush(); //消息
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="error"></param>
        /// <param name="intervalMinutes">间隔多久不推送</param>
        public void Log(string id, string error, int intervalMinutes)
        {
            var db = new BaseContext();
            var task = db.S_D_PushTask.Find(id);
            task.State = TaskState.Error.ToString();

            if (intervalMinutes > 0)
            {
                task.DoneLog = intervalMinutes.ToString() + "-" + error;
            }
            else
            {
                task.DoneLog = " -" + error;
            }

            db.SaveChanges();
        }

        /// <summary>
        /// 清除已完成的推送或已超出两天未成功的推送
        /// 注：只推送最近2天的且只推送一次，若超出无论成功与否均不在推送
        /// </summary>
        private void DeleteExpirePush()
        {
            var cycleDay = Int32.Parse(ConfigurationManager.AppSettings["ExpireDay"]);
            var db = new BaseContext();
            var sql =
                string.Format("DELETE FROM S_D_PushTask WHERE DateDiff(day,CreateTime,getDate())>{0}",
                    cycleDay);
            db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 添加分类新闻推送
        /// </summary>
        private void AddNewsPush()
        {
            var db = new BaseContext();

            //获取新的新闻
            string sqlNews = string.Format(@"SELECT Info.ID,CatalogName AS Title,Title AS Content,'News' as SourceType,UserID,UserName,DeptID,DeptName,PushType FROM (SELECT ID,CatalogId,Title,ReceiveDeptId as DeptID,ReceiveDeptName as DeptName,ReceiveUserId as UserID,ReceiveUserName as UserName,case when ReceiveUserId is not NULL and ReceiveUserId <> ''  THEN 'Personal' when ReceiveDeptId is not null and ReceiveDeptId <>'' then 'Group' else 'Broadcast' end as PushType FROM S_I_PublicInformation WHERE DateDiff(day,CreateTime,getDate())<='{0}') Info INNER JOIN S_I_PublicInformCatalog AS cat on info.CatalogId = cat.ID", cycleDay);
            var news = db.Database.SqlQuery<PushModel>(sqlNews);
            AddPush(news);
        }

        /// <summary>
        /// 添加图片新闻推送
        /// </summary>
        private void AddImageNewsPush()
        {
            var db = new BaseContext();
            string sql = string.Format(@"select ID,Title,Remark as Content,'ImageNews' as SourceType,case when DeptDoorId is null or DeptDoorId='' then 'Broadcast' else 'Group' end as PushType,DeptDoorId as DeptID,DeptDoorName as DeptName FROM S_I_NewsImageGroup WHERE DateDiff(day,CreateTime,getDate())<='{0}'", cycleDay);
            var news = db.Database.SqlQuery<PushModel>(sql);
            AddPush(news);
        }


        /// <summary>
        /// 添加任务推送
        /// </summary>
        private void AddFlowTaskPush()
        {
            string sqlTask = string.Format(@"select S_WF_InsTaskExec.InsTaskID as ID
                                ,'任务提醒' as Title
                                ,TaskName as Content
                                ,S_WF_InsTaskExec.TaskUserID as UserID
                                ,S_WF_InsTaskExec.TaskUserName as UserName
                                ,'Task' as SourceType
                                ,'Personal' as PushType
                                from S_WF_InsTaskExec
                                join S_WF_InsTask on ExecTime is null and S_WF_InsTask.Type in('Normal','Inital') and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='') and S_WF_InsTask.ID=InsTaskID
                                join S_WF_InsFlow on S_WF_InsFlow.Status='Processing' and S_WF_InsFlow.ID=S_WF_InsTask.InsFlowID  
                                join S_WF_InsDefFlow on InsDefFlowID=S_WF_InsDefFlow.ID
                                join S_WF_InsDefStep on InsDefStepID = S_WF_InsDefStep.ID 
                                join S_WF_DefStep on S_WF_DefStep.ID=DefStepID
                                where AllowToMobile='1' and DateDiff(day,S_WF_InsTask.CreateTime,getDate())<='{0}'
                                order by S_WF_InsTask.CreateTime desc",cycleDay);
            var db = new FlowContext();
            var tasks = db.Database.SqlQuery<PushModel>(sqlTask);
            AddPush(tasks);
        }

        /// <summary>
        /// 添加提醒推送
        /// </summary>
        private void AddAlarmPush()
        {
            var db = new BaseContext();

            //获取新的提醒
            string sqlAlarm = string.Format(@"select ID,AlarmTitle as Title,AlarmContent as Content,OwnerID as UserID,OwnerName as UserName,'Alarm' as SourceType,'Personal' as PushType from S_S_Alarm where SendTime<'{0}' and DeadlineTime>'{0}'", DateTime.Now);
            var alarm = db.Database.SqlQuery<PushModel>(sqlAlarm);
            AddPush(alarm);
        }

        /// <summary>
        /// 添加消息推送
        /// </summary>
        private void AddMsgPush()
        {
            var db = new BaseContext();

            //获取新的消息
            string sqlMsg = string.Format(@"select ID,Title,ContentText as Content,ReceiverIDs as UserID,ReceiverNames as UserName,'Msg' as SourceType,ReceiverDeptIDs as DeptID,ReceiverDeptNames as DeptName,case when ReceiverIDs is not NULL and ReceiverIDs <> ''  THEN 'Personal' when ReceiverDeptIDs is not null and ReceiverDeptIDs <>'' then 'Group' else 'Broadcast' end as PushType from S_S_MsgBody where DateDiff(day,SendTime,getDate())<='{0}'", cycleDay);
            var msgs = db.Database.SqlQuery<PushModel>(sqlMsg);
            AddPush(msgs);
        }

        /// <summary>
        /// 添加推送信息
        /// </summary>
        /// <param name="pushs"></param>
        public void AddPush(IEnumerable<PushModel> pushs)
        {
            var db = new BaseContext();
            var oldpushs = db.S_D_PushTask;
            foreach (var item in pushs)
            {

                var existTask = oldpushs.FirstOrDefault(p => p.SourceID == item.ID);

                if (existTask != null)
                {
                    //判断如果推送未成功，再次进入推送队列
                    //if (existTask.State == TaskState.Error.ToString() && !string.IsNullOrWhiteSpace(existTask.DoneLog))
                    if (existTask.State == TaskState.Error.ToString())
                    {
                        existTask.State = TaskState.New.ToString();
                        //existTask.DoneLog = string.Empty;
                    }
                }
                else
                {
                    var task = new S_D_PushTask
                    {
                        ID = GuidTool.CreateGuid(),
                        SourceID = item.ID,
                        Title = item.Title,
                        ShortContent = GetContent(item.Content, item.Title),
                        UserID = item.UserID,
                        UserName = item.UserName,
                        DeptID = item.DeptID,
                        DeptName = item.DeptName,
                        CreateTime = DateTime.Now,
                        SourceType = item.SourceType,
                        PushType = item.PushType,
                        State = TaskState.New.ToString()
                    };
                    db.S_D_PushTask.Add(task);
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                
            }
            
        }
    }

    /// <summary>
    /// 推送模型
    /// </summary>
    public class PushModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string SourceType { get; set; }
        public string PushType { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
    }
}
