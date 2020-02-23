using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Enum;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class ContractSearchBll
    {
        /// <summary>
        /// 搜索合同信息
        /// </summary>
        /// <param name="content">搜索条件</param>
        /// <returns></returns>
        public static ResultDTO GetSearchData(string content)
        {
            var db = new MarketContext();
            var result = new Dictionary<string, object>();

            //按客户搜索
            var sql = @"select PartyA as ID,PartyAName as Name,count(PartyA) as Count from dbo.S_C_ManageContract
group by PartyA,PartyAName having PartyAName like '%{0}%'";
            var customers = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content));
            result.Add(ContractSearchEnum.Customers.ToString(), new Dictionary<string, object> { { "Count", customers.Count() }, { "Data", customers } });

            //按合同搜索
            sql = @"select ID,Name from dbo.S_C_ManageContract where Name like '%{0}%'";
            var contracts = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content)).Select(p => new { ID = p.ID, Name = p.Name });
            result.Add(ContractSearchEnum.Contracts.ToString(), new Dictionary<string, object> { { "Count", contracts.Count() }, { "Data", contracts } });

            //按销售负责人搜索
            sql = @"select BusinessManager as ID,BusinessManagerName as Name,count(BusinessManager) as Count 
from dbo.S_C_ManageContract 
group by BusinessManagerName,BusinessManager 
having BusinessManagerName like '%{0}%'";
            var users = db.Database.SqlQuery<ContractStatisticsDTO>(string.Format(sql, content));
            result.Add(ContractSearchEnum.Users.ToString(), new Dictionary<string, object> { { "Count", users.Count() }, { "Data", users } });
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
            var db = new MarketContext();
            string condition = type == ContractSearchEnum.Customers.ToString() ? "PartyA" : "BusinessManager";

            var sql = @"select ID,Name,SerialNumber,BusinessManagerName
                        ,Convert(decimal(18,2),isnull(ContractRMBAmount,0)/10000) as ContractRMBAmount
                        ,Convert(decimal(18,2),isnull(SumReceiptValue,0)/10000) as SummaryReceiptValue
                        ,Convert(decimal(18,2),(isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0))/10000) as SurplusReceipt
                        from dbo.S_C_ManageContract 
                        where {0}='{1}'";

            return WebApi.Success(db.Database.SqlQuery<ContractSearchBll>(string.Format(sql, condition, id)));
        }

        #region 返回模型
        /// <summary>
        /// 合同ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 合同负责人
        /// </summary>
        public string HeadOfSalesName { get; set; }

        /// <summary>
        /// 合同总额
        /// </summary>
        public decimal ContractRMBAmount { get; set; }

        /// <summary>
        /// 合同到款
        /// </summary>
        public decimal SummaryReceiptValue { get; set; }

        /// <summary>
        /// 合同剩余
        /// </summary>
        public decimal SurplusReceipt { get; set; }
        #endregion
    }
}
