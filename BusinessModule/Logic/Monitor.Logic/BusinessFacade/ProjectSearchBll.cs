using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Enum;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class ProjectSearchBll
    {
        /// <summary>
        /// 搜索项目信息
        /// </summary>
        /// <param name="content">搜索条件</param>
        /// <returns></returns>
        public static ResultDTO GetSearchData(string content)
        {
            var db = new ProjectContext();
            var result = new Dictionary<string, object>();

            //按项目搜索
            var sql = @"SELECT ID,Name FROM S_I_ProjectInfo WHERE Name like  '%{0}%'";
            var projects = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content)).Select(p => new { ID = p.ID, Name = p.Name });
            result.Add(ProjectSearchEnum.Projects.ToString(), new Dictionary<string, object> { { "Count", projects.Count() }, { "Data", projects } });

            //按客户搜索
            sql = @"SELECT CASE WHEN ISNULL(CustomerID,'')='' THEN CustomerName ELSE CustomerID END AS ID,CustomerName AS Name,COUNT(CustomerName) AS COUNT FROM S_I_ProjectInfo GROUP BY CustomerName,CustomerID HAVING CustomerName LIKE '%{0}%'";
            var customers = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content));
            result.Add(ProjectSearchEnum.Customers.ToString(), new Dictionary<string, object> { { "Count", customers.Count() }, { "Data", customers } });

            //按负责人搜索
            sql = @"SELECT ChargeUserID AS ID,ChargeUserName AS Name,COUNT(ChargeUserID) AS COUNT FROM S_I_ProjectInfo GROUP BY ChargeUserID,ChargeUserName HAVING ChargeUserName LIKE '%{0}%'";
            var users = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content));
            result.Add(ProjectSearchEnum.Users.ToString(), new Dictionary<string, object> { { "Count", users.Count() }, { "Data", users } });
            return WebApi.Success(result);
        }

        /// <summary>
        /// 查询明细
        /// </summary>
        /// <param name="id">客户ID或负责人ID</param>
        /// <param name="type">ContractSearchEnum</param>
        /// <returns></returns>
        public static ResultDTO SearchDetail(string id, string type)
        {
            var db = new ProjectContext();
            string condition = string.Format(type == ProjectSearchEnum.Customers.ToString() ? "(p.CustomerID='{0}' OR p.CustomerName='{0}')" : "p.ChargeUserID='{0}'", id);

            var sql = @"select ID,Name,Code,ChargeUserName,PlanStartDate,PlanFinishDate,CreateDate,
    CASE WHEN m.state is null then 1 else m.state end as state
                        FROM S_I_ProjectInfo p 
                        left join (SELECT ProjectInfoID,Max((CASE WHEN PlanFinishDate>=FactFinishDate THEN 0
                        WHEN PlanFinishDate<FactFinishDate or (FactFinishDate is null and PlanFinishDate < getdate()) 
                        THEN 2 ELSE 1 END)) as State 
                        FROM S_P_MileStone
                        group by ProjectInfoID) m
                        ON p.ID = m.ProjectInfoID 
                        WHERE  {0}  ORDER BY State DESC,CreateDate DESC";
            var list = db.Database.SqlQuery<ProjectOverviewBll>(string.Format(sql, condition));
            var result = new Dictionary<string, object> { { "Count", list.Count() }, { "Data", list } };
            return WebApi.Success(result);
        }
    }
}
