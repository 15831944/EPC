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

namespace Market.Areas.Analysis.Controllers
{
    public class ContractReportController : MarketController
    {
        public ActionResult ContractReportList()
        {
            var tab = new Tab();
            var industoryCategory = CategoryFactory.GetCategory("Market.NewOrAdded", "合同性质", "NewOrAdded");
            industoryCategory.SetDefaultItem();
            tab.Categories.Add(industoryCategory);

            var importantCategory = CategoryFactory.GetCategory(typeof(ContractIsSigned), "签约状态", "IsSigned");
            importantCategory.SetDefaultItem(ContractIsSigned.Signed.ToString());
            //importantCategory.Method = QueryMethod.Equal;
            importantCategory.Multi = false;
            tab.Categories.Add(importantCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7, 1, true, "所属年份");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select *,(SumInvoiceValue-SumReceiptValue-SumBadDebtValue) as ReceivableValue,
case when isnull(ContractRMBAmount,0)=0 then 0 else 
Round( isnull(SumReceiptValue,0)/ContractRMBAmount,4) end as ReceiptRate,
(ContractRMBAmount-isnull(SumReceiptValue,0)-isnull(SumBadDebtValue,0)) as RemainContractValue
 from S_C_ManageContract left join (select Sum(SumPaymentValue) as SumPaymentValue,
Sum(ContractValue) as SupplierContractValue,ManagerContract
from S_SP_SupplierContract group by ManagerContract) SupplierInfo 
on SupplierInfo.ManagerContract=S_C_ManageContract.ID ";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            string summarySql = @"select isnull(Sum(ContractRMBAmount),0) as SumContractRMBAmount,
 isnull(Sum(SumInvoiceValue),0) as SumInvoiceValue,
 isnull(Sum(SumReceiptValue),0) as SumReceiptValue,
 isnull(Sum(SumBadDebtValue),0) as SumBadDebtValue,
 isnull(Sum(ReceivableValue),0) as SumReceivableValue,
 isnull(Sum(SumPaymentValue),0) as SumPaymentValue,
 isnull(Sum(SupplierContractValue),0) as SumSupplierContractValue    
            from (" + sql + qb.GetWhereString() + ") tableInfo ";
            var dt = this.SqlHelper.ExecuteDataTable(summarySql);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                data.sumData.SetValue("ContractRMBAmount", Convert.ToDecimal(row["SumContractRMBAmount"]).ToString("c"));
                data.sumData.SetValue("SumInvoiceValue", Convert.ToDecimal(row["SumInvoiceValue"]).ToString("c"));
                data.sumData.SetValue("SumReceiptValue", Convert.ToDecimal(row["SumReceiptValue"]).ToString("c"));
                data.sumData.SetValue("SumBadDebtValue", Convert.ToDecimal(row["SumBadDebtValue"]).ToString("c"));
                data.sumData.SetValue("ReceivableValue", Convert.ToDecimal(row["SumReceivableValue"]).ToString("c"));
                data.sumData.SetValue("PaymentValue", Convert.ToDecimal(row["SumPaymentValue"]).ToString("c"));
                data.sumData.SetValue("SupplierContractValue", Convert.ToDecimal(row["SumSupplierContractValue"]).ToString("c"));
            }
            return Json(data);
        }
    }
}
