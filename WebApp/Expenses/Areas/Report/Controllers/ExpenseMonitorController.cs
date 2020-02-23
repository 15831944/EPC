using Config;
using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Areas.Report.Controllers
{
    public class ExpenseMonitorController : ExpensesController
    {
        public ActionResult ReportView()
        {
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            #region 时间参数
            string dType = this.GetQueryString("dType");
            if (string.IsNullOrEmpty(dType))
                throw new Formula.Exceptions.BusinessValidationException("请选择【统计期间】");

            string year = this.GetQueryString("year");
            if (string.IsNullOrEmpty(year))
                throw new Formula.Exceptions.BusinessValidationException("请选择【年】");
            var yearNum = Convert.ToInt32(year);

            string quarter = this.GetQueryString("quarter");
            string month = this.GetQueryString("month");

            var endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var queryDate = string.Empty;
            switch (dType)
            {
                case "year":
                    endDate = string.Format("{0}-01-01", yearNum + 1);
                    queryDate = string.Format(" and BelongYear={0} ", yearNum);
                    break;

                case "quarter":
                    if (string.IsNullOrEmpty(quarter))
                        throw new Formula.Exceptions.BusinessValidationException("请选择【季】");
                    var quarterNum = Convert.ToInt32(quarter);
                    if (quarterNum == 4)
                    {
                        endDate = string.Format("{0}-01-01", yearNum + 1);
                    }
                    else
                    {
                        endDate = string.Format("{0}-{1}-01", yearNum, quarterNum * 3 + 1);
                    }
                    queryDate = string.Format(" and BelongYear={0} and BelongQuarter={1} ", yearNum, quarterNum);
                    break;

                case "month":
                    if (string.IsNullOrEmpty(month))
                        throw new Formula.Exceptions.BusinessValidationException("请选择【月】");

                    var monthrNum = Convert.ToInt32(month);
                    if (monthrNum == 12)
                    {
                        endDate = string.Format("{0}-01-01", yearNum + 1);
                    }
                    else
                    {
                        endDate = string.Format("{0}-{1}-01", yearNum, monthrNum);
                    }
                    queryDate = string.Format(" and BelongYear={0} and BelongMonth={1} ", yearNum, monthrNum);
                    break;

                default:
                    throw new Formula.Exceptions.BusinessValidationException("参数有误！");
            }
            #endregion

            #region 查询
            string sql = string.Format(@"
select c.*,
case when TotalInvoiceValue-TotalReceiptValue<=0 then 0 else TotalInvoiceValue-TotalReceiptValue end as Receivables,--应收款
isnull(TotalCostValue/nullif(LastBudgetValue,0),0) as CostAndBudgetRate,--预算执行率
isnull(TotalRevenueValue/nullif(ContractValue,0),0) as TotalRevenueRate,--完工百分比
isnull((TotalInvoiceValue-TotalReceiptValue)/nullif(TotalReceiptValue,0),0) as DiffInvoiceAndReceiptRate,--开票额超过到款额百分比
CurrentRevenueValue-CurrentCostValue as CurrentProfitValue,--利润_本期
TotalRevenueValue-TotalCostValue as TotalProfitValue,--利润_累计
case when TotalRevenueValue-TotalCostValue<=0 then 0 else 
isnull((TotalRevenueValue-TotalCostValue)/nullif(TotalCostValue,0),1) end as ProfitRate,--利润率
'2019-05-01' as EndDate

from (
select
cbsNode.ID as CBSNodeID,
cbsNode.ContractInfoID,
cbsNode.FullID as CBSFullID,
cbsNode.CBSInfoID,
--project.*,
project.ID,
project.Name,--项目名称
project.Code,--项目编号
project.ProjectClass as ProjectClass,--业务类型
project.ChargerDeptName as ChargerDeptName,--承担部门
project.State as ProjectState,--项目状态
customer.Name as CustomerName,--客户名称
customer.Area as Area,--区域
customer.Industry as Industry,--客户类型（所属行业）
case when S_C_ManageContractIDs is NULL then 'N' else 'Y' end as IsBandContract,--是否绑定合同
ISNULL(ProjectContract.ProjectContractValue,0) as ContractValue,--合同金额

ISNULL(currentProjectInvoiceInfo.ProjectInvoiceValue,0) as CurrentInvoiceValue,--开票_本期
ISNULL(totalProjectValueInfo.ProjectInvoiceValue,0) as TotalInvoiceValue,--开票_累计
ISNULL(currentProjectReceiptInfo.ProjectReceiptValue,0) as CurrentReceiptValue,--到款_本期
ISNULL(totalProjectValueInfo.ProjectReceiptValue,0) as TotalReceiptValue,--到款_累计

primitiveBudget.BudgetID as PrimitiveBudgetID,--初版预算ID
lastBudget.BudgetID as LastBudgetID,--最新预算ID
ISNULL(primitiveBudget.BudgetValue,0) as PrimitiveBudgetValue,--初版预算
ISNULL(lastBudget.BudgetValue,0) as LastBudgetValue,--终版预算
ISNULL((select SUM(TotalPrice) from S_EP_CostInfo
where State='Finish' {1} and CBSInfoID=cbsNode.CBSInfoID 
and CBSNodeFullID like cbsNode.FullID+'%'),0) as CurrentCostValue,--成本_本期
ISNULL((
select SUM(TotalPrice) from S_EP_CostInfo where State='Finish'
and CBSInfoID=cbsNode.CBSInfoID and CBSNodeFullID like cbsNode.FullID+'%'
),0) as TotalCostValue,--成本_累计
ISNULL((
select SUM(S_EP_RevenueInfo_Detail.CurrentIncomeTotalValue) as TotalRevenueValue from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where S_EP_RevenueInfo.State='Finish'  {1}
and CBSInfoID=cbsNode.CBSInfoID  and CBSNodeFullID like cbsNode.FullID+'%'
),0) as CurrentRevenueValue,--收入_本期
ISNULL((
select SUM(S_EP_RevenueInfo_Detail.CurrentIncomeTotalValue) as TotalRevenueValue from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where S_EP_RevenueInfo.State='Finish'
and CBSInfoID=cbsNode.CBSInfoID  and CBSNodeFullID like cbsNode.FullID+'%'
),0) as TotalRevenueValue,--收入_累计
totalProgress.NodeName as ProgressNodeName--当前节点

from  S_I_Project project 
left join S_F_Customer customer on project.Customer=customer.ID
left join (--项目合同
	select ProjectID,
	Sum(ProjectValue) as ProjectContractValue,--项目合同额
	S_C_ManageContractIDs = STUFF((
											   SELECT ',' +S_C_ManageContract_ProjectRelation.S_C_ManageContractID
											   FROM S_C_ManageContract_ProjectRelation
											   WHERE S_C_ManageContract_ProjectRelation.ProjectID = ProjectID
											   FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
                                           
	from S_C_ManageContract_ProjectRelation 
	where ProjectID is not null group by ProjectID 
) ProjectContract  on project.ID=ProjectContract.ProjectID
left join (--开票_本期
	select ProjectInfo,Sum(FactInvoiceValue) as ProjectInvoiceValue--开票
	from S_C_Invoice 
	inner join (
		select Sum(RelationValue) as FactInvoiceValue,ProjectInfo,S_C_InvoiceID InvoiceID
		from  S_C_Invoice_ReceiptObject inner join S_C_ManageContract_ReceiptObj 
		on S_C_Invoice_ReceiptObject.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID
		where ProjectInfo is not null and S_C_InvoiceID is not null group by ProjectInfo,S_C_InvoiceID 
	) ProjectInvoice on S_C_Invoice.ID = ProjectInvoice.InvoiceID 
	where ProjectInfo is not null {1} group by ProjectInfo
) currentProjectInvoiceInfo on project.ID=currentProjectInvoiceInfo.ProjectInfo
left join (--到款_本期
	select ProjectInfo,Sum(FactReceiptValue) as ProjectReceiptValue--到款
	from  S_C_Receipt 
	inner join (
		select Sum(RelationValue) as FactReceiptValue,ProjectInfo,ReceiptID
		from  S_C_ReceiptPlanRelation inner join S_C_ManageContract_ReceiptObj 
		on S_C_ReceiptPlanRelation.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID
		where ProjectInfo is not null and ReceiptID is not null  group by ProjectInfo,ReceiptID 
	) ProjectReceipt on S_C_Receipt.ID = ProjectReceipt.ReceiptID
	where ProjectInfo is not null {1} group by ProjectInfo
) currentProjectReceiptInfo on project.ID=currentProjectReceiptInfo.ProjectInfo
left join (--项目的开票到款_累计
	select 
	Sum(FactReceiptValue) as ProjectReceiptValue,--已收款
	Sum(FactInvoiceValue) as ProjectInvoiceValue,--已开票
	ProjectInfo 
	from S_C_ManageContract_ReceiptObj 
	where ProjectInfo is not null group by ProjectInfo
) totalProjectValueInfo on project.ID=totalProjectValueInfo.ProjectInfo

left join (
	select * from S_EP_CBSNode where NodeType='Project'
) cbsNode on cbsNode.ProjectInfoID=project.ID
left join (--初版预算
	select budget.ID as BudgetID,budgetUnit.CBSNodeID,budget.BudgetValue from(
	select * from S_EP_BudgetVersion where VersionNumber=1
	) budget
	left join S_EP_BudgetUnit budgetUnit on budget.BudgetUnitID=budgetUnit.ID
) primitiveBudget on cbsNode.ID=primitiveBudget.CBSNodeID
left join (--终版预算
	select budget.ID as BudgetID,budgetUnit.CBSNodeID,budget.BudgetValue from S_EP_BudgetVersion budget 
	inner join (
	select ProjectInfoID,MAX(VersionNumber) as MaxVersionNumber from S_EP_BudgetVersion group by ProjectInfoID
	) maxVersionBudget on budget.ProjectInfoID=maxVersionBudget.ProjectInfoID and budget.VersionNumber=maxVersionBudget.MaxVersionNumber
	left join S_EP_BudgetUnit budgetUnit on budget.BudgetUnitID=budgetUnit.ID
) lastBudget on cbsNode.ID=lastBudget.CBSNodeID

left join (--项目进度_累计
	select incomeUnit.CBSNodeID,progressNode.IncomeUnitID,progressNode.TotalAllScale,progressNode.NodeName from S_EP_IncomeUnit_ProgressNode progressNode
	inner join(
	select IncomeUnitID,MAX(SortIndex) maxSortIndex  from S_EP_IncomeUnit_ProgressNode where State='Finish' group by IncomeUnitID
	)lastProgressNode on progressNode.IncomeUnitID=lastProgressNode.IncomeUnitID and progressNode.SortIndex=lastProgressNode.maxSortIndex
	left join S_EP_IncomeUnit incomeUnit on lastProgressNode.IncomeUnitID=incomeUnit.ID
) totalProgress on cbsNode.ID=totalProgress.CBSNodeID

) c

",
endDate, queryDate
);

            var dt = SQLDB.ExecuteGridData(sql, qb, "");
            return Json(dt);
            #endregion

        }


    }
}
