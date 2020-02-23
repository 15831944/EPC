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
    public class IndicatorCompanyController : MarketController<S_KPITemp_IndicatorCompany>
    {
        #region  公司年度指标
        public ActionResult IndicatorCompanyYear()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Company);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Company);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public ActionResult YearHistory()
        {
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Company);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetYearList(string Year)
        {
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var year = DateTime.Now.Year;
            var dt = new DataTable();
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var Version = this.Request["Version"];
            if (string.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Company);
                Version = fo.GetTmpMaxVersion(year, IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Company).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Company, Convert.ToInt32(Version));
            if (dt.Rows.Count == 0)
            {
                //根据业务类型生成默认的数据
                var category = fo.GetIndicatorCategory(IndicatorOrgType.Company);
                foreach (var item in category)
                {
                    var row = dt.NewRow();
                    row["BusiniessCategory"] = item.Code;
                    dt.Rows.Add(row);
                }
                if (dt.Rows.Count == 0)
                {
                    var row = dt.NewRow(); 
                    dt.Rows.Add(row);
                }
            }
            var result = fo.ValidateCurrentVersion(year, Convert.ToInt32(Version), IndicatorType.YearIndicator.ToString(), IndicatorOrgType.Company);
            var jsondata = new { data = dt, Flag = result };
            return Json(jsondata);
        }

        public JsonResult SaveData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.YearIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult Upgrade(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.YearIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult Publish(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var list = JsonHelper.ToList(ListData);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.YearIndicator, IndicatorOrgType.Company);
            return Json("");
        }
        #endregion

        #region 公司季度指标
        public ActionResult IndicatorCompanyQuarter()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Company);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Company);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
          
        }

        public ActionResult QuarterHistory()
        {
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Company);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetQuarterList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var dt = new DataTable();
            var Version = this.Request["Version"];
            if (String.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Company);
                Version = fo.GetTmpMaxVersion(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Company).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Company, Convert.ToInt32(Version));
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
            var result = fo.ValidateCurrentVersion(year, fo.GetTmpMaxVersion(year, IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Company),
                IndicatorType.QuarterIndicator.ToString(), IndicatorOrgType.Company);
            var jsondata = new { data = dt, Flag = result};
            return Json(jsondata);
        }

        public JsonResult SaveQuarterData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.QuarterIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult PublishQuarterData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.QuarterIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult UpgradeQuarter(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.QuarterIndicator, IndicatorOrgType.Company);
            return Json("");
        }
        #endregion

        #region 公司月度指标
        public ActionResult IndicatorCompanyMonth()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 2, false, "年度");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;

            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = fo.GetIndicatorDefine(IndicatorOrgType.Company);
            ViewBag.IndicatorDefines = list;

            var category = fo.GetIndicatorCategory(IndicatorOrgType.Company);
            ViewBag.Categories = category;
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public ActionResult MonthHistory()
        {
            var state = YesOrNo.Yes.ToString();
            var list = S_KPI_IndicatorConfig.GetCurrentVersionIndicatorConfig(IndicatorOrgType.Company);
            var writeFiels = string.Empty;
            foreach (var item in list)
                writeFiels += item.IndicatorCode + ",";
            ViewBag.IndicatorConfig = list;
            ViewBag.WriteFields = writeFiels.TrimEnd(',');
            return View();
        }

        public JsonResult GetMonthList(string Year)
        {
            var year = DateTime.Now.Year;
            if (!String.IsNullOrEmpty(Year)) year = Convert.ToInt32(Year);
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var dt = new DataTable();
            var Version = this.Request["Version"];
            if (String.IsNullOrEmpty(Version))
            {
                dt = fo.GetTmpMaxVersionTable(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Company);
                Version = fo.GetTmpMaxVersion(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Company).ToString();
            }
            else
                dt = fo.GetTmpVersionTable(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Company, Convert.ToInt32(Version));
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

            var result = fo.ValidateCurrentVersion(year, fo.GetTmpMaxVersion(year, IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Company),
              IndicatorType.MonthIndicator.ToString(), IndicatorOrgType.Company);
            var jsondata = new { data = dt, Flag = result};
            return Json(jsondata);
        }

        public JsonResult SaveMonthData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.SaveIndicator(Convert.ToInt32(Year), list, IndicatorType.MonthIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult PublishMonthData(string Year, string ListData)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            var list = JsonHelper.ToList(ListData);
            fo.PublishIndicator(Convert.ToInt32(Year), list, IndicatorType.MonthIndicator, IndicatorOrgType.Company);
            return Json("");
        }

        public JsonResult UpgradeMonth(string Year)
        {
            if (String.IsNullOrEmpty(Year)) throw new Formula.Exceptions.BusinessException("请选择年份");
            var fo = FormulaHelper.CreateFO<IndicatorFO>();
            fo.UpgradeIndicator(Convert.ToInt32(Year), IndicatorType.MonthIndicator, IndicatorOrgType.Company);
            return Json("");
        }
        #endregion

        #region 公用方法
        public JsonResult GetVersionList(QueryBuilder qb)
        {
            var year = this.Request["Year"];
            var indicatorType = this.Request["IndicatorType"];
            string sql = @"select distinct BelongYear, Version,CurrentVersion,Max(CreateDate) as CreateDate   from S_KPITemp_IndicatorCompany 
where BelongYear='{0}' and IndicatorType='{1}' group by BelongYear, Version,CurrentVersion";
            var data = this.SqlHelper.ExecuteGridData(string.Format(sql, year, indicatorType), qb);
            return Json(data);
        }

        //public ActionResult ValidateData(string ListData, string Year, string IndicatorType)
        //{
        //    var msg = S_KPITemp_IndicatorCompany.ValidateSaveOrPublish(ListData, Year, IndicatorType);
        //    var str = new { Msg = msg };
        //    return Json(str);
        //}
        #endregion
    }
}
