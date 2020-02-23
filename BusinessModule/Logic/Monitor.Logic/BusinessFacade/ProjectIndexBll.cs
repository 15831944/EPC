using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class ProjectIndexBll
    {
        public static ResultDTO GetData()
        {
            var markdbname = Base.GetDbName("Market");
            var prjdbname = Base.GetDbName("Project");
            var prjBasedbname = Base.GetDbName("ProjectBaseConfig");

            var db = new ProjectContext();
            var result = new Dictionary<string, object>();
            //基础统计
            var sql = @"select (SELECT COUNT(DISTINCT ID) 
                        FROM [{1}].[dbo].[S_I_ProjectInfo]
                        WHERE State = 'Plan' OR State = 'Execute') as ExecuteProject,
                        (SELECT COUNT(DISTINCT(ProjectInfoID)) FROM [{1}].[dbo].S_P_MileStone m
                        where (((m.PlanFinishDate-m.FactFinishDate)<0) OR (m.PlanFinishDate <= getDate() AND m.FactFinishDate is NULL)) 
                        AND m.ProjectInfoID IN (SELECT Distinct p.ID
                        FROM [{1}].[dbo].[S_I_ProjectInfo] p
                        where p.State = 'Plan' OR p.State = 'Execute')) DelayProject, 
                        (SELECT ISNULL(SUM(ISNULL(ReceiptValue,0)-ISNULL(FactReceiptValue,0)-ISNULL(FactBadValue,0)),0) 
                        FROM [{0}].[dbo].[S_C_ManageContract_ReceiptObj]
                        WHERE ProjectInfo IN (SELECT Distinct p.ID
                        FROM [{1}].[dbo].[S_I_ProjectInfo] p
                        where p.State = 'Plan' OR p.State = 'Execute')) RestMoney,
                        (SELECT ISNULL(SUM(ISNULL(ReceiptValue,0)),0) 
                        FROM [{0}].[dbo].[S_C_ManageContract_ReceiptObj]
                        WHERE ProjectInfo IN (SELECT Distinct p.ID
                        FROM [{1}].[dbo].[S_I_ProjectInfo] p
                        where p.State = 'Plan' OR p.State = 'Execute')) TotalMoney,
                        (SELECT COUNT(ID) FROM [{1}].[dbo].[S_I_ProjectInfo])  TotalProject";
            var projectInfo = db.Database.SqlQuery<ProjectStatisticsDTO>(string.Format(sql, markdbname, prjdbname)).FirstOrDefault();
            result.Add("ProjectInfo", projectInfo);

            //阶段分布
            sql = @"select Code as [Key],sum(isnull(Value,0)) as Value from (select Code,SortIndex from 
                    {0}.dbo.S_D_WBSAttrDefine where type='Phase') d
                    left join (select COUNT(PhaseValue) as Value,PhaseValue from dbo.S_I_ProjectInfo
                    where State = 'Plan' OR State = 'Execute'
                    GROUP BY PhaseValue) p on p.PhaseValue like '%'+d.Code+'%'
                    GROUP BY Code,SortIndex
                    order by SortIndex";
            var phaseInfo = db.Database.SqlQuery<StageDistributionDTO>(string.Format(sql, prjBasedbname)).ToDictionary(item => item.Key, item => item.Value);
            result.Add("PhaseInfo", phaseInfo);

            //部门分布
            sql = @"select a.ChargeDeptName Name,a.ExecuteProject-b.DelayProject as ExecuteProject,b.DelayProject from
                        (SELECT COUNT(ChargeDeptID) ExecuteProject,ChargeDeptName,ChargeDeptID
                        FROM dbo.S_I_ProjectInfo p
                        WHERE State = 'Plan' OR State = 'Execute'
                        GROUP BY ChargeDeptID,ChargeDeptName) a
                        join (SELECT COUNT(Distinct ProjectInfoID) DelayProject,p.ChargeDeptName,p.ChargeDeptID 
                        FROM dbo.S_P_MileStone m
                        RIGHT JOIN  dbo.S_I_ProjectInfo p
                        ON m.ProjectInfoID = p.ID AND (p.State = 'Plan' OR p.State = 'Execute')
                        where (((m.PlanFinishDate-m.FactFinishDate)<0) OR (m.PlanFinishDate <= getDate() 
                        AND m.FactFinishDate is NULL))
                        GROUP BY  p.ChargeDeptID,p.ChargeDeptName) b 
                        on a.ChargeDeptID=b.ChargeDeptID";
            var departmentInfo = db.Database.SqlQuery<SectorDistributionDTO>(sql);
            result.Add("DepartmentInfo", departmentInfo);
            return WebApi.Success(result);
        }
    }
}
