using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Config.Logic;
using Config;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using MvcAdapter;
using Formula;
using Expenses.Logic.BusinessFacade;


namespace Expenses.Areas.InCome.Controllers
{
    public class RevenueRecognitionController : ExpensesFormController
    {
        public ActionResult List()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 5, 1, false, "年份", "BelongYear");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            var resultDt = new DataTable();

            var LastMonthCount = Convert.ToInt32(SysParams.Params.GetValue("IncomeLastMonthCount"));
            var DefaultMonthCount = Convert.ToInt32(SysParams.Params.GetValue("IncomeDefaultMonthCount"));
            var removeValidate = SysParams.Params.GetValue("IncomeRemoveValidateMode");
            var adjustValidate = SysParams.Params.GetValue("IncomeRemoveValidateMode");
            var createValidate = SysParams.Params.GetValue("IncomeRemoveValidateMode");

            string sql = @"SELECT 'false' as CanRemove,'false' as CanCreate,
case when S_EP_RevenueInfo.State='Finish' then 'true' else 'false' end as CanAdjust, 
S_EP_RevenueInfo.*,'true' as HasReven,isnull(AdjustCount,0) as ChangeCount,MaxAdjustID,
S_EP_RevenueAdjustInfo.CreateUser as LastAdjustUser,
S_EP_RevenueAdjustInfo.CreateDate as LastAdjustDate
 FROM S_EP_RevenueInfo
left join (select count(ID) as AdjustCount,RevenueInfoID from S_EP_RevenueAdjustInfo
group by RevenueInfoID) AdjustInfo
on S_EP_RevenueInfo.ID=AdjustInfo.RevenueInfoID 
left join (select Max(ID) as MaxAdjustID,RevenueInfoID from S_EP_RevenueAdjustInfo
group by RevenueInfoID) MaxRevenueAdjust on MaxRevenueAdjust.RevenueInfoID=S_EP_RevenueInfo.ID
left join S_EP_RevenueAdjustInfo on S_EP_RevenueAdjustInfo.ID=MaxRevenueAdjust.MaxAdjustID where State<>'Removed'";
            resultDt = this.MarketSQLDB.ExecuteDataTable(sql, qb);
            var item = qb.Items.FirstOrDefault(c => c.Field == "BelongYear");
            var list = resultDt.AsEnumerable().ToList();
            var miniDateTime = DateTime.Now.AddMonths(0 - DefaultMonthCount);
            var maxDatetTime = DateTime.Now.AddMonths(LastMonthCount);
            for (DateTime i = maxDatetTime; i > miniDateTime; i = i.AddMonths(-1))
            {
                var year = i.Year;
                var month = i.Month;
                if (item != null && item.Value != null)
                {
                    if (year != Convert.ToInt32(item.Value))
                    {
                        continue;
                    }
                }
                if (list.Count(c => Convert.ToInt32(c["BelongYear"]) == year && Convert.ToInt32(c["BelongMonth"]) == month) > 0)
                {
                    var row = list.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == year && Convert.ToInt32(c["BelongMonth"]) == month);



                    continue;
                }
                else
                {
                    var row = resultDt.NewRow();
                    row["Name"] = i.Year + "年" + i.Month + "月确认收入";
                    row["BelongYear"] = i.Year;
                    row["BelongMonth"] = i.Month;
                    row["SumIncomeValue"] = 0;
                    row["HasReven"] = "false";
                    row["ChangeCount"] = 0;

                    if (SysParams.Params.GetValue("IncomeRemoveValidateMode") == IncomeValidateMode.OnlyAfter.ToString())
                    {

                    }

                    resultDt.Rows.Add(row);
                }
            }
            var resultRows = resultDt.AsEnumerable().OrderByDescending(c => c["BelongYear"]).ThenByDescending(c => c["BelongMonth"]).ToList();
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in resultRows)
            {
                result.Add(FormulaHelper.DataRowToDic(row));
            }
            return Json(result);
        }

        public JsonResult SubmitRevenueInfo(string BelongYear, string BelongMonth)
        {
            if (String.IsNullOrEmpty(BelongYear)) throw new Formula.Exceptions.BusinessValidationException("收入计算时必须指定年份");
            if (String.IsNullOrEmpty(BelongMonth)) throw new Formula.Exceptions.BusinessValidationException("收入计算时必须指定月份");
            string sql = "SELECT TOP 1 * FROM S_EP_RevenueInfo WHERE BelongYear={0} AND BelongMonth={1} and State<>'Removed'";

            var fo = FormulaHelper.CreateFO<IncomeFo>();
            fo.ValidateIncome(Convert.ToInt32(BelongYear), Convert.ToInt32(BelongMonth));

            var dt = this.MarketSQLDB.ExecuteDataTable(String.Format(sql, BelongYear, BelongMonth));
            var dic = new Dictionary<string, object>();
            if (dt.Rows.Count > 0)
            {
                dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
            }
            return Json(dic);
        }

        public JsonResult ValidateAdjust(string ID)
        {
            var dic = this.GetDataDicByID("S_EP_RevenueInfo", ID);
            if (dic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的数据");
            }

            if (SysParams.Params.GetValue("CloseOperation").ToLower() == CloseOperationEnum.LockCostAndIncome.ToString().ToLower())
            {
                //判定是否关账，如果系统设置为关账后不允许调整收入，那此处就需要强制性校验
                var belongYear = Convert.ToInt32(dic.GetValue("BelongYear"));
                var belongMonth = Convert.ToInt32(dic.GetValue("BelongMonth"));
                var dt = this.MarketSQLDB.ExecuteDataTable(
                String.Format("select top 1 ID,BelongYear,BelongMonth from S_EP_LockAccount where State='{0}' and BelongYear='{1}' and BelongMonth='{2}'",
               "Finish", belongYear, belongMonth));
                if (dt.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经结账，无法调整收入", belongYear, belongMonth));
                }
            }
            if (SysParams.Params.GetValue("ValidateAdjustMode").ToLower() == IncomeAdjustValidateMode.OnlyAfter.ToString().ToLower())
            {
                var belongYear = Convert.ToInt32(dic.GetValue("BelongYear"));
                var belongMonth = Convert.ToInt32(dic.GetValue("BelongMonth"));
                var date = new DateTime(belongYear, belongMonth, 1);
                var dt = this.MarketSQLDB.ExecuteDataTable(
                    String.Format("select top 1 ID,BelongYear,BelongMonth from S_EP_RevenueInfo where State<>'{0}' and IncomeDate>'{1}' order by IncomeDate",
                   "Removed", date.ToString()));
                if (dt.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("已经存在{0}年{1}月的收入确认信息，无法调整收入，请撤销后续收入后再进行调整");
                }
            }
            var adjustDt = this.MarketSQLDB.ExecuteDataTable("SELECT * FROM S_EP_RevenueAdjustInfo WHERE RevenueInfoID='" + ID + "' AND FLOWPHASE<>'End'");
            var result = new Dictionary<string, object>();
            if (adjustDt.Rows.Count > 0)
            {
                result = FormulaHelper.DataRowToDic(adjustDt.Rows[0]);
            }
            return Json(result);
        }

        public JsonResult RemoveRevenueInfo(string ID)
        {
            var revDic = this.GetDataDicByID("S_EP_RevenueInfo", ID);
            if (revDic != null)
            {
                var revInfo = new S_EP_RevenueInfo(revDic);
                var detailDt = this.MarketSQLDB.ExecuteDataTable("select * from S_EP_RevenueInfo_Detail where S_EP_RevenueInfoID = '" + ID + "'");
                foreach (DataRow dr in detailDt.Rows)
                {
                    DataInterfaceFo.ValidateDataSyn("S_EP_RevenueInfo_Detail", dr["ID"].ToString());
                }
                revInfo.Remove();
            }
            return Json("");
        }

        bool validateRemove(string mode, DataRow row, List<DataRow> list)
        {
            var result = true;
            if (row["IncomeDate"] == DBNull.Value || row["IncomeDate"] == null) return false;
            if (mode == IncomeValidateMode.OnlyAfter.ToString())
            {
                //后月份校验，暨当所需撤销的收入之后的月份有收入时，禁止删除
                if (list.Exists(c => c["IncomeDate"] != DBNull.Value && Convert.ToDateTime(c["IncomeDate"]) > Convert.ToDateTime(row["IncomeDate"])))
                {
                    result = false;
                }
            }
            else if (mode == IncomeValidateMode.BeforeAndAfter.ToString())
            {
                if (list.Exists(c => c["IncomeDate"] != DBNull.Value
                    &&
                    (Convert.ToDateTime(c["IncomeDate"]) > Convert.ToDateTime(row["IncomeDate"]) ||
                    Convert.ToDateTime(c["IncomeDate"]) < Convert.ToDateTime(row["IncomeDate"]))))
                {
                    result = false;
                }
            }
            else if (mode == IncomeValidateMode.OnlyBefore.ToString())
            {
                if (list.Exists(c => c["IncomeDate"] != DBNull.Value && Convert.ToDateTime(c["IncomeDate"]) < Convert.ToDateTime(row["IncomeDate"])))
                {
                    result = false;
                }
            }
            return result;
        }

        bool validateAdjust(string mode, DataRow row, List<DataRow> list)
        {
            var result = false;
            return result;
        }

        bool validateCreate(string mode, DataRow row, List<DataRow> list)
        {
            var result = false;

            return result;
        }
    }
}
