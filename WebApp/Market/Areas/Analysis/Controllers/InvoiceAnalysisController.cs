using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Collections;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;


namespace Market.Areas.Analysis.Controllers
{
    public class InvoiceAnalysisController : MarketController<S_C_Invoice>
    {
        public override ActionResult List()
        {
            var tab = new Tab();
            var invoiceTypeCategory = CategoryFactory.GetCategory(typeof(InvoiceType), "InvoiceType");
            invoiceTypeCategory.SetDefaultItem("All");
            tab.Categories.Add(invoiceTypeCategory);

            var stateCategory = CategoryFactory.GetCategory(typeof(InvoiceState), "State");
            stateCategory.Multi = false;
            stateCategory.SetDefaultItem(InvoiceState.Normal.ToString());
            tab.Categories.Add(stateCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(yearCategory);

            var receiptStateCategory = CategoryFactory.GetCategory(typeof(InvoiceReceiptState), "ReceiptState");
            receiptStateCategory.SetDefaultItem("All");
            tab.Categories.Add(receiptStateCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select * from (select S_C_Invoice.*,RelationValue,isnull(Amount,0)-isnull(RelationValue,0) as Receivable,
case when  isnull(Amount,0)-isnull(RelationValue,0)=0 then '{0}' 
when isnull(RelationValue,0)=0 then '{1}' else '{2}' end as ReceiptState
from S_C_Invoice left join (select Sum(RelationValue) as RelationValue,InvoiceID from dbo.S_C_InvoiceReceiptRelation
group by InvoiceID) RelationInfo on S_C_Invoice.ID = RelationInfo.InvoiceID) TableInfo";
            sql = String.Format(sql, InvoiceReceiptState.Receipt.ToString(),
                     InvoiceReceiptState.None.ToString(),
                     InvoiceReceiptState.Part.ToString()
                );
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            string sumSql = @"select isnull(Sum(Amount),0) as SumAmount,isnull(Sum(Receivable),0) as SumReceivable,
            isnull(Sum(TaxesAmount),0) as SumTaxesAmount,isnull(Sum(ClearAmount),0) as SumClearAmount,
isnull(Sum(Amount),0) as SumAmount,isnull(Sum(RelationValue),0) as SumRelationValue from (" + sql + qb.GetWhereString() + ") SumDataInfo";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt != null && sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("Amount", Convert.ToDecimal(sumDt.Rows[0]["SumAmount"]).ToString("c"));
                data.sumData.SetValue("Receivable", Convert.ToDecimal(sumDt.Rows[0]["SumReceivable"]).ToString("c"));
                data.sumData.SetValue("RelationValue", Convert.ToDecimal(sumDt.Rows[0]["SumRelationValue"]).ToString("c"));
                data.sumData.SetValue("TaxesAmount", Convert.ToDecimal(sumDt.Rows[0]["SumTaxesAmount"]).ToString("c"));
                data.sumData.SetValue("ClearAmount", Convert.ToDecimal(sumDt.Rows[0]["SumClearAmount"]).ToString("c"));
            }
            return Json(data);
        }

        public JsonResult GetReceiptList(QueryBuilder qb, string InvoiceID)
        {
            string sql = @"select * from (select Sum(RelationValue) as RelationValue,InvoiceID,ReceiptID 
from dbo.S_C_InvoiceReceiptRelation group by InvoiceID,ReceiptID) RelationInfo
left join S_C_Receipt on RelationInfo.ReceiptID =S_C_Receipt.ID where InvoiceID = '{0}' {1}";
            string orderStr = " order by "+qb.SortField+" "+qb.SortOrder;
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, InvoiceID, orderStr));
            return Json(dt);
        }

    }
}
