using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Config.Logic;
using Formula.Helper;
using Expenses.Logic;

namespace Expenses.Areas.Budget.Controllers
{
    public class ComparisonController : ExpensesController
    {
        public ActionResult Comparison()
        {
            string sourceID = this.Request["SourceID"];
            string targetID = this.Request["TargetID"];
            var source = this.GetDataDicByID("S_EP_BudgetVersion", sourceID);
            var target = this.GetDataDicByID("S_EP_BudgetVersion", targetID);
            if (source == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到要对比的预算信息");
            }
            if (target == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到要对比的预算信息");
            }

            ViewBag.TargetBudget = source;
            ViewBag.SourceBudget = target;
            return View();
        }

        public JsonResult GetTreeList(string ListIDs)
        {
            var versionList = ListIDs.Split(',');
            var result = new DataTable();
            result.Columns.Add("ID");            
            result.Columns.Add("ParentID");
            result.Columns.Add("Code");
            result.Columns.Add("ParentCode");
            result.Columns.Add("Name");
            result.Columns.Add("NodeType");
            result.Columns.Add("SortIndex");
            result.Columns.Add("Diff");
            foreach (var item in versionList)
            {
                result.Columns.Add(item + "_TotalValue", typeof(decimal));
                result.Columns.Add(item + "_UnitPrice", typeof(decimal));
                result.Columns.Add(item + "_Quantity", typeof(decimal));
            }

            string sql = @"select CBSID as ID,Name,Code,CBSParentID as ParentID,SortIndex,NodeType,S_EP_BudgetVersionID,
                  UnitPrice,Quantity,TotalValue from S_EP_BudgetVersion_Detail where S_EP_BudgetVersionID in ('{0}') ";
            var dataTable = this.SQLDB.ExecuteDataTable(String.Format(sql, ListIDs.Replace(",", "','")));
            var totalRow = result.NewRow();
            totalRow["Code"] = GuidHelper.CreateGuid();
            totalRow["ID"] = GuidHelper.CreateGuid();
            totalRow["ParentID"] = "";
            totalRow["ParentCode"] = "";
            totalRow["Name"] = "预算单元";
            totalRow["NodeType"] = "Root";
            totalRow["SortIndex"] = "-100";
            totalRow["Diff"] = false.ToString();
            result.Rows.Add(totalRow);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["S_EP_BudgetVersionID"] == null || row["S_EP_BudgetVersionID"] == DBNull.Value || String.IsNullOrEmpty(row["S_EP_BudgetVersionID"].ToString()))
                    continue;
                var versionID = row["S_EP_BudgetVersionID"].ToString();
                var resultRows = result.Select("Code='" + row["Code"] + "'");
                var parentRows = dataTable.Select("ID = " + "'" + row["ParentID"].ToString() + "'");

                if (resultRows.Length == 0)
                {
                    var resultRow = result.NewRow();
                    resultRow["ID"] = row["ID"];
                    resultRow["Code"] = row["Code"];
                    resultRow["Name"] = row["Name"];
                    resultRow["NodeType"] = row["NodeType"];
                    resultRow["SortIndex"] = row["SortIndex"];
                    resultRow["Diff"] = false.ToString();

                   
                    if (parentRows.Length == 0)
                    {
                        resultRow["ParentID"] = totalRow["ID"];
                        resultRow["ParentCode"] = totalRow["Code"];
                    }
                    else
                    {
                        resultRow["ParentID"] = row["ParentID"];
                        resultRow["ParentCode"] = parentRows[0]["Code"];
                    }
                    resultRow[versionID + "_TotalValue"] = row["TotalValue"];
                    resultRow[versionID + "_UnitPrice"] = row["UnitPrice"];
                    resultRow[versionID + "_Quantity"] = row["Quantity"];
                    result.Rows.Add(resultRow);
                }
                else
                {
                    var resultRow = resultRows.FirstOrDefault();
                    resultRow[versionID + "_TotalValue"] = row["TotalValue"];
                    resultRow[versionID + "_UnitPrice"] = row["UnitPrice"];
                    resultRow[versionID + "_Quantity"] = row["Quantity"];
                }

                if (parentRows.Length == 0)
                {
                    decimal totalTmp = 0, tmp = 0;
                    decimal.TryParse(totalRow[versionID + "_TotalValue"].ToString(), out totalTmp);
                    decimal.TryParse(row["TotalValue"].ToString(), out tmp);
                    totalRow[versionID + "_TotalValue"] = totalTmp + tmp;
                }
            }

            foreach (DataRow item in result.Rows)
            {
                var lastTotalValue = -1m;
                for (int i = 0; i < versionList.Length; i++)
                {
                    var field = versionList[i] + "_TotalValue";
                    var totalValue = item[field] == null || item[field] == DBNull.Value ? 0m : Convert.ToDecimal(item[field]);
                    if (totalValue != lastTotalValue && (lastTotalValue >= 0))
                    {
                        item["Diff"] = true.ToString();
                        break;
                    }
                    lastTotalValue = totalValue;
                }
            }
            var rows = result.Select();
            var resultDt = result.Clone();
            resultDt.Clear();
            foreach (var item in rows)
            {
                resultDt.ImportRow(item);
            }
            return Json(resultDt);
        }
    }
}
