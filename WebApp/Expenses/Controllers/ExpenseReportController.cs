using Expenses;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config;
using Config.Logic;
using Expenses.Logic;
using Formula;
using MvcAdapter;
using Formula.Exceptions;

namespace Expenses.Controllers
{
    public class ExpenseReportController : ExpensesController
    {
        #region 主营业务收入

        public ActionResult IncomeAnalysisView()
        {
            return View();
        }


        public JsonResult GetIncomeAnalysisList()
        {
            string queryData = this.Request["QueryData"];

            var endYear = DateTime.Now.Year;
            var lastYear = 5;
            var startYear = DateTime.Now.Year - 5 + 1;
            string xAxisType = XAxisType.Month.ToString();
            string analysisType = MonthAnalysisType.Month.ToString();

            string sql = @"select Sum({1}) as Value,BelongYear,BelongMonth from {0}
where BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' {2} group by BelongYear,BelongMonth";

            if (!String.IsNullOrEmpty(queryData))
            {
                var query = JsonHelper.ToObject(queryData);
                if (!String.IsNullOrEmpty(query.GetValue("LastYear")))
                    lastYear = Convert.ToInt32(query.GetValue("LastYear"));
                if (!String.IsNullOrEmpty(query.GetValue("EndYear")))
                    endYear = DateTime.Now.Year;
                startYear = DateTime.Now.Year - lastYear + 1;
                if (endYear < startYear) throw new Formula.Exceptions.BusinessException("结束年份不能小于开始年份");
                if (!String.IsNullOrEmpty(query.GetValue("XAxisType")))
                    xAxisType = query.GetValue("XAxisType");
                if (!String.IsNullOrEmpty(query.GetValue("AnalysisType")))
                    analysisType = query.GetValue("AnalysisType");
            }
            var dt = CreateTable(startYear, endYear);
            sql = String.Format(sql, "S_EP_RevenueInfo", "SumIncomeValue", "and State = 'Finish'");

            var data = this.SQLDB.ExecuteDataTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                var year = Convert.ToInt32(item["Year"].ToString());
                var total = 0M;
                for (int j = 1; j <= 12; j++)
                {
                    var row = data.Select("BelongYear='" + year + "' and BelongMonth='" + j + "' ").FirstOrDefault();
                    //统计类型单月和累计统计值的数据源拼接逻辑不通
                    if (analysisType == MonthAnalysisType.Month.ToString())
                    {
                        //这句的意思如果当前未来肯定不应该有数值
                        //if (year == DateTime.Now.Year && j > DateTime.Now.Month) { item[j + "Month"] = DBNull.Value; continue; }
                        if (row == null || row["Value"] == null || row["Value"] == DBNull.Value) continue;
                        item[j + "Month"] = row["Value"];
                        total += Convert.ToDecimal(row["Value"]);
                    }
                    else
                    {
                        var value = 0M;
                        if (year == DateTime.Now.Year && j > DateTime.Now.Month)
                        {
                            item[j + "Month"] = DBNull.Value;
                        }
                        else
                        {
                            if (row != null && row["Value"] != null && row["Value"] != DBNull.Value)
                                value = Convert.ToDecimal(row["Value"]);
                            if (j == 1)
                                item[j + "Month"] = value;
                            else
                            {
                                var prevalue = Convert.ToDecimal(item[(j - 1) + "Month"]);
                                value = prevalue + value;
                                item[j + "Month"] = value;
                            }
                            total = value;
                        }
                    }
                }
                item["Total"] = total;
            }
            #region 计算同比增长
            var rateRow = dt.NewRow();
            rateRow["Year"] = "同比增长率";
            var currentYearRow = dt.Select("Year='" + DateTime.Now.Year + "'").FirstOrDefault();
            var lastYearRow = dt.Select("Year='" + (DateTime.Now.Year - 1) + "'").FirstOrDefault();
            for (int i = 1; i <= 12; i++)
            {
                if (i > DateTime.Now.Month)
                {
                    rateRow[i + "Month"] = DBNull.Value; continue;
                }
                var currentValue = currentYearRow[i + "Month"] == DBNull.Value ? 0 : Convert.ToDecimal(currentYearRow[i + "Month"]);
                var lastValue = lastYearRow[i + "Month"] == DBNull.Value ? 0 : Convert.ToDecimal(lastYearRow[i + "Month"]);
                if (currentValue == 0 && lastValue > 0)
                {
                    rateRow[i + "Month"] = -100;
                }
                else if (lastValue == 0 && currentValue > 0)
                {
                    rateRow[i + "Month"] = 100;
                }
                else if (lastValue == 0 && currentValue == 0)
                {
                    rateRow[i + "Month"] = 0;
                }
                else
                {
                    rateRow[i + "Month"] = Math.Round((currentValue - lastValue) / lastValue * 100, 2);
                }
            }

            dt.Rows.Add(rateRow);
            #endregion
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var chartData = this.GetIncomeChartData(xAxisType, analysisType, startYear, endYear);
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            chartData.SetValue("credits", credits);
            result.SetValue("chartData", chartData);
            return Json(result);
        }

        private Dictionary<string, object> GetIncomeChartData(string xAxisType, string analysisType, int startYear, int endYear)
        {
            string series = string.Empty;
            string serieFields = string.Empty;
            string sql = @"select Sum(SumIncomeValue) as Value,BelongYear {0} from S_EP_RevenueInfo
where State='Finish' and BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' group by BelongYear {0}";

            #region 按月为X轴统计图表
            if (xAxisType == XAxisType.Month.ToString())
            {
                sql = String.Format(sql, ",BelongMonth");
                var dt = this.SQLDB.ExecuteDataTable(sql);
                var dataSource = new DataTable();
                dataSource.Columns.Add("Month", typeof(string));
                for (int i = startYear; i <= endYear; i++)
                {
                    series += i.ToString() + "年,";
                    serieFields += i.ToString() + ",";
                    dataSource.Columns.Add(i.ToString(), typeof(decimal));
                }
                for (int i = 1; i <= 12; i++)
                {
                    var row = dataSource.NewRow();
                    row["Month"] = i + "月";
                    for (int j = startYear; j <= endYear; j++)
                    {
                        var data = dt.Select("BelongMonth='" + i + "' and BelongYear='" + j + "'").FirstOrDefault();
                        if (analysisType == MonthAnalysisType.Month.ToString())
                        {
                            if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                                row[j.ToString()] = 0;
                            else
                                row[j.ToString()] = data["Value"];
                        }
                        else
                        {
                            var value = 0M;
                            if (j == DateTime.Now.Year && i > DateTime.Now.Month)
                            {
                                row[j.ToString()] = value;
                            }
                            else
                            {
                                if (data != null && data["Value"] != null && data["Value"] != DBNull.Value)
                                    value = Convert.ToDecimal(data["Value"]);
                                if (i == 1)
                                    row[j.ToString()] = value;
                                else
                                {
                                    var preDataRow = dataSource.Select("Month='" + (i - 1) + "月'").FirstOrDefault();
                                    var preValue = 0M;
                                    if (preDataRow != null)
                                    {
                                        preValue = Convert.ToDecimal(preDataRow[j.ToString()]);
                                        value += preValue;
                                    }
                                }
                            }
                            row[j.ToString()] = value;
                        }
                    }
                    dataSource.Rows.Add(row);
                }
                series = series.TrimEnd(',');
                serieFields = serieFields.TrimEnd(',');
                var columChart = HighChartHelper.CreateColumnChart("", dataSource, "Month", series.Split(','), serieFields.Split(','));
                return columChart.Render();
            }
            #endregion
            #region 按年统计图表
            else
            {
                sql = String.Format(sql, "");
                var dt = this.SQLDB.ExecuteDataTable(sql);
                var dataSource = new DataTable();
                dataSource.Columns.Add("Value", typeof(decimal));
                dataSource.Columns.Add("Year", typeof(string));
                for (int i = startYear; i <= endYear; i++)
                {
                    var row = dataSource.NewRow();
                    row["Year"] = i;
                    var data = dt.Select(" BelongYear='" + i + "' ").FirstOrDefault();
                    //单月统计
                    if (analysisType == YearAnalysisType.Year.ToString())
                    {
                        if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                            row["Value"] = 0;
                        else
                            row["Value"] = data["Value"];
                        dataSource.Rows.Add(row);
                    }
                    else //累计统计
                    {
                        var value = 0M;
                        if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                            value = 0;
                        else
                            value = Convert.ToDecimal(data["Value"]);
                        if (i == startYear)
                            row["Value"] = value;
                        else
                        {
                            var preRow = dataSource.Select("Year='" + (i - 1) + "'").FirstOrDefault();
                            var preValue = 0M;
                            if (preRow != null && preRow["Value"] != null && preRow["Value"] != DBNull.Value)
                                preValue = Convert.ToDecimal(preRow["Value"]);
                            row["Value"] = preValue + value;
                        }
                        dataSource.Rows.Add(row);
                    }
                }
                //series =EnumBaseHelper.GetEnumDescription(typeof(AnlysisValue), anlysisValue);
                serieFields = "Value";
                var columChart = HighChartHelper.CreateColumnChart("", dataSource, "Year", series.Split(','), serieFields.Split(','));
                return columChart.Render();
            }
            #endregion
        }
        #endregion

        #region 主营业务成本
        public ActionResult CostAnalysisView()
        {
            return View();
        }


        public JsonResult GetCostAnalysisList()
        {
            string queryData = this.Request["QueryData"];

            var endYear = DateTime.Now.Year;
            var lastYear = 5;
            var startYear = DateTime.Now.Year - 5 + 1;
            string xAxisType = XAxisType.Month.ToString();
            string analysisType = YearAnalysisType.Year.ToString();

            string sql = @"select Sum({1}) as Value,BelongYear,BelongMonth from {0}
where BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' {2} group by BelongYear,BelongMonth";

            if (!String.IsNullOrEmpty(queryData))
            {
                var query = JsonHelper.ToObject(queryData);
                if (!String.IsNullOrEmpty(query.GetValue("LastYear")))
                    lastYear = Convert.ToInt32(query.GetValue("LastYear"));
                if (!String.IsNullOrEmpty(query.GetValue("EndYear")))
                    endYear = DateTime.Now.Year;
                startYear = DateTime.Now.Year - lastYear + 1;
                if (endYear < startYear) throw new Formula.Exceptions.BusinessException("结束年份不能小于开始年份");
                if (!String.IsNullOrEmpty(query.GetValue("XAxisType")))
                    xAxisType = query.GetValue("XAxisType");
                if (!String.IsNullOrEmpty(query.GetValue("AnalysisType")))
                    analysisType = query.GetValue("AnalysisType");
            }
            var dt = CreateTable(startYear, endYear);
            sql = String.Format(sql, "S_EP_CostInfo", "TotalPrice", "and State = 'Finish'");

            var data = this.SQLDB.ExecuteDataTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                var year = Convert.ToInt32(item["Year"].ToString());
                var total = 0M;
                for (int j = 1; j <= 12; j++)
                {
                    var row = data.Select("BelongYear='" + year + "' and BelongMonth='" + j + "' ").FirstOrDefault();
                    //统计类型单月和累计统计值的数据源拼接逻辑不通
                    if (analysisType == YearAnalysisType.Year.ToString())
                    {
                        //这句的意思如果当前未来肯定不应该有数值
                        //if (year == DateTime.Now.Year && j > DateTime.Now.Month) { item[j + "Month"] = DBNull.Value; continue; }
                        if (row == null || row["Value"] == null || row["Value"] == DBNull.Value) continue;
                        item[j + "Month"] = row["Value"];
                        total += Convert.ToDecimal(row["Value"]);
                    }
                    else
                    {
                        var value = 0M;
                        if (year == DateTime.Now.Year && j > DateTime.Now.Month)
                        {
                            item[j + "Month"] = DBNull.Value;
                        }
                        else
                        {
                            if (row != null && row["Value"] != null && row["Value"] != DBNull.Value)
                                value = Convert.ToDecimal(row["Value"]);
                            if (j == 1)
                                item[j + "Month"] = value;
                            else
                            {
                                var prevalue = Convert.ToDecimal(item[(j - 1) + "Month"]);
                                value = prevalue + value;
                                item[j + "Month"] = value;
                            }
                            total = value;
                        }
                    }
                }
                item["Total"] = total;
            }
            #region 计算同比增长
            var rateRow = dt.NewRow();
            rateRow["Year"] = "同比增长率";
            var currentYearRow = dt.Select("Year='" + DateTime.Now.Year + "'").FirstOrDefault();
            var lastYearRow = dt.Select("Year='" + (DateTime.Now.Year - 1) + "'").FirstOrDefault();
            for (int i = 1; i <= 12; i++)
            {
                if (i > DateTime.Now.Month)
                {
                    rateRow[i + "Month"] = DBNull.Value; continue;
                }
                var currentValue = currentYearRow[i + "Month"] == DBNull.Value ? 0 : Convert.ToDecimal(currentYearRow[i + "Month"]);
                var lastValue = lastYearRow[i + "Month"] == DBNull.Value ? 0 : Convert.ToDecimal(lastYearRow[i + "Month"]);
                if (currentValue == 0 && lastValue > 0)
                {
                    rateRow[i + "Month"] = -100;
                }
                else if (lastValue == 0 && currentValue > 0)
                {
                    rateRow[i + "Month"] = 100;
                }
                else if (lastValue == 0 && currentValue == 0)
                {
                    rateRow[i + "Month"] = 0;
                }
                else
                {
                    rateRow[i + "Month"] = Math.Round((currentValue - lastValue) / lastValue * 100, 2);
                }
            }

            dt.Rows.Add(rateRow);
            #endregion
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var chartData = this.GetCostChartData(xAxisType, analysisType, startYear, endYear);
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            chartData.SetValue("credits", credits);
            result.SetValue("chartData", chartData);
            return Json(result);
        }

        private Dictionary<string, object> GetCostChartData(string xAxisType, string analysisType, int startYear, int endYear)
        {
            string topSql = @"select Sum(TotalPrice) as Value,BelongYear,CostType from S_EP_CostInfo
where State='Finish' and BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' group by BelongYear, CostType";

            #region 按年统计图表
            var dicList = FormulaHelper.GetService<IEnumService>().GetEnumDataSource("Expenses.CostType");
            var dataSource = new DataTable();
            dataSource.Columns.Add("Year", typeof(string));
            foreach (var dic in dicList)
            {
                dataSource.Columns.Add(dic.Value, typeof(decimal));
            }
            string series = string.Join(",", dicList.Select(a => a.Text));
            string serieFields = string.Join(",", dicList.Select(a => a.Value));

            string sql = String.Format(topSql, "");
            var dt = this.SQLDB.ExecuteDataTable(sql);
            for (int i = startYear; i <= endYear; i++)
            {
                var row = dataSource.NewRow();
                dataSource.Rows.Add(row);
                row["Year"] = i;
                foreach (var dic in dicList)
                {
                    var data = dt.Select(" BelongYear='" + i + "' and CostType = '" + dic.Value + "'").FirstOrDefault();
                    //单年统计
                    if (analysisType == YearAnalysisType.Year.ToString())
                    {
                        if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                            row[dic.Value] = 0;
                        else
                            row[dic.Value] = data["Value"];
                    }
                    else //累计统计
                    {
                        var value = 0M;
                        if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                            value = 0;
                        else
                            value = Convert.ToDecimal(data["Value"]);
                        if (i == startYear)
                            row[dic.Value] = value;
                        else
                        {
                            var preRow = dataSource.Select("Year='" + (i - 1) + "'").FirstOrDefault();
                            var preValue = 0M;
                            if (preRow != null && preRow[dic.Value] != null && preRow[dic.Value] != DBNull.Value)
                                preValue = Convert.ToDecimal(preRow[dic.Value]);
                            row[dic.Value] = preValue + value;
                        }
                    }
                }
            }

            var columChart = HighChartHelper.CreateColumnChart("", dataSource, "Year", series.Split(','), serieFields.Split(','));
            return columChart.Render();
            #endregion
        }
        #endregion

        #region 部门汇总统计
        public ActionResult DeptAnalysisView()
        {
            return View();
        }

        public JsonResult GetDeptAnalysisList()
        {
            string analysisType = GetQueryString("AnalysisType");
            if (string.IsNullOrEmpty(analysisType))
                analysisType = AnalysisType.Month.ToString();

            string sql = @"select '" + analysisType + @"' as DateType, mainTB.ID, mainTB.Name, tmp4.ChargerDept, isnull(tmp4.IncomeValue,0) IncomeValue,isnull(tmp4.CostValue,0) CostValue, isnull(tmp1.UserCount,0) UserCount,isnull(tmp2.ProjectCount,0) ProjectCount,isnull(tmp3.Amount,0) as ReceiptValue,
                           case when isnull(tmp1.UserCount,0) = 0 then 0 else tmp4.IncomeValue/isnull(tmp1.UserCount,0) end PersonIncomeValue,
                           case when isnull(tmp1.UserCount,0) = 0 then 0 else tmp3.Amount/isnull(tmp1.UserCount,0) end PersonReceiptValue,
                           case when isnull(tmp4.CostValue,0) = 0 then 0 else tmp3.Amount*100/isnull(tmp4.CostValue,0) end IncomeBiCost
                           from EPM_Base..S_A_Org mainTB
                           left join (select count(UserID) UserCount,EPM_Base..S_A__OrgUser.OrgID from EPM_Base..S_A__OrgUser group by EPM_Base..S_A__OrgUser.OrgID) tmp1 on tmp1.OrgID = mainTB.ID
                           left join (select count(ID) ProjectCount, Chargerdept from S_I_Project group by Chargerdept) tmp2 on tmp2.Chargerdept = mainTB.ID
                           left join (select sum(S_C_Receipt.Amount) Amount,S_C_Receipt.ProductUnit from S_C_Receipt
                           where 1=1 {0} group by S_C_Receipt.ProductUnit) tmp3 on tmp3.ProductUnit = mainTB.ID
                           left join (
                           select S_EP_CBSNode.ChargerDept,
                           sum(isnull(costInfo.PartCostValue,0)) CostValue,
                           sum(isnull(incomeInfo.PartIncomeValue,0)) IncomeValue
                           from S_EP_CBSNode
                           left join S_EP_DefineCBSNode on S_EP_DefineCBSNode.ID = S_EP_CBSNode.DefineID 
                           left join (select sum(TotalPrice) as PartCostValue, CBSNodeID from S_EP_CostInfo where State = 'Finish' {0} group by CBSNodeID) costInfo on costInfo.CBSNodeID = S_EP_CBSNode.ID
                           left join (select sum(IncomeValue) as PartIncomeValue,CBSNodeID 
                           from S_EP_RevenueInfo_Detail inner join S_EP_RevenueInfo on S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID where State = 'Finish' {0} group by CBSNodeID) incomeInfo on incomeInfo.CBSNodeID = S_EP_CBSNode.ID
                           where AnalysisUnit = 'true' group by ChargerDept) tmp4 on tmp4.ChargerDept =  mainTB.ID
                           where mainTB.Type = 'ManufactureDept'";

            string filterSql = "";
            DateTime now = DateTime.Now;
            if (analysisType == AnalysisType.Month.ToString())
            {
                filterSql = " and BelongYear = " + now.Year + " and BelongMonth = " + now.Month;
            }
            else if (analysisType == AnalysisType.Year.ToString())
            {
                filterSql = " and BelongYear = " + now.Year;
            }

            var dt = this.SQLDB.ExecuteDataTable(string.Format(sql, filterSql));
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var chartData = this.GetDeptChartData(analysisType);
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            chartData.SetValue("credits", credits);
            result.SetValue("chartData", chartData);
            return Json(result);
        }

        private Dictionary<string, object> GetDeptChartData(string analysisType)
        {
            string deptSql = @"select ID, Name from EPM_Base..S_A_Org where Type in ('ManufactureDept')";//没有加入Company
            var deptDT = this.SQLDB.ExecuteDataTable(deptSql);
            var deptDicList = FormulaHelper.DataTableToListDic(deptDT);

            string filterSql = "";
            DateTime now = DateTime.Now;
            if (analysisType == AnalysisType.Month.ToString())
            {
                filterSql = " and BelongYear = " + now.Year + " and BelongMonth = " + now.Month;
            }
            else if (analysisType == AnalysisType.Year.ToString())
            {
                filterSql = " and BelongYear = " + now.Year;
            }

            //收入
            string incomeSql = @"select sum(S_EP_RevenueInfo_Detail.IncomeValue) as Value,ChargerDept
                           from S_EP_RevenueInfo inner join S_EP_RevenueInfo_Detail on S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID
                           where State = 'Finish' {0} group by ChargerDept";

            var incomeDT = this.SQLDB.ExecuteDataTable(string.Format(incomeSql, filterSql));

            //成本
            string costSql = @"select sum(TotalPrice) as Value,BelongDept
                           from S_EP_CostInfo
                           where State = 'Finish' {0} group by BelongDept";

            var costDT = this.SQLDB.ExecuteDataTable(string.Format(costSql, filterSql));

            //收款
            string receiptSql = @"select sum(ttmp.Amount) as Value,ttmp.ProductionDept
                           from (select S_C_Receipt.*,S_C_ManageContract.ProductionDept from S_C_Receipt inner join S_C_ManageContract on S_C_ManageContract.ID = S_C_Receipt.ContractInfoID) ttmp 
                           where 1=1 {0}  group by ttmp.ProductionDept";

            var receiptDT = this.SQLDB.ExecuteDataTable(string.Format(receiptSql, filterSql));


            var dicList = DeptAnalysisType;

            var dataSource = new DataTable();
            dataSource.Columns.Add("Dept", typeof(string));
            foreach (var dic in dicList)
            {
                dataSource.Columns.Add(dic.Value, typeof(decimal));
            }

            string series = string.Join(",", dicList.Select(a => a.Text));
            string serieFields = string.Join(",", dicList.Select(a => a.Value));


            foreach (var deptDic in deptDicList)
            {
                var row = dataSource.NewRow();
                dataSource.Rows.Add(row);
                row["Dept"] = deptDic.GetValue("Name");
                foreach (var dic in dicList)
                {
                    DataRow data = null;
                    if (dic.Value == "Income")
                    {
                        data = incomeDT.Select("ChargerDept = '" + deptDic.GetValue("ID") + "'").FirstOrDefault();
                    }
                    else if (dic.Value == "Cost")
                    {
                        data = costDT.Select("BelongDept = '" + deptDic.GetValue("ID") + "'").FirstOrDefault();
                    }
                    else if (dic.Value == "Receipt")
                    {
                        data = receiptDT.Select("ProductionDept = '" + deptDic.GetValue("ID") + "'").FirstOrDefault();
                    }

                    if (data == null || data["Value"] == null || data["Value"] == DBNull.Value)
                        row[dic.Value] = 0;
                    else
                        row[dic.Value] = data["Value"];
                }
            }

            var columChart = HighChartHelper.CreateColumnChart("", dataSource, "Dept", series.Split(','), serieFields.Split(','));
            return columChart.Render();
        }
        #endregion

        #region 部门统计分析
        public ActionResult DepartAnalysisView()
        {
            var idObj = this.SQLDB.ExecuteScalar("select top 1 ID from S_EP_DefineCBSInfo order by SortIndex");
            if (idObj == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有确认账套定义信息");
            }
            var resDT = this.SQLDB.ExecuteDataTable(string.Format("select ID,Name from S_EP_DefineCBSNode where DefineID = '{0}' and AnalysisUnit = 'true'", idObj.ToString()));
            var resDicList = FormulaHelper.DataTableToListDic(resDT).Select(a => new { text = a.GetValue("Name"), value = a.GetValue("ID") });
            ViewBag.CBSNodeDefineList = JsonHelper.ToJson(resDicList);
            if (resDT.Rows.Count > 0)
                ViewBag.DefautCBSNodeDefine = resDT.Rows[0]["ID"];
            return View();
        }

        public JsonResult GetDepartAnalysisList(QueryBuilder qb)
        {
            string belongYearStr = GetQueryString("BelongYear");
            string belongQuarterStr = GetQueryString("BelongQuarter");
            string BelongMonthStr = GetQueryString("BelongMonth");
            string cbsDefineNodeIDStr = GetQueryString("CbsDefineNodeID");

            //本期,上一期,本期末累计,上一期末累计
            string currentDateFilter = "", lastDateFilter = "", currentDateTotalFilter, lastDateTotalFilter;
            if (!string.IsNullOrEmpty(BelongMonthStr))
            {
                currentDateFilter = string.Format("BelongMonth = {0} and BelongYear = {1}", BelongMonthStr, belongYearStr);
                currentDateTotalFilter = string.Format("BelongMonth <= {0} and BelongYear = {1}", BelongMonthStr, belongYearStr);
                lastDateFilter = string.Format("BelongMonth = {0} and BelongYear = ({1} - 1)", BelongMonthStr, belongYearStr);
                lastDateTotalFilter = string.Format("BelongMonth <= {0} and BelongYear = ({1} - 1)", BelongMonthStr, belongYearStr);
            }
            else
            {
                currentDateFilter = string.Format("BelongQuarter = {0} and BelongYear = {1}", belongQuarterStr, belongYearStr);
                currentDateTotalFilter = string.Format("BelongQuarter <= {0} and BelongYear = {1}", belongQuarterStr, belongYearStr);
                lastDateFilter = string.Format("BelongQuarter = {0} and BelongYear = ({1} - 1)", belongQuarterStr, belongYearStr);
                lastDateTotalFilter = string.Format("BelongQuarter <= {0} and BelongYear = ({1} - 1)", belongQuarterStr, belongYearStr);
            }

            string invoiceSql = @"select sum(Amount) Amount, DepartID from(select S_C_Invoice.Amount,S_I_Engineering.MainDept as DepartID,S_C_Invoice.BelongYear,S_C_Invoice.BelongQuarter,S_C_Invoice.BelongMonth
                                  from S_C_Invoice inner join S_C_ManageContract on S_C_Invoice.ContractInfoID = S_C_ManageContract.ID 
                                  inner join S_I_Engineering on S_I_Engineering.ID = S_C_ManageContract.EngineeringInfo) dd
                                  where {0}
                                  group by DepartID";

            string receiptSql = @"select sum(Amount) Amount, DepartID from(select S_C_Receipt.Amount,S_I_Engineering.MainDept as DepartID,S_C_Receipt.BelongYear,S_C_Receipt.BelongQuarter,S_C_Receipt.BelongMonth
                                  from S_C_Receipt inner join S_C_ManageContract on S_C_Receipt.ContractInfoID = S_C_ManageContract.ID 
                                  inner join S_I_Engineering on S_I_Engineering.ID = S_C_ManageContract.EngineeringInfo) dd
                                  where {0}
                                  group by DepartID";

            string contractSql = @"select sum(Amount) Amount, DepartID  from (select S_EP_CBSNode.ContractValue as Amount, S_EP_CBSNode.ChargerDept as DepartID,
                                   S_C_ManageContract.BelongYear,S_C_ManageContract.BelongQuarter,S_C_ManageContract.BelongMonth from S_EP_CBSNode inner join 
                                   S_C_ManageContract on S_C_ManageContract.ID = S_EP_CBSNode.ContractInfoID where S_EP_CBSNode.DefineID = '" + cbsDefineNodeIDStr + @"') dd
                                   where {0}
                                   group by DepartID";

            string costSql = @"select sum(Amount) Amount, DepartID  from (select S_EP_CostInfo.TotalPrice as Amount, S_EP_CBSNode.ChargerDept as DepartID,
                               BelongYear,BelongQuarter,BelongMonth from S_EP_CostInfo inner join 
                               S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostInfo.CBSNodeID where S_EP_CBSNode.DefineID = '" + cbsDefineNodeIDStr + @"') dd
                               where {0}
                               group by DepartID";

            string incomeSql = @"select sum(S_EP_RevenueInfo_Detail.IncomeValue) Amount,S_EP_CBSNode.ChargerDept as DepartID
                                 from S_EP_RevenueInfo_Detail
                                 inner join S_EP_RevenueInfo on S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID
                                 inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_RevenueInfo_Detail.CBSNodeID
                                 where S_EP_RevenueInfo.State = 'Finish' and S_EP_CBSNode.DefineID = '" + cbsDefineNodeIDStr + @"' and {0} group by S_EP_CBSNode.ChargerDept";


            string mainSql = @"select Name , ID,
                                            --合同
                                            --本期值
                                            isnull(contractCurrent.Amount,0) Contract_Current,
                                            --期末累计值
                                            isnull(contractFinalTotal.Amount,0) Contract_FinalTotal,
                                            --上一年当期值
                                            isnull(contractCurrentLastYear.Amount,0) Contract_CurrentLastYear,
                                            --上一年当期期末累计值
                                            isnull(contractFinalTotalLastYear.Amount,0) Invoice_FinalTotalLastYear,
                                            case when isnull(contractCurrentLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(contractCurrent.Amount,0) - isnull(contractCurrentLastYear.Amount,0)) * 100/isnull(contractCurrentLastYear.Amount,0))) end Contract_CurrentYoY,
                                            case when isnull(contractFinalTotalLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(contractFinalTotal.Amount,0) - isnull(contractFinalTotalLastYear.Amount,0)) * 100/isnull(contractFinalTotalLastYear.Amount,0))) end Contract_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),isnull(contractFinalTotal.Amount,0)/isnull(orgUser.UserCount,0))) end Contract_Personal,
                                            --收入
                                            --本期值
                                            isnull(incomeCurrent.Amount,0) Income_Current,
                                            --期末累计值
                                            isnull(incomeFinalTotal.Amount,0) Income_FinalTotal,
                                            --上一年当期值
                                            isnull(incomeCurrentLastYear.Amount,0) Income_CurrentLastYear,
                                            --上一年当期期末累计值
                                            isnull(incomeFinalTotalLastYear.Amount,0) Income_FinalTotalLastYear,
                                            case when isnull(incomeCurrentLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(incomeCurrent.Amount,0) - isnull(incomeCurrentLastYear.Amount,0)) * 100/isnull(incomeCurrentLastYear.Amount,0))) end Income_CurrentYoY,
                                            case when isnull(incomeFinalTotalLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(incomeFinalTotal.Amount,0) - isnull(incomeFinalTotalLastYear.Amount,0)) * 100/isnull(incomeFinalTotalLastYear.Amount,0))) end Income_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),isnull(incomeFinalTotal.Amount,0)/isnull(orgUser.UserCount,0))) end Income_Personal,
                                            --开票
                                            --本期值
                                            isnull(invoiceCurrent.Amount,0) Invoice_Current,
                                            --期末累计值
                                            isnull(invoiceFinalTotal.Amount,0) Invoice_FinalTotal,
                                            --上一年当期值
                                            isnull(invoiceCurrentLastYear.Amount,0) Invoice_CurrentLastYear,
                                            --上一年当期期末累计值
                                            isnull(invoiceFinalTotalLastYear.Amount,0) Invoice_FinalTotalLastYear,
                                            case when isnull(invoiceCurrentLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(invoiceCurrent.Amount,0) - isnull(invoiceCurrentLastYear.Amount,0)) * 100/isnull(invoiceCurrentLastYear.Amount,0))) end Invoice_CurrentYoY,
                                            case when isnull(invoiceFinalTotalLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(invoiceFinalTotal.Amount,0) - isnull(invoiceFinalTotalLastYear.Amount,0)) * 100/isnull(invoiceFinalTotalLastYear.Amount,0))) end Invoice_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),isnull(invoiceFinalTotal.Amount,0)/isnull(orgUser.UserCount,0))) end Invoice_Personal,
                                            --收款
                                            --本期值
                                            isnull(receiptCurrent.Amount,0) Receipt_Current,
                                            --期末累计值
                                            isnull(receiptFinalTotal.Amount,0) Receipt_FinalTotal,
                                            --上一年当期值
                                            isnull(receiptCurrentLastYear.Amount,0) Receipt_CurrentLastYear,
                                            --上一年当期期末累计值
                                            isnull(receiptFinalTotalLastYear.Amount,0) Receipt_FinalTotalLastYear,
                                            case when isnull(receiptCurrentLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(receiptCurrent.Amount,0) - isnull(receiptCurrentLastYear.Amount,0)) * 100/isnull(receiptCurrentLastYear.Amount,0))) end Receipt_CurrentYoY,
                                            case when isnull(receiptFinalTotalLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(receiptFinalTotal.Amount,0) - isnull(receiptFinalTotalLastYear.Amount,0)) * 100/isnull(receiptFinalTotalLastYear.Amount,0))) end Receipt_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),isnull(receiptFinalTotal.Amount,0)/isnull(orgUser.UserCount,0))) end Receipt_Personal,
                                            --成本
                                            --本期值
                                            isnull(costCurrent.Amount,0) Cost_Current,
                                            --期末累计值
                                            isnull(costFinalTotal.Amount,0) Cost_FinalTotal,
                                            --上一年当期值
                                            isnull(costCurrentLastYear.Amount,0) Cost_CurrentLastYear,
                                            --上一年当期期末累计值
                                            isnull(costFinalTotalLastYear.Amount,0) Cost_FinalTotalLastYear,
                                            case when isnull(costCurrentLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(costCurrent.Amount,0) - isnull(costCurrentLastYear.Amount,0)) * 100/isnull(costCurrentLastYear.Amount,0))) end Cost_CurrentYoY,
                                            case when isnull(costFinalTotalLastYear.Amount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(costFinalTotal.Amount,0) - isnull(costFinalTotalLastYear.Amount,0)) * 100/isnull(costFinalTotalLastYear.Amount,0))) end Cost_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),isnull(costFinalTotal.Amount,0)/isnull(orgUser.UserCount,0))) end Cost_Personal,
                                            --利润
                                            --本期值
                                            (isnull(incomeCurrent.Amount,0)- isnull(costCurrent.Amount,0)) Profit_Current,
                                            --期末累计值
                                            (isnull(incomeFinalTotal.Amount,0)- isnull(costFinalTotal.Amount,0)) Profit_FinalTotal,
                                            --上一年当期值
                                            (isnull(incomeCurrentLastYear.Amount,0)- isnull(costCurrentLastYear.Amount,0)) Profit_CurrentLastYear,
                                            --上一年当期期末累计值
                                            (isnull(incomeFinalTotalLastYear.Amount,0)- isnull(costFinalTotalLastYear.Amount,0)) Profit_FinalTotalLastYear,
                                            case when (isnull(incomeCurrentLastYear.Amount,0) - isnull(costCurrentLastYear.Amount,0)) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),((isnull(incomeCurrent.Amount,0) - isnull(costCurrent.Amount,0)) - (isnull(incomeCurrentLastYear.Amount,0) - isnull(costCurrentLastYear.Amount,0))) * 100/(isnull(incomeCurrentLastYear.Amount,0) - isnull(costCurrentLastYear.Amount,0)))) end Profit_CurrentYoY,
                                            case when (isnull(incomeFinalTotalLastYear.Amount,0) - isnull(costFinalTotalLastYear.Amount,0)) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),((isnull(incomeFinalTotal.Amount,0) - isnull(costFinalTotal.Amount,0)) - (isnull(incomeFinalTotalLastYear.Amount,0) - isnull(costFinalTotalLastYear.Amount,0))) * 100/(isnull(incomeFinalTotalLastYear.Amount,0) - isnull(costFinalTotalLastYear.Amount,0)))) end Profit_CurrentYoYTotal,
                                            case when isnull(orgUser.UserCount,0) = 0 then '' 
                                            else Convert(varchar(20),Convert(decimal(18,2),(isnull(incomeFinalTotal.Amount,0)- isnull(costFinalTotal.Amount,0))/isnull(orgUser.UserCount,0))) end Profit_Personal
                                            from EPM_Base..S_A_Org Org 
                                            
                                            left join (" + string.Format(contractSql, currentDateFilter) + @") contractCurrent on contractCurrent.DepartID = Org.ID
                                            left join (" + string.Format(contractSql, currentDateTotalFilter) + @") contractFinalTotal on contractFinalTotal.DepartID = Org.ID
                                            left join (" + string.Format(contractSql, lastDateFilter) + @") contractCurrentLastYear on contractCurrentLastYear.DepartID = Org.ID
                                            left join (" + string.Format(contractSql, lastDateTotalFilter) + @") contractFinalTotalLastYear on contractFinalTotalLastYear.DepartID = Org.ID
                                            
                                            left join (" + string.Format(incomeSql, currentDateFilter) + @") incomeCurrent on incomeCurrent.DepartID = Org.ID
                                            left join (" + string.Format(incomeSql, currentDateTotalFilter) + @") incomeFinalTotal on incomeFinalTotal.DepartID = Org.ID
                                            left join (" + string.Format(incomeSql, lastDateFilter) + @") incomeCurrentLastYear on incomeCurrentLastYear.DepartID = Org.ID
                                            left join (" + string.Format(incomeSql, lastDateTotalFilter) + @") incomeFinalTotalLastYear on incomeFinalTotalLastYear.DepartID = Org.ID
                                            
                                            left join (" + string.Format(invoiceSql, currentDateFilter) + @") invoiceCurrent on invoiceCurrent.DepartID = Org.ID
                                            left join (" + string.Format(invoiceSql, currentDateTotalFilter) + @") invoiceFinalTotal on invoiceFinalTotal.DepartID = Org.ID
                                            left join (" + string.Format(invoiceSql, lastDateFilter) + @") invoiceCurrentLastYear on invoiceCurrentLastYear.DepartID = Org.ID
                                            left join (" + string.Format(invoiceSql, lastDateTotalFilter) + @") invoiceFinalTotalLastYear on invoiceFinalTotalLastYear.DepartID = Org.ID
                                            
                                            left join (" + string.Format(receiptSql, currentDateFilter) + @") receiptCurrent on receiptCurrent.DepartID = Org.ID
                                            left join (" + string.Format(receiptSql, currentDateTotalFilter) + @") receiptFinalTotal on receiptFinalTotal.DepartID = Org.ID
                                            left join (" + string.Format(receiptSql, lastDateFilter) + @") receiptCurrentLastYear on receiptCurrentLastYear.DepartID = Org.ID
                                            left join (" + string.Format(receiptSql, lastDateTotalFilter) + @") receiptFinalTotalLastYear on receiptFinalTotalLastYear.DepartID = Org.ID
                                            
                                            left join (" + string.Format(costSql, currentDateFilter) + @") costCurrent on costCurrent.DepartID = Org.ID
                                            left join (" + string.Format(costSql, currentDateTotalFilter) + @") costFinalTotal on costFinalTotal.DepartID = Org.ID
                                            left join (" + string.Format(costSql, lastDateFilter) + @") costCurrentLastYear on costCurrentLastYear.DepartID = Org.ID
                                            left join (" + string.Format(costSql, lastDateTotalFilter) + @") costFinalTotalLastYear on costFinalTotalLastYear.DepartID = Org.ID
                                            left join (select count(UserID) UserCount,EPM_Base..S_A__OrgUser.OrgID from EPM_Base..S_A__OrgUser group by EPM_Base..S_A__OrgUser.OrgID) orgUser on orgUser.OrgID = Org.ID
                                            where Org.Type='ManufactureDept'
                                            ";

            var resDT = this.SQLDB.ExecuteDataTable(mainSql);
            List<Dictionary<string, object>> resList = FormulaHelper.DataTableToListDic(resDT);

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


                foreach (var dic in resList)
                {
                    decimal Contract_Current = Convert.ToDecimal(dic["Contract_Current"]);
                    dic["Contract_CurrentRate"] = "";
                    if (Contract_CurrentAll != 0)
                    {
                        dic["Contract_CurrentRate"] = (Contract_Current * 100 / Contract_CurrentAll).ToString("0.00");
                    }
                    decimal Contract_FinalTotal = Convert.ToDecimal(dic["Contract_FinalTotal"]);

                    dic["Contract_CurrentRateTotal"] = "";
                    if (Contract_FinalTotalAll != 0)
                    {
                        dic["Contract_CurrentRateTotal"] = (Contract_FinalTotal * 100 / Contract_FinalTotalAll).ToString("0.00");
                    }

                    decimal Income_Current = Convert.ToDecimal(dic["Income_Current"]);
                    dic["Income_CurrentRate"] = "";
                    if (Income_CurrentAll != 0)
                    {
                        dic["Income_CurrentRate"] = (Income_Current * 100 / Income_CurrentAll).ToString("0.00");
                    }
                    decimal Income_FinalTotal = Convert.ToDecimal(dic["Income_FinalTotal"]);
                    dic["Income_CurrentRateTotal"] = "";
                    if (Income_FinalTotalAll != 0)
                    {
                        dic["Income_CurrentRateTotal"] = (Income_FinalTotal * 100 / Income_FinalTotalAll).ToString("0.00");
                    }

                    decimal Invoice_Current = Convert.ToDecimal(dic["Invoice_Current"]);
                    dic["Invoice_CurrentRate"] = "";
                    if (Invoice_CurrentAll != 0)
                    {
                        dic["Invoice_CurrentRate"] = (Invoice_Current * 100 / Invoice_CurrentAll).ToString("0.00");
                    }
                    decimal Invoice_FinalTotal = Convert.ToDecimal(dic["Invoice_FinalTotal"]);
                    dic["Invoice_CurrentRateTotal"] = "";
                    if (Invoice_FinalTotalAll != 0)
                    {
                        dic["Invoice_CurrentRateTotal"] = (Invoice_FinalTotal * 100 / Invoice_FinalTotalAll).ToString("0.00");
                    }

                    decimal Receipt_Current = Convert.ToDecimal(dic["Receipt_Current"]);
                    dic["Receipt_CurrentRate"] = "";
                    if (Receipt_CurrentAll != 0)
                    {
                        dic["Receipt_CurrentRate"] = (Receipt_Current * 100 / Receipt_CurrentAll).ToString("0.00");
                    }
                    decimal Receipt_FinalTotal = Convert.ToDecimal(dic["Receipt_FinalTotal"]);
                    dic["Receipt_CurrentRateTotal"] = "";
                    if (Receipt_FinalTotalAll != 0)
                    {
                        dic["Receipt_CurrentRateTotal"] = (Receipt_FinalTotal * 100 / Receipt_FinalTotalAll).ToString("0.00");
                    }

                    decimal Cost_Current = Convert.ToDecimal(dic["Cost_Current"]);
                    dic["Cost_CurrentRate"] = "";
                    if (Cost_CurrentAll != 0)
                    {
                        dic["Cost_CurrentRate"] = (Cost_Current * 100 / Cost_CurrentAll).ToString("0.00");
                    }
                    decimal Cost_FinalTotal = Convert.ToDecimal(dic["Cost_FinalTotal"]);
                    dic["Cost_CurrentRateTotal"] = "";
                    if (Cost_FinalTotalAll != 0)
                    {
                        dic["Cost_CurrentRateTotal"] = (Cost_FinalTotal * 100 / Cost_FinalTotalAll).ToString("0.00");
                    }

                    decimal Profit_Current = Convert.ToDecimal(dic["Profit_Current"]);
                    dic["Profit_CurrentRate"] = "";
                    if (Profit_CurrentAll != 0)
                    {
                        dic["Profit_CurrentRate"] = (Profit_Current * 100 / Profit_CurrentAll).ToString("0.00");
                    }
                    decimal Profit_FinalTotal = Convert.ToDecimal(dic["Profit_FinalTotal"]);
                    dic["Profit_CurrentRateTotal"] = "";
                    if (Profit_FinalTotalAll != 0)
                    {
                        dic["Profit_CurrentRateTotal"] = (Profit_FinalTotal * 100 / Profit_FinalTotalAll).ToString("0.00");
                    }
                }

                Dictionary<string, object> dicTotal = new Dictionary<string, object>();
                //导出excel必须列一致
                dicTotal.SetValue(GroupFieldType.BusinessType.ToString(), 0);
                dicTotal.SetValue(GroupFieldType.Area.ToString(), 0);

                dicTotal.SetValue("Name", "总计");
                dicTotal.SetValue("Contract_Current", Contract_CurrentAll);
                dicTotal.SetValue("Contract_FinalTotal", Contract_FinalTotalAll);
                dicTotal.SetValue("Contract_CurrentLastYear", 0);//导出excel必须列一致
                dicTotal.SetValue("Contract_FinalTotalLastYear", 0);//导出excel必须列一致
                dicTotal.SetValue("Contract_CurrentYoY", "");
                dicTotal.SetValue("Contract_CurrentYoYTotal", "");
                dicTotal.SetValue("Contract_CurrentRate", "");
                dicTotal.SetValue("Contract_CurrentRateTotal", "");
                dicTotal.SetValue("Income_Current", Income_CurrentAll);
                dicTotal.SetValue("Income_FinalTotal", Income_FinalTotalAll);
                dicTotal.SetValue("Income_CurrentLastYear", 0);
                dicTotal.SetValue("Income_FinalTotalLastYear", 0);
                dicTotal.SetValue("Income_CurrentYoY", "");
                dicTotal.SetValue("Income_CurrentYoYTotal", "");
                dicTotal.SetValue("Income_CurrentRate", "");
                dicTotal.SetValue("Income_CurrentRateTotal", "");
                dicTotal.SetValue("Invoice_Current", Invoice_CurrentAll);
                dicTotal.SetValue("Invoice_FinalTotal", Invoice_FinalTotalAll);
                dicTotal.SetValue("Invoice_CurrentLastYear", 0);
                dicTotal.SetValue("Invoice_FinalTotalLastYear", 0);
                dicTotal.SetValue("Invoice_CurrentYoY", "");
                dicTotal.SetValue("Invoice_CurrentYoYTotal", "");
                dicTotal.SetValue("Invoice_CurrentRate", "");
                dicTotal.SetValue("Invoice_CurrentRateTotal", "");
                dicTotal.SetValue("Receipt_Current", Receipt_CurrentAll);
                dicTotal.SetValue("Receipt_FinalTotal", Receipt_FinalTotalAll);
                dicTotal.SetValue("Receipt_CurrentLastYear", 0);
                dicTotal.SetValue("Receipt_FinalTotalLastYear", 0);
                dicTotal.SetValue("Receipt_CurrentYoY", "");
                dicTotal.SetValue("Receipt_CurrentYoYTotal", "");
                dicTotal.SetValue("Receipt_CurrentRate", "");
                dicTotal.SetValue("Receipt_CurrentRateTotal", "");
                dicTotal.SetValue("Cost_Current", Cost_CurrentAll);
                dicTotal.SetValue("Cost_FinalTotal", Cost_FinalTotalAll);
                dicTotal.SetValue("Cost_CurrentLastYear", 0);
                dicTotal.SetValue("Cost_FinalTotalLastYear", 0);
                dicTotal.SetValue("Cost_CurrentYoY", "");
                dicTotal.SetValue("Cost_CurrentYoYTotal", "");
                dicTotal.SetValue("Cost_CurrentRate", "");
                dicTotal.SetValue("Cost_CurrentRateTotal", "");
                dicTotal.SetValue("Profit_Current", Profit_CurrentAll);
                dicTotal.SetValue("Profit_FinalTotal", Profit_FinalTotalAll);
                dicTotal.SetValue("Profit_CurrentLastYear", 0);
                dicTotal.SetValue("Profit_FinalTotalLastYear", 0);
                dicTotal.SetValue("Profit_CurrentYoY", "");
                dicTotal.SetValue("Profit_CurrentYoYTotal", "");
                dicTotal.SetValue("Profit_CurrentRate", "");
                dicTotal.SetValue("Profit_CurrentRateTotal", "");
                resList.Add(dicTotal);
            }

            return Json(new { data = resList, total = resList.Count });
        }
        #endregion

        public enum AnalysisType
        {
            [System.ComponentModel.Description("本年")]
            Year,
            //[System.ComponentModel.Description("本季")]
            //Quarter,
            [System.ComponentModel.Description("本月")]
            Month
        }

        public enum XAxisType
        {
            [System.ComponentModel.Description("月")]
            Month,
            [System.ComponentModel.Description("年")]
            Year
        }

        public enum YearAnalysisType
        {
            [System.ComponentModel.Description("单年统计")]
            Year,
            [System.ComponentModel.Description("累计统计")]
            Total
        }

        public enum MonthAnalysisType
        {
            [System.ComponentModel.Description("单月统计")]
            Month,
            [System.ComponentModel.Description("累计统计")]
            Total
        }

        public List<dynamic> DeptAnalysisType
        {
            get
            {
                return new List<dynamic>() { 
                 new{Text="收入",Value="Income"}
                 ,new{Text="成本",Value="Cost"}
                 ,new{Text="收款",Value="Receipt"}
                };
            }
        }

        public enum GroupFieldType
        {
            [System.ComponentModel.Description("业务类型")]
            BusinessType,
            [System.ComponentModel.Description("区域")]
            Area
        }

        private DataTable CreateTable(int startYear, int endYear)
        {
            var dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Columns.Add("1Month", typeof(decimal));
            dt.Columns.Add("2Month", typeof(decimal));
            dt.Columns.Add("3Month", typeof(decimal));
            dt.Columns.Add("4Month", typeof(decimal));
            dt.Columns.Add("5Month", typeof(decimal));
            dt.Columns.Add("6Month", typeof(decimal));
            dt.Columns.Add("7Month", typeof(decimal));
            dt.Columns.Add("8Month", typeof(decimal));
            dt.Columns.Add("9Month", typeof(decimal));
            dt.Columns.Add("10Month", typeof(decimal));
            dt.Columns.Add("11Month", typeof(decimal));
            dt.Columns.Add("12Month", typeof(decimal));
            dt.Columns.Add("Total", typeof(decimal));
            for (int i = startYear; i <= endYear; i++)
            {
                var row = dt.NewRow();
                row["Year"] = i.ToString();
                row["1Month"] = 0;
                row["2Month"] = 0;
                row["3Month"] = 0;
                row["4Month"] = 0;
                row["5Month"] = 0;
                row["6Month"] = 0;
                row["7Month"] = 0;
                row["8Month"] = 0;
                row["9Month"] = 0;
                row["10Month"] = 0;
                row["11Month"] = 0;
                row["12Month"] = 0;
                row["Total"] = 0;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }

}
