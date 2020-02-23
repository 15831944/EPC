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
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
namespace Market.Areas.Basic.Controllers
{
    public class IndicatorPersonController : MarketController<S_KPITemp_IndicatorPerson>
    {
        #region 个人年度指标

        public ActionResult IndicatorPersonYear()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Person);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Person);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetYearList(string Year, QueryBuilder qb)
        {
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var Version = this.Request["Version"];
            var dt = new DataTable();
            if (string.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, "", "", IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Person, qb.GetWhereString(false));
                Version = fo.GetTmpMaxVersion(year, IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Person).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, "", "", IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Person, Convert.ToInt32(Version), qb.GetWhereString(false));
            var result = fo.ValidateCurrentVersion(year, Convert.ToInt32(Version), IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Person);
            var jsondata = new { data = dt, Flag = result };
            return Json(jsondata);
        }

        public JsonResult SaveYearData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.YearIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult PublishYearData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var list = JsonHelper.ToList(ListData);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.YearIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult UpgradeYearData(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.YearIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public ActionResult YearHistory()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Person);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');


            //业务字段
            //var categoryList = S_KPI_Category.GetCurrentVersionCategories(IndicatorOrgType.Person);
            //var categoryFields = "";
            //foreach (var category in categoryList)
            //    categoryFields += category.Code + ",";
            //categoryFields = categoryFields.TrimEnd(',');
            //ViewBag.categoryFields = categoryFields;
            //ViewBag.CategoryList = categoryList;

            return View();
        }
        #endregion

        #region   个人季度指标
        public ActionResult IndicatorPersonQuarter()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            var quarterCategory = CategoryFactory.GetQuarterCategory("BelongQuarter");
            quarterCategory.SetDefaultItem();
            quarterCategory.Multi = true;
            tab.Categories.Add(quarterCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Person);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Person);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public ActionResult QuarterHistory()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Person);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetQuarterList(string Year, string Quarter, QueryBuilder qb)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var dt = new DataTable();
            var Version = this.Request["Version"];
            if (String.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, Quarter, "", IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Person, qb.GetWhereString(false));
                Version = fo.GetTmpMaxVersion(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Person).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, Quarter, "", IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Person, Convert.ToInt32(Version), qb.GetWhereString(false));
            dt.Columns.Add("EditFlag");
            foreach (DataRow item in dt.Rows)
            {
                var belongMonth = Convert.ToInt32(item["BelongQuarter"]) * 3;
                var date = new DateTime(Convert.ToInt32(item["BelongYear"]), belongMonth, 1);
                date = date.AddMonths(1).AddDays(-1);
                if (date < DateTime.Now)
                    item["EditFlag"] = false;
                else
                    item["EditFlag"] = true;
            }
            var result = fo.ValidateCurrentVersion(year, fo.GetTmpMaxVersion(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Person),
              IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Person);
            var jsondata = new { data = dt, Flag = result };
            return Json(jsondata);
        }

        public JsonResult SaveQuarterData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.QuarterIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult PublishQuarterData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var list = JsonHelper.ToList(ListData);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.QuarterIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult UpgradeQuarterData(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.QuarterIndicator, IndicatorOrgType.Person);
            return Json("");
        }
        #endregion

        #region   个人月度指标
        public ActionResult IndicatorPersonMonth()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var MonthCategory = CategoryFactory.GetMonthCategory("BelongMonth");
            MonthCategory.SetDefaultItem();
            MonthCategory.Multi = true;
            tab.Categories.Add(MonthCategory);

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Org);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Org);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public ActionResult MonthHistory()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Person);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetMonthList(string Year, string Month, QueryBuilder qb)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var dt = new DataTable();
            var Version = this.Request["Version"];
            if (String.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, "", Month, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Person, qb.GetWhereString(false));
                Version = fo.GetTmpMaxVersion(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Person).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, "", Month, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Person, Convert.ToInt32(Version), qb.GetWhereString(false));
            dt.Columns.Add("EditFlag");
            foreach (DataRow item in dt.Rows)
            {
                var belongMonth = Convert.ToInt32(item["BelongMonth"]);
                var date = new DateTime(Convert.ToInt32(item["BelongYear"]), belongMonth, 1);
                date = date.AddMonths(1).AddDays(-1);
                if (date < DateTime.Now)
                    item["EditFlag"] = false;
                else
                    item["EditFlag"] = true;
            }
            var result = fo.ValidateCurrentVersion(year, fo.GetTmpMaxVersion(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Person),
              IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Person);
            var jsondata = new { data = dt, Flag = result };
            return Json(jsondata);

        }

        public JsonResult SaveMonthData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.MonthIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult PublishMonthData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var list = JsonHelper.ToList(ListData);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.MonthIndicator, IndicatorOrgType.Person);
            return Json("");
        }

        public JsonResult UpgradeMonthData(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.MonthIndicator, IndicatorOrgType.Person);
            return Json("");
        }
        #endregion

        #region 公用方法
        //获取历史版本
        public JsonResult GetVersionList(string belongYear, string IndicatorType, QueryBuilder qb)
        {
            if (string.IsNullOrEmpty(belongYear))
                return null;
            string sql = @" select distinct BelongYear, Version,CurrentVersion,Max(CreateDate) as CreateDate  from S_KPITemp_IndicatorPerson 
                           where BelongYear='{0}' and IndicatorType='{1}'
                           group by BelongYear, Version,CurrentVersion";
            sql = string.Format(sql, belongYear, IndicatorType);

            GridData grid = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(grid);
        }

        #endregion
    }
}
