using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
using Config.Logic;

namespace Market.Areas.Basic.Controllers
{
    public class CustomerController : MarketController<S_F_Customer>
    {
        #region 概览数据

        public ActionResult OverView()
        {
            string id = this.Request["ID"];
            var customer = this.GetEntityByID(id);
            if (customer == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + id + "】的客户信息");
            ViewBag.Customer = customer;
            ViewBag.ContractCount = entities.Set<S_C_ManageContract>().Count(d => d.PartyA == customer.ID);
            var query = entities.Set<S_C_ManageContract>().Where(d => d.PartyA == customer.ID && d.IsSigned == "Signed");
            ViewBag.ProcessContractCount = query.Count(d => d.ContractState != "Pause" && d.ContractState != "Terminate" && d.ContractState != "Finish");
            ViewBag.FinishContractCount = query.Count(d => d.ContractState == "Finish");
            ViewBag.TerminateContractCount = query.Count(d => d.ContractState == "Terminate");
            var contractSummaryValue = Convert.ToDecimal(query.Sum(d => d.ContractRMBAmount));
            var summaryBadDebtValue = Convert.ToDecimal(query.Sum(d => d.SumBadDebtValue));
            var summaryInvoiceValue = Convert.ToDecimal(query.Sum(d => d.SumInvoiceValue));
            var summaryReceiptValue = Convert.ToDecimal(query.Sum(d => d.SumReceiptValue));
            var revisibleValue = summaryInvoiceValue - summaryReceiptValue - summaryBadDebtValue;
            ViewBag.ContractSummaryValue = contractSummaryValue.ToString("c");
            ViewBag.SummaryBadDebtValue = summaryBadDebtValue.ToString("c");
            ViewBag.SummaryInvoiceValue = summaryInvoiceValue.ToString("c");
            ViewBag.SummaryReceiptValue = summaryReceiptValue.ToString("c");
            ViewBag.ProjectCount = entities.Set<S_I_Project>().Count(d => d.Customer == customer.ID);

            ViewBag.RemainContractValue = (contractSummaryValue - summaryReceiptValue - summaryBadDebtValue).ToString("c");
            ViewBag.RevisibleValue = revisibleValue.ToString("c");
            ViewBag.DefaultStartYear = DateTime.Now.Year - 1;
            ViewBag.ChartOption = JsonHelper.ToJson(this.GetChartOption(customer.ID, DateTime.Now.Year - 1, DateTime.Now.Year));
            return View();
        }

        #endregion

        #region 概览图标数据

        public JsonResult GetChartInfo(string customerID, int lastYear = 3)
        {

            var startYear = DateTime.Now.Year - lastYear + 1;
            var endYear = DateTime.Now.Year;
            var result = GetChartOption(customerID, Convert.ToInt32(startYear), Convert.ToInt32(endYear));
            return Json(result);
        }

        private Dictionary<string, object> GetChartOption(string customerID, int startYear, int endYear)
        {
            var dt = this.GetChartData(customerID, startYear, endYear);
            string seriesNames = "计划收款,到款金额,开票金额";
            string seriesFields = "PlanReceiptValue,ReceiptValue,InvoiceValue";
            var columChart = HighChartHelper.CreateColumnChart("", dt, "Title", seriesNames.Split(','), seriesFields.Split(','));
            return columChart.Render();
        }

        private DataTable GetChartData(string customerID, int startYear, int endYear)
        {
            var data = this.GetDefaultTable(startYear, endYear);
            string sql = @"select Sum(ReceiptValue) as PlanReceiptValue, PlanFinishYear,PlanFinishQuarter from (
select PartyA,ReceiptValue,Year(PlanFinishDate) as  PlanFinishYear,(Month(PlanFinishDate)-1)/3+1 PlanFinishQuarter 
 from S_C_ManageContract_ReceiptObj left join S_C_ManageContract on S_C_ManageContract_ReceiptObj.S_C_ManageContractID =S_C_ManageContract.ID) ReceiptObj 
where PartyA='{0}' and PlanFinishYear>='{1}' and PlanFinishYear<='{2}' group by PlanFinishYear,PlanFinishQuarter";
            var planRecieptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, customerID, startYear, endYear));
            sql = @"select Sum(Amount) as ReceiptValue,BelongYear,BelongQuarter from dbo.S_C_Receipt
where CustomerID='" + customerID + "' and BelongYear<='" + endYear + "' and BelongYear>='" + startYear + "' group by BelongYear,BelongQuarter";
            var recieptDt = this.SqlHelper.ExecuteDataTable(sql);
            sql = @"select Sum(Amount) as InvoiceValue,BelongYear,BelongQuarter from dbo.S_C_Invoice
where State='" + InvoiceState.Normal.ToString() + "' and PayerUnitID='" + customerID + "' and BelongYear<='" + endYear + "' and BelongYear>='" + startYear + "' group by BelongYear,BelongQuarter";
            var invoiceDt = this.SqlHelper.ExecuteDataTable(sql);
            foreach (DataRow row in data.Rows)
            {
                var planReceiptRow = planRecieptDt.Select("PlanFinishYear='" + row["Year"] + "' and PlanFinishQuarter='" + row["Quater"] + "'").FirstOrDefault();
                if (planReceiptRow != null && planReceiptRow["PlanReceiptValue"] != null && planReceiptRow["PlanReceiptValue"] != DBNull.Value)
                    row["PlanReceiptValue"] = planReceiptRow["PlanReceiptValue"];
                var receiptRow = recieptDt.Select("BelongYear='" + row["Year"] + "' and BelongQuarter='" + row["Quater"] + "'").FirstOrDefault();
                if (receiptRow != null && receiptRow["ReceiptValue"] != null && receiptRow["ReceiptValue"] != DBNull.Value)
                    row["ReceiptValue"] = receiptRow["ReceiptValue"];

                var invoiceRow = invoiceDt.Select("BelongYear='" + row["Year"] + "' and BelongQuarter='" + row["Quater"] + "'").FirstOrDefault();
                if (invoiceRow != null && invoiceRow["InvoiceValue"] != null && invoiceRow["InvoiceValue"] != DBNull.Value)
                    row["InvoiceValue"] = invoiceRow["InvoiceValue"];
            }
            return data;
        }

        private DataTable GetDefaultTable(int startYear, int endYear)
        {
            var data = new DataTable();
            data.Columns.Add("Title", typeof(string));
            data.Columns.Add("Year", typeof(int));
            data.Columns.Add("Quater", typeof(int));
            data.Columns.Add("ReceiptValue", typeof(decimal));
            data.Columns.Add("PlanReceiptValue", typeof(decimal));
            data.Columns.Add("InvoiceValue", typeof(decimal));
            if (startYear > endYear) throw new Formula.Exceptions.BusinessException("起始年份不能大于结束年份！");

            for (int i = startYear; i < endYear; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    var row = data.NewRow();
                    row["Year"] = i;
                    row["Quater"] = j;
                    row["Title"] = i.ToString() + "年" + j + "季度";
                    row["ReceiptValue"] = 0;
                    row["PlanReceiptValue"] = 0;
                    row["InvoiceValue"] = 0;
                    data.Rows.Add(row);
                }
            }

            var currentQuarter = MarketHelper.GetQuarter(DateTime.Now);
            if (endYear != DateTime.Now.Year)
                currentQuarter = 4;

            for (int j = 1; j <= currentQuarter; j++)
            {
                var row = data.NewRow();
                row["Year"] = endYear;
                row["Quater"] = j;
                row["Title"] = endYear.ToString() + "年" + j + "季度";
                row["ReceiptValue"] = 0;
                row["PlanReceiptValue"] = 0;
                row["InvoiceValue"] = 0;
                data.Rows.Add(row);
            }
            return data;
        }
        #endregion       

        public JsonResult GetReceiptObjectList(QueryBuilder qb)
        {
            string sql = @" select * from (select S_C_ManageContract_ReceiptObj.*,S_C_ManageContract.Name as ContractName,
S_C_ManageContract.PartyA as CustomerID,BusinessManagerName,ProductionManagerName,
S_C_ManageContract.SerialNumber as ContractCode from S_C_ManageContract_ReceiptObj left join S_C_ManageContract 
on S_C_ManageContract_ReceiptObj.S_C_ManageContractID= S_C_ManageContract.ID ) TableInfo ";
            this.FillQueryBuilderFilter(qb, true);
            qb.PageSize = 0;
            var data = this.SqlHelper.ExecuteGridData(String.Format(sql, DateTime.Now.Year.ToString()), qb);
            return Json(data);
        }

        public JsonResult GetProjectList(QueryBuilder qb)
        {
            this.FillQueryBuilderFilter<S_I_Project>(qb);
            var data = this.entities.Set<S_I_Project>().WhereToGridData(qb);
            return Json(data);
        }

        public JsonResult GetCustomer(string cId)
        {
            var entity = entities.Set<S_F_Customer>().Find(cId);
            var result = new Dictionary<string, object>();
            if (entity != null)
            {
                result = FormulaHelper.ModelToDic<S_F_Customer>(entity);
                if (entity.S_F_Customer_BankAccounts.Count > 0)
                {
                    var account = entity.S_F_Customer_BankAccounts.OrderBy(c => c.SortIndex).FirstOrDefault();
                    result.SetValue("BankAccounts", account.BankAccount);
                    result.SetValue("BankName", account.BankName);
                }
            }
            return Json(result);
        }
    }
}
