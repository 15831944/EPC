using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Enum;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class BusinessDetailBll
    {
        public static ResultDTO GetData(string condition,string type)
        {
            EnumBusiness con =(EnumBusiness)System.Enum.Parse(typeof (EnumBusiness), condition);
            ResultDTO result=new ResultDTO();
            switch (con)
            {
                case EnumBusiness.CurrentMonthActualReceipt: 
                    result=GetCurrentActualReceipt(type, DateEnum.Month);
                    break;
                case EnumBusiness.CurrentQuarterActualReceipt:
                    result = GetCurrentActualReceipt(type, DateEnum.Quarter);
                    break;
                case EnumBusiness.CurrentWeekActualReceipt:
                    result = GetCurrentActualReceipt(type, DateEnum.Week);
                    break;
                case EnumBusiness.CurrentYearPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Year);
                    break;
                case EnumBusiness.CurrentYearAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.AfterYear);
                    break;
                case EnumBusiness.CurrentQuarterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Quarter);
                    break;
                case EnumBusiness.CurrentQuarterAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Quarter);
                    break;
                case EnumBusiness.CurrentMonthPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.Month);
                    break;
                case EnumBusiness.CurrentMonthAfterPlanReceipt:
                    result = GetCurrentPlanReceipt(type, DateEnum.AfterMonth);
                    break;
                case EnumBusiness.CurrentQuarterSignContract:
                    result = GetCurrentContract(type, DateEnum.Quarter);
                    break;
                case EnumBusiness.CurrentMonthSignContract:
                    result = GetCurrentContract(type, DateEnum.Month);
                    break;
                case EnumBusiness.CurrentWeekSignContract:
                    result = GetCurrentContract(type, DateEnum.Week);
                    break;
                case EnumBusiness.CurrentUnSignContract:
                    result = GetCurrentContract(type, DateEnum.Year);
                    break;
                case EnumBusiness.CurrentAccountReceivable:
                    result = GetCurrentAccountReceivable(type);
                    break;
                case EnumBusiness.CurrentSurplusContract:
                    result = GetCurrentSurplusContract(type);
                    break;
            }
            
            return result;
        }


        /// <summary>
        /// 获取实际收款(当季、当月、本周)
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentActualReceipt(string type,DateEnum date)
        {
            string sql,condition="";
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
                    sql = @"select CustomerID as ID,CustomerName AS Name
                            ,Convert(decimal(18,2),isnull(SUM(Amount),0)/10000) AS TotalMoney
                            ,count(distinct ContractInfoID)  as ContractCount
                            ,COUNT(DISTINCT ID) AS RecordNum from S_C_Receipt WHERE {0} 
                            GROUP BY CustomerName,CustomerID
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.Department:
                    sql = @"SELECT ReceiptMasterUnitID as ID,ReceiptMasterUnit as Name
                            ,Convert(decimal(18,2),isnull(SUM(Amount),0)/10000) AS TotalMoney
                            ,count(distinct ContractInfoID) as ContractCount
                            ,COUNT(DISTINCT CustomerID) AS RecordNum  
                            FROM S_C_Receipt WHERE {0} 
                            GROUP BY ReceiptMasterUnit,ReceiptMasterUnitID
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.NotGroup:
                    sql = @"select ContractInfoID as ID,ContractName as Name,
                            COUNT(ContractName) AS RecordNum,
                            Convert(decimal(18,2),isnull(SUM(Amount)/10000,0)) AS ReceiptValue,
                            MAX(ArrivedDate) AS LastTime from S_C_Receipt WHERE {0}
                            GROUP BY ContractName,ContractInfoID
                            ORDER BY ReceiptValue DESC";
                    return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql,condition)));
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            return WebApi.Success(db.Database.SqlQuery<BusinessDetailBll>(string.Format(sql,condition)));
        }

        /// <summary>
        /// 获取计划收款（本年、本年后、本季、本季后、本月、本月后）
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentPlanReceipt(string type,DateEnum date)
        {
            string sql, condition="";
            switch (date)
            {
                case DateEnum.Year:
                    condition = "S_C_PlanReceipt.BelongYear <= YEAR(getdate()) ";
                    break;
                case DateEnum.AfterYear:
                    condition = "(S_C_PlanReceipt.BelongYear > datepart(year,getdate()) OR S_C_PlanReceipt.BelongYear = NULL)";
                    break;
                case DateEnum.Quarter:
                    condition = "(S_C_PlanReceipt.BelongYear <= datepart(year,getdate()) AND S_C_PlanReceipt.BelongQuarter <= datepart(quarter,getdate()))";
                    break;
                case DateEnum.AfterQuarter:
                    condition =
                        "((S_C_PlanReceipt.BelongYear = datepart(year,getdate()) AND S_C_PlanReceipt.BelongQuarter > datepart(quarter,getdate())) OR(S_C_PlanReceipt.BelongYear > datepart(year,getdate())) OR (S_C_PlanReceipt.BelongYear = NULL OR S_C_PlanReceipt.BelongQuarter = NULL))";
                    break;
                case DateEnum.Month:
                    condition = "(S_C_PlanReceipt.BelongYear <= datepart(year,getdate()) AND S_C_PlanReceipt.BelongMonth <= datepart(month,getdate()))";
                    break;
                case DateEnum.AfterMonth:
                    condition =
                        "((S_C_PlanReceipt.BelongYear = datepart(year,getdate()) AND S_C_PlanReceipt.BelongMonth >datepart(month,getdate())) OR (S_C_PlanReceipt.BelongYear > datepart(year,getdate())) OR (S_C_PlanReceipt.BelongYear = NULL OR S_C_PlanReceipt.BelongQuarter = NULL OR S_C_PlanReceipt.BelongMonth = NULL))";
                    break;

            }

            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0))/10000,0)) as TotalMoney, 
                            CustomerId as ID,CustomerName as Name,count(distinct ContractInfoID) as ContractCount 
                            FROM S_C_PlanReceipt
                            WHERE S_C_PlanReceipt.State ='UnReceipt' and {0}
                            GROUP BY CustomerId,CustomerName
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.Department:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0))/10000,0)) AS TotalMoney,
                            ProductionUnitID as ID,ProductionUnitName as Name,count(distinct ContractInfoID) as ContractCount  
                            FROM S_C_PlanReceipt WHERE S_C_PlanReceipt.State ='UnReceipt' and {0}
                            GROUP BY ProductionUnitID,ProductionUnitName
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.NotGroup:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0))/10000,0)) AS ReceiptValue
                            ,S_C_PlanReceipt.ContractInfoID as ID,mc.Name as Name,
                            case when ISNULL(FirstDutyManName,'')='' then '未安排' else ISNULL(FirstDutyManName,'') end as UserName
                            FROM S_C_PlanReceipt LEFT JOIN S_C_ManageContract mc
							ON S_C_PlanReceipt.ContractInfoID=mc.ID
                            WHERE S_C_PlanReceipt.State ='UnReceipt' and {0}
                            GROUP BY S_C_PlanReceipt.ContractInfoID,mc.Name,FirstDutyManName
                            ORDER BY ReceiptValue DESC";
                    return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, condition)));
                default:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0))/10000,0)) AS TotalMoney,
                        ISNULL(FirstDutyManID,'') as ID,
                        case when ISNULL(FirstDutyManName,'')='' then '未安排' else ISNULL(FirstDutyManName,'') end as Name
                        ,count(distinct ContractInfoID) as ContractCount FROM S_C_PlanReceipt
                        WHERE S_C_PlanReceipt.State ='UnReceipt' and {0}
                        GROUP BY ISNULL(FirstDutyManID,''),ISNULL(FirstDutyManName,'')
                        ORDER BY TotalMoney DESC";
                    break;
            }
            return WebApi.Success(db.Database.SqlQuery<BusinessDetailBll>(string.Format(sql, condition)));
        }

        /// <summary>
        /// 获取已签合同（当季、当月、本周）和待签合同额（当年）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentContract(string type, DateEnum date)
        {
            string sql, condition = "";
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
                    sql = @"select PartyA as ID,PartyAName as Name,
                            Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney 
                            ,count(distinct ID) ContractCount
                            from S_C_ManageContract
                            where {0}
                            GROUP BY PartyA,PartyAName
                            ORDER BY TotalMoney DESC";
                    break;
//                case EnumQueryType.Department:
//                    sql = @"select HeadOfSalesDeptID as ID,HeadOfSalesDeptName as Name,
//                            Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney 
//                            ,count(distinct ID) ContractCount
//                            from S_C_ManageContract
//                            where {0}
//                            group by HeadOfSalesDeptID,HeadOfSalesDeptName
//                            ORDER BY TotalMoney DESC";
//                    break;
                case EnumQueryType.NotGroup:
                    sql = @"select ID,Name,Convert(decimal(18,2),isnull(ContractRMBAmount,0)/10000) as ReceiptValue,
                            SignDate from S_C_ManageContract
                            where {0}
                            order by ReceiptValue desc";
                    return WebApi.Success(db.Database.SqlQuery<ContractBll>(string.Format(sql, condition)));
                default:
                    sql = @"select BusinessManager as ID,BusinessManagerName as Name,
                        Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney 
                        ,count(distinct ID) ContractCount
                        from S_C_ManageContract
                        where {0}
                        group by BusinessManager,BusinessManagerName
                        ORDER BY TotalMoney DESC";
                    break;
            }
            return WebApi.Success(db.Database.SqlQuery<BusinessDetailBll>(string.Format(sql, condition)));
        }

        /// <summary>
        /// 获取当前应收款
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentAccountReceivable(string type)
        {
            string sql;
            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(SumInvoiceValue-ISNULL(SumReceiptValue,0)),0)/10000) as TotalMoney,
                            PartyA as ID,PartyAName as Name
                            ,count(distinct ID) as ContractCount
                            FROM S_C_ManageContract
                            WHERE IsSigned='Signed'
                            GROUP BY PartyAName,PartyA
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.Department:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(SumInvoiceValue-ISNULL(SumReceiptValue,0)),0)/10000) AS TotalMoney,
                            ProductionDeptName as Name,ProductionDept as ID,
                            count(distinct ID) as ContractCount
                            FROM S_C_ManageContract
                            WHERE IsSigned='Signed'
                            GROUP BY ProductionDept,ProductionDeptName
                            ORDER BY TotalMoney DESC";
                    break;
                case EnumQueryType.NotGroup:
                    sql = @"SELECT Convert(decimal(18,2),isnull(SUM(SumInvoiceValue-ISNULL(SumReceiptValue,0)),0)/10000) AS ReceiptValue,
                            S_C_ManageContract.ID,S_C_ManageContract.Name,MAX(S_C_Invoice.InvoiceDate) InvoiceDate 
                            FROM S_C_ManageContract
                            LEFT JOIN S_C_Invoice on S_C_ManageContract.ID = S_C_Invoice.ContractInfoID
                            WHERE IsSigned='Signed'
                            GROUP BY S_C_ManageContract.ID,S_C_ManageContract.Name
                            ORDER BY ReceiptValue DESC";
                    return WebApi.Success(db.Database.SqlQuery<ContractBll>(sql));
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            return WebApi.Success(db.Database.SqlQuery<BusinessDetailBll>(sql));
        }

        /// <summary>
        /// 获取当前合同余额
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ResultDTO GetCurrentSurplusContract(string type)
        {
            string sql;
            var db = new MarketContext();

            EnumQueryType con = (EnumQueryType)System.Enum.Parse(typeof(EnumQueryType), type);
            switch (con)
            {
                case EnumQueryType.Customer:
                    sql = @"select PartyA as ID,PartyAName as Name,
                            Convert(decimal(18,2),(isnull(Sum(isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0)),0))/10000) as TotalMoney,
                            count(distinct ID) ContractCount
                            from S_C_ManageContract 
                            where IsSigned='Signed'
                            GROUP BY PartyAName,PartyA
                            ORDER BY TotalMoney DESC";
                    break;
//                case EnumQueryType.Department:
//                    sql = @"select HeadOfSalesDeptID as ID,HeadOfSalesDeptName as Name,
//                            Convert(decimal(18,2),(isnull(Sum(isnull(ContractRMBAmount,0)-isnull(SummaryReceiptValue,0)-isnull(SummaryBadDebtValue,0)),0))/10000) as TotalMoney,
//                            count(distinct ID) ContractCount
//                            from S_C_ManageContract
//                            where IsSigned='Signed'
//                            group by HeadOfSalesDeptID,HeadOfSalesDeptName
//                            ORDER BY TotalMoney DESC";
//                    break;
                case EnumQueryType.NotGroup:
                    sql = @"select ID,Name,Convert(decimal(18,2),((isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0))/10000)) as ReceiptValue,
                            SignDate from S_C_ManageContract 
                            where IsSigned='Signed'
                            order by ReceiptValue desc";
                    return WebApi.Success(db.Database.SqlQuery<ContractBll>(sql));
                default:
                    return WebApi.Error(EnumException.请求参数不合法);
            }
            return WebApi.Success(db.Database.SqlQuery<BusinessDetailBll>(sql));
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public decimal TotalMoney { get; set; }
        public int ContractCount { get; set; }
        public int RecordNum { get; set; }
    }
}
