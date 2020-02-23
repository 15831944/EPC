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
    public class MainChartController : MarketController
    {
        public ActionResult ColmunChartZoom()
        {
            ViewBag.ColumnOption = JsonHelper.ToJson(this.GetColumnChartOption());
            return View();
        }

        public ActionResult XYChartZoom()
        {
            ViewBag.YearOnYearOptionOption = JsonHelper.ToJson(this.GetColumnXYChartOption());
            ViewBag.DefaultStartYear = DateTime.Now.Year - 5;
            ViewBag.DefaultEndYear = DateTime.Now.Year;
            ViewBag.DefaultDeptID = Config.Constant.OrgRootID;
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
            return View();
        }

        public ActionResult PieChartZoom()
        {
            ViewBag.PieOption = JsonHelper.ToJson(this.GetPieChartOption());
            ViewBag.DefaultDeptID = Config.Constant.OrgRootID;
            return View();
        }

        public ActionResult MainChart()
        {
            var indexView = new MarketIndexView();
            ViewBag.IndexView = indexView;
            ViewBag.PieOption = JsonHelper.ToJson(this.GetPieChartOption());
            ViewBag.ColumnOption = JsonHelper.ToJson(this.GetColumnChartOption());
            ViewBag.YearOnYearOptionOption = JsonHelper.ToJson(this.GetColumnXYChartOption());
            ViewBag.DefaultDeptID = Config.Constant.OrgRootID;
            ViewBag.DefaultStartYear = DateTime.Now.Year - 5;
            ViewBag.DefaultEndYear = DateTime.Now.Year;

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
            return View();
        }

        public JsonResult GeXYChart()
        {
            var result = GetColumnXYChartOption();
            return Json(result);
        }

        public JsonResult GetPieChart()
        {
            var result = GetPieChartOption();
            return Json(result);
        }

        #region 饼图
        private Dictionary<string, object> GetPieChartOption()
        {
            var result = this.createPieSql();
            var dt = this.SqlHelper.ExecuteDataTable(result[0]);
            var pieChart = HighChartHelper.CreatePieChart("", result[1], dt);
            return pieChart.Render();
        }

        private string[] createPieSql()
        {
            string queryData = this.Request["QueryData"];
            string DeptID = Config.Constant.OrgRootID;
            var type = PieAnlysisType.CurrentYearReceiptValue;
            string group = GraphicPieGroupType.Industry.ToString();
            var result = new string[3];

            if (!String.IsNullOrEmpty(queryData))
            {
                var query = JsonHelper.ToObject(queryData);
                if (!String.IsNullOrEmpty(query.GetValue("PieDept"))) DeptID = query.GetValue("PieDept");
                if (!String.IsNullOrEmpty(query.GetValue("PieAnlysisValue"))) type = (PieAnlysisType)Enum.Parse(typeof(PieAnlysisType), query.GetValue("PieAnlysisValue"));
                if (!String.IsNullOrEmpty(query.GetValue("PieDept"))) DeptID = query.GetValue("PieDept");
                if (!String.IsNullOrEmpty(query.GetValue("PieDimensionType"))) group = query.GetValue("PieDimensionType");
            }

            //查询当年实际收款
            string receiptSql = @"select top 10 Sum(ReceiptValue) as valueField,{1} as nameField from 
(select Sum(Amount) as ReceiptValue,CustomerID from dbo.S_C_Receipt
where  CustomerID is not null {0} group by CustomerID) ReceiptInfo left join S_F_Customer
on ReceiptInfo.CustomerID = S_F_Customer.ID group by {1}";

            //合同
            string contractSql = @"select top 10 Sum(ContractValue) as valueField,{1} as nameField from (
select Sum(ContractRMBAmount) as ContractValue,PartyAID from S_C_ManageContract
where PartyAID is not null {0} group by PartyAID) TableInfo left join S_F_Customer 
on PartyAID = S_F_Customer.ID group by {1}";

            //合同余额
            string remainContractSql = @"select top 10 Sum(ContractValue) as valueField,{1} as nameField from (
select Sum( isnull(ContractRMBAmount,0)-isnull(SummaryReceiptValue,0)-isnull(SummaryBadDebtValue,0)) 
as ContractValue,PartyAID from S_C_ManageContract where PartyAID is not null {0}
group by PartyAID) TableInfo left join S_F_Customer 
on PartyAID = S_F_Customer.ID group by {1}";

            //应收款
            string recivableSql = @"select top 10 case when Sum(ContractValue)<0 then 0 else Sum(ContractValue) end as valueField,{1} as nameField from (
select Sum(isnull(SummaryInvoiceValue,0)-isnull(SummaryReceiptValue,0)-isnull(SummaryBadDebtValue,0)) 
as ContractValue,PartyAID from S_C_ManageContract
where  PartyAID is not null {0} group by PartyAID) TableInfo
left join S_F_Customer 
on PartyAID = S_F_Customer.ID group by {1}";
            string whereStr = "";
            result[1] = EnumBaseHelper.GetEnumDescription(typeof(PieAnlysisType), type.ToString());
            result[2] = EnumBaseHelper.GetEnumDescription(typeof(PieAnlysisType), type.ToString());
            switch (type)
            {
                default:
                case PieAnlysisType.CurrentYearReceiptValue:
                    whereStr = " and BelongYear='" + DateTime.Now.Year + "' ";
                    if (DeptID != Config.Constant.OrgRootID) whereStr += " and ReceiptMasterUnitID='" + DeptID + "'";
                    result[0] = String.Format(receiptSql, whereStr, group);
                    break;
                case PieAnlysisType.CurrentYearSignContractValue:
                    whereStr = " and  IsSigned = '" + ContractIsSigned.Signed + "' and BelongYear='" + DateTime.Now.Year + "' ";
                    if (DeptID != Config.Constant.OrgRootID) whereStr += " and HeadOfSalesDeptID='" + DeptID + "'";
                    result[0] = String.Format(contractSql, whereStr, group);
                    break;
                case PieAnlysisType.ReceivableValue:
                    whereStr = " and  IsSigned = '" + ContractIsSigned.Signed + "' ";
                    if (DeptID != Config.Constant.OrgRootID) whereStr += " and HeadOfSalesDeptID='" + DeptID + "'";
                    result[0] = String.Format(recivableSql, whereStr, group);
                    break;
                case PieAnlysisType.RemainContractValue:
                    whereStr = " and  IsSigned = '" + ContractIsSigned.Signed + "' ";
                    if (DeptID != Config.Constant.OrgRootID) whereStr += " and HeadOfSalesDeptID='" + DeptID + "'";
                    result[0] = String.Format(remainContractSql, whereStr, group);
                    break;
                case PieAnlysisType.UnSignContractValue:
                    whereStr = " and  (IsSigned != '" + ContractIsSigned.Signed + "' or IsSigned is null) ";
                    if (DeptID != Config.Constant.OrgRootID) whereStr += " and HeadOfSalesDeptID='" + DeptID + "'";
                    result[0] = String.Format(contractSql, whereStr, group);
                    break;
            }

            return result;
        }
        #endregion

        #region 柱状图
        private Dictionary<string, object> GetColumnChartOption()
        {
            var sql = @"select ID,Name from dbo.S_A_Org where  Type='ManufactureDept' order by SortIndex";
            var baseSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var mainDt = baseSqlDB.ExecuteDataTable(sql);

            mainDt.Columns.Add("CurrentYearReceiptValue", typeof(decimal));  //当年实际收款金额
            mainDt.Columns.Add("CurrentYearContractValue", typeof(decimal)); //当年已签合同金额
            mainDt.Columns.Add("ContractValue", typeof(decimal)); //待签合同金额
            mainDt.Columns.Add("RemainContractValue", typeof(decimal)); //合同余额
            mainDt.Columns.Add("ReceivableValue", typeof(decimal)); //应收款

            //当年实际到款
            sql = @"select Sum(Amount) as ReceiptValue,ReceiptMasterUnitID from S_C_Receipt
where BelongYear='" + DateTime.Now.Year + "' group by ReceiptMasterUnitID";
            var currentReceiptDt = this.SqlHelper.ExecuteDataTable(sql);

            //当年已签合同
            sql = @"select Sum(ContractRMBAmount) as ContractValue,ProductionUnitID from dbo.S_C_ManageContract
where BelongYear='" + DateTime.Now.Year + "' and IsSigned = '" + ContractIsSigned.Signed + "' group by ProductionUnitID";
            var currentContractDt = this.SqlHelper.ExecuteDataTable(sql);

            //当前合同余额
            sql = @"select Sum(isnull(ContractRMBAmount,0) - isnull(SummaryReceiptValue,0)- isnull(SummaryBadDebtValue,0)) as RemainContractValue,ProductionUnitID from S_C_ManageContract
where BelongYear='" + DateTime.Now.Year + "' and IsSigned = '" + ContractIsSigned.Signed + "' group by ProductionUnitID";
            var currentRemainContractDt = this.SqlHelper.ExecuteDataTable(sql);

            //当前待签合同金额
            sql = @"select Sum(ContractRMBAmount) as ContractValue,ProductionUnitID from dbo.S_C_ManageContract
where (IsSigned != '" + ContractIsSigned.Signed + "' or IsSigned is null) group by ProductionUnitID";
            var contractDt = this.SqlHelper.ExecuteDataTable(sql);

            //当前应收款
            sql = @"select Sum(isnull(SummaryInvoiceValue,0) - isnull(SummaryReceiptValue,0)) as ReceivableValue,ProductionUnitID from S_C_ManageContract
where BelongYear='" + DateTime.Now.Year + "' and IsSigned = '" + ContractIsSigned.Signed + "' group by ProductionUnitID";
            var receivableDt = this.SqlHelper.ExecuteDataTable(sql);

            foreach (DataRow item in mainDt.Rows)
            {
                item["CurrentYearReceiptValue"] = GetDataSourceValue(currentReceiptDt, "ReceiptValue", "ReceiptMasterUnitID", item["ID"].ToString());
                item["CurrentYearContractValue"] = GetDataSourceValue(currentContractDt, "ContractValue", "ProductionUnitID", item["ID"].ToString());
                item["RemainContractValue"] = GetDataSourceValue(currentRemainContractDt, "RemainContractValue", "ProductionUnitID", item["ID"].ToString());
                item["ContractValue"] = GetDataSourceValue(contractDt, "ContractValue", "ProductionUnitID", item["ID"].ToString());
                item["ReceivableValue"] = GetDataSourceValue(receivableDt, "ReceivableValue", "ProductionUnitID", item["ID"].ToString());
            }

            string series = "当年实际收款金额,当年已签合同金额,待签合同金额,当前合同余额,当前应收款";
            string serieFields = "CurrentYearReceiptValue,CurrentYearContractValue,ContractValue,RemainContractValue,ReceivableValue";
            var columChart = HighChartHelper.CreateColumnChart("", mainDt, "Name", series.Split(','), serieFields.Split(','));
            columChart.Is3D = false;
            columChart.TitleInfo.Text = "";
            return columChart.Render();
        }

        private decimal GetDataSourceValue(DataTable dt, string field, string deptfield, string deptID)
        {
            var result = 0M;
            var row = dt.Select(deptfield + "='" + deptID + "'").FirstOrDefault();
            if (row == null) return result;
            if (row[field] == null || row[field] == DBNull.Value) return result;
            result = Convert.ToDecimal(row[field]);
            return result;
        }


        #endregion

        #region 混合图表
        private Dictionary<string, object> GetColumnXYChartOption()
        {
            string field = "BelongYear";
            string tableName = "S_C_Receipt"; string sumField = "Amount"; string deptField = "ReceiptMasterUnitID";
            string queryData = this.Request["QueryData"];
            string whereStr = "";

            string sql = @"select * from (select 0 as IncreaseRate,Sum({0}) as SumValue,{1} as XAxisName from {2}  where 1=1 {3} group by {1} ) TableInfo order by XAxisName";
            var start = DateTime.Now.Year - 5; var end = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(queryData))
            {
                var query = JsonHelper.ToObject(queryData);
                start = Convert.ToInt32(query.GetValue("BeginYear"));
                end = Convert.ToInt32(query.GetValue("EndYear"));
                if (query.GetValue("DataType") == AnlysisType.ContractAmount.ToString())
                {
                    sumField = "ContractRMBAmount";
                    tableName = "S_C_ManageContract";
                    deptField = "HeadOfSalesDeptID";
                    //添加已签约限制
                    whereStr += " and IsSigned='" + ContractIsSigned.Signed.ToString() + "'";
                }
                if (query.GetValue("DeptID") != Config.Constant.OrgRootID)
                {
                    whereStr += " and " + deptField + "='" + query.GetValue("DeptID") + "'";
                }
                if (query.GetValue("EndYear") == query.GetValue("BeginYear"))
                {
                    field = "BelongMonth";
                    start = Convert.ToInt32(query.GetValue("BeginMonth"));
                    end = Convert.ToInt32(query.GetValue("EndMonth"));
                }

                #region 单独年月

                string beginYear = query.GetValue("BeginYear") == null ? "" : query.GetValue("BeginYear");
                string endYear = query.GetValue("EndYear") == null ? "" : query.GetValue("EndYear");
                string beginMonth = query.GetValue("BeginMonth") == null ? "" : query.GetValue("BeginMonth");
                string endMonth = query.GetValue("EndMonth") == null ? "" : query.GetValue("EndMonth");

                //同年不同月查询
                if (beginYear != "" && endYear != "" && beginYear == endYear)
                {
                    whereStr += string.Format(" and BelongYear = '{2}' and BelongMonth  between '{0}' and '{1}'", beginMonth, endMonth, beginYear);
                }
                //不同年不同月查询，将时间分为三段，一段是起始年月至该年末，一段为查询截至年初至查询截至月
                else if (beginYear != "" && endYear != "")
                {
                    if (string.IsNullOrWhiteSpace(beginMonth))
                        beginMonth = "1";
                    if (string.IsNullOrWhiteSpace(endMonth))
                        endMonth = "12";
                    //endYear-beginYear = 1
                    if (Convert.ToInt32(endYear) - Convert.ToInt32(beginYear) == 1)
                    {
                        whereStr += string.Format(" and ((BelongYear = '{0}' and BelongMonth>='{1}') or (BelongYear = '{2}' and BelongMonth<='{3}'))", beginYear, beginMonth, endYear, endMonth);
                    }
                    //endYear-beginYear > 1
                    else if (Convert.ToInt32(endYear) - Convert.ToInt32(beginYear) > 1)
                    {
                        whereStr += string.Format(" and ((BelongYear = '{0}' and BelongMonth>='{1}') or (BelongYear between '{2}' and '{3}') or (BelongYear = '{4}' and BelongMonth<='{5}'))"
                                               , beginYear, beginMonth, Convert.ToInt32(beginYear) + 1, Convert.ToInt32(endYear) - 1, endYear, endMonth);
                    }

                }

                #endregion

            }
            else
            {
                whereStr = string.Format(whereStr, DateTime.Now.Month, 1, DateTime.Now.Year, DateTime.Now.Year - 5);
            }
            sql = String.Format(sql, sumField, field, tableName, whereStr);

            var dt = GetXYChartData(sql, start, end);
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
                categories[i] = dt.Rows[i]["XAxisName"].ToString();
            xAxis.SetValue("categories", categories);
            xAxisis.Add(xAxis);
            result.SetValue("xAxis", xAxisis);

            var yAxisis = new List<Dictionary<string, object>>();
            yAxisis.Add(this.CreateYAxis("#4572A7", "金额", "{value}", false));
            yAxisis.Add(this.CreateYAxis("#89A54E", "同比增长率", "{value} %", true));
            result.SetValue("yAxis", yAxisis);
            var tooltip = new Dictionary<string, object>();
            tooltip.SetValue("shared", true);
            result.SetValue("tooltip", tooltip);

            var series = new List<Dictionary<string, object>>();
            series.Add(this.CreateSeries(dt, "SumValue", "金额", "column", "元", "#4572A7"));
            series.Add(this.CreateSeries(dt, "IncreaseRate", "同比增长率", "spline", "%", "#89A54E", 1));

            result.SetValue("series", series);
            return result;
        }

        private DataTable GetXYChartData(string sql, int start, int end)
        {
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            var result = this.CreateDefaultXYDt(start, end);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                var row = result.Rows[i];
                var sRow = dt.Select("XAxisName='" + row["XAxisName"] + "'").FirstOrDefault();
                if (sRow == null) continue;
                row["SumValue"] = sRow["SumValue"];
                if (i >= 1)
                {
                    var preRow = result.Rows[i - 1];
                    var value = row["SumValue"] == null || row["SumValue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SumValue"]);
                    var preValue = preRow["SumValue"] == null || preRow["SumValue"] == DBNull.Value ? 0 : Convert.ToDecimal(preRow["SumValue"]);
                    if (preValue == 0)
                    {
                        row["IncreaseRate"] = 100;
                    }
                    else
                    {
                        var rate = Math.Round((value - preValue) / preValue * 100, 2);
                        row["IncreaseRate"] = rate;
                    }
                }
            }
            return result;
        }

        private DataTable CreateDefaultXYDt(int start, int end)
        {
            var result = new DataTable();
            result.Columns.Add("XAxisName");
            result.Columns.Add("SumValue", typeof(decimal));
            result.Columns.Add("IncreaseRate", typeof(decimal));
            for (int i = start; i <= end; i++)
            {
                var row = result.NewRow();
                row["XAxisName"] = i;
                row["IncreaseRate"] = 0;
                row["SumValue"] = 0;
                result.Rows.Add(row);
            }
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
        #endregion
    }
}
