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
    public class BusinessObjectiveQueryController : MarketController
    {
        //
        // GET: /Analysis/BusinessObjectiveQuery/

        public ActionResult YearBusinessObjectiveQuery()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false);
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();

        }
        public JsonResult YearBusinessList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);

            string sql = "select * from S_KPI_IndicatorConfig";
            var dt = SqlHelper.ExecuteDataTable(sql);
            var json = JsonHelper.ToJson(dt);
            var result = new List<Dictionary<string, object>>();
            result = JsonHelper.ToList(json);


            string col = "";
            foreach (var item in result)
            {
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "),0) " + item.GetValue("IndicatorCode") + ",";
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "cost),0) " + item.GetValue("IndicatorCode") + "cost,";
            }
            col = col.TrimEnd(',');
            string IndicatorCompanySql = "select " + col + " from S_KPI_IndicatorCompany where IndicatorType='YearIndicator' ";

            IndicatorCompanySql += " and BelongYear='" + year + "'";
            DataTable IndicatorCompanyDt = SqlHelper.ExecuteDataTable(IndicatorCompanySql);

            string PersonSql = "select " + col + " from S_KPI_IndicatorPerson where IndicatorType='YearIndicator'";

            PersonSql += " and BelongYear='" + year + "'";

            DataTable IndicatorPersonDt = SqlHelper.ExecuteDataTable(PersonSql);

            string LastYearSql = "select " + col + " from S_KPI_IndicatorCompany where IndicatorType='YearIndicator' ";

            LastYearSql += "and BelongYear='" + (year - 1) + "'";
            DataTable LastYearDt = SqlHelper.ExecuteDataTable(LastYearSql);
            var IndicatorCode = string.Empty;
            foreach (var item in result)
            {
                IndicatorCode = item.GetValue("IndicatorCode");
                var AnnualPlan = Convert.ToDouble(IndicatorCompanyDt.Rows[0][IndicatorCode]);
                var CompleteCount = Convert.ToDouble(IndicatorCompanyDt.Rows[0][IndicatorCode + "cost"]);
                var MarketPlan = Convert.ToDouble(IndicatorPersonDt.Rows[0][IndicatorCode]);
                var LastYearCompleteCount = Convert.ToDouble(LastYearDt.Rows[0][IndicatorCode + "cost"]);
                item.SetValue("AnnualPlan", AnnualPlan);//年度计划
                item.SetValue("CompleteCount", CompleteCount);//实际完成
                item.SetValue("MarketPlan", MarketPlan);//市场部年度计划
                var CompletionRate = 0.00;
                if (AnnualPlan != 0 && CompleteCount != 0)
                {
                    CompletionRate = Math.Round(CompleteCount / AnnualPlan * 100, 2);
                }
                item.SetValue("CompletionRate", CompletionRate);//年度计划完成率
                Double MarketCompletionRate = 0;
                if (MarketPlan != 0 && CompleteCount != 0)
                {
                    MarketCompletionRate = Math.Round(CompleteCount / MarketPlan * 100, 2);
                }
                item.SetValue("MarketCompletionRate", MarketCompletionRate);//市场年度计划完成率
                item.SetValue("LastYearCompleteCount", LastYearCompleteCount);//去年同期完成
                Double LastYearRate = 0;
                if ((CompleteCount - LastYearCompleteCount) != 0 && LastYearCompleteCount != 0)
                    LastYearRate = Math.Round((CompleteCount - LastYearCompleteCount) / LastYearCompleteCount * 100, 2);
                item.SetValue("LastYearRate", LastYearRate);//较去年同比增长率
            }
            return Json(result);
        }

        public ActionResult QuarterBusinessObjectiveQuery()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false);
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();

        }

        public JsonResult QuarterBusinessList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);

            string sql = "select * from S_KPI_IndicatorConfig";
            var dt = SqlHelper.ExecuteDataTable(sql);
            var json = JsonHelper.ToJson(dt);
            var result = new List<Dictionary<string, object>>();
            result = JsonHelper.ToList(json);


            string col = "";
            foreach (var item in result)
            {
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "),0) " + item.GetValue("IndicatorCode") + ",";
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "cost),0) " + item.GetValue("IndicatorCode") + "cost,";
            }
            col = col.TrimEnd(',');

            string YearSql = "select " + col + " from S_KPI_IndicatorCompany where IndicatorType='YearIndicator' ";

            YearSql += " and BelongYear='" + year + "'";
            DataTable YearDt = SqlHelper.ExecuteDataTable(YearSql);

            string IndicatorCompanySql = "select " + col + ",belongquarter from S_KPI_IndicatorCompany where IndicatorType='QuarterIndicator'";
            IndicatorCompanySql += " and BelongYear='" + year + "'  group by belongquarter ";
            DataTable IndicatorCompanyDt = SqlHelper.ExecuteDataTable(IndicatorCompanySql);

            string PersonSql = "select " + col + ",belongquarter from S_KPI_IndicatorPerson where IndicatorType='QuarterIndicator' ";
            PersonSql += " and BelongYear='" + year + "' group by belongquarter";
            DataTable IndicatorPersonDt = SqlHelper.ExecuteDataTable(PersonSql);

            string LastYearSql = "select " + col + ",belongquarter from S_KPI_IndicatorCompany where IndicatorType='QuarterIndicator'";
            LastYearSql += "and BelongYear='" + (year - 1) + "'  group by belongquarter ";
            DataTable LastYearDt = SqlHelper.ExecuteDataTable(LastYearSql);
            var IndicatorCode = string.Empty;
            foreach (var item in result)
            {
                IndicatorCode = item.GetValue("IndicatorCode");
                var YearPlan = Convert.ToDouble(YearDt.Rows[0][IndicatorCode]);
                item.SetValue("AnnualPlan", YearPlan);
                for (int i = 1; i < 5; i++)
                {



                    var AnnualPlan = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + ")", "belongquarter=" + i);
                    Double AnnualPlanCount = 0;
                    if (AnnualPlan != null && AnnualPlan != DBNull.Value)
                        AnnualPlanCount = Convert.ToDouble(AnnualPlan);

                    var MarketPlan = IndicatorPersonDt.Compute("sum(" + IndicatorCode + ")", "belongquarter=" + i);

                    var Complete = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + "cost)", "belongquarter=" + i);
                    Double CompleteCount = 0;
                    if (Complete != null && Complete != DBNull.Value)
                        CompleteCount = Convert.ToDouble(Complete);
                    var LastYearComplete = LastYearDt.Compute("sum(" + IndicatorCode + "cost)", "belongquarter=" + i);
                    Double LastYearCompleteCount = 0;
                    if (LastYearComplete != null && LastYearComplete != DBNull.Value)
                        LastYearCompleteCount = Convert.ToDouble(LastYearComplete);

                    item.SetValue("MarketPlan" + i, MarketPlan == DBNull.Value ? 0 : MarketPlan);
                    item.SetValue("AnnualPlan" + i, AnnualPlanCount);
                    item.SetValue("CompleteCount" + i, CompleteCount);

                    var CompletionRate = 0.00;
                    if (AnnualPlanCount != 0 && CompleteCount != 0)
                    {
                        CompletionRate = Math.Round(CompleteCount / AnnualPlanCount * 100, 2);
                    }
                    item.SetValue("CompletionRate" + i, CompletionRate);
                    item.SetValue("LastYearCompleteCount" + i, LastYearCompleteCount);
                    Double LastYearRate = 0;
                    if ((CompleteCount - LastYearCompleteCount) != 0 && LastYearCompleteCount != 0)
                        LastYearRate = Math.Round((CompleteCount - LastYearCompleteCount) / LastYearCompleteCount * 100, 2);
                    item.SetValue("LastYearRate" + i, LastYearRate);
                }
            }
            return Json(result);
        }
        public ActionResult MonthBusinessObjectiveQuery()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false);
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();

        }
        public JsonResult MonthBusinessList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);

            string sql = "select * from S_KPI_IndicatorConfig";
            var dt = SqlHelper.ExecuteDataTable(sql);
            var json = JsonHelper.ToJson(dt);
            var result = new List<Dictionary<string, object>>();
            result = JsonHelper.ToList(json);


            string col = "";
            foreach (var item in result)
            {
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "),0) " + item.GetValue("IndicatorCode") + ",";
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "cost),0) " + item.GetValue("IndicatorCode") + "cost,";
            }
            col = col.TrimEnd(',');

            string YearSql = "select " + col + " from S_KPI_IndicatorCompany where IndicatorType='YearIndicator' ";

            YearSql += " and BelongYear='" + year + "'";
            DataTable YearDt = SqlHelper.ExecuteDataTable(YearSql);

            string IndicatorCompanySql = "select " + col + ",BelongMonth from S_KPI_IndicatorCompany where IndicatorType='MonthIndicator'";
            IndicatorCompanySql += " and BelongYear='" + year + "'  group by BelongMonth ";
            DataTable IndicatorCompanyDt = SqlHelper.ExecuteDataTable(IndicatorCompanySql);

            string PersonSql = "select " + col + ",BelongMonth from S_KPI_IndicatorPerson where IndicatorType='MonthIndicator' ";
            PersonSql += " and BelongYear='" + year + "' group by BelongMonth";
            DataTable IndicatorPersonDt = SqlHelper.ExecuteDataTable(PersonSql);

            string LastYearSql = "select " + col + ",BelongMonth from S_KPI_IndicatorCompany where IndicatorType='MonthIndicator'";
            LastYearSql += "and BelongYear='" + (year - 1) + "'  group by BelongMonth ";
            DataTable LastYearDt = SqlHelper.ExecuteDataTable(LastYearSql);

            string LastYearPersonSql = "select " + col + ",BelongMonth from S_KPI_IndicatorPerson where IndicatorType='MonthIndicator'";
            LastYearPersonSql += "and BelongYear='" + (year - 1) + "'  group by BelongMonth ";
            DataTable LastYearPersonDt = SqlHelper.ExecuteDataTable(LastYearPersonSql);
            var IndicatorCode = string.Empty;
            foreach (var item in result)
            {
                IndicatorCode = item.GetValue("IndicatorCode");
                var YearPlan = Convert.ToDouble(YearDt.Rows[0][IndicatorCode]);
                item.SetValue("AnnualPlan", YearPlan);
                for (int i = 1; i < 13; i++)
                {


                    //计划
                    var AnnualPlan = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + i);
                    Double AnnualPlanCount = 0;
                    if (AnnualPlan != null && AnnualPlan != DBNull.Value)
                        AnnualPlanCount = Convert.ToDouble(AnnualPlan);
                    //市场计划
                    var MarketPlan = IndicatorPersonDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + i);
                    //去年市场计划
                    var ListMarketPlan = LastYearPersonDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + i);
                    //实际完成
                    var Complete = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + "cost)", "BelongMonth=" + i);
                    Double CompleteCount = 0;
                    if (Complete != null && Complete != DBNull.Value)
                        CompleteCount = Convert.ToDouble(Complete);
                    //去年实际完成
                    var LastYearComplete = LastYearDt.Compute("sum(" + IndicatorCode + "cost)", "BelongMonth=" + i);
                    Double LastYearCompleteCount = 0;
                    if (LastYearComplete != null && LastYearComplete != DBNull.Value)
                        LastYearCompleteCount = Convert.ToDouble(LastYearComplete);

                    item.SetValue("MarketPlan" + i, MarketPlan == DBNull.Value ? 0 : MarketPlan);
                    item.SetValue("ListMarketPlan" + i, ListMarketPlan == DBNull.Value ? 0 : ListMarketPlan);
                    item.SetValue("AnnualPlan" + i, AnnualPlanCount);
                    item.SetValue("CompleteCount" + i, CompleteCount);

                    var CompletionRate = 0.00;
                    if (AnnualPlanCount != 0 && CompleteCount != 0)
                    {
                        CompletionRate = Math.Round(CompleteCount / AnnualPlanCount * 100, 2);
                    }
                    item.SetValue("CompletionRate" + i, CompletionRate);
                    //去年完成率
                    item.SetValue("LastYearCompleteCount" + i, LastYearCompleteCount);
                    Double LastYearRate = 0;
                    if ((CompleteCount - LastYearCompleteCount) != 0 && LastYearCompleteCount != 0)
                        LastYearRate = Math.Round((CompleteCount - LastYearCompleteCount) / LastYearCompleteCount * 100, 2);
                    //去年同期增长率
                    item.SetValue("LastYearRate" + i, LastYearRate);
                }
            }


            return Json(result);
        }

        public ActionResult MonthBusinessObjectiveQueryByMonth()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false);
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();

        }
        public JsonResult MonthBusinessByMonthList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);

            string sql = "select '1' as CurrentMonth,* from S_KPI_IndicatorConfig ";
            for (int i = 2; i <= 12; i++)
            {
                sql += @" union all 
                        select '" + i + "' as CurrentMonth,* from S_KPI_IndicatorConfig ";
            }
            var dt = SqlHelper.ExecuteDataTable(sql);
            var json = JsonHelper.ToJson(dt);
            var result = new List<Dictionary<string, object>>();
            result = JsonHelper.ToList(json);


            //字段数据
            sql = "select * from S_KPI_IndicatorConfig";
            var dt_IndicatorConfig = SqlHelper.ExecuteDataTable(sql);
            var json_IndicatorConfig = JsonHelper.ToJson(dt_IndicatorConfig);
            var result_IndicatorConfig = new List<Dictionary<string, object>>();
            result_IndicatorConfig = JsonHelper.ToList(json_IndicatorConfig);

            string col = "";
            foreach (var item in result_IndicatorConfig)
            {
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "),0) " + item.GetValue("IndicatorCode") + ",";
                col += "isnull(sum(" + item.GetValue("IndicatorCode") + "cost),0) " + item.GetValue("IndicatorCode") + "cost,";
            }
            col = col.TrimEnd(',');

            string YearSql = "select " + col + " from S_KPI_IndicatorCompany where IndicatorType='YearIndicator' ";

            YearSql += " and BelongYear='" + year + "'";
            DataTable YearDt = SqlHelper.ExecuteDataTable(YearSql);

            string IndicatorCompanySql = "select " + col + ",BelongMonth from S_KPI_IndicatorCompany where IndicatorType='MonthIndicator'";
            IndicatorCompanySql += " and BelongYear='" + year + "'  group by BelongMonth ";
            DataTable IndicatorCompanyDt = SqlHelper.ExecuteDataTable(IndicatorCompanySql);

            string PersonSql = "select " + col + ",BelongMonth from S_KPI_IndicatorPerson where IndicatorType='MonthIndicator' ";
            PersonSql += " and BelongYear='" + year + "' group by BelongMonth";
            DataTable IndicatorPersonDt = SqlHelper.ExecuteDataTable(PersonSql);

            string LastYearSql = "select " + col + ",BelongMonth from S_KPI_IndicatorCompany where IndicatorType='MonthIndicator'";
            LastYearSql += "and BelongYear='" + (year - 1) + "'  group by BelongMonth ";
            DataTable LastYearDt = SqlHelper.ExecuteDataTable(LastYearSql);

            string LastYearPersonSql = "select " + col + ",BelongMonth from S_KPI_IndicatorPerson where IndicatorType='MonthIndicator'";
            LastYearPersonSql += "and BelongYear='" + (year - 1) + "'  group by BelongMonth ";
            DataTable LastYearPersonDt = SqlHelper.ExecuteDataTable(LastYearPersonSql);
            var IndicatorCode = string.Empty;
            foreach (var item in result)
            {
                IndicatorCode = item.GetValue("IndicatorCode");
                var YearPlan = Convert.ToDouble(YearDt.Rows[0][IndicatorCode]);
                item.SetValue("AnnualPlan", YearPlan);

                //当前月份
                var Month = item.GetValue("CurrentMonth");

                //计划
                var AnnualPlan = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + Month);
                Double AnnualPlanCount = 0;
                if (AnnualPlan != null && AnnualPlan != DBNull.Value)
                    AnnualPlanCount = Convert.ToDouble(AnnualPlan);
                //市场计划
                var MarketPlan = IndicatorPersonDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + Month);
                //去年市场计划
                var ListMarketPlan = LastYearPersonDt.Compute("sum(" + IndicatorCode + ")", "BelongMonth=" + Month);
                //实际完成
                var Complete = IndicatorCompanyDt.Compute("sum(" + IndicatorCode + "cost)", "BelongMonth=" + Month);
                Double CompleteCount = 0;
                if (Complete != null && Complete != DBNull.Value)
                    CompleteCount = Convert.ToDouble(Complete);
                //去年实际完成
                var LastYearComplete = LastYearDt.Compute("sum(" + IndicatorCode + "cost)", "BelongMonth=" + Month);
                Double LastYearCompleteCount = 0;
                if (LastYearComplete != null && LastYearComplete != DBNull.Value)
                    LastYearCompleteCount = Convert.ToDouble(LastYearComplete);

                item.SetValue("MarketPlan", MarketPlan == DBNull.Value ? 0 : MarketPlan);
                item.SetValue("ListMarketPlan", ListMarketPlan == DBNull.Value ? 0 : ListMarketPlan);
                item.SetValue("AnnualPlan", AnnualPlanCount);
                item.SetValue("CompleteCount", CompleteCount);

                var CompletionRate = 0.00;
                if (AnnualPlanCount != 0 && CompleteCount != 0)
                {
                    CompletionRate = Math.Round(CompleteCount / AnnualPlanCount * 100, 2);
                }
                item.SetValue("CompletionRate", CompletionRate);
                //去年完成率
                item.SetValue("LastYearCompleteCount", LastYearCompleteCount);
                Double LastYearRate = 0;
                if ((CompleteCount - LastYearCompleteCount) != 0 && LastYearCompleteCount != 0)
                    LastYearRate = Math.Round((CompleteCount - LastYearCompleteCount) / LastYearCompleteCount * 100, 2);
                //去年同期增长率
                item.SetValue("LastYearRate", LastYearRate);

            }


            return Json(result);
        }

    }
}
