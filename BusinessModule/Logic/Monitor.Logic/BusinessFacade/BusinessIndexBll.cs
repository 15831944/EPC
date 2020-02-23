using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.Logic.Domain;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class BusinessIndexBll
    {
        /// <summary>
        /// <para name="Receipt">收款总览</para>
        /// <para name="CurrentSurplusContract">当前合同余额</para>
        /// <para name="CurrentAccountReceivable">当前应收款</para>
        /// <para name="CurrentQuarterActualReceipt">当季实际收款</para>
        /// <para name="CurrentMonthActualReceipt">当月实际收款</para>
        /// <para name="CurrentWeekActualReceipt">本周实际到款</para>

        /// <para name="CurrentYearPlanReceipt">本年计划收款</para>
        /// <para name="CurrentYearAfterPlanReceipt">本年后计划收款</para>
        /// <para name="CurrentQuarterPlanReceipt">当季计划收款</para>
        /// <para name="CurrentQuarterAfterPlanReceipt">本季后计划收款</para>
        /// <para name="CurrentMonthPlanReceipt">本月计划收款</para>
        /// <para name="CurrentMonthAfterPlanReceipt">本月后计划收款</para>

        /// <para name="Contract">合同总览</para>
        /// <para name="CurrentUnSignContract">当年待签合同额</para>
        /// <para name="CurrentQuarterSignContract">当季已签合同</para>
        /// <para name="CurrentMonthSignContract">当月已签合同</para>
        /// <para name="CurrentWeekSignContract">本周已签合同额</para>
        /// </summary>
        /// <returns></returns>
        public static ResultDTO GetData()
        {
            try
            {
                var sql =
                    @"--当季实际收款
				(select isnull(Count(ContractInfoID),0) as ContractCount,Convert(decimal(18,2),
				isnull(SUM([Amount]),0)/10000) as TotalMoney,'CurrentQuarterActualReceipt' as Condition,
				'当季实际收款' as Title from dbo.S_C_Receipt  S_C_Receipt
				WHERE BelongYear = datepart(year,getdate()) AND BelongQuarter = datepart(quarter,getdate()))
                union
                --当月实际收款
                (select isnull(Count(ContractInfoID),0) as ContractCount,Convert(decimal(18,2),
                isnull(SUM([Amount]),0)/10000) as TotalMoney,
                'CurrentMonthActualReceipt' as Condition,'当月实际收款' as Title 
                from dbo.S_C_Receipt  S_C_Receipt
                WHERE BelongYear = datepart(year,getdate()) 
                AND BelongMonth = datepart(month,getdate()))
                union
                --本周实际到款
                (select isnull(Count(ContractInfoID),0) as ContractCount,
                Convert(decimal(18,2),isnull(SUM([Amount]),0)/10000) as TotalMoney,
                'CurrentWeekActualReceipt' as Condition,'本周实际到款' as Title 
                from dbo.S_C_Receipt S_C_Receipt
                WHERE ArrivedDate between dateadd(wk, datediff(wk,0,getdate()), 0)  and getdate())
                union
                --当前应收款
                --当一个合同有多个未到款的发票时合同个数不对
                select Count(ID) as ContractCount,
                Convert(decimal(18,2),SUM(ISNULL(SumInvoiceValue,0)-ISNULL(SumReceiptValue,0))/10000) as TotalMoney,
                'CurrentAccountReceivable' as Condition,'当前应收款' as Title  
                from dbo.S_C_ManageContract S_C_ManageContract
                where IsSigned='Signed'
                union
                --当前合同余额
                (select isnull(Count(ID),0) as ContractCount,
                Convert(decimal(18,2),(isnull(Sum(isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0)),0))/10000) as TotalMoney,
                'CurrentSurplusContract' as Condition,'当前合同余额' as Title 
                from dbo.S_C_ManageContract S_C_ManageContract
                where IsSigned='Signed')
                union
                --本年计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentYearPlanReceipt' as Condition,'本年计划收款' as Title 
                FROM dbo.S_C_PlanReceipt WHERE BelongYear <= datepart(year,getdate()) and State ='UnReceipt')
                union
                --本年后计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentYearAfterPlanReceipt' as Condition,'本年后计划收款' as Title 
                FROM dbo.S_C_PlanReceipt WHERE (BelongYear > datepart(year,getdate()) OR BelongYear = NULL) and State in ('UnReceipt'))
                union
                --当季计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentQuarterPlanReceipt' as Condition,'当季计划收款' as Title  
                FROM dbo.S_C_PlanReceipt WHERE BelongYear <= datepart(year,getdate()) AND BelongQuarter <= datepart(quarter,getdate()) and State in ('UnReceipt'))
                union
                --本季后计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentQuarterAfterPlanReceipt' as Condition,'本季后计划收款' as Title 
                FROM dbo.S_C_PlanReceipt WHERE ((BelongYear = datepart(year,getdate()) AND BelongQuarter > datepart(quarter,getdate())) OR(BelongYear > datepart(year,getdate())) OR (BelongYear = NULL OR BelongQuarter = NULL)) and state = 'UnReceipt')
                 union
                 --本月计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentMonthPlanReceipt' as Condition,'本月计划收款' as Title 
                FROM dbo.S_C_PlanReceipt WHERE BelongYear <= datepart(year,getdate()) AND BelongMonth <= datepart(month,getdate()) and State in ('UnReceipt'))
                 union
                 --本月后计划收款
                (SELECT count(distinct ContractInfoID) as ContractCount,
                Convert(decimal(18,2),ISNULL(SUM(PlanReceiptValue-ISNULL(FactReceiptValue,0)),0)/10000) as TotalMoney,
                'CurrentMonthAfterPlanReceipt' as Condition,'本月后计划收款' as Title 
                FROM dbo.S_C_PlanReceipt WHERE ((BelongYear = datepart(year,getdate()) AND BelongMonth >datepart(month,getdate())) OR (BelongYear > datepart(year,getdate())) OR (BelongYear = NULL OR BelongQuarter = NULL OR BelongMonth = NULL)) and State in ('UnReceipt'))
                union
                --当前待签合同额
                (select count(ID)as ContractCount,Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney,
                'CurrentUnSignContract' as Condition,'当前待签合同额' as Title 
                from S_C_ManageContract where  IsSigned!='Signed'  --and BelongYear=datepart(year,getdate())
                )
                union
                (select count(ID)as ContractCount,Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney,'CurrentQuarterSignContract' as Condition,'当季已签合同' as Title from S_C_ManageContract where  IsSigned='Signed'  and BelongYear=datepart(year,getdate()) and BelongQuarter=datepart(quarter,getdate()))
                union
                (select count(ID)as ContractCount,Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) as TotalMoney,'CurrentMonthSignContract' as Condition,'当月已签合同' as Title from S_C_ManageContract where  IsSigned='Signed'  and BelongYear=datepart(year,getdate()) and BelongMonth=datepart(month,getdate()))
                union
                (select count(ID)as ContractCount,Convert(decimal(18,2),isnull(SUM(ContractRMBAmount),0)/10000) as TotalMoney,'CurrentWeekSignContract' as Condition,'本周已签合同额' as Title FROM dbo.S_C_ManageContract WHERE IsSigned = 'Signed' AND SignDate between dateadd(wk, datediff(wk,0,getdate()), 0) and getdate())";
                var db = new MarketContext();
                var result =
                    db.Database.SqlQuery<StatisticsDTO>(sql)
                        .ToDictionary<StatisticsDTO, string, object>(item => item.Condition,
                            item =>
                                new Dictionary<string, object>
                                {
                                    {"ContractCount", item.ContractCount},
                                    {"TotalMoney", item.TotalMoney},
                                    {"Title", item.Title}
                                });



                sql = @"(select (select Convert(decimal(18,2),isnull(Sum(Amount),0)/10000) from 
                    dbo.S_C_Receipt where BelongYear=datepart(year,getdate())) as ActualValue,
                    (SELECT Convert(decimal(18,2),isnull(SUM(Amount),0)/10000) 
                    FROM dbo.S_C_Receipt WHERE ArrivedDate 
                    between dateadd(year,-1,DATEADD(yy,DATEDIFF(yy,0,getdate()), 0)) 
                    and dateadd(year,-1,getdate())) as RelativeValue,'Receipt' as Condition)
                    union
                    (select (select Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) 
                    from S_C_ManageContract where  IsSigned='Signed' and 
                    BelongYear=datepart(year,getdate())) as ActualValue,
                    (SELECT Convert(decimal(18,2),isnull(SUM(ContractRMBAmount),0)/10000) 
                    FROM dbo.S_C_ManageContract WHERE IsSigned = 'Signed' 
                    AND SignDate between dateadd(year,-1,DATEADD(yy,DATEDIFF(yy,0,getdate()), 0)) 
                    and dateadd(year,-1,getdate())) as RelativeValue,'Contract' as Condition)";
                var list = db.Database.SqlQuery<Statistics1DTO>(sql);
                int belongYear = DateTime.Now.Year;
                var indicatorDt = db.S_KPI_IndicatorCompany.FirstOrDefault(d => d.IndicatorType == "YearIndicator" && d.BelongYear == belongYear);

                var receiptGoalValue = indicatorDt != null && indicatorDt.ReceiptValue.HasValue ? indicatorDt.ReceiptValue.Value : 0;
                var contractGoalValue = indicatorDt != null && indicatorDt.ContractValue.HasValue ? indicatorDt.ContractValue.Value : 0;

                foreach (var item in list)
                {
                    decimal goalValue = contractGoalValue;
                    if (item.Condition == "Receipt")
                    {
                        goalValue = receiptGoalValue;
                    }
                    decimal goal = goalValue == 0 ? 100 : Math.Round(item.ActualValue / goalValue * 100, 2);
                    if (item.RelativeValue == 0)
                    {
                        result.Add(item.Condition, new Dictionary<string, object>
                         {
                             {"ActualValue", item.ActualValue},
                             {"GoalValue", goalValue},
                             {"FinishRate", goal},
                             {"GrowthRate", "--"}
                         });
                    }
                    else
                    {
                        result.Add(item.Condition, new Dictionary<string, object>
                    {
                        {"ActualValue", item.ActualValue},
                        {"GoalValue", goalValue},
                        {"FinishRate",100},
                        {"GrowthRate", Math.Round(((item.ActualValue - item.RelativeValue) / item.RelativeValue) * 100,2)}
                    });
                    }
                }

                return WebApi.Success(result);
            }
            catch (Exception e)
            {
                return WebApi.Error(e.Message);
            }
            
        }
    }

    public class StatisticsDTO
    {
        public int ContractCount { get; set; }
        public decimal TotalMoney { get; set; }
        public string Condition { get; set; }
        public string Title { get; set; }
    }

    public class Statistics1DTO
    {
        public decimal ActualValue { get; set; }
        //public decimal GoalValue { get; set; }
        public decimal RelativeValue { get; set; }
        public string Condition { get; set; }
    }
}
