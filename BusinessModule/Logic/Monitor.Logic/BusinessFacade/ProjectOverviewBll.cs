using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Helper;
using System.Text.RegularExpressions;

namespace Monitor.Logic.BusinessFacade
{
    public class ProjectOverviewBll
    {
        /// <summary>
        /// 获取在建项目列表
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public static ResultDTO GetPrjList(string condition)
        {
            string newCondition = "";
            string phaseCondition = "";
            string[] conArr = Regex.Split(condition, "and", RegexOptions.IgnoreCase);
            foreach (var item in conArr)
            {
                if (item.Contains("PhaseValue"))
                {
                    var phaseArr = item.Split('\'');
                    bool isValue = false;
                    foreach (var phase in phaseArr)
                    {
                        if (isValue)
                        {
                            if (string.IsNullOrEmpty(phaseCondition))
                                phaseCondition = " PhaseValue like '%" + phase + "%' ";
                            else
                                phaseCondition += "or PhaseValue like '%" + phase + "%' ";
                            isValue = false;
                        }
                        else
                        {
                            isValue = true;
                            continue;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(newCondition))
                        newCondition = item;
                    else
                        newCondition += " and " + item;
                }
            }
            if (!string.IsNullOrEmpty(phaseCondition))
                if (string.IsNullOrEmpty(newCondition))
                    newCondition = phaseCondition;
                else
                    newCondition += " and (" + phaseCondition + ") ";


            var sql = @"select ID,Name,Code,ChargeUserName,PlanStartDate,PlanFinishDate,CreateDate,
	CASE WHEN m.state is null then 1 else m.state end as state
                        FROM S_I_ProjectInfo p 
                        left join (SELECT ProjectInfoID,Max((CASE WHEN PlanFinishDate>=FactFinishDate THEN 0
                        WHEN PlanFinishDate<FactFinishDate or (FactFinishDate is null and PlanFinishDate < getdate()) 
                        THEN 2 ELSE 1 END)) as State 
                        FROM S_P_MileStone
                        group by ProjectInfoID) m
                        ON p.ID = m.ProjectInfoID 
                        WHERE {0} ORDER BY State DESC,CreateDate DESC";
            if (string.IsNullOrEmpty(newCondition))
                newCondition = " 1=1 ";
            var db = new ProjectContext();
            var list = db.Database.SqlQuery<ProjectOverviewBll>(string.Format(sql, newCondition));
            var result = new Dictionary<string, object> { { "Count", list.Count() }, { "Data", list } };
            return WebApi.Success(result);
        }

        /// <summary>
        /// 获取项目里程碑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ResultDTO GetPrjMileStones(string id)
        {
            var sql = @"SELECT Name,PlanFinishDate,FactFinishDate,(CASE WHEN PlanFinishDate>=FactFinishDate THEN '已完成'
WHEN PlanFinishDate<FactFinishDate or (FactFinishDate is null and PlanFinishDate < getdate()) THEN '延期'
ELSE '进行中' END) State FROM S_P_MileStone
WHERE ProjectInfoID = '{0}' AND MileStoneType='Normal'";
            var db = new ProjectContext();
            var list = db.Database.SqlQuery<MileStoneDTO>(string.Format(sql, id));
            return WebApi.Success(list);
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ChargeUserName { get; set; }

        /// <summary>
        /// 项目计划开始时间
        /// </summary>
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 项目计划完成时间
        /// </summary>
        public DateTime? PlanFinishDate { get; set; }

        /// <summary>
        /// 项目立项时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 项目状态（2:延期）
        /// </summary>
        public int State { get; set; }
    }
}
