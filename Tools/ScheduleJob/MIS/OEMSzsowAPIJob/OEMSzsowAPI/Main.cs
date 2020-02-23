using Config;
using Config.Logic;
using OEMSzsowAPI.ApiLogic;
using OEMSzsowAPI.Common;
using OEMSzsowAPI.Helper;
using OEMSzsowAPI.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Formula;

namespace OEMSzsowAPI
{
    public class Main : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log4NetConfig.Configure();
            try
            {
                Start();
            }
            catch (Exception e)
            {
                LogWriter.Info(string.Format("同步程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Message));
                LogWriter.Error(e, e.Message);
            }
        }

        private void Start()
        {
            SQLHelper baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            SQLHelper businessHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            //根据时间生成队列数据
            CreateSyncTask(baseHelper, businessHelper);

            //读取队列表
            var list = baseHelper.ExecuteList<S_OEM_TaskList>("select * from S_OEM_TaskList where OEMCode='Szsow' and SyncState is null or SyncState=''");

            LogWriter.Info(string.Format("接口调用开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            foreach (var task in list)
            {
                BaseApi logic = null;
                LogWriter.Info(string.Format("TaskID：{0} 开始", task.ID.ToString()));
                switch (task.BusinessCode.ToLower())
                {
                    case "project":
                        logic = new Project(task);
                        break;
                    case "user":
                        logic = new User(task);
                        break;
                    case "plan":
                        logic = new Plan(task);
                        break;
                    case "exchange":
                        logic = new Exchange(task);
                        break;
                    case "task":
                        logic = new Task(task);
                        break;
                    case "projectareadata"://项目资料及成果附件
                        logic = new ProjectAreaData(task);
                        break;
                    default:
                        break;
                }
                //LogWriter.Info(string.Format("TaskID：{0} Logic实例化完成", task.ID.ToString()));
                logic.Sync();
                //LogWriter.Info(string.Format("TaskID：{0} 结束", task.ID.ToString()));
            }
            LogWriter.Info(string.Format("接口调用结束：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        private void CreateSyncTask(SQLHelper baseHelper, SQLHelper businessHelper)
        {
            //根据时间生成队列数据
            try
            {
                LogWriter.Info(string.Format("根据时间生成队列数据开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                //删除一周以前同步成功的数据
                baseHelper.ExecuteNonQuery(@"delete from S_OEM_TaskList where OEMCode='Szsow' and SyncState in ('Complate') 
                and CreateTime <= '" + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd HH:mm:ss") + @"' 
                delete from S_OEM_TaskList where OEMCode='Szsow' and SyncState in ('Error')  and BusinessType ='GetData' 
                and CreateTime <= '" + DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                CreateUserList(baseHelper);
                CreateProjectList(baseHelper, businessHelper);
                CreateProjectPlanList(baseHelper, businessHelper);
                CreateProjectExchangeList(baseHelper, businessHelper);
                CreateProjectTaskList(baseHelper, businessHelper);
            }
            catch (Exception e)
            {
                LogWriter.Info(string.Format("根据时间生成队列数据异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Message));
                LogWriter.Error(e, e.Message);
            }
            LogWriter.Info(string.Format("根据时间生成队列数据结束：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        private void CreateUserList(SQLHelper baseHelper)
        {
            //增改
            var lastSyncTime = baseHelper.ExecuteScalar("select top 1 CreateTime from S_OEM_TaskList where OEMCode='Szsow' and BusinessCode='User'  order by CreateTime desc");
            if (lastSyncTime == null || lastSyncTime == DBNull.Value)
            {
                lastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select 'Szsow','Save','User',ID,'" + lastSyncTime + "' from S_A_User where ModifyTime is not null";
                baseHelper.ExecuteNonQuery(sql);
            }
            else
            {
                var startDate = Convert.ToDateTime(lastSyncTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select 'Szsow','Save','User',ID,'" + endDate + "' from S_A_User where ModifyTime >='" + startDate + "' and ModifyTime <='" + endDate + "'";
                baseHelper.ExecuteNonQuery(sql);
            }

            #region 删除
            var deleteUser = true;
            bool.TryParse(ConfigurationManager.AppSettings["SyncDeleteUser"], out deleteUser);
            if (deleteUser)
            {
                #region 获得OEM所有ID
                var task = new S_OEM_TaskList();
                task.OEMCode = "Szsow";
                task.BusinessCode = "User";
                task.BusinessType = BusinessType.GetData.ToString();
                task.CreateTime = DateTime.Now;
                var getSql = string.Format(@"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,CreateTime) values( '{0}','{1}','{2}','{3}')
select @@IDENTITY", task.OEMCode, task.BusinessType, task.BusinessCode, task.CreateTime.ToString());
                task.ID = Convert.ToInt64(baseHelper.ExecuteScalar(getSql));
                var sync = new User(task);
                var oemIDs = sync.GetIDs();
                #endregion
                //获得自己系统所有ID
                var epmIDs = GlobalData.UserDt.AsEnumerable().Select(c => c.Field<string>("id")).ToList();
                //OEM有的数据，我们没有的数据，就是需要删除的数据
                var saID = ConfigurationManager.AppSettings["OEMSaUserID"];//不能删除超级管理员
                var deleteIDs = oemIDs.Where(a => !epmIDs.Contains(a) && a != saID).ToArray();
                //插入删除记录
                if (deleteIDs.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var deleteID in deleteIDs)
                    {
                        sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                            task.OEMCode, BusinessType.Remove.ToString(), task.BusinessCode, deleteID, task.CreateTime.ToString());
                        sb.AppendLine();
                    }
                    baseHelper.ExecuteNonQuery(sb.ToString());
                }
            #endregion
            }
        }

        private void CreateProjectList(SQLHelper baseHelper, SQLHelper businessHelper)
        {
            //增改
            var lastSyncTime = baseHelper.ExecuteScalar("select top 1 CreateTime from S_OEM_TaskList where OEMCode='Szsow' and BusinessCode='Project' and BusinessType!='" + BusinessType.GetData.ToString() + "' order by CreateTime desc");
            if (lastSyncTime == null || lastSyncTime == DBNull.Value)
            {
                lastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select 'Szsow','Save','Project',ID,'" + lastSyncTime + "' from " + businessHelper.DbName + "..S_I_ProjectInfo where ModifyDate is not null and ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
            else
            {
                var startDate = Convert.ToDateTime(lastSyncTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select 'Szsow','Save','Project',ID,'" + endDate + "' from " + businessHelper.DbName + "..S_I_ProjectInfo where ModifyDate >='" + startDate + "' and ModifyDate <='" + endDate + "' and ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
            #region 获得OEM所有ID
            var task = new S_OEM_TaskList();
            task.OEMCode = "Szsow";
            task.BusinessCode = "Project";
            task.BusinessType = BusinessType.GetData.ToString();
            task.CreateTime = DateTime.Now;
            var getSql = string.Format(@"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,CreateTime) values( '{0}','{1}','{2}','{3}')
select @@IDENTITY", task.OEMCode, task.BusinessType, task.BusinessCode, task.CreateTime.ToString());
            task.ID = Convert.ToInt64(baseHelper.ExecuteScalar(getSql));
            var sync = new Project(task);
            var oemIDs = sync.GetIDs();
            StringBuilder sb = new StringBuilder();
            #endregion
            var prjDt = businessHelper.ExecuteDataTable("select id,state from S_I_ProjectInfo");
            //获得自己系统所有ID
            var epmIDs = prjDt.AsEnumerable().Select(c => c.Field<string>("id")).ToList();
            #region 删除
            var deletePrj = true;
            bool.TryParse(ConfigurationManager.AppSettings["SyncDeleteProject"], out deletePrj);
            if (deletePrj)
            {
                //OEM有的数据，我们没有的数据，就是需要删除的数据
                var deleteIDs = oemIDs.Where(a => !epmIDs.Contains(a)).ToArray();
                //插入删除记录
                if (deleteIDs.Length > 0)
                {
                    foreach (var deleteID in deleteIDs)
                    {
                        sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                            task.OEMCode, BusinessType.Remove.ToString(), task.BusinessCode, deleteID, task.CreateTime.ToString());
                        sb.AppendLine();
                    }
                }
            }
            #endregion
            #region 插入获得项目的文件记录的队列数据，发图计划、提资计划、工作任务、公共区数据
            epmIDs = prjDt.Select("state not in ('Finish','Pause','Terminate')").AsEnumerable().Select(c => c.Field<string>("id")).ToList();
            if (epmIDs.Count > 0)
            {
                foreach (var projectId in epmIDs)
                {
                    //只同步在运行项目，并且存在于四方系统的项目
                    if (!oemIDs.Contains(projectId))
                        continue;
                    sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        task.OEMCode, BusinessType.GetData.ToString(), "Plan", projectId, task.CreateTime.ToString());//发图计划节点下文件记录
                    sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        task.OEMCode, BusinessType.GetData.ToString(), "Exchange", projectId, task.CreateTime.ToString());//提资计划节点下文件记录
                    sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        task.OEMCode, BusinessType.GetData.ToString(), "Task", projectId, task.CreateTime.ToString());//工作任务节点下文件记录
                    sb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        task.OEMCode, BusinessType.GetData.ToString(), "ProjectAreaData", projectId, task.CreateTime.ToString());//公共区目录及文件
                    sb.AppendLine();
                }
            }
            #endregion
            if (sb.Length > 0)
                baseHelper.ExecuteNonQuery(sb.ToString());
        }

        private void CreateProjectPlanList(SQLHelper baseHelper, SQLHelper businessHelper)
        {
            //增改
            var lastSyncTime = baseHelper.ExecuteScalar("select top 1 CreateTime from S_OEM_TaskList where OEMCode='Szsow' and BusinessCode='Plan' and BusinessType!='" + BusinessType .GetData.ToString()+ "' order by CreateTime desc");
            if (lastSyncTime == null || lastSyncTime == DBNull.Value)
            {
                lastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Plan',ProjectInfoID,'" + lastSyncTime + "' from " + businessHelper.DbName + @"..S_P_MileStone m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where MileStoneType = 'Major' and m.ModifyDate is not null and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
            else
            {
                var startDate = Convert.ToDateTime(lastSyncTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Plan',ProjectInfoID,'" + endDate + "' from " + businessHelper.DbName + @"..S_P_MileStone  m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where MileStoneType = 'Major' and m.ModifyDate >='" + startDate + "' and m.ModifyDate <='" + endDate + "' and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
        }

        private void CreateProjectExchangeList(SQLHelper baseHelper, SQLHelper businessHelper)
        {
            //增改
            var lastSyncTime = baseHelper.ExecuteScalar("select top 1 CreateTime from S_OEM_TaskList where OEMCode='Szsow' and BusinessCode='Exchange' and BusinessType!='" + BusinessType.GetData.ToString() + "'  order by CreateTime desc");
            if (lastSyncTime == null || lastSyncTime == DBNull.Value)
            {
                lastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Exchange',ProjectInfoID,'" + lastSyncTime + "' from " + businessHelper.DbName + @"..S_P_MileStone m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where MileStoneType = 'Cooperation' and m.ModifyDate is not null and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
            else
            {
                var startDate = Convert.ToDateTime(lastSyncTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Exchange',ProjectInfoID,'" + endDate + "' from " + businessHelper.DbName + @"..S_P_MileStone m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where MileStoneType = 'Cooperation' and m.ModifyDate >='" + startDate + "' and m.ModifyDate <='" + endDate + "' and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
        }

        private void CreateProjectTaskList(SQLHelper baseHelper, SQLHelper businessHelper)
        {
            //增改
            var lastSyncTime = baseHelper.ExecuteScalar("select top 1 CreateTime from S_OEM_TaskList where OEMCode='Szsow' and BusinessCode='Task' and BusinessType!='" + BusinessType.GetData.ToString() + "'  order by CreateTime desc");
            if (lastSyncTime == null || lastSyncTime == DBNull.Value)
            {
                lastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Task',ProjectInfoID,'" + lastSyncTime + "' from " + businessHelper.DbName + @"..S_W_TaskWork m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where m.ModifyDate is not null and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
            else
            {
                var startDate = Convert.ToDateTime(lastSyncTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sql = @"insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime)
select distinct 'Szsow','Save','Task',ProjectInfoID,'" + endDate + "' from " + businessHelper.DbName + @"..S_W_TaskWork m
left join " + businessHelper.DbName + @"..S_I_ProjectInfo p on p.ID = m.ProjectInfoID
                where m.ModifyDate >='" + startDate + "' and m.ModifyDate <='" + endDate + "' and p.ModeCode='Architecture_OEM_Szsow'";
                baseHelper.ExecuteNonQuery(sql);
            }
        }

    }
}
