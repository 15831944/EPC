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
using Formula.Exceptions;

namespace Expenses.Areas.Report.Controllers
{
    public class CustomizeReportController : ExpensesController
    {
        public enum GroupFieldType
        {
            [System.ComponentModel.Description("业务类型")]
            BusinessType,
            [System.ComponentModel.Description("区域")]
            Area
        }

        #region 自定义汇总统计
        public ActionResult CustomizeAnalysisView()
        {
            ViewBag.DefautGroupField = GroupFieldType.BusinessType.ToString();            
            return View();
        }

        public JsonResult GetCustomizeAnalysisList(QueryBuilder qb)
        {
            string belongYearStr = GetQueryString("BelongYear");
            string belongQuarterStr = GetQueryString("BelongQuarter");
            string BelongMonthStr = GetQueryString("BelongMonth");
            string groupFieldStr = GetQueryString("GroupField");

            //本期,上一期,本期末累计,上一期末累计
            string currentDateFilter = "", lastDateFilter = "", currentDateTotalFilter, lastDateTotalFilter;
            if (!string.IsNullOrEmpty(BelongMonthStr))
            {
                currentDateFilter = string.Format("BelongMonth = {0} and BelongYear = {1}", BelongMonthStr, belongYearStr);
                currentDateTotalFilter = string.Format("BelongMonth <= {0} and BelongYear = {1}", BelongMonthStr, belongYearStr);
                lastDateFilter = string.Format("BelongMonth = {0} and BelongYear = ({1} - 1)", BelongMonthStr, belongYearStr);
                lastDateTotalFilter = string.Format("BelongMonth <= {0} and BelongYear = ({1} - 1)", BelongMonthStr, belongYearStr);
            }
            else if (!string.IsNullOrEmpty(belongQuarterStr))
            {
                currentDateFilter = string.Format("BelongQuarter = {0} and BelongYear = {1}", belongQuarterStr, belongYearStr);
                currentDateTotalFilter = string.Format("BelongQuarter <= {0} and BelongYear = {1}", belongQuarterStr, belongYearStr);
                lastDateFilter = string.Format("BelongQuarter = {0} and BelongYear = ({1} - 1)", belongQuarterStr, belongYearStr);
                lastDateTotalFilter = string.Format("BelongQuarter <= {0} and BelongYear = ({1} - 1)", belongQuarterStr, belongYearStr);
            }
            else
            {
                currentDateFilter = string.Format(" BelongYear = {0}", belongYearStr);
                currentDateTotalFilter = string.Format(" BelongYear = {0}", belongYearStr);
                lastDateFilter = string.Format(" BelongYear = ({0} - 1)", belongYearStr);
                lastDateTotalFilter = string.Format(" BelongYear = ({0} - 1)", belongYearStr);
            }

            string cbsNodeSql = @"select 
            --成本
            isnull(CostCurrent.CostValue,0) as Cost_Current,---本期值
            isnull(CostFinalTotal.CostValue,0) as Cost_FinalTotal,---期末累计值
            isnull(CostCurrentLastYear.CostValue,0) as Cost_CurrentLastYear,---上一期值
            isnull(CostFinalTotalLastYear.CostValue,0) as Cost_FinalTotalLastYear,---上一期期末累计值
            '' as Cost_CurrentRate,---本期占比
            '' as Cost_CurrentRateTotal,---累计占比
            --其他
			S_EP_CBSNode.Area,
			S_EP_CBSNode.BusinessType
            from S_EP_CBSNode
            --成本
            left join (select sum(TotalPrice) CostValue,CBSNodeID from S_EP_CostInfo where S_EP_CostInfo.State = 'Finish' and {0} group by CBSNodeID) CostCurrent on CostCurrent.CBSNodeID = S_EP_CBSNode.ID
            left join (select sum(TotalPrice) CostValue,CBSNodeID from S_EP_CostInfo where S_EP_CostInfo.State = 'Finish' and {1} group by CBSNodeID) CostFinalTotal on CostFinalTotal.CBSNodeID = S_EP_CBSNode.ID
            left join (select sum(TotalPrice) CostValue,CBSNodeID from S_EP_CostInfo where S_EP_CostInfo.State = 'Finish' and {2} group by CBSNodeID) CostCurrentLastYear on CostCurrentLastYear.CBSNodeID = S_EP_CBSNode.ID
            left join (select sum(TotalPrice) CostValue,CBSNodeID from S_EP_CostInfo where S_EP_CostInfo.State = 'Finish' and {3} group by CBSNodeID) CostFinalTotalLastYear on CostFinalTotalLastYear.CBSNodeID = S_EP_CBSNode.ID";

            //包括补充合同
            string contractSql = @"select S_C_ManageContract_ReceiptObj.ID,S_C_ManageContract.ID as ContractID, S_C_ManageContract_ReceiptObj.ReceiptValue as Amount, S_I_Project.ProjectClass as BusinessType, '' as Area,
                                   S_C_ManageContract.BelongYear,S_C_ManageContract.BelongMonth,S_C_ManageContract.BelongQuarter
                                   from S_C_ManageContract_ReceiptObj 
                                   inner join S_C_ManageContract on S_C_ManageContract_ReceiptObj.S_C_ManageContractID = S_C_ManageContract.ID
                                   inner join S_I_Project on S_I_Project.ID = S_C_ManageContract_ReceiptObj.ProjectInfo
                                   union all
                                   select S_C_ManageContract_Supplementary_ReceiptObj.ID, S_C_ManageContract_Supplementary.ID as ContractID, S_C_ManageContract_Supplementary_ReceiptObj.ReceiptValue as Amount, S_I_Project.ProjectClass as BusinessType, '' as Area,
                                   S_C_ManageContract_Supplementary.BelongYear,S_C_ManageContract_Supplementary.BelongMonth,S_C_ManageContract_Supplementary.BelongQuarter
                                   from S_C_ManageContract_Supplementary_ReceiptObj 
                                   inner join S_C_ManageContract_Supplementary on S_C_ManageContract_Supplementary_ReceiptObj.S_C_ManageContract_SupplementaryID = S_C_ManageContract_Supplementary.ID
                                   inner join S_I_Project on S_I_Project.ID = S_C_ManageContract_Supplementary_ReceiptObj.ProjectInfo";

            string invoiceSql = @"select RelationValue as Amount, S_I_Project.ProjectClass as BusinessType, S_I_Project.Area as Area,S_C_Invoice.BelongYear,S_C_Invoice.BelongQuarter,S_C_Invoice.BelongMonth
                                  from S_C_Invoice_ReceiptObject
                                  inner join S_C_ManageContract_ReceiptObj on S_C_ManageContract_ReceiptObj.ID = S_C_Invoice_ReceiptObject.ReceiptObjectID
                                  inner join S_I_Project on S_I_Project.ID = S_C_ManageContract_ReceiptObj.ProjectInfo
                                  inner join S_C_Invoice on S_C_Invoice.ID = S_C_Invoice_ReceiptObject.S_C_InvoiceID";

            string receiptSql = @"select RelationValue as Amount, S_I_Project.ProjectClass as BusinessType, S_I_Project.Area as Area,S_C_Receipt.BelongYear,S_C_Receipt.BelongQuarter,S_C_Receipt.BelongMonth
                                  from S_C_ReceiptPlanRelation 
                                  inner join S_C_ManageContract_ReceiptObj on S_C_ManageContract_ReceiptObj.ID = S_C_ReceiptPlanRelation.ReceiptObjectID
                                  inner join S_I_Project on S_I_Project.ID = S_C_ManageContract_ReceiptObj.ProjectInfo
                                  inner join S_C_Receipt on S_C_Receipt.ID = S_C_ReceiptPlanRelation.ReceiptID";

            string incomeSql = @"select CurrentIncomeTotalValue as Amount, Area,BusinessType, BelongYear,BelongQuarter,BelongMonth from S_EP_RevenueInfo_Detail
                                 inner join S_EP_RevenueInfo on S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID = S_EP_RevenueInfo.ID where S_EP_RevenueInfo.State='Finish'";

            var resDT = this.SQLDB.ExecuteDataTable(string.Format(cbsNodeSql, currentDateFilter, currentDateTotalFilter,
                lastDateFilter, lastDateTotalFilter));

            var codeList = GetEnumCodeList(groupFieldStr);

            List<Dictionary<string, object>> resList = new List<Dictionary<string, object>>();
            foreach (var code in codeList)
            {
                Dictionary<string, object> groupRes = new Dictionary<string, object>();
                groupRes.SetValue(groupFieldStr, code);

                #region 不依赖CBSNode查
                //合同
                {
                    fillTheAmount(groupFieldStr, currentDateFilter, lastDateFilter, currentDateTotalFilter, lastDateTotalFilter, contractSql, code, "Contract", groupRes);
                }
                //开票
                {
                    fillTheAmount(groupFieldStr, currentDateFilter, lastDateFilter, currentDateTotalFilter, lastDateTotalFilter, invoiceSql, code, "Invoice", groupRes);
                }
                //收款
                {
                    fillTheAmount(groupFieldStr, currentDateFilter, lastDateFilter, currentDateTotalFilter, lastDateTotalFilter, receiptSql, code, "Receipt", groupRes);
                }
                //收入
                {
                    fillTheAmount(groupFieldStr, currentDateFilter, lastDateFilter, currentDateTotalFilter, lastDateTotalFilter, incomeSql, code, "Income", groupRes);
                }
                #endregion
                #region 依赖CBSNode查
                foreach (DataColumn dc in resDT.Columns)
                {
                    if (dc.ColumnName == groupFieldStr) continue;
                    var val = resDT.Select(groupFieldStr + " = '" + code + "'").Sum(a =>
                    {
                        decimal tmp = 0;
                        decimal.TryParse(a[dc.ColumnName].ToString(), out tmp);
                        return tmp;
                    });
                    groupRes.SetValue(dc.ColumnName, val);

                    //计算本期同比
                    if (dc.ColumnName.Contains("CurrentLastYear"))
                    {
                        string pre = dc.ColumnName.Split('_')[0];
                        decimal current = Convert.ToDecimal(groupRes.GetValue(pre + "_Current"));
                        if (val != 0)
                        {
                            groupRes.SetValue(pre + "_CurrentYoY", (100 * (current - val) / val).ToString("0.00"));
                        }
                        else
                        {
                            groupRes.SetValue(pre + "_CurrentYoY", "0");
                        }

                    }
                    //计算年末累计同比
                    else if (dc.ColumnName.Contains("FinalTotalLastYear"))
                    {
                        string pre = dc.ColumnName.Split('_')[0];
                        decimal finalTotal = Convert.ToDecimal(groupRes.GetValue(pre + "_FinalTotal"));
                        if (val != 0)
                        {
                            groupRes.SetValue(pre + "_CurrentYoYTotal", (100 * (finalTotal - val) / val).ToString("0.00"));
                        }
                        else
                        {
                            groupRes.SetValue(pre + "_CurrentYoYTotal", "0");
                        }
                    }
                }
                #endregion
                #region 利润(收入-成本)
                //本期值
                decimal revenueCurrent = 0, costCurrent = 0;
                decimal.TryParse(groupRes.GetValue("Income_Current"), out revenueCurrent);
                decimal.TryParse(groupRes.GetValue("Cost_Current"), out costCurrent);
                decimal profitCurrent = revenueCurrent - costCurrent;
                groupRes.SetValue("Profit_Current", profitCurrent);
                //期末累计值
                decimal revenueFinalTotal = 0, costFinalTotal = 0;
                decimal.TryParse(groupRes.GetValue("Income_FinalTotal"), out revenueFinalTotal);
                decimal.TryParse(groupRes.GetValue("Cost_FinalTotal"), out costFinalTotal);
                decimal profitFinalTotal = revenueFinalTotal - costFinalTotal;
                groupRes.SetValue("Profit_FinalTotal", profitFinalTotal);
                //上一期值
                decimal revenueCurrentLastYear = 0, costCurrentLastYear = 0;
                decimal.TryParse(groupRes.GetValue("Income_CurrentLastYear"), out revenueCurrentLastYear);
                decimal.TryParse(groupRes.GetValue("Cost_CurrentLastYear"), out costCurrentLastYear);
                decimal profitCurrentLastYear = revenueCurrentLastYear - costCurrentLastYear;
                groupRes.SetValue("Profit_CurrentLastYear", profitCurrentLastYear);
                //上一期末累计值
                decimal revenueFinalTotalLastYear = 0, costFinalTotalLastYear = 0;
                decimal.TryParse(groupRes.GetValue("Income_FinalTotalLastYear"), out revenueFinalTotalLastYear);
                decimal.TryParse(groupRes.GetValue("Cost_FinalTotalLastYear"), out costFinalTotalLastYear);
                decimal profitFinalTotalLastYear = revenueFinalTotalLastYear - costFinalTotalLastYear;
                groupRes.SetValue("Profit_FinalTotalLastYear", profitFinalTotalLastYear);
                //本期同比
                groupRes.SetValue("Profit_CurrentYoY", "0");
                if (profitCurrentLastYear != 0)
                    groupRes.SetValue("Profit_CurrentYoY", ((profitCurrent - profitCurrentLastYear) * 100 / profitCurrentLastYear).ToString("0.00"));
                //年末累计统计
                groupRes.SetValue("Profit_CurrentYoYTotal", "0");
                if (profitFinalTotalLastYear != 0)
                    groupRes.SetValue("Profit_CurrentYoYTotal", ((profitCurrentLastYear - profitFinalTotalLastYear) * 100 / profitFinalTotalLastYear).ToString("0.00"));
                #endregion
                resList.Add(groupRes);
            }

            if (resList.Count > 0)
            {
                decimal Contract_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Contract_Current")));//Convert.ToDecimal(resDT.Compute("Sum(Contract_Current)", "true"));
                decimal Contract_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Contract_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Contract_FinalTotal)", "true"));
                decimal Income_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Income_Current")));//Convert.ToDecimal(resDT.Compute("Sum(Income_Current)", "true"));
                decimal Income_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Income_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Income_FinalTotal)", "true"));
                decimal Invoice_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Invoice_Current")));// Convert.ToDecimal(resDT.Compute("Sum(Invoice_Current)", "true"));
                decimal Invoice_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Invoice_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Invoice_FinalTotal)", "true"));
                decimal Receipt_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Receipt_Current")));//Convert.ToDecimal(resDT.Compute("Sum(Receipt_Current)", "true"));
                decimal Receipt_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Receipt_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Receipt_FinalTotal)", "true"));
                decimal Cost_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Cost_Current")));//Convert.ToDecimal(resDT.Compute("Sum(Cost_Current)", "true"));
                decimal Cost_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Cost_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Cost_FinalTotal)", "true"));
                decimal Profit_CurrentAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Profit_Current")));//Convert.ToDecimal(resDT.Compute("Sum(Profit_Current)", "true"));
                decimal Profit_FinalTotalAll = resList.Sum(a => Convert.ToDecimal(a.GetValue("Profit_FinalTotal")));//Convert.ToDecimal(resDT.Compute("Sum(Profit_FinalTotal)", "true"));


                foreach (var dr in resList)
                {
                    decimal Contract_Current = Convert.ToDecimal(dr["Contract_Current"]);
                    dr["Contract_CurrentRate"] = "0";
                    if (Contract_CurrentAll != 0)
                    {
                        dr["Contract_CurrentRate"] = (Contract_Current * 100 / Contract_CurrentAll).ToString("0.00");
                    }
                    decimal Contract_FinalTotal = Convert.ToDecimal(dr["Contract_FinalTotal"]);

                    dr["Contract_CurrentRateTotal"] = "0";
                    if (Contract_FinalTotalAll != 0)
                    {
                        dr["Contract_CurrentRateTotal"] = (Contract_FinalTotal * 100 / Contract_FinalTotalAll).ToString("0.00");
                    }

                    decimal Income_Current = Convert.ToDecimal(dr["Income_Current"]);
                    dr["Income_CurrentRate"] = "0";
                    if (Income_CurrentAll != 0)
                    {
                        dr["Income_CurrentRate"] = (Income_Current * 100 / Income_CurrentAll).ToString("0.00");
                    }
                    decimal Income_FinalTotal = Convert.ToDecimal(dr["Income_FinalTotal"]);
                    dr["Income_CurrentRateTotal"] = "0";
                    if (Income_FinalTotalAll != 0)
                    {
                        dr["Income_CurrentRateTotal"] = (Income_FinalTotal * 100 / Income_FinalTotalAll).ToString("0.00");
                    }

                    decimal Invoice_Current = Convert.ToDecimal(dr["Invoice_Current"]);
                    dr["Invoice_CurrentRate"] = "0";
                    if (Invoice_CurrentAll != 0)
                    {
                        dr["Invoice_CurrentRate"] = (Invoice_Current * 100 / Invoice_CurrentAll).ToString("0.00");
                    }
                    decimal Invoice_FinalTotal = Convert.ToDecimal(dr["Invoice_FinalTotal"]);
                    dr["Invoice_CurrentRateTotal"] = "0";
                    if (Invoice_FinalTotalAll != 0)
                    {
                        dr["Invoice_CurrentRateTotal"] = (Invoice_FinalTotal * 100 / Invoice_FinalTotalAll).ToString("0.00");
                    }

                    decimal Receipt_Current = Convert.ToDecimal(dr["Receipt_Current"]);
                    dr["Receipt_CurrentRate"] = "0";
                    if (Receipt_CurrentAll != 0)
                    {
                        dr["Receipt_CurrentRate"] = (Receipt_Current * 100 / Receipt_CurrentAll).ToString("0.00");
                    }
                    decimal Receipt_FinalTotal = Convert.ToDecimal(dr["Receipt_FinalTotal"]);
                    dr["Receipt_CurrentRateTotal"] = "0";
                    if (Receipt_FinalTotalAll != 0)
                    {
                        dr["Receipt_CurrentRateTotal"] = (Receipt_FinalTotal * 100 / Receipt_FinalTotalAll).ToString("0.00");
                    }

                    decimal Cost_Current = Convert.ToDecimal(dr["Cost_Current"]);
                    dr["Cost_CurrentRate"] = "0";
                    if (Cost_CurrentAll != 0)
                    {
                        dr["Cost_CurrentRate"] = (Cost_Current * 100 / Cost_CurrentAll).ToString("0.00");
                    }
                    decimal Cost_FinalTotal = Convert.ToDecimal(dr["Cost_FinalTotal"]);
                    dr["Cost_CurrentRateTotal"] = "0";
                    if (Cost_FinalTotalAll != 0)
                    {
                        dr["Cost_CurrentRateTotal"] = (Cost_FinalTotal * 100 / Cost_FinalTotalAll).ToString("0.00");
                    }

                    decimal Profit_Current = Convert.ToDecimal(dr["Profit_Current"]);
                    dr["Profit_CurrentRate"] = "0";
                    if (Profit_CurrentAll != 0)
                    {
                        dr["Profit_CurrentRate"] = (Profit_Current * 100 / Profit_CurrentAll).ToString("0.00");
                    }
                    decimal Profit_FinalTotal = Convert.ToDecimal(dr["Profit_FinalTotal"]);
                    dr["Profit_CurrentRateTotal"] = "0";
                    if (Profit_FinalTotalAll != 0)
                    {
                        dr["Profit_CurrentRateTotal"] = (Profit_FinalTotal * 100 / Profit_FinalTotalAll).ToString("0.00");
                    }
                }

                Dictionary<string, object> dicTotal = new Dictionary<string, object>();
                //导出excel必须列一致
                dicTotal.SetValue(GroupFieldType.BusinessType.ToString(), 0);
                dicTotal.SetValue(GroupFieldType.Area.ToString(), 0);

                dicTotal.SetValue(groupFieldStr, "总计");
                dicTotal.SetValue("Contract_Current", Contract_CurrentAll);
                dicTotal.SetValue("Contract_FinalTotal", Contract_FinalTotalAll);
                dicTotal.SetValue("Contract_CurrentLastYear", 0);//导出excel必须列一致
                dicTotal.SetValue("Contract_FinalTotalLastYear", 0);//导出excel必须列一致
                dicTotal.SetValue("Contract_CurrentYoY", "0");
                dicTotal.SetValue("Contract_CurrentYoYTotal", "0");
                dicTotal.SetValue("Contract_CurrentRate", "0");
                dicTotal.SetValue("Contract_CurrentRateTotal", "0");
                dicTotal.SetValue("Income_Current", Income_CurrentAll);
                dicTotal.SetValue("Income_FinalTotal", Income_FinalTotalAll);
                dicTotal.SetValue("Income_CurrentLastYear", 0);
                dicTotal.SetValue("Income_FinalTotalLastYear", 0);
                dicTotal.SetValue("Income_CurrentYoY", "0");
                dicTotal.SetValue("Income_CurrentYoYTotal", "0");
                dicTotal.SetValue("Income_CurrentRate", "0");
                dicTotal.SetValue("Income_CurrentRateTotal", "0");
                dicTotal.SetValue("Invoice_Current", Invoice_CurrentAll);
                dicTotal.SetValue("Invoice_FinalTotal", Invoice_FinalTotalAll);
                dicTotal.SetValue("Invoice_CurrentLastYear", 0);
                dicTotal.SetValue("Invoice_FinalTotalLastYear", 0);
                dicTotal.SetValue("Invoice_CurrentYoY", "0");
                dicTotal.SetValue("Invoice_CurrentYoYTotal", "0");
                dicTotal.SetValue("Invoice_CurrentRate", "0");
                dicTotal.SetValue("Invoice_CurrentRateTotal", "0");
                dicTotal.SetValue("Receipt_Current", Receipt_CurrentAll);
                dicTotal.SetValue("Receipt_FinalTotal", Receipt_FinalTotalAll);
                dicTotal.SetValue("Receipt_CurrentLastYear", 0);
                dicTotal.SetValue("Receipt_FinalTotalLastYear", 0);
                dicTotal.SetValue("Receipt_CurrentYoY", "0");
                dicTotal.SetValue("Receipt_CurrentYoYTotal", "0");
                dicTotal.SetValue("Receipt_CurrentRate", "0");
                dicTotal.SetValue("Receipt_CurrentRateTotal", "0");
                dicTotal.SetValue("Cost_Current", Cost_CurrentAll);
                dicTotal.SetValue("Cost_FinalTotal", Cost_FinalTotalAll);
                dicTotal.SetValue("Cost_CurrentLastYear", 0);
                dicTotal.SetValue("Cost_FinalTotalLastYear", 0);
                dicTotal.SetValue("Cost_CurrentYoY", "0");
                dicTotal.SetValue("Cost_CurrentYoYTotal", "0");
                dicTotal.SetValue("Cost_CurrentRate", "0");
                dicTotal.SetValue("Cost_CurrentRateTotal", "0");
                dicTotal.SetValue("Profit_Current", Profit_CurrentAll);
                dicTotal.SetValue("Profit_FinalTotal", Profit_FinalTotalAll);
                dicTotal.SetValue("Profit_CurrentLastYear", 0);
                dicTotal.SetValue("Profit_FinalTotalLastYear", 0);
                dicTotal.SetValue("Profit_CurrentYoY", "0");
                dicTotal.SetValue("Profit_CurrentYoYTotal", "0");
                dicTotal.SetValue("Profit_CurrentRate", "0");
                dicTotal.SetValue("Profit_CurrentRateTotal", "0");
                resList.Add(dicTotal);
            }

            return Json(new { data = resList, total = resList.Count });
        }

        private void fillTheAmount(string groupFieldStr, string currentDateFilter, string lastDateFilter, string currentDateTotalFilter, string lastDateTotalFilter, string sql, string code, string pre, Dictionary<string, object> groupRes)
        {
            //本期
            string currentSql = string.Format("select sum(isnull(dd.Amount,0)) from({1}) dd where {0}='{2}' and {3}  group by {0}", groupFieldStr, sql, code, currentDateFilter);
            var currentDBRes = this.SQLDB.ExecuteScalar(currentSql);
            decimal current = 0;
            if (currentDBRes != null)
            {
                decimal.TryParse(currentDBRes.ToString(), out current);
            }
            groupRes.SetValue(pre + "_Current", current);

            //期末
            string finalTotalSql = string.Format("select sum(isnull(dd.Amount,0)) from({1}) dd where {0}='{2}' and {3}  group by {0}", groupFieldStr, sql, code, currentDateTotalFilter);
            var finalTotalDBRes = this.SQLDB.ExecuteScalar(finalTotalSql);
            decimal finalTotal = 0;
            if (finalTotalDBRes != null)
            {
                decimal.TryParse(finalTotalDBRes.ToString(), out finalTotal);
            }
            groupRes.SetValue(pre + "_FinalTotal", finalTotal);


            //上一期值
            string lastYearCurrentSql = string.Format("select sum(isnull(dd.Amount,0)) from({1}) dd where {0}='{2}' and {3}  group by {0}", groupFieldStr, sql, code, lastDateFilter);
            var lastYearCurrentDBRes = this.SQLDB.ExecuteScalar(lastYearCurrentSql);
            decimal lastYearCurrent = 0;
            if (lastYearCurrentDBRes != null)
            {
                decimal.TryParse(lastYearCurrentDBRes.ToString(), out lastYearCurrent);
            }
            groupRes.SetValue(pre + "_CurrentLastYear", lastYearCurrent);

            //上一期期末累计值
            string lastYearFinalTotalSql = string.Format("select sum(isnull(dd.Amount,0)) from({1}) dd where {0}='{2}' and {3}  group by {0}", groupFieldStr, sql, code, lastDateTotalFilter);
            var lastYearFinalTotalDBRes = this.SQLDB.ExecuteScalar(lastYearFinalTotalSql);
            decimal lastYearFinalTotal = 0;
            if (lastYearFinalTotalDBRes != null)
            {
                decimal.TryParse(lastYearFinalTotalDBRes.ToString(), out lastYearFinalTotal);
            }
            groupRes.SetValue(pre + "_FinalTotalLastYear", lastYearFinalTotal);

            //本期同比
            groupRes.SetValue(pre + "_CurrentYoY", "0");
            if (lastYearCurrent != 0)
            {
                groupRes.SetValue(pre + "_CurrentYoY", (100 * (current - lastYearCurrent) / lastYearCurrent).ToString("0.00"));
            }

            //累计同比
            groupRes.SetValue(pre + "_CurrentYoYTotal", "0");
            if (lastYearFinalTotal != 0)
            {
                groupRes.SetValue(pre + "_CurrentYoYTotal", (100 * (finalTotal - lastYearFinalTotal) / lastYearFinalTotal).ToString("0.00"));
            }

            groupRes.SetValue(pre + "_CurrentRate", "0");//本期占比
            groupRes.SetValue(pre + "_CurrentRateTotal", "0");//累计占比
        }



        private IEnumerable<string> GetEnumCodeList(string groupFieldTypeStr)
        {
            List<string> resList = new List<string>();
            if (groupFieldTypeStr == GroupFieldType.BusinessType.ToString())
            {
                resList = EnumBaseHelper.GetEnumDef("Market.BusinessType").EnumItem.Select(a => a.Code).ToList();
            }
            else if (groupFieldTypeStr == GroupFieldType.Area.ToString())
            {
                resList = EnumBaseHelper.GetEnumDef("System.Area").EnumItem.Select(a => a.Code).ToList();
            }
            else
            {
                throw new BusinessException("请选择统计维度");
            }

            return resList;
        }

        #endregion

    }
}
