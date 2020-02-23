using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Text;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;

namespace Expenses.Areas.Report.Controllers
{
    public class DeptReportController : ExpensesController
    {
        public ActionResult DeptOverView()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7, 1, false, "年份");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = !String.IsNullOrEmpty(this.GetQueryString("Multi")) && this.GetQueryString("Multi").ToLower() == true.ToString().ToLower() ? true : false;
            tab.Categories.Add(yearCategory);

            var monthCategory = CategoryFactory.GetMonthCategory("BelongMonth", true, "月份");
            //monthCategory.SetDefaultItem(DateTime.Now.Month.ToString());
            monthCategory.Multi = !String.IsNullOrEmpty(this.GetQueryString("Multi")) && this.GetQueryString("Multi").ToLower() == true.ToString().ToLower() ? true : false;
            tab.Categories.Add(monthCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            var incomeUnitType = String.IsNullOrEmpty(this.GetQueryString("InComeType")) ? "Project" : this.GetQueryString("InComeType");
            var enumService = FormulaHelper.GetService<IEnumService>();
            var deptDt = enumService.GetEnumTable("Expense.ReportDept");
            var resultDt = createResultTableSchema(deptDt);
            var baseDb = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var userCountDt = baseDb.ExecuteDataTable(@"select count(UserID) as UserCount,OrgID from S_A__OrgUser group by OrgID");

            string dateWhereStr = " and 1=1 ";
            var belongYearCnd = qb.Items.FirstOrDefault(c => c.Field == "BelongYear");
            var belongMonthCnd = qb.Items.FirstOrDefault(c => c.Field == "BelongMonth");
            var belongQuarterCnd = qb.Items.FirstOrDefault(c => c.Field == "BelongQuarter");
            var belongYear = ""; var belongMonth = ""; var belongQuarter = "";
            if (belongYearCnd != null && belongYearCnd.Value != null && !String.IsNullOrEmpty(belongYearCnd.Value.ToString()))
            {
                dateWhereStr += " and BelongYear in (" + belongYearCnd.Value.ToString() + ")";
                belongYear = belongYearCnd.Value.ToString();
            }

            if (belongMonthCnd != null && belongMonthCnd.Value != null && !String.IsNullOrEmpty(belongMonthCnd.Value.ToString()))
            {
                dateWhereStr += " and BelongMonth in (" + belongMonthCnd.Value.ToString() + ")";
                belongMonth = belongMonthCnd.Value.ToString();
            }

            if (belongQuarterCnd != null && belongQuarterCnd.Value != null && !String.IsNullOrEmpty(belongQuarterCnd.Value.ToString()))
            {
                // dateWhereStr += " and BelongMonth in (" + belongQuarterCnd.Value.ToString() + ")";
                belongQuarter = belongQuarterCnd.Value.ToString();
            }


            string nodeType = String.IsNullOrEmpty(this.Request["NodeType"]) ? CBSNodeType.Project.ToString() : this.Request["NodeType"];

            #region 查询合同信息
            string contractSql = String.Format(@"select * from (
select isnull(Sum(ContractRMBAmount),0) as ContractValue,ProductionDept 
from S_C_ManageContract where SignDate is not null  {0}
group by ProductionDept 
union all
select Sum(ContractValue),ProductionDept from
(select isnull(Sum(SupplementaryRMBAmount),0) as ContractValue,S_C_ManageContract.ProductionDept,
S_C_ManageContract_Supplementary.BelongYear,S_C_ManageContract_Supplementary.BelongMonth
from S_C_ManageContract_Supplementary
left join S_C_ManageContract on
S_C_ManageContract_Supplementary.ContractInfoID = S_C_ManageContract.ID
group by S_C_ManageContract.ProductionDept,S_C_ManageContract_Supplementary.BelongYear,
S_C_ManageContract_Supplementary.BelongMonth) SupplementaryInfo
where 1=1 {0}  group by ProductionDept) MainContract", dateWhereStr);
            var contractDt = this.SQLDB.ExecuteDataTable(contractSql);

            contractSql = String.Format(@"select * from (
select isnull(Sum(ContractRMBAmount),0) as ContractValue,ProductionDept 
from S_C_ManageContract where SignDate is not null {0}
group by ProductionDept 
union all
select Sum(ContractValue),ProductionDept from
(select isnull(Sum(SupplementaryRMBAmount),0) as ContractValue,S_C_ManageContract.ProductionDept,
S_C_ManageContract_Supplementary.BelongYear,S_C_ManageContract_Supplementary.BelongMonth
from S_C_ManageContract_Supplementary
left join S_C_ManageContract on
S_C_ManageContract_Supplementary.ContractInfoID = S_C_ManageContract.ID
group by S_C_ManageContract.ProductionDept,S_C_ManageContract_Supplementary.BelongYear,
S_C_ManageContract_Supplementary.BelongMonth) SupplementaryInfo
where 1=1 {0}  group by ProductionDept) MainContract", "");

            var AllContractDt = this.SQLDB.ExecuteDataTable(contractSql);
            var contractList = contractDt.AsEnumerable();
            var allContractList = AllContractDt.AsEnumerable();
            #endregion


            string incomeSQL = String.Format(@"select isnull(Sum(CurrentIncomeTotalValue),0) as IncomeValue,ChargerDept from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID=S_EP_RevenueInfo.ID
where S_EP_RevenueInfo.State='{0}' {1} group by ChargerDept", "Finish", dateWhereStr);
            var incomeDt = this.SQLDB.ExecuteDataTable(incomeSQL);


            string invoiceSQL = String.Format(@"select isnull(Sum(InvoiceValue),0) as InvoiceValue,ProductionDept from (
select Sum(Amount) as InvoiceValue,S_C_Invoice.BelongYear,S_C_Invoice.BelongMonth,S_C_Invoice.BelongQuarter,
ProductionDept from S_C_Invoice left join S_C_ManageContract
on S_C_Invoice.ContractInfoID=S_C_ManageContract.ID
group by S_C_Invoice.BelongYear,S_C_Invoice.BelongMonth,S_C_Invoice.BelongQuarter,
ProductionDept) InvoiceInfo where 1=1 {0} group by ProductionDept", dateWhereStr);
            var invoiceDt = this.SQLDB.ExecuteDataTable(invoiceSQL);

            var allInvoiceDt = this.SQLDB.ExecuteDataTable(@"select isnull(Sum(Amount),0) as InvoiceValue,
ProductionDept from S_C_Invoice left join S_C_ManageContract on S_C_Invoice.ContractInfoID=S_C_ManageContract.ID group by ProductionDept");

            var receiptDt = this.SQLDB.ExecuteDataTable(String.Format(@"select Sum(Amount) as Value,ProductUnit as ChargerDept from S_C_Receipt
where 1=1 {0} group by ProductUnit", dateWhereStr));
            var allReceiptDt = this.SQLDB.ExecuteDataTable(@"select Sum(Amount) as Value,ProductUnit as ChargerDept from S_C_Receipt group by ProductUnit");

            var costDt = this.SQLDB.ExecuteDataTable(String.Format(@"select isnull(Sum(TotalPrice),0) as Value,BelongDept as ChargerDept from S_EP_CostInfo
where State='Finish' {0} group by BelongDept", dateWhereStr));
            foreach (DataRow item in resultDt.Rows)
            {
                item["BelongYear"] = belongYear;
                item["BelongMonth"] = belongMonth;
                item["BelongQuarter"] = belongQuarter;
                var contractRow = contractList.FirstOrDefault(c => c["ProductionDept"] != DBNull.Value && c["ProductionDept"].ToString() == item["ID"].ToString());
                item["CurrentContractValue"] = contractRow == null || contractRow["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(contractRow["ContractValue"]);

                var allContractRow = allContractList.FirstOrDefault(c => c["ProductionDept"] != DBNull.Value && c["ProductionDept"].ToString() == item["ID"].ToString());
                item["ContractValue"] = allContractRow == null || allContractRow["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(allContractRow["ContractValue"]);

                var userCountRow = userCountDt.AsEnumerable().FirstOrDefault(c => c["OrgID"] != DBNull.Value && c["OrgID"].ToString() == item["ID"].ToString());
                item["UserCount"] = userCountRow == null || userCountRow["UserCount"] == DBNull.Value ? 0m : Convert.ToDecimal(userCountRow["UserCount"]);

                var incomeValueRow = incomeDt.AsEnumerable().FirstOrDefault(c => c["ChargerDept"] != DBNull.Value && c["ChargerDept"].ToString() == item["ID"].ToString());
                item["CurrentIncomeValue"] = incomeValueRow == null || incomeValueRow["IncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(incomeValueRow["IncomeValue"]);
                item["AvgIncomeValue"] = Convert.ToDecimal(item["UserCount"]) > 0 ? Math.Round(Convert.ToDecimal(item["CurrentIncomeValue"]) / Convert.ToDecimal(item["UserCount"]), 2) : 0;

                var costValueRow = costDt.AsEnumerable().FirstOrDefault(c => c["ChargerDept"] != DBNull.Value && c["ChargerDept"].ToString() == item["ID"].ToString());
                item["CurrentCostValue"] = costValueRow == null || costValueRow["Value"] == DBNull.Value ? 0m : Convert.ToDecimal(costValueRow["Value"]);
                item["CurrentProfit"] = Convert.ToDecimal(item["CurrentIncomeValue"]) - Convert.ToDecimal(item["CurrentCostValue"]);


                var invoiceRow = invoiceDt.AsEnumerable().FirstOrDefault(c => c["ProductionDept"] != DBNull.Value && c["ProductionDept"].ToString() == item["ID"].ToString());
                item["CurrentInvoiceValue"] = invoiceRow == null || invoiceRow["InvoiceValue"] == DBNull.Value ? 0m : Convert.ToDecimal(invoiceRow["InvoiceValue"]);

                var allInvoiceRow = allInvoiceDt.AsEnumerable().FirstOrDefault(c => c["ProductionDept"] != DBNull.Value && c["ProductionDept"].ToString() == item["ID"].ToString());
                item["AllInvoiceValue"] = allInvoiceRow == null || allInvoiceRow["InvoiceValue"] == DBNull.Value ? 0m : Convert.ToDecimal(allInvoiceRow["InvoiceValue"]);


                var receiptRow = receiptDt.AsEnumerable().FirstOrDefault(c => c["ChargerDept"] != DBNull.Value && c["ChargerDept"].ToString() == item["ID"].ToString());
                item["CurrentReceiptValue"] = receiptRow == null || receiptRow["Value"] == DBNull.Value ? 0m : Convert.ToDecimal(receiptRow["Value"]);
                item["AvgReceiptValue"] = Convert.ToDecimal(item["UserCount"]) > 0 ? Math.Round(Convert.ToDecimal(item["CurrentReceiptValue"]) / Convert.ToDecimal(item["UserCount"]), 2) : 0;

                var allReceiptRow = allReceiptDt.AsEnumerable().FirstOrDefault(c => c["ChargerDept"] != DBNull.Value && c["ChargerDept"].ToString() == item["ID"].ToString());
                item["AllReceiptValue"] = allReceiptRow == null || allReceiptRow["Value"] == DBNull.Value ? 0m : Convert.ToDecimal(allReceiptRow["Value"]);

                item["Receivables"] = Convert.ToDecimal(item["AllInvoiceValue"]) - Convert.ToDecimal(item["AllReceiptValue"]);
                if (Convert.ToDecimal(item["Receivables"]) < 0)
                {
                    item["Receivables"] = 0;
                }

                //应收账款周转率 = 当期收入/应收账款/2
                item["ReceivablesTurnoverRatio"] = Convert.ToDecimal(item["Receivables"]) > 0 ? Convert.ToDecimal(item["CurrentIncomeValue"]) / Convert.ToDecimal(item["Receivables"]) / 2 : 0;
            }
            var data = new Dictionary<string, object>();
            data.SetValue("data", resultDt);
            return Json(data);
        }

        DataTable createResultTableSchema(DataTable mainTable = null)
        {
            var result = new DataTable();
            result.Columns.Add("Name");
            result.Columns.Add("ID");
            result.Columns.Add("BelongYear");
            result.Columns.Add("BelongMonth");
            result.Columns.Add("BelongQuarter");
            result.Columns.Add("UserCount", typeof(decimal));
            result.Columns.Add("CurrentContractValue", typeof(decimal));
            result.Columns.Add("ContractValue", typeof(decimal));
            result.Columns.Add("CurrentIncomeValue", typeof(decimal));
            result.Columns.Add("CurrentCostValue", typeof(decimal));
            result.Columns.Add("CurrentProfit", typeof(decimal));
            result.Columns.Add("AvgIncomeValue", typeof(decimal));
            result.Columns.Add("CurrentInvoiceValue", typeof(decimal));
            result.Columns.Add("AllInvoiceValue", typeof(decimal));
            result.Columns.Add("CurrentReceiptValue", typeof(decimal));
            result.Columns.Add("AllReceiptValue", typeof(decimal));
            result.Columns.Add("AvgReceiptValue", typeof(decimal));
            result.Columns.Add("Receivables", typeof(decimal));
            result.Columns.Add("ReceivablesTurnoverRatio", typeof(decimal));
            result.Columns.Add("ProductionValue", typeof(decimal));
            result.Columns.Add("CurrentProductionValue", typeof(decimal));
            result.Columns.Add("ProductionConfirmValue", typeof(decimal));
            result.Columns.Add("AvgProductionConfirmValue", typeof(decimal));
            result.Columns.Add("RemainProductionValue", typeof(decimal));

            if (mainTable != null && mainTable.Rows.Count > 0)
            {
                foreach (DataRow item in mainTable.Rows)
                {
                    var row = result.NewRow();
                    row["Name"] = item["text"];
                    row["ID"] = item["value"];
                    result.Rows.Add(row);
                }
            }
            return result;
        }
    }
}
