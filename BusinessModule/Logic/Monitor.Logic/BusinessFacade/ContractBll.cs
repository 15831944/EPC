using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Enum;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class ContractBll
    {
        #region 获取合同
        public static ResultDTO GetContracts(string condition, string type, string id)
        {
            EnumBusiness con = (EnumBusiness)System.Enum.Parse(typeof(EnumBusiness), condition);
            ResultDTO result = new ResultDTO();
            switch (con)
            {
                case EnumBusiness.CurrentMonthActualReceipt:
                    result = GetCurrentActualReceipt(type, DateEnum.Month, id);
                    break;
                case EnumBusiness.CurrentQuarterActualReceipt:
                    result = GetCurrentActualReceipt(type, DateEnum.Quarter, id);
                    break;
                case EnumBusiness.CurrentWeekActualReceipt:
                    result = GetCurrentActualReceipt(type, DateEnum.Week, id);
                    break;
                case EnumBusiness.CurrentYearPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Year, id);
                    break;
                case EnumBusiness.CurrentYearAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.AfterYear, id);
                    break;
                case EnumBusiness.CurrentQuarterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Quarter, id);
                    break;
                case EnumBusiness.CurrentQuarterAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Quarter, id);
                    break;
                case EnumBusiness.CurrentMonthPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Month, id);
                    break;
                case EnumBusiness.CurrentMonthAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.AfterMonth, id);
                    break;
                case EnumBusiness.CurrentQuarterSignContract:
                    result = GetCurrentContract(type, DateEnum.Quarter, id);
                    break;
                case EnumBusiness.CurrentMonthSignContract:
                    result = GetCurrentContract(type, DateEnum.Month, id);
                    break;
                case EnumBusiness.CurrentWeekSignContract:
                    result = GetCurrentContract(type, DateEnum.Week, id);
                    break;
                case EnumBusiness.CurrentUnSignContract:
                    result = GetCurrentContract(type, DateEnum.Year, id);
                    break;
                case EnumBusiness.CurrentAccountReceivable:
                    result = GetCurrentAccountReceivable(type, id);
                    break;
                case EnumBusiness.CurrentSurplusContract:
                    result = GetCurrentSurplusContract(type, id);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获取实际收款详情(当季、当月、本周)
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="date"></param>
        /// <param name="id">客户ID或部门ID</param>
        /// <returns></returns>
        public static ResultDTO GetCurrentActualReceipt(string type, DateEnum date, string id)
        {
            string condition = "", idName = "";
            switch (date)
            {
                case DateEnum.Month:
                    condition = "BelongYear = YEAR(getDate()) AND BeLongMonth = MONTH(getDate())";
                    break;
                case DateEnum.Quarter:
                    condition = "BelongYear = YEAR(getDate()) AND BelongQuarter = datepart(quarter,getdate())";
                    break;
                case DateEnum.Week:
                    condition = "(ArrivedDate between dateadd(wk, datediff(wk,0,getdate()), 0)  and getdate())";
                    break;
            }

            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    idName = "con.CustomerID";
                    break;
                case EnumQueryType.Department:
                    idName = "ReceiptMasterUnitID";
                    break;
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            var sql = @"SELECT con.ContractID as ID,con.ContractName as Name,COUNT(ID) AS RecordNum,
                        Convert(decimal(18,2),isnull(SUM(Amount),0)/10000) AS ReceiptValue,
                        MAX(ArrivedDate) AS LastTime FROM S_C_Receipt left join
                        (
	                        SELECT ID as ContractID,Name as ContractName,PartyA as CustomerID 
	                        FROM S_C_ManageContract WHERE  IsSigned='Signed'
                        )con on S_C_Receipt.CustomerID=con.CustomerID and S_C_Receipt.ContractInfoID=con.ContractID
                        WHERE {0} = '{1}'  AND {2}
                        GROUP BY con.ContractName,con.ContractID
                        ORDER BY ReceiptValue DESC";
            return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, idName, id, condition)));
        }

        /// <summary>
        /// 获取计划收款详情（本年、本年后、本季、本季后、本月、本月后）
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentPlanReceipt(string type, DateEnum date, string id)
        {
            string sql, condition = "", idName = "";
            switch (date)
            {
                case DateEnum.Year:
                    condition = "S_C_PlanReceipt.BelongYear <= YEAR(getdate())";
                    break;
                case DateEnum.AfterYear:
                    condition = "S_C_PlanReceipt.BelongYear > datepart(year,getdate()) OR S_C_PlanReceipt.BelongYear = NULL";
                    break;
                case DateEnum.Quarter:
                    condition = "S_C_PlanReceipt.BelongYear <= datepart(year,getdate()) AND S_C_PlanReceipt.BelongQuarter <= datepart(quarter,getdate())";
                    break;
                case DateEnum.AfterQuarter:
                    condition =
                        "(S_C_PlanReceipt.BelongYear = datepart(year,getdate()) AND S_C_PlanReceipt.BelongQuarter > datepart(quarter,getdate())) OR(S_C_PlanReceipt.BelongYear > datepart(year,getdate())) OR (S_C_PlanReceipt.BelongYear = NULL OR S_C_PlanReceipt.BelongQuarter = NULL)";
                    break;
                case DateEnum.Month:
                    condition = "S_C_PlanReceipt.BelongYear <= datepart(year,getdate()) AND S_C_PlanReceipt.BelongMonth <= datepart(month,getdate())";
                    break;
                case DateEnum.AfterMonth:
                    condition =
                        "(S_C_PlanReceipt.BelongYear = datepart(year,getdate()) AND S_C_PlanReceipt.BelongMonth >datepart(month,getdate())) OR (S_C_PlanReceipt.BelongYear > datepart(year,getdate())) OR (S_C_PlanReceipt.BelongYear = NULL OR S_C_PlanReceipt.BelongQuarter = NULL OR S_C_PlanReceipt.BelongMonth = NULL)";
                    break;

            }

            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    idName = "S_C_PlanReceipt.CustomerId";
                    break;
                case EnumQueryType.Department:
                    idName = "S_C_PlanReceipt.ProductionUnitID";
                    break;
                case EnumQueryType.ManagerUser:
                    idName = "S_C_PlanReceipt.FirstDutyManID";
                    break;
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            sql = @"SELECT Convert(decimal(18,2),isnull(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) AS ReceiptValue,
                    ContractInfoID as ID,mc.Name as Name FROM S_C_PlanReceipt
                    LEFT JOIN S_C_ManageContract mc
                    ON S_C_PlanReceipt.ContractInfoID=mc.ID
                    WHERE {0} = '{1}' and S_C_PlanReceipt.State in ('UnReceipt') AND {2}
                    GROUP BY ContractInfoID,mc.Name
                    ORDER BY ReceiptValue DESC";
            return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, idName, id, condition)));
        }

        /// <summary>
        /// 获取已签合同详情（当季、当月、本周）和待签合同额（当年）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentContract(string type, DateEnum date, string id)
        {
            string sql, condition = "", idName = "";
            switch (date)
            {
                case DateEnum.Quarter:
                    condition = "IsSigned='Signed' and BelongYear = YEAR(getDate()) AND BelongQuarter=datepart(quarter,getdate())";
                    break;
                case DateEnum.Month:
                    condition = "IsSigned='Signed' and BelongYear = YEAR(getDate()) AND BelongMonth=datepart(month,getdate())";
                    break;
                case DateEnum.Week:
                    condition = "IsSigned='Signed' and SignDate between dateadd(wk, datediff(wk,0,getdate()), 0) and getdate()";
                    break;
                case DateEnum.Year:
                    condition = "IsSigned!='Signed' --and BelongYear=datepart(year,getdate())";
                    break;
            }

            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    idName = "PartyA";
                    break;
                //case EnumQueryType.Department:
                //    idName = "HeadOfSalesDeptID";
                //    break;
                case EnumQueryType.ManagerUser:
                    idName = "BusinessManager";
                    break;
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            sql = @"select ID,Name,Convert(decimal(18,2),isnull(ContractRMBAmount,0)/10000) as ReceiptValue,
                            SignDate,BusinessManagerName as UserName from S_C_ManageContract
                            where {0} and {1}='{2}'
                            order by ReceiptValue desc";
            return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, condition, idName, id)));
        }

        /// <summary>
        /// 获取当前应收款详情
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentAccountReceivable(string type, string id)
        {
            string sql, idName = "";
            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    idName = "con.PartyA";
                    break;
                case EnumQueryType.Department:
                    idName = "con.ProductionDept";
                    break;
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            sql = @"SELECT Convert(decimal(18,2),isnull(SUM(SumInvoiceValue-ISNULL(SumReceiptValue,0)),0)/10000) AS ReceiptValue,
                    con.ID as ID,con.Name AS Name,
                    MAX(S_C_Invoice.InvoiceDate) InvoiceDate 
                    FROM S_C_ManageContract con 
                    left join S_C_Invoice
                    on con.ID = S_C_Invoice.ContractInfoID
                    WHERE {0} = '{1}'
					and con.IsSigned='Signed'
                    GROUP BY con.ID,con.Name
                    ORDER BY ReceiptValue DESC";
            return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, idName, id)));
        }

        /// <summary>
        /// 获取当前合同余额详情
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentSurplusContract(string type, string id)
        {
            string sql, idName = "";
            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    idName = "PartyA";
                    break;
                //case EnumQueryType.Department:
                //    idName = "HeadOfSalesDeptID";
                //    break;
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            sql = @"select ID,Name,Convert(decimal(18,2),((isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0))/10000)) as ReceiptValue,
                    SignDate from S_C_ManageContract 
                    where IsSigned='Signed' and {0}='{1}'
                    order by ReceiptValue desc";
            return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, idName, id)));
        }


        /// <summary>
        /// 合同ID
        /// </summary>
        [Description("合同ID")]
        public string ID { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        [Description("合同名称")]
        public string Name { get; set; }

        /// <summary>
        /// 合同笔数
        /// </summary>
        [Description("合同笔数")]
        public int RecordNum { get; set; }

        /// <summary>
        /// 合同额
        /// </summary>
        [Description("合同额")]
        public decimal ReceiptValue { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Description("负责人")]
        public string UserName { get; set; }

        /// <summary>
        /// 最后一笔时间
        /// </summary>
        [Description("最后一笔时间")]
        public DateTime? LastTime { get; set; }

        /// <summary>
        /// 签订时间
        /// </summary>
        [Description("签订时间")]
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 开票时间
        /// </summary>
        [Description("开票时间")]
        public DateTime? InvoiceDate { get; set; }
        #endregion

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        public static ResultDTO GetDetail(string id)
        {
            var sql = @"SELECT Name,SerialNumber 
                        ,Convert(decimal(18,2),isnull(ContractRMBAmount,0)/10000) as ContractRMBAmount
                        ,Convert(decimal(18,2),isnull(SumInvoiceValue,0)/10000) as SummaryInvoiceValue
                        ,Convert(decimal(18,2),isnull(SumReceiptValue,0)/10000) as SummaryReceiptValue
                        ,case when isNull(CONVERT(varchar(100), SignDate, 23),'')='' then '--' else isNull(CONVERT(varchar(100), SignDate, 23),'') end as StartDate
                        ,case when isNull(CONVERT(varchar(100), EndDate, 23),'')='' then '--' else isNull(CONVERT(varchar(100), EndDate, 23),'') end as EndDate
                        ,case when isNull(BusinessManagerName,'')='' then '未安排' else isNull(BusinessManagerName,'') end as HeadOfSalesName
                        ,case when isNull(ProductionManagerName,'')='' then '未安排' else isNull(ProductionManagerName,'') end as ProduceMasterName
                        ,'' as SignDept FROM S_C_ManageContract 
                        where ID='{0}'";
            var db = new MarketContext();
            return WebApi.Success(db.Database.SqlQuery<ContractDTO>(string.Format(sql, id)).FirstOrDefault());
        }

        /// <summary>
        /// 获取到款记录
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        public static ResultDTO GetReceiptRecords(string id)
        {
            var sql = @"SELECT CONVERT(varchar(100), a.ArrivedDate, 20) as ArrivedDate
                        ,Convert(decimal(18,2),isnull(a.Amount,0)/10000) as ArrivedMoney
                        ,c.Name,a.ID
                         FROM S_C_Receipt AS a
                        Left JOIN S_C_ReceiptPlanRelation AS b
                        ON a.ID=b.ReceiptID
                        Left JOIN S_C_PlanReceipt AS c
                        ON b.PlanID=c.ID
                        left join (select ID,PartyA from S_C_ManageContract where IsSigned='Signed')con
                        on a.CustomerID = con.PartyA and con.ID=a.ContractInfoID
                        WHERE con.ID='{0}'
                        ORDER BY ArrivedDate DESC";
            var db = new MarketContext();
            var list = db.Database.SqlQuery<ReceiptRecordDTO>(string.Format(sql, id));
            var results = new List<ReceiptRecordDTO>();
            foreach (var item in list.GroupBy(p => p.ID))
            {
                string names = "";
                string arrivedDate = "";
                decimal arrivedMoney = 0;
                var t = item.GetEnumerator();
                while (t.MoveNext())
                {
                    names += t.Current.Name + ",";
                    arrivedDate = t.Current.ArrivedDate;
                    arrivedMoney = t.Current.ArrivedMoney;
                }
                if (names.IndexOf(',') >= 0)
                {
                    names = names.Substring(0, names.Length - 1);
                }
                if (string.IsNullOrEmpty(names))
                {
                    names = "";
                }
                results.Add(new ReceiptRecordDTO
                {
                    ArrivedMoney = arrivedMoney,
                    ArrivedDate = arrivedDate,
                    Name = names
                });
            }

            return WebApi.Success(results);
        }

        /// <summary>
        /// 获取收款
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        public static ResultDTO GetPlanReceipts(string id)
        {
            var sql = @"SELECT ID,Name,FirstDutyManName,   
                        CONVERT(varchar(100), ISNULL(PlanReceiptDate,'9999-12-31'), 23) as PlanReceiptDate,
                        Convert(decimal(18,2),isnull(PlanReceiptValue,0)/10000) as PlanReceiptValue,
                        Convert(decimal(18,2),ISNULL(FactReceiptValue,0)/10000) AS FactReceiptValue
                        ,S_C_PlanReceipt.State FROM S_C_PlanReceipt
                        WHERE S_C_PlanReceipt.State in ('UnReceipt') AND ContractInfoID = '{0}' 
                        ORDER BY State DESC,PlanReceiptDate ASC";
            var db = new MarketContext();
            return WebApi.Success(db.Database.SqlQuery<PlanReceiptDTO>(string.Format(sql, id)));
        }

        /// <summary>
        /// 修改计划收款日期
        /// </summary>
        /// <param name="id">计划收款ID</param>
        /// <param name="date">修改时间</param>
        /// <returns></returns>
        public static ResultDTO UpdatePlanReceiptDate(string id, string date)
        {
            DateTime dt = DateTime.Parse(date);
            var db = new MarketContext();
            var plan = db.S_C_PlanReceipt.Find(id);
            if (plan.State == "UnReceipt")//PlanReceiptState.UnReceipt.ToString()
            {
                if (plan.PlanReceiptDate.HasValue)
                {
                    if (Convert.ToDateTime(plan.PlanReceiptDate) > dt)
                        return WebApi.Error("延迟的新日期不能早于原计划完成日期");
                }

                var newplan = new S_C_PlanReceipt()
                {
                    ID = Base.CreateGuid(),
                    PlanReceiptDate = dt,
                    BelongYear = dt.Year,
                    BelongMonth = dt.Month,
                    BelongQuarter = Base.GetQuarter(dt),
                    PlanReceiptYearMonth = dt.Year.ToString() + dt.Month.ToString("00"),

                    BadDebtValue = plan.BadDebtValue,
                    ContractCode = plan.ContractCode,
                    ContractInfoID = plan.ContractInfoID,
                    ContractName = plan.ContractName,
                    CustomerCode = plan.CustomerCode,
                    CustomerName = plan.CustomerName,
                    CustomerID = plan.CustomerID,
                    CreateDate = plan.CreateDate,
                    CreateUser = plan.CreateUser,
                    CreateUserID = plan.CreateUserID,
                    DutyType = plan.DutyType,
                    FactReceiptValue = plan.FactReceiptValue,
                    FirstDutyManID = plan.FirstDutyManID,
                    FirstDutyManName = plan.FirstDutyManName,
                    Importance = plan.Importance,
                    IsBadDebt = plan.IsBadDebt,
                    MasterID = plan.MasterID,
                    MasterName = plan.MasterName,
                    MasterUnit = plan.MasterUnit,
                    MasterUnitID = plan.MasterUnitID,
                    ModifyDate = plan.ModifyDate,
                    ModifyUser = plan.ModifyUser,
                    ModifyUserID = plan.ModifyUserID,
                    Name = plan.Name,
                    PlanReceiptValue = plan.PlanReceiptValue,
                    ProduceMasterID = plan.ProduceMasterID,
                    ProduceMasterName = plan.ProduceMasterName,
                    ProductionUnitID = plan.ProductionUnitID,
                    ProductionUnitName = plan.ProductionUnitName,
                    ProjectCode = plan.ProjectCode,
                    ProjectID = plan.ProjectID,
                    ProjectName = plan.ProjectName,
                    ReceiptDate = plan.ReceiptDate,
                    ReceiptObjectID = plan.ReceiptObjectID,
                    RelateParentID = plan.RelateParentID,
                    RiskLevel = plan.RiskLevel,
                    Remark = plan.Remark,
                    S_C_ManageContract = plan.S_C_ManageContract,
                    S_C_ManageContract_ReceiptObj = plan.S_C_ManageContract_ReceiptObj,
                    State = plan.State
                };
                
                db.S_C_PlanReceipt.Add(newplan);
                plan.State = "UnFinished";//PlanReceiptState.UnFinished.ToString();
                return WebApi.Success(db.SaveChanges() > 0);
            }
            else
            {
                return WebApi.Error(EnumException.操作异常);
            }
        }
    }
}
