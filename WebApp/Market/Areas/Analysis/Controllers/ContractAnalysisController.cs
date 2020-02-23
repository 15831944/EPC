using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;
using System.Collections;


namespace Market.Areas.Analysis.Controllers
{
    public class ContractAnalysisController : MarketController
    {
        #region 重点合同图标分析
        public ActionResult ContractAnalysisChart()
        {
            ViewBag.StartYear = DateTime.Now.Year;
            List<object> yearEnums = new List<object>();
            for (int i = 0; i < 8; i++)
            {
                var year = DateTime.Now.Year - i;
                yearEnums.Add(new { value = year, text = year });
            }
            ViewBag.YearEnum = JsonHelper.ToJson(yearEnums);
            var monthEnums = new List<object>();
            for (int i = 1; i <= 12; i++)
                monthEnums.Add(new { value = i, text = i });
            ViewBag.MonthEnum = JsonHelper.ToJson(monthEnums);
            ViewBag.DefaultDeptID = Config.Constant.OrgRootID;
            return View();
        }

        public JsonResult GetList()
        {
            string queryData = this.Request["QueryData"];
            var startDate = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
            string analysisValue = ContractAnlysisType.ContractValue.ToString();
            string sql = @" select top 20 * from (select '' as Parent,S_C_ManageContract.ID ContractID,S_C_ManageContract.PartyA ,Name,ProductionDept as DeptID,
ProductionDeptName as DeptName,SignDate,ThisContractRMBAmount as ContractValue,isnull(ReceiptValue,0) as ReceiptValue,
isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0)-isnull(SumBadDebtValue,0) as RemainContractValue,
isnull(SumInvoiceValue,0)-isnull(SumReceiptValue,0)-isnull(SumBadDebtValue,0) as ReceivableValue
from S_C_ManageContract left join (select Sum(Amount) as ReceiptValue,ContractInfoID from dbo.S_C_Receipt
{0} group by ContractInfoID) ReceiptInfo on ReceiptInfo.ContractInfoID = S_C_ManageContract.ID
{1}
union all
select ContractInfoID as Parent,ad.ID as ContractID,con.PartyA,ad.Name,con.ProductionDept,con.ProductionDeptName as DeptName,
ad.SignDate,SupplementaryRMBAmount as ContractValue,0 as ReceiptValue,
0 as RemainContractValue,0 as ReceivableValue from S_C_ManageContract_Supplementary ad
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where IsSigned='Signed'
) TableInfo order by {2} desc";
            string contractWhereStr = " where SignDate is not null and IsSigned='" + ContractIsSigned.Signed + "' ";
            string receiptWhereStr = string.Empty;
            if (!String.IsNullOrEmpty(queryData))
            {
                var query = JsonHelper.ToObject(queryData);
                var deptID = query.GetValue("DeptID");
                var lastYear = String.IsNullOrEmpty(query.GetValue("StartYear")) ? 1 : Convert.ToInt32(query.GetValue("StartYear"));
                startDate = new DateTime(DateTime.Now.Year - lastYear + 1, 1, 1).ToShortDateString();
                if (!String.IsNullOrEmpty(query.GetValue("AnlysisValue")))
                    analysisValue = query.GetValue("AnlysisValue");
                if (analysisValue == ContractAnlysisType.ContractValue.ToString())
                    contractWhereStr += " and SignDate >='" + startDate + "' and SignDate<='" + endDate + "' ";
                else if (analysisValue == ContractAnlysisType.ReceiptValue.ToString())
                {
                    receiptWhereStr += " where ArrivedDate>='" + startDate + "' and ArrivedDate<='" + endDate + "'";
                    contractWhereStr += " and ReceiptValue>0  ";
                }
                if (!String.IsNullOrEmpty(deptID) && deptID != Config.Constant.OrgRootID)
                {
                    contractWhereStr += " and ProductionDept='" + deptID + "'";
                }
            }
            sql = String.Format(sql, receiptWhereStr, contractWhereStr, analysisValue);
            var data = this.SqlHelper.ExecuteDataTable(sql);
            var result = new Dictionary<string, object>();
            result.SetValue("data", data);
            result["chartData"] = createChartOption(data, analysisValue);
            return Json(result);
        }

        private Dictionary<string, object> createChartOption(DataTable dt, string field)
        {
            FillDataSourceWithChart(dt, field);
            var result = new Dictionary<string, object>();
            var chart = new Dictionary<string, object>();
            chart.SetValue("zoomType", "xy");
            result.SetValue("chart", chart);
            var title = new Dictionary<string, object>();
            title.SetValue("text", "");
            result.SetValue("title", title);
            var xAxisis = new List<Dictionary<string, object>>();
            var xAxis = new Dictionary<string, object>();
            var categories = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
                categories[i] = dt.Rows[i]["Name"].ToString();
            xAxis.SetValue("categories", categories);
            xAxisis.Add(xAxis);
            result.SetValue("xAxis", xAxisis);
            result.SetValue("credits", new { enabled = false });
            var yAxisis = new List<Dictionary<string, object>>();
            yAxisis.Add(this.CreateYAxis("#4572A7", EnumBaseHelper.GetEnumDescription(typeof(CustomerAnlysisType), field), "{value}", false));
            yAxisis.Add(this.CreateYAxis("#89A54E", "", "{value} %", true));
            result.SetValue("yAxis", yAxisis);
            var tooltip = new Dictionary<string, object>();
            tooltip.SetValue("shared", true);
            result.SetValue("tooltip", tooltip);

            var series = new List<Dictionary<string, object>>();
            series.Add(this.CreateSeries(dt, field, EnumBaseHelper.GetEnumDescription(typeof(CustomerAnlysisType), field), "column", "元", "#4572A7"));
            series.Add(this.CreateSeries(dt, "Rate", "累计比例", "spline", "%", "#89A54E", 1));
            result.SetValue("series", series);
            return result;
        }

        private Dictionary<string, object> CreateSeries(DataTable dt, string field, string name, string type, string toolTipUnit, string color, int yAxis = 0)
        {
            var result = new Dictionary<string, object>();
            result.SetValue("name", name);
            result.SetValue("color", color);
            result.SetValue("type", type);
            if (yAxis > 0)
                result.SetValue("yAxis", yAxis);
            var tooltip = new Dictionary<string, object>();
            tooltip.SetValue("valueSuffix", toolTipUnit);
            result.SetValue("tooltip", tooltip);
            var data = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data.Add(dt.Rows[i][field]);
            }
            result.SetValue("data", data);
            return result;
        }

        private Dictionary<string, object> CreateYAxis(string color, string text, string format, bool opposite = false)
        {
            var result = new Dictionary<string, object>();
            var yAxisLabels = new Dictionary<string, object>();
            var yAxisLabelsStyle = new Dictionary<string, object>();
            yAxisLabelsStyle.SetValue("color", color);
            yAxisLabelsStyle.SetValue("format", format);
            yAxisLabels.SetValue("style", yAxisLabelsStyle);
            result.SetValue("labels", yAxisLabels);
            var yAxisTitle = new Dictionary<string, object>();
            var yAxisTitleStyle = new Dictionary<string, object>();
            yAxisTitleStyle.SetValue("color", color);
            yAxisTitle.SetValue("text", text);
            yAxisTitle.SetValue("style", yAxisTitleStyle);
            result.SetValue("title", yAxisTitle);
            if (opposite)
                result.SetValue("opposite", opposite);
            return result;
        }

        private void FillDataSourceWithChart(DataTable dt, string field)
        {
            dt.Columns.Add("Rate", typeof(decimal));
            var obj = dt.Compute("Sum(" + field + ")", "");
            var sumValue = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
            var sumRate = 0M;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sumValue == 0)
                {
                    dt.Rows[i]["Rate"] = 0;
                    continue;
                }
                var value = dt.Rows[i][field] == null || dt.Rows[i][field] == DBNull.Value ? 0 : Convert.ToDecimal(dt.Rows[i][field]);
                var currentRate = Math.Round(value / sumValue * 100, 4);
                var rate = Math.Round(value / sumValue * 100, 4) + sumRate;
                sumRate += currentRate;
                dt.Rows[i]["Rate"] = Math.Round(rate, 2);
            }
        }
        #endregion

        #region 合同及收款分析
        public ActionResult ContractAnalysisView()
        {
            ViewBag.StartYear = DateTime.Now.Year - 5;
            List<object> yearEnums = new List<object>();
            for (int i = 0; i < 8; i++)
            {
                var year = DateTime.Now.Year - i;
                yearEnums.Add(new { value = year, text = year });
            }
            ViewBag.YearEnum = JsonHelper.ToJson(yearEnums);
            return View();
        }

        public JsonResult GetAnalysisList()
        {
            string queryData = this.Request["QueryData"];

            var endYear = DateTime.Now.Year;
            var lastYear = 5;
            var startYear = DateTime.Now.Year - 5 + 1;
            string xAxisType = XAxisType.Month.ToString();
            string anlysisValue = AnlysisValue.ReceiptValue.ToString();
            string analysisType = AnalysisType.Month.ToString();

            string sql = @"select Sum({1}) as Value,BelongYear,BelongMonth from {0}
where BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' group by BelongYear,BelongMonth";

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
                if (!String.IsNullOrEmpty(query.GetValue("AnlysisValue")))
                    anlysisValue = query.GetValue("AnlysisValue");
                if (!String.IsNullOrEmpty(query.GetValue("AnalysisType")))
                    analysisType = query.GetValue("AnalysisType");
            }
            var dt = CreateTable(startYear, endYear);

            if (anlysisValue == AnlysisValue.ContractValue.ToString())
            {
                var subsql = string.Format(@"
(select isnull(ThisContractRMBAmount,0) as DataValue,BelongYear,BelongQuarter,BelongMonth
from S_C_ManageContract where IsSigned='{0}'
union all
select isnull(SupplementaryRMBAmount,0) as DataValue,ad.BelongYear,ad.BelongQuarter,ad.BelongMonth
from S_C_ManageContract_Supplementary ad
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where IsSigned='{0}'
)tb", ContractIsSigned.Signed.ToString());
                sql = String.Format(sql, subsql, "DataValue");
            }
            else
                sql = String.Format(sql, "S_C_Receipt", "Amount");

            var data = this.SqlHelper.ExecuteDataTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                var year = Convert.ToInt32(item["Year"].ToString());
                var total = 0M;
                for (int j = 1; j <= 12; j++)
                {
                    var row = data.Select("BelongYear='" + year + "' and BelongMonth='" + j + "' ").FirstOrDefault();
                    //统计类型单月和累计统计值的数据源拼接逻辑不通
                    if (analysisType == AnalysisType.Month.ToString())
                    {
                        if (year == DateTime.Now.Year && j > DateTime.Now.Month) { item[j + "Month"] = DBNull.Value; continue; }
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
            var chartData = this.GetChartData(xAxisType, anlysisValue, analysisType, startYear, endYear);
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            chartData.SetValue("credits", credits);
            result.SetValue("chartData", chartData);
            return Json(result);

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

        private Dictionary<string, object> GetChartData(string xAxisType, string anlysisValue, string analysisType, int startYear, int endYear)
        {
            string series = string.Empty;
            string serieFields = string.Empty;
            string sql = @"select Sum(DataValue) as Value,BelongYear {0} from
(select isnull(ThisContractRMBAmount,0) as DataValue,BelongYear,BelongQuarter,BelongMonth
from S_C_ManageContract where IsSigned='" + ContractIsSigned.Signed + @"'
union all
select isnull(SupplementaryRMBAmount,0) as DataValue,ad.BelongYear,ad.BelongQuarter,ad.BelongMonth
from S_C_ManageContract_Supplementary ad
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where IsSigned='" + ContractIsSigned.Signed + @"'
)tb where BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' group by BelongYear {0}";

            if (anlysisValue == AnlysisValue.ReceiptValue.ToString())
            {
                sql = @"select Sum(Amount) as Value,BelongYear {0} from S_C_Receipt
where BelongYear>='" + startYear + "' and BelongYear<='" + endYear + "' group by BelongYear {0}";
            }

            #region 按月为X轴统计图表
            if (xAxisType == XAxisType.Month.ToString())
            {
                sql = String.Format(sql, ",BelongMonth");
                var dt = this.SqlHelper.ExecuteDataTable(sql);
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
                        if (analysisType == AnalysisType.Month.ToString())
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
                var dt = this.SqlHelper.ExecuteDataTable(sql);
                var dataSource = new DataTable();
                dataSource.Columns.Add("Value", typeof(decimal));
                dataSource.Columns.Add("Year", typeof(string));
                for (int i = startYear; i <= endYear; i++)
                {
                    var row = dataSource.NewRow();
                    row["Year"] = i;
                    var data = dt.Select(" BelongYear='" + i + "' ").FirstOrDefault();
                    //单月统计
                    if (analysisType == AnalysisType.Month.ToString())
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
                series = EnumBaseHelper.GetEnumDescription(typeof(AnlysisValue), anlysisValue);
                serieFields = "Value";
                var columChart = HighChartHelper.CreateColumnChart("", dataSource, "Year", series.Split(','), serieFields.Split(','));
                return columChart.Render();
            }
            #endregion
        }

        #endregion
    }

    public enum XAxisType
    {
        [System.ComponentModel.Description("月")]
        Month,
        [System.ComponentModel.Description("年")]
        Year
    }

    public enum AnlysisValue
    {
        [System.ComponentModel.Description("实际收款")]
        ReceiptValue,
        [System.ComponentModel.Description("合同金额")]
        ContractValue
    }

    public enum AnalysisType
    {
        [System.ComponentModel.Description("单月统计")]
        Month,
        [System.ComponentModel.Description("累计统计")]
        Total
    }
}
