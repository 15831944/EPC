using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Collections;
using Formula;
using Formula.Helper;
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;

namespace Market.Areas.Analysis.Controllers
{
    public class CustomerReportController : MarketController
    {

        public ActionResult CustomerInfoReport()
        {
            var tab = new Tab();
            var industoryCategory = CategoryFactory.GetCategory("Market.Industry", "行业", "Industry");
            industoryCategory.SetDefaultItem();
            tab.Categories.Add(industoryCategory);

            var importantCategory = CategoryFactory.GetCategory(typeof(YesOrNo), "是否重点客户", "IsImportantCustomer");
            importantCategory.SetDefaultItem();
            importantCategory.Multi = false;
            tab.Categories.Add(importantCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7);
            yearCategory.SetDefaultItem();
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public ActionResult CustomerNewContract()
        {
            var tab = new Tab();
            var industoryCategory = CategoryFactory.GetCategory("Market.Industry", "行业", "Industry");
            industoryCategory.SetDefaultItem();
            tab.Categories.Add(industoryCategory);

            var importantCategory = CategoryFactory.GetCategory(typeof(YesOrNo), "是否重点客户", "IsImportantCustomer");
            importantCategory.SetDefaultItem();
            importantCategory.Multi = false;
            tab.Categories.Add(importantCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7, 1, false);
            yearCategory.Multi = false;
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetNewContractInfoList(QueryBuilder qb, string Year)
        {
            #region
            string sql = @"select S_F_Customer.*,isnull(ContractAmount,0) as ContractValue,
isnull(InvoiceValue,0) as InvoiceValue,
isnull(PlanReceiptValue,0) as PlanReceiptValue,
isnull(ReceiptValue,0) as ReceiptValue
from dbo.S_F_Customer
left join (select Sum(ContractRMBAmount) as ContractAmount,PartyA
from dbo.S_C_ManageContract where IsSigned='" + ContractIsSigned.Signed + @"' and BelongYear='{0}'
group by PartyA) ContractTable on S_F_Customer.ID = ContractTable.PartyA
left join (select Sum(InvoiceValue) as InvoiceValue,CustomerID from (
select InvoiceInfo.* from (
select Sum(Amount) as InvoiceValue,ContractInfoID,PayerUnitID as CustomerID from S_C_Invoice where State='" + InvoiceState.Normal.ToString() + @"'
group by ContractInfoID,PayerUnitID ) InvoiceInfo
left join S_C_ManageContract on InvoiceInfo.ContractInfoID=S_C_ManageContract.ID
where S_C_ManageContract.BelongYear='{0}') InvoiceTable
group by InvoiceTable.CustomerID) InvoiceInfoTable
on  S_F_Customer.ID = InvoiceInfoTable.CustomerID 
left join (select Sum(PlanReceiptValue) as PlanReceiptValue,CustomerID from (
select ReceiptObject.* from (
select isnull(Sum(ReceiptValue),0) as PlanReceiptValue,
isnull(Sum(FactBadValue),0) as BadDebtValue,PartyA as CustomerID,S_C_ManageContractID as ContractInfoID
 from S_C_ManageContract_ReceiptObj
left join S_C_ManageContract 
on S_C_ManageContract_ReceiptObj.S_C_ManageContractID=S_C_ManageContract.ID
group by PartyA,S_C_ManageContractID
) ReceiptObject
left join S_C_ManageContract on ReceiptObject.ContractInfoID=S_C_ManageContract.ID
where S_C_ManageContract.BelongYear='{0}' ) PlanTable 
group by PlanTable.CustomerID ) PlanInfoTable
on  S_F_Customer.ID = PlanInfoTable.CustomerID 
left join (select Sum(ReceiptValue) as ReceiptValue,CustomerID from (
select ReceiptInfo.* from (select Sum(Amount) as ReceiptValue,ContractInfoID,CustomerID from S_C_Receipt
group by ContractInfoID,CustomerID) ReceiptInfo
left join S_C_ManageContract on ReceiptInfo.ContractInfoID=S_C_ManageContract.ID
where S_C_ManageContract.BelongYear='{0}') ReceiptTable
group by ReceiptTable.CustomerID )ReceiptInfoTable
on  S_F_Customer.ID = ReceiptInfoTable.CustomerID
where Parent is null or Parent = ''";
            #endregion
            sql = String.Format(sql, Year);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            var sumSql = @"select isnull(Sum(ContractValue),0) as SumContractValue, isnull(Sum(InvoiceValue),0) as SumInvoiceValue,
 isnull(Sum(ReceiptValue),0) as SumReceiptValue, isnull(Sum(PlanReceiptValue),0) as SumPlanReceiptValue
            from (" + sql + "" + qb.GetWhereString() + ") SummaryData";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("ContractValue", Convert.ToDecimal(sumDt.Rows[0]["SumContractValue"]).ToString("c"));
                data.sumData.SetValue("InvoiceValue", Convert.ToDecimal(sumDt.Rows[0]["SumInvoiceValue"]).ToString("c"));
                data.sumData.SetValue("ReceiptValue", Convert.ToDecimal(sumDt.Rows[0]["SumReceiptValue"]).ToString("c"));
                data.sumData.SetValue("PlanReceiptValue", Convert.ToDecimal(sumDt.Rows[0]["SumPlanReceiptValue"]).ToString("c"));
            }
            return Json(data);
        }

        public JsonResult GetList(QueryBuilder qb, string Year)
        {
            string sql = this.CreateSql(Year);
            this.FillQueryBuilderFilter<S_F_Customer>(qb);
            if (!string.IsNullOrEmpty(GetQueryString("Date")))
                qb.Add("BusinessDate", QueryMethod.GreaterThanOrEqual, GetQueryString("Date"));

            var data = this.SqlHelper.ExecuteGridData(sql, qb);

            var sumSql = @"select isnull(Sum(ContractValue),0) as SumContractValue, isnull(Sum(InvoiceValue),0) as SumInvoiceValue,
 isnull(Sum(ReceiptValue),0) as SumReceiptValue, isnull(Sum(PlanReceiptValue),0) as SumPlanReceiptValue,
 isnull(Sum(BadDebtValue),0) as SumBadDebtValue, isnull(Sum(PlanReceivableValue),0) as SumPlanReceivableValue,
 isnull(Sum(RemainContractValue),0) as SumRemainContractValue, isnull(Sum(ReceivableValue),0) as SumReceivableValue
            from (" + sql + "" + qb.GetWhereString() + ") SummaryData";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("ContractValue", Convert.ToDecimal(sumDt.Rows[0]["SumContractValue"]).ToString("c"));
                data.sumData.SetValue("InvoiceValue", Convert.ToDecimal(sumDt.Rows[0]["SumInvoiceValue"]).ToString("c"));
                data.sumData.SetValue("ReceiptValue", Convert.ToDecimal(sumDt.Rows[0]["SumReceiptValue"]).ToString("c"));
                data.sumData.SetValue("PlanReceiptValue", Convert.ToDecimal(sumDt.Rows[0]["SumPlanReceiptValue"]).ToString("c"));
                data.sumData.SetValue("BadDebtValue", Convert.ToDecimal(sumDt.Rows[0]["SumBadDebtValue"]).ToString("c"));
                data.sumData.SetValue("PlanReceivableValue", Convert.ToDecimal(sumDt.Rows[0]["SumPlanReceivableValue"]).ToString("c"));
                data.sumData.SetValue("RemainContractValue", Convert.ToDecimal(sumDt.Rows[0]["SumRemainContractValue"]).ToString("c"));
                data.sumData.SetValue("ReceivableValue", Convert.ToDecimal(sumDt.Rows[0]["SumReceivableValue"]).ToString("c"));
            }
            return Json(data);
        }

        private string CreateSql(string BelongYear)
        {
            string sql = @"select * from (select S_F_Customer.*,isnull(ContractValue,0) as ContractValue,
isnull(InvoiceValue,0) as InvoiceValue,
isnull(ReceiptValue,0) as ReceiptValue,
case when isnull(ContractValue,0)=0 then 0 else Round(isnull(ReceiptValue,0)/isnull(ContractValue,0)*100,2)
end as ReceiptRate,
isnull(PlanReceiptValue,0) as PlanReceiptValue,
isnull(BadDebtValue,0) as BadDebtValue,
isnull(PlanReceiptValue,0) - isnull(ReceiptValue,0) - isnull(BadDebtValue,0) as PlanReceivableValue,
isnull(ContractValue,0) - isnull(ReceiptValue,0) - isnull(BadDebtValue,0) as RemainContractValue,
isnull(InvoiceValue,0) - isnull(ReceiptValue,0) - isnull(BadDebtValue,0) as ReceivableValue
from S_F_Customer
left join (select Sum(ContractRMBAmount) as ContractValue,PartyA from S_C_ManageContract
where   IsSigned='" + ContractIsSigned.Signed + @"' {0}
group by PartyA) ContractInfo on S_F_Customer.ID = PartyA
left join (select Sum(Amount) as InvoiceValue,PayerUnitID from S_C_Invoice 
where 1=1  and State='" + InvoiceState.Normal + @"' {1}
group by PayerUnitID) InvoiceInfo on S_F_Customer.ID = PayerUnitID
left join (select Sum(Amount) as ReceiptValue,CustomerID from S_C_Receipt
where 1=1 {2}
group by CustomerID) ReceiptInfo on S_F_Customer.ID = ReceiptInfo.CustomerID
left join (select isnull(Sum(ReceiptValue),0) as PlanReceiptValue,
isnull(Sum(FactBadValue),0) as BadDebtValue,PartyA 
 from S_C_ManageContract_ReceiptObj
left join S_C_ManageContract 
on S_C_ManageContract_ReceiptObj.S_C_ManageContractID=S_C_ManageContract.ID
where 1=1 {3}
group by PartyA) ReceiptObjectInfo 
on S_F_Customer.ID = ReceiptObjectInfo.PartyA
where Parent is null or Parent = ''
) ResultTable";
            string whereStr = String.Empty;
            string PlanWhereStr = String.Empty;
            if (!String.IsNullOrEmpty(BelongYear) && BelongYear != Formula.Constant.CategoryAllKey)
            {
                whereStr = " and BelongYear in ('" + BelongYear.Replace(",", "','") + "') ";
                PlanWhereStr = " and Year(PlanFinishDate) in ('" + BelongYear.Replace(",", "','") + "') ";
            }
            sql = String.Format(sql, whereStr, whereStr, whereStr, PlanWhereStr);
            return sql;
        }
    }
}
