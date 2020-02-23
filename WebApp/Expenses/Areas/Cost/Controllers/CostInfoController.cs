using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Newtonsoft.Json;
using Formula.ImportExport;
using Config;
using Base.Logic.Domain;
using MvcAdapter;

namespace Expenses.Areas.Cost.Controllers
{
    public class CostInfoController : ExpensesFormController<S_EP_CostInfo>
    {
        private readonly List<Dictionary<string, string>> TableFlowPhaseToCheckBelongLock = new List<Dictionary<string, string>>() 
        {
            new Dictionary<string,string>(){ {"TableName","报销申请"},{"Table","S_EP_ReimbursementApply"},{"BelongYear","year(ApplyDate)"},{"BelongMonth","month(ApplyDate)"}  },
            new Dictionary<string,string>(){ {"TableName","进度节点确认"},{"Table","S_EP_ProgressConfirm"},{"BelongYear","year(FactEndDate)"},{"BelongMonth","month(FactEndDate)"}  },
            new Dictionary<string,string>(){ {"TableName","收入节点确认"},{"Table","S_EP_IncomeSubmit"},{"BelongYear","year(CreateDate)"},{"BelongMonth","month(CreateDate)"}  },
            new Dictionary<string,string>(){ {"TableName","部门工时上报"},{"Table","S_EP_WorkHourDepartSubmit"},{"BelongYear","BelongYear"},{"BelongMonth","BelongMonth"}  },
            new Dictionary<string,string>(){ {"TableName","项目工时上报"},{"Table","S_EP_WorkHourSubmit"},{"BelongYear","BelongYear"},{"BelongMonth","BelongMonth"}  },
        };

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            ValidateData(dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"));
        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var sqlCBSInfo = "select * from S_EP_CBSInfo where ID ='{0}' ";
            var cbsDt = SQLDB.ExecuteDataTable(string.Format(sqlCBSInfo, dic["CBSInfoID"]));
            if (cbsDt.Rows.Count > 0)
            {
                var cbsInfo = new S_EP_CBSInfo(FormulaHelper.DataRowToDic(cbsDt.Rows[0]));
                cbsInfo.SummaryCostValue();
            }
        }

        public JsonResult DeleteCostInfo(string listJson)
        {
            var dicList = JsonHelper.ToList(listJson);
            string delSql = "";
            foreach (var dic in dicList)
            {
                var dateTime = Convert.ToDateTime(string.Format("{0}-{1}-01", dic.GetValue("BelongYear"), dic.GetValue("BelongMonth")));
                CostFO.ValidatePeriodIsClosed(dateTime);

                delSql += string.Format(" delete S_EP_CostInfo where ID = '{0}' ", dic.GetValue("ID"));
            }

            this.SQLDB.ExecuteNonQuery(delSql);

            var distinctCBSInfoList = dicList.Select(a => a.GetValue("CBSInfoID")).Distinct();
            var sqlCBSInfo = "select * from S_EP_CBSInfo where ID ='{0}' ";
            foreach (var cbsInfoID in distinctCBSInfoList)
            {
                var cbsDt = SQLDB.ExecuteDataTable(string.Format(sqlCBSInfo, cbsInfoID));
                if (cbsDt.Rows.Count > 0)
                {
                    var cbsInfo = new S_EP_CBSInfo(FormulaHelper.DataRowToDic(cbsDt.Rows[0]));
                    cbsInfo.SummaryCostValue();
                }
            }

            return Json("true");
        }


        #region 关账
        public ActionResult LockList()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetCategory("YearEnum", "年份", "BelongYear");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        public JsonResult GetList(QueryBuilder qb)
        {
            var LastMonthCount = Convert.ToInt32(SysParams.Params.GetValue("CostLastMonthCount"));
            var DefaultMonthCount = Convert.ToInt32(SysParams.Params.GetValue("CostDefaultMonthCount"));

            var sql = @"select 'true' as Closed,Convert(varchar, BelongYear)+'年'+Convert(varchar,BelongMonth)+'月' as Name,
* from S_EP_LockAccount where State='" + IncomeState.Finish.ToString() + "'";
            var resultDt = this.SQLDB.ExecuteDataTable(sql, qb);

            var incomeDt = this.SQLDB.ExecuteDataTable(@"select Sum(IncomeValue) as SumIncomeValue,BelongYear,BelongMonth from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where S_EP_RevenueInfo.State='Finish' group by BelongYear,BelongMonth").AsEnumerable().ToList();

            var costDt = this.SQLDB.ExecuteDataTable(@"select Sum(TotalPrice) as SumCostValue,BelongYear,BelongMonth from S_EP_CostInfo where S_EP_CostInfo.State = 'Finish'
group by BelongYear,BelongMonth").AsEnumerable().ToList();

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
                    continue;
                else
                {
                    var row = resultDt.NewRow();
                    row["Name"] = i.Year + "年" + i.Month + "月";
                    row["BelongYear"] = i.Year;
                    row["BelongMonth"] = i.Month;
                    var costRow = costDt.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == i.Year && Convert.ToInt32(c["BelongMonth"]) == i.Month);
                    row["SumCostValue"] = costRow == null ? 0 : costRow["SumCostValue"];
                    var incomeRow = incomeDt.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == i.Year && Convert.ToInt32(c["BelongMonth"]) == i.Month);
                    row["SumIncomeValue"] = incomeRow == null ? 0 : incomeRow["SumIncomeValue"];
                    row["Closed"] = "false";
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
        public JsonResult LockAccount(string BelongYear, string BelongMonth)
        {
            if (String.IsNullOrEmpty(BelongYear))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定年份");
            }
            if (String.IsNullOrEmpty(BelongMonth))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定月份");
            }

            //上月未关账本月关不了
            string lastMonthSql = @"select count(*) from 
                                  (select year(DATEADD(mm,-1,'{0}-{1}-1')) as BelongYear, month(DATEADD(mm,-1,'{0}-{1}-1')) as BelongMonth ) main
                                  left join S_EP_LockAccount on S_EP_LockAccount.BelongYear = main.BelongYear and S_EP_LockAccount.BelongMonth = main.BelongMonth 
                                  left join 
                                  (select sum(TotalPrice) CostValue, BelongYear, BelongMonth from S_EP_CostInfo
                                  where BelongYear=year(DATEADD(mm,-1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,-1,'{0}-{1}-1'))
                                  group by BelongYear, BelongMonth) CostInfo on CostInfo.BelongYear = main.BelongYear and CostInfo.BelongMonth = main.BelongMonth
                                  left join 
                                  (select sum(IncomeValue) IncomeValue, BelongYear, BelongMonth from S_EP_RevenueInfo inner join S_EP_RevenueInfo_Detail on
                                  S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID
                                  where BelongYear=year(DATEADD(mm,-1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,-1,'{0}-{1}-1'))
                                  group by BelongYear, BelongMonth
                                  ) IncomeInfo on IncomeInfo.BelongYear = main.BelongYear and IncomeInfo.BelongMonth = main.BelongMonth
                                  
                                  where
                                  --大于0时必须关账
                                  ((isnull(CostInfo.CostValue,0) > 0 or isnull(IncomeInfo.IncomeValue,0) > 0) and S_EP_LockAccount.State = 'Finish')
                                   or (isnull(CostInfo.CostValue,0)= 0 and isnull(IncomeInfo.IncomeValue,0) = 0)";

            var lastMonthObj = this.SQLDB.ExecuteScalar(String.Format(lastMonthSql, BelongYear, BelongMonth));
            if ((int)lastMonthObj == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("上个月未关账，本月无法关账", BelongYear, BelongMonth));
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("select * from S_EP_LockAccount where BelongYear={0} and BelongMonth={1} and State != 'Removed'", BelongYear, BelongMonth));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经被锁定了，不能重复锁定", BelongYear, BelongMonth));
            }

            #region 验证是否有进行中的流程,如果存在不能关账
            foreach (var dicTable in TableFlowPhaseToCheckBelongLock)
            {
                if (string.IsNullOrEmpty(dicTable.GetValue("Table"))) continue;

                string sql = string.Format("select count(*) from {0} where {1} = {2} and {3} = {4} and FlowPhase != 'End'"
                                           , dicTable.GetValue("Table"), BelongYear, dicTable.GetValue("BelongYear"), BelongMonth, dicTable.GetValue("BelongMonth"));
                var countObj = this.SQLDB.ExecuteScalar(sql);
                if (countObj != null && countObj != DBNull.Value && (int)countObj > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("本月存在未审批完成的【{0}】流程,无法关账", dicTable.GetValue("TableName")));
                }
            }
            #endregion

            var obj = this.SQLDB.ExecuteScalar(String.Format(@"select isnull(Sum(IncomeValue),0) as SumIncomeValue from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID 
where S_EP_RevenueInfo.State='Finish' and BelongYear={0} and BelongMonth={1}", BelongYear, BelongMonth));

            var incomeValue = obj == DBNull.Value || obj == null ? 0m : Convert.ToDecimal(obj);

            obj = this.SQLDB.ExecuteScalar(String.Format(@"select isnull(Sum(TotalPrice),0) as SumCostValue from S_EP_CostInfo
where BelongYear={0} and BelongMonth={1} and S_EP_CostInfo.State = 'Finish'", BelongYear, BelongMonth));
            var costValue = obj == DBNull.Value || obj == null ? 0m : Convert.ToDecimal(obj);

            var profit = incomeValue - costValue;
            var dic = new Dictionary<string, object>();
            dic.SetValue("BelongYear", BelongYear);
            dic.SetValue("BelongMonth", BelongMonth);
            dic.SetValue("SumIncomeValue", incomeValue);
            dic.SetValue("SumCostValue", costValue);
            dic.SetValue("ProductionValue", profit);
            dic.SetValue("Profit", 0);
            dic.SetValue("CreateUser", this.CurrentUserInfo.UserName);
            dic.SetValue("CreateUserID", this.CurrentUserInfo.UserID);
            dic.SetValue("CreateDate", DateTime.Now);
            dic.SetValue("State", "Finish");
            dic.InsertDB(this.SQLDB, "S_EP_LockAccount");
            return Json("");
        }
        public JsonResult UnLockAccount(string BelongYear, string BelongMonth)
        {
            if (String.IsNullOrEmpty(BelongYear))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定年份");
            }
            if (String.IsNullOrEmpty(BelongMonth))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定月份");
            }

            //下个月未撤销,本月无法撤销
            var lastSql = @"select ID from S_EP_LockAccount where BelongYear=year(DATEADD(mm,1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,1,'{0}-{1}-1'))
                          and State = 'Finish'";
            var lastDt = this.SQLDB.ExecuteDataTable(String.Format(lastSql, BelongYear, BelongMonth));
            if (lastDt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("需先撤销下个月关账", BelongYear, BelongMonth));
            }

            //撤销关账之前需先撤销结账
            var settleDt = this.SQLDB.ExecuteDataTable(String.Format("SELECT ID FROM S_EP_SettleAccount WHERE BELONGYEAR={0} AND BELONGMONTH={1} and State != 'Removed'", BelongYear, BelongMonth));
            if (settleDt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经结账，需先撤销结账", BelongYear, BelongMonth));
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT ID FROM S_EP_LockAccount WHERE BELONGYEAR={0} AND BELONGMONTH={1} and State != 'Removed'", BelongYear, BelongMonth));
            if (dt.Rows.Count > 0)
            {
                this.SQLDB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_LockAccount SET 
State='Removed',RemoveDate='{1}',RemoveUser='{2}',RemoveUserID='{3}' 
where ID='{0}'"
                    , dt.Rows[0]["ID"], DateTime.Now, this.CurrentUserInfo.UserName, this.CurrentUserInfo.UserID));
            }
            else
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月没有被锁定了，无法撤销", BelongYear, BelongMonth));
            }
            return Json("");
        }
        #endregion

        #region 结账
        public ActionResult SettleList()
        {
            var tab = new Tab();
            var yearCategory = CategoryFactory.GetCategory("YearEnum", "年份", "BelongYear");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            yearCategory.Multi = false;
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        public JsonResult GetSettleList(QueryBuilder qb)
        {
            var LastMonthCount = Convert.ToInt32(SysParams.Params.GetValue("CostLastMonthCount"));
            var DefaultMonthCount = Convert.ToInt32(SysParams.Params.GetValue("CostDefaultMonthCount"));

            var sql = @"select 'false' as CanSettle, 'true' as Closed,Convert(varchar, S_EP_SettleAccount.BelongYear)+'年'+Convert(varchar,S_EP_SettleAccount.BelongMonth)+'月' as Name,
                      S_EP_SettleAccount.* from S_EP_SettleAccount
                      where S_EP_SettleAccount.State ='" + IncomeState.Finish.ToString() + "'";
            var resultDt = this.SQLDB.ExecuteDataTable(sql, qb);

            var incomeDt = this.SQLDB.ExecuteDataTable(@"select Sum(IncomeValue) as SumIncomeValue,BelongYear,BelongMonth from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where S_EP_RevenueInfo.State='Finish' group by BelongYear,BelongMonth").AsEnumerable().ToList();

            var costDt = this.SQLDB.ExecuteDataTable(@"select Sum(TotalPrice) as SumCostValue,BelongYear,BelongMonth from S_EP_CostInfo  where S_EP_CostInfo.State = 'Finish'
group by BelongYear,BelongMonth").AsEnumerable().ToList();

            var lockDt = this.SQLDB.ExecuteDataTable("select * from S_EP_LockAccount where State ='" + IncomeState.Finish.ToString() + "'")
                .AsEnumerable().ToList();

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
                    continue;
                else
                {
                    var row = resultDt.NewRow();
                    row["Name"] = i.Year + "年" + i.Month + "月";
                    row["BelongYear"] = i.Year;
                    row["BelongMonth"] = i.Month;
                    var costRow = costDt.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == i.Year && Convert.ToInt32(c["BelongMonth"]) == i.Month);
                    row["SumCostValue"] = costRow == null ? 0 : costRow["SumCostValue"];
                    var incomeRow = incomeDt.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == i.Year && Convert.ToInt32(c["BelongMonth"]) == i.Month);
                    row["SumIncomeValue"] = incomeRow == null ? 0 : incomeRow["SumIncomeValue"];
                    row["Closed"] = "false";
                    var lockRow = lockDt.FirstOrDefault(c => Convert.ToInt32(c["BelongYear"]) == i.Year && Convert.ToInt32(c["BelongMonth"]) == i.Month);
                    row["CanSettle"] = (lockRow != null && lockRow["State"].ToString() == "Finish").ToString().ToLower();
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
        public JsonResult SettleAccount(string BelongYear, string BelongMonth)
        {
            if (String.IsNullOrEmpty(BelongYear))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定年份");
            }
            if (String.IsNullOrEmpty(BelongMonth))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定月份");
            }

            //结账前需先关账
            var lockDt = this.SQLDB.ExecuteDataTable(String.Format("SELECT ID FROM S_EP_LockAccount WHERE BELONGYEAR={0} AND BELONGMONTH={1} and State != 'Removed'", BelongYear, BelongMonth));
            if (lockDt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月还未关账，需先关账才能结账", BelongYear, BelongMonth));
            }

            //上月未结账本月结不了
            string lastMonthSql = @"select count(*) from 
                                  (select year(DATEADD(mm,-1,'{0}-{1}-1')) as BelongYear, month(DATEADD(mm,-1,'{0}-{1}-1')) as BelongMonth ) main
                                  left join S_EP_SettleAccount on S_EP_SettleAccount.BelongYear = main.BelongYear and S_EP_SettleAccount.BelongMonth = main.BelongMonth 
                                  left join 
                                  (select sum(TotalPrice) CostValue, BelongYear, BelongMonth from S_EP_CostInfo
                                  where BelongYear=year(DATEADD(mm,-1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,-1,'{0}-{1}-1'))
                                  group by BelongYear, BelongMonth) CostInfo on CostInfo.BelongYear = main.BelongYear and CostInfo.BelongMonth = main.BelongMonth
                                  left join 
                                  (select sum(IncomeValue) IncomeValue, BelongYear, BelongMonth from S_EP_RevenueInfo inner join S_EP_RevenueInfo_Detail on
                                  S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID
                                  where BelongYear=year(DATEADD(mm,-1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,-1,'{0}-{1}-1'))
                                  group by BelongYear, BelongMonth
                                  ) IncomeInfo on IncomeInfo.BelongYear = main.BelongYear and IncomeInfo.BelongMonth = main.BelongMonth
                                  
                                  where
                                  --大于0时必须关账
                                  ((isnull(CostInfo.CostValue,0) > 0 or isnull(IncomeInfo.IncomeValue,0) > 0) and S_EP_SettleAccount.State = 'Finish')
                                   or (isnull(CostInfo.CostValue,0)= 0 and isnull(IncomeInfo.IncomeValue,0) = 0)";

            var lastMonthObj = this.SQLDB.ExecuteScalar(String.Format(lastMonthSql, BelongYear, BelongMonth));
            if ((int)lastMonthObj == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("上个月未结账，本月无法结账", BelongYear, BelongMonth));
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("select * from S_EP_SettleAccount where BelongYear={0} and BelongMonth={1}  and State != 'Removed'", BelongYear, BelongMonth));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经被锁定了，不能重复锁定", BelongYear, BelongMonth));
            }

            var obj = this.SQLDB.ExecuteScalar(String.Format(@"select isnull(Sum(IncomeValue),0) as SumIncomeValue from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID 
where S_EP_RevenueInfo.State='Finish' and BelongYear={0} and BelongMonth={1}", BelongYear, BelongMonth));

            var incomeValue = obj == DBNull.Value || obj == null ? 0m : Convert.ToDecimal(obj);

            obj = this.SQLDB.ExecuteScalar(String.Format(@"select isnull(Sum(TotalPrice),0) as SumCostValue from S_EP_CostInfo
where BelongYear={0} and BelongMonth={1} and S_EP_CostInfo.State = 'Finish'", BelongYear, BelongMonth));
            var costValue = obj == DBNull.Value || obj == null ? 0m : Convert.ToDecimal(obj);

            var profit = incomeValue - costValue;
            var dic = new Dictionary<string, object>();
            dic.SetValue("BelongYear", BelongYear);
            dic.SetValue("BelongMonth", BelongMonth);
            dic.SetValue("SumIncomeValue", incomeValue);
            dic.SetValue("SumCostValue", costValue);
            dic.SetValue("ProductionValue", profit);
            dic.SetValue("Profit", 0);
            dic.SetValue("CreateUser", this.CurrentUserInfo.UserName);
            dic.SetValue("CreateUserID", this.CurrentUserInfo.UserID);
            dic.SetValue("CreateDate", DateTime.Now);
            dic.SetValue("State", "Finish");
            dic.InsertDB(this.SQLDB, "S_EP_SettleAccount");
            return Json("");
        }
        public JsonResult UnSettleAccount(string BelongYear, string BelongMonth)
        {
            if (String.IsNullOrEmpty(BelongYear))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定年份");
            }
            if (String.IsNullOrEmpty(BelongMonth))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须指定月份");
            }

            //下个月未撤销,本月无法撤销
            var lastSql = @"select ID from S_EP_SettleAccount where BelongYear=year(DATEADD(mm,1,'{0}-{1}-1')) and BelongMonth=month(DATEADD(mm,1,'{0}-{1}-1'))
                          and State = 'Finish'";
            var lastDt = this.SQLDB.ExecuteDataTable(String.Format(lastSql, BelongYear, BelongMonth));
            if (lastDt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("需先撤销下个月结账", BelongYear, BelongMonth));
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT ID FROM S_EP_SettleAccount WHERE BELONGYEAR={0} AND BELONGMONTH={1} and State != 'Removed'", BelongYear, BelongMonth));
            if (dt.Rows.Count > 0)
            {
                this.SQLDB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_SettleAccount SET 
State='Removed',RemoveDate='{1}',RemoveUser='{2}',RemoveUserID='{3}' 
where ID='{0}'"
                    , dt.Rows[0]["ID"], DateTime.Now, this.CurrentUserInfo.UserName, this.CurrentUserInfo.UserID));
            }
            else
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月没有被锁定了，无法撤销", BelongYear, BelongMonth));
            }
            return Json("");
        }
        #endregion

        #region Excel导入
        public JsonResult ValidateData(string belongYear, string belongMonth)
        {
            var i = 0;
            if (string.IsNullOrWhiteSpace(belongYear) || string.IsNullOrWhiteSpace(belongMonth))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择年月！");
            }
            if (!int.TryParse(belongYear, out i) || !int.TryParse(belongMonth, out i))
            {
                throw new Formula.Exceptions.BusinessValidationException("年月格式不正确！");
            }

            var dateTime = Convert.ToDateTime(string.Format("{0}-{1}-01", belongYear, belongMonth));
            CostFO.ValidatePeriodIsClosed(dateTime);

            return Json("");
        }

        public JsonResult ValidateExcelData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var excelData = JsonConvert.DeserializeObject<ExcelData>(tempdata["data"]);

            var BaseSQLDB = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var sqlUser = "select ID,Name from S_A_User where Code='{0}' ";
            var sqlSubject = "select ID,Name from S_EP_DefineSubject where Code='{0}' ";

            var sqlSubjectCBSNode = @"select * from S_EP_CBSNode where FullID like '{0}' and Code = '{1}'";

            DataTable dt = new DataTable();
            decimal applyValue = 0m;
            var errors = excelData.Vaildate(e =>
            {
                switch (e.FieldName)
                {
                    case "TotalPrice":
                        if (string.IsNullOrWhiteSpace(e.Value))
                        {
                            e.IsValid = false;
                            e.ErrorText = "不能为空";
                        }
                        else if (!decimal.TryParse(e.Value, out applyValue))
                        {
                            e.IsValid = false;
                            e.ErrorText = "金额格式不正确";
                        }
                        break;

                    case "SubjectCode":
                        if (string.IsNullOrWhiteSpace(e.Value))
                        {
                            e.IsValid = false;
                            e.ErrorText = "不能为空";
                        }
                        else
                        {
                            dt = this.SQLDB.ExecuteDataTable(string.Format(sqlSubject, e.Value));
                            if (dt.Rows.Count == 0)
                            {
                                e.IsValid = false;
                                e.ErrorText = "科目信息不存在";
                            }
                        }
                        break;

                    case "UserCode":
                        if (!string.IsNullOrWhiteSpace(e.Value))
                        {
                            dt = BaseSQLDB.ExecuteDataTable(string.Format(sqlUser, e.Value));
                            if (dt.Rows.Count == 0)
                            {
                                e.IsValid = false;
                                e.ErrorText = "工号信息不存在";
                            }
                        }
                        break;
                    case "UnitCode":
                        if (!string.IsNullOrWhiteSpace(e.Value))
                        {
                            var objDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*, isnull(S_EP_CBSNode.IsClosed,'false') IsClosed from S_EP_CostUnit inner join S_EP_CBSNode 
                                       on S_EP_CostUnit.CBSNodeID = S_EP_CBSNode.ID where S_EP_CostUnit.Code = '" + e.Value.Trim() + "'");
                            if (objDT.Rows.Count == 0)
                            {
                                e.ErrorText = "未找到该编号的项目";
                                e.IsValid = false;
                            }
                            else if (objDT.Rows[0]["IsClosed"].ToString().ToLower() == "true")
                            {
                                e.ErrorText = "该项目已经关闭";
                                e.IsValid = false;
                            }
                        }
                        break;
                    default:
                        break;
                }

            });

            return Json(errors);
        }

        public JsonResult SaveExcelData()
        {
            var param = Request["param"].Split(new char[] { ',' });
            var belongYear = Convert.ToInt32(param[0]);
            var belongMonth = Convert.ToInt32(param[1]);
            var createDate = DateTime.Now;

            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);
            var BaseSQLDB = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var sqlUser = "select * from S_A_User where Code='{0}' ";
            var sqlSubject = "select * from S_EP_DefineSubject where Code='{0}' ";
            var sqlCBSUnitNode = @"select S_EP_CBSNode.*,S_EP_CostUnit.ID CostUnitID from S_EP_CostUnit inner join S_EP_CBSNode 
                                       on S_EP_CostUnit.CBSNodeID = S_EP_CBSNode.ID where S_EP_CostUnit.Code = '{0}'";

            var sqlSubjectCBSNode = @"select * from S_EP_CBSNode where FullID like '%{0}%' and Code = '{1}'";

            var sqlCBSInfo = "select * from S_EP_CBSInfo where ID ='{0}' ";
            var sqlInsert = string.Empty;
            var sqlUpdate = string.Empty;
            DataTable dt = new DataTable();
            var dic = new Dictionary<string, object>();
            var cbsInfoDicList = new List<Dictionary<string, object>>();

            foreach (var item in list)
            {
                dic.Clear();
                try
                {
                    dt = this.SQLDB.ExecuteDataTable(string.Format(sqlSubject, item.GetValue("SubjectCode")));
                    dic.SetValue("SubjectCode", dt.Rows[0]["Code"]);
                    dic.SetValue("Name", dt.Rows[0]["Name"]);
                    dic.SetValue("ExpenseType", dt.Rows[0]["ExpenseType"]);

                    dt = BaseSQLDB.ExecuteDataTable(string.Format(sqlUser, item.GetValue("UserCode")));
                    dic.SetValue("UserID", dt.Rows[0]["ID"]);
                    dic.SetValue("UserName", dt.Rows[0]["Name"]);
                    dic.SetValue("UserDept", dt.Rows[0]["DeptID"]);
                    dic.SetValue("UserDeptName", dt.Rows[0]["DeptName"]);

                    dt = SQLDB.ExecuteDataTable(string.Format(sqlCBSUnitNode, item.GetValue("UnitCode")));
                    var subjectCbsNodeDt = SQLDB.ExecuteDataTable(string.Format(sqlSubjectCBSNode, dt.Rows[0]["FullID"], item.GetValue("SubjectCode")));

                    dic.SetValue("Code", dt.Rows[0]["Code"]);
                    dic.SetValue("CBSInfoID", dt.Rows[0]["CBSInfoID"]);
                    dic.SetValue("CostUnitID", dt.Rows[0]["CostUnitID"]);
                    dic.SetValue("CBSFullCode", subjectCbsNodeDt.Rows[0]["FullCode"]);
                    dic.SetValue("CBSNodeID", subjectCbsNodeDt.Rows[0]["ID"]);
                    dic.SetValue("CBSNodeFullID", subjectCbsNodeDt.Rows[0]["FullID"]);
                    dic.SetValue("BelongDept", subjectCbsNodeDt.Rows[0]["ChargerDept"]);
                    dic.SetValue("BelongDeptName", subjectCbsNodeDt.Rows[0]["ChargerDeptName"]);
                    dic.SetValue("BelongUser", subjectCbsNodeDt.Rows[0]["ChargerUser"]);
                    dic.SetValue("BelongUserName", subjectCbsNodeDt.Rows[0]["ChargerUserName"]);



                    var cbsDt = SQLDB.ExecuteDataTable(string.Format(sqlCBSInfo, dt.Rows[0]["CBSInfoID"]));
                    cbsInfoDicList.Add(FormulaHelper.DataRowToDic(cbsDt.Rows[0]));
                }
                catch (Exception)
                {
                    throw new Formula.Exceptions.BusinessValidationException("信息有误，无法导入！");
                }

                dic.SetValue("ID", FormulaHelper.CreateGuid());
                dic.SetValue("CreateDate", createDate.ToString("yyyy-MM-dd"));
                dic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                dic.SetValue("CreateUser", CurrentUserInfo.UserName);
                dic.SetValue("ModifyDate", createDate.ToString("yyyy-MM-dd"));
                dic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                dic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                dic.SetValue("BelongYear", belongYear);
                dic.SetValue("BelongQuarter", (belongMonth + 2) / 3);
                dic.SetValue("BelongMonth", belongMonth);
                dic.SetValue("State", "Finish");
                dic.SetValue("Status", "Finish");

                dic.SetValue("CostType", "DirectCost");
                dic.SetValue("TotalPrice", item.GetValue("TotalPrice"));
                dic.SetValue("Quantity", 1);
                dic.SetValue("UnitPrice", item.GetValue("TotalPrice"));
                dic.SetValue("CostDate", DateTime.Now.ToString("yyyy-MM-dd"));

                sqlInsert += dic.CreateInsertSql(SQLDB, "S_EP_CostInfo", dic.GetValue("ID"));

            }
            string allSql = sqlInsert + " " + sqlUpdate;
            if (!string.IsNullOrEmpty(allSql))
            {
                SQLDB.ExecuteNonQuery(allSql);
            }

            foreach (var item in cbsInfoDicList)
            {
                var cbsInfo = new S_EP_CBSInfo(item);
                cbsInfo.SummaryCostValue();
            }
            return Json("Success");
        }

        #endregion


    }
}
