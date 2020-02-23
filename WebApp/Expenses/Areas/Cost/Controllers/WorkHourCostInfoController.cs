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
using Formula.Exceptions;

namespace Expenses.Areas.Cost.Controllers
{
    public class WorkHourCostInfoController : ExpensesFormController<S_EP_CostInfo>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(dic["BelongYear"]), Convert.ToInt32(dic["BelongMonth"]), 1), "不能增加人工成本数据");

            var costUnitID = dic.GetValue("CostUnitID");

            var costUnitDic = this.GetDataDicByID("S_EP_CostUnit", costUnitID);
            if (costUnitDic == null)
                throw new BusinessException("成本单元为空");
            var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", costUnitDic.GetValue("CBSNodeID"));
            if (cbsNodeDic == null)
                throw new BusinessException("未找到成本单元的cbsNode节点");

            var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", cbsNodeDic.GetValue("FullID")));

            var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                && c["SubjectType"].ToString() == SubjectType.HRCost.ToString());

            if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.HRCost.ToString() + "】的节点");

            dic["Name"] = subjectNode["Name"].ToString();
            dic["Code"] = subjectNode["Code"].ToString();
            dic["CBSNodeFullID"] = subjectNode["FullID"].ToString();
            dic["CBSFullCode"] = subjectNode["FullCode"].ToString();
            dic["CBSNodeID"] = subjectNode["ID"].ToString();
            dic["SubjectCode"] = subjectNode["SubjectCode"].ToString();
            dic["BelongDept"] = subjectNode["ChargerDept"].ToString();
            dic["BelongDeptName"] = subjectNode["ChargerDeptName"].ToString();
            dic["BelongUser"] = subjectNode["ChargerUser"].ToString();
            dic["BelongUserName"] = subjectNode["ChargerUserName"].ToString();
            dic["CostType"] = SubjectType.HRCost.ToString();
            if (!String.IsNullOrEmpty(dic["BelongMonth"].ToString()))
            {
                var nBelongMonth = Convert.ToInt32(dic["BelongMonth"].ToString());
                dic["BelongQuarter"] = ((nBelongMonth - 1) / 3 + 1).ToString();
            }
        }

        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (!isNew)
            {
                if (!dt.Columns.Contains("CostUnitIDName")) dt.Columns.Add(new DataColumn("CostUnitIDName"));

                DataRow dr = dt.Rows[0];
                var costUnitDic = this.GetDataDicByID("S_EP_CostUnit", dr["CostUnitID"].ToString());
                if (costUnitDic == null)
                    throw new BusinessException("成本单元为空");

                dr["CostUnitIDName"] = costUnitDic.GetValue("Name");
            }
        }

        public JsonResult CheckLockAccount(string belongYear, string belongMonth)
        {
            CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), 1), "不能新增人工成本数据");
            return Json("true");
        }

        public JsonResult CalcTotalValue(string belongYear, string belongMonth, string isActual)
        {
            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_LockAccount WHERE BelongYear={0} and BelongMonth={1} and State='Finish'", belongYear, belongMonth));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经关账锁定，无法计算人工成本", belongYear, belongMonth));
            }

            string workHourUserSql = "select * from S_EP_WorkHourUser where BelongYear = {0} and BelongMonth = {1}";
            var workHourUserDT = this.SQLDB.ExecuteDataTable(string.Format(workHourUserSql, belongYear, belongMonth));
            DataTable costInfoToInsert = new DataTable();
            foreach (DataRow workHourUserDr in workHourUserDT.Rows)
            {
                string costInfoSql = "select * from S_EP_CostInfo where RelateID = '{0}'";
                var costInfoDT = this.SQLDB.ExecuteDataTable(string.Format(costInfoSql, workHourUserDr["ID"]));

                if (costInfoToInsert.Columns.Count == 0)
                {
                    foreach (DataColumn col in costInfoDT.Columns)
                        costInfoToInsert.Columns.Add(new DataColumn(col.ColumnName));
                }

                decimal unitPrice = CostFO.GetUserUnitPrice(workHourUserDr["UserInfo"].ToString(), Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), isActual.ToLower() == "true");
                decimal workHour = 0;
                decimal.TryParse(workHourUserDr["WorkHour"].ToString(), out workHour);

                //add
                if (costInfoDT.Rows.Count == 0)
                {
                    var costUnitDic = this.GetDataDicByID("S_EP_CostUnit", workHourUserDr["CostUnitInfo"].ToString());
                    if (costUnitDic == null) continue;
                    var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", costUnitDic.GetValue("CBSNodeID"));
                    if (cbsNodeDic == null)
                        continue;

                    var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", cbsNodeDic.GetValue("FullID")));

                    var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                                      && c["SubjectType"].ToString() == SubjectType.HRCost.ToString());

                    if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.HRCost.ToString() + "】的节点");

                    DataRow newCostInfoDr = costInfoToInsert.NewRow();
                    newCostInfoDr["ID"] = FormulaHelper.CreateGuid();
                    newCostInfoDr["Name"] = subjectNode["Name"];
                    newCostInfoDr["Code"] = subjectNode["Code"];
                    newCostInfoDr["CostType"] = SubjectType.HRCost.ToString();
                    newCostInfoDr["CBSInfoID"] = costUnitDic.GetValue("CBSInfoID");
                    newCostInfoDr["CostUnitID"] = costUnitDic.GetValue("ID");
                    newCostInfoDr["CBSFullCode"] = subjectNode["FullCode"];
                    newCostInfoDr["CBSNodeID"] = subjectNode["ID"];
                    newCostInfoDr["CBSNodeFullID"] = subjectNode["FullID"];
                    newCostInfoDr["RelateID"] = workHourUserDr["ID"];
                    newCostInfoDr["CostDate"] = workHourUserDr["CreateDate"];
                    newCostInfoDr["BelongYear"] = workHourUserDr["BelongYear"];
                    newCostInfoDr["BelongMonth"] = workHourUserDr["BelongMonth"];

                    if (!String.IsNullOrEmpty(workHourUserDr["BelongMonth"].ToString()))
                    {
                        var nBelongMonth = Convert.ToInt32(workHourUserDr["BelongMonth"].ToString());
                        newCostInfoDr["BelongQuarter"] = (nBelongMonth - 1) / 3 + 1;
                    }
                    newCostInfoDr["UnitPrice"] = unitPrice;
                    newCostInfoDr["Quantity"] = workHourUserDr["WorkHour"];
                    newCostInfoDr["TotalPrice"] = workHour * unitPrice;
                    newCostInfoDr["SubjectCode"] = subjectNode["SubjectCode"];
                    newCostInfoDr["ExpenseType"] = subjectNode["ExpenseType"];

                    newCostInfoDr["BelongDept"] = subjectNode["ChargerDept"];
                    newCostInfoDr["BelongDeptName"] = subjectNode["ChargerDeptName"];
                    newCostInfoDr["BelongUser"] = subjectNode["ChargerUser"];
                    newCostInfoDr["BelongUserName"] = subjectNode["ChargerUserName"];
                    newCostInfoDr["UserID"] = workHourUserDr["UserInfo"];
                    newCostInfoDr["UserName"] = workHourUserDr["UserInfoName"];
                    newCostInfoDr["UserDept"] = workHourUserDr["UserDeptInfo"];
                    newCostInfoDr["UserDeptName"] = workHourUserDr["UserDeptInfoName"];
                    newCostInfoDr["State"] = IncomeState.Finish.ToString();
                    newCostInfoDr["Status"] = IncomeState.Finish.ToString();

                    newCostInfoDr["CreateUser"] = CurrentUserInfo.UserID;
                    newCostInfoDr["CreateUserName"] = CurrentUserInfo.UserName;
                    newCostInfoDr["CreateDate"] = DateTime.Now;
                    newCostInfoDr["ModifyUser"] = CurrentUserInfo.UserID;
                    newCostInfoDr["ModifyUserName"] = CurrentUserInfo.UserName;
                    newCostInfoDr["ModifyDate"] = DateTime.Now;
                    costInfoToInsert.Rows.Add(newCostInfoDr);
                }
                //update
                else
                {
                    DataRow updateDr = costInfoDT.Rows[0];

                    var costUnitDic = this.GetDataDicByID("S_EP_CostUnit", workHourUserDr["CostUnitInfo"].ToString());
                    if (costUnitDic == null) continue;
                    var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", costUnitDic.GetValue("CBSNodeID"));
                    if (cbsNodeDic == null)
                        continue;
                    var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", cbsNodeDic.GetValue("FullID")));

                    var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                                      && c["SubjectType"].ToString() == SubjectType.HRCost.ToString());

                    if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.HRCost.ToString() + "】的节点");

                    updateDr["SubjectCode"] = subjectNode["Code"];
                    updateDr["ModifyUser"] = CurrentUserInfo.UserID;
                    updateDr["ModifyUserName"] = CurrentUserInfo.UserID;
                    updateDr["ModifyDate"] = DateTime.Now;
                    updateDr["UserID"] = workHourUserDr["UserInfo"];
                    updateDr["UserName"] = workHourUserDr["UserInfoName"];
                    updateDr["UserDept"] = workHourUserDr["UserDeptInfo"];
                    updateDr["UserDeptName"] = workHourUserDr["UserDeptInfoName"];
                    updateDr["UnitPrice"] = unitPrice;
                    //updateDr["Quantity"] = workHourUserDr["WorkHour"];
                    updateDr["TotalPrice"] = workHour * unitPrice;
                    updateDr["ExpenseType"] = subjectNode["ExpenseType"];
                    var updateDic = FormulaHelper.DataRowToDic(updateDr);
                    updateDic.UpdateDB(this.SQLDB, "S_EP_CostInfo", updateDr["ID"].ToString());
                }
            }
            this.SQLDB.BulkInsertToDB(costInfoToInsert, "S_EP_CostInfo");
            return Json("");
        }

        public JsonResult ValidateDelete(string ids)
        {
            var costInfoDT = this.SQLDB.ExecuteDataTable(string.Format("select * from S_EP_CostInfo where id in ('{0}')", ids.Replace(",", "','")));
            var costInfoDicList = FormulaHelper.DataTableToListDic(costInfoDT);

            foreach (var dic in costInfoDicList)
            {
                CostFO.ValidatePeriodIsClosed(new DateTime((int)dic["BelongYear"], (int)dic["BelongMonth"], 1), "不能删除");
            }

            return Json("");
        }

        public JsonResult ValidateData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var excelData = JsonConvert.DeserializeObject<ExcelData>(tempdata["data"]);

            var hrHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            List<string> tmpCodes = new List<string>();
            var errors = excelData.Vaildate(e =>
            {
                if (e.FieldName == "CostUnitCode")
                {
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "项目编号不能为空";
                    }
                    else
                    {
                        var objDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*, isnull(S_EP_CBSNode.IsClosed,'false') from S_EP_CostUnit inner join S_EP_CBSNode 
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
                }
                //if (e.FieldName == "CostUnitInfoName" && String.IsNullOrEmpty(e.Value.Trim()))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = "项目名称不能为空";
                //}
                else if (e.FieldName == "UserInfoCode")
                {
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "人员编号不能为空";
                    }
                    else
                    {
                        var obj = hrHelper.ExecuteScalar("select count(*) from T_Employee where Code = '" + e.Value.Trim() + "'");
                        if (obj == null || (int)obj == 0)
                        {
                            e.ErrorText = "未找到该编号的人员";
                            e.IsValid = false;
                        }
                    }
                }
                //else if (e.FieldName == "UserInfoName" && String.IsNullOrEmpty(e.Value.Trim()))
                //{
                //    e.IsValid = false;
                //    e.ErrorText = "人员名称不能为空";
                //}
                else if (e.FieldName == "TotalPrice")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
            });
            return Json(errors);
        }

        public JsonResult SaveExcelData()
        {
            string belongYearMonth = GetQueryString("BelongYearMonthExpenseType");
            var arr = belongYearMonth.Split(',');
            string belongYear = arr[0];
            string belongMonth = arr[1];

            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);

            var costCodeList = list.Select(a => a.GetValue("CostUnitCode").Trim()).Distinct();

            var hrHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            DataTable costInfoToInsert = this.SQLDB.ExecuteDataTable("select top 1 * from S_EP_CostInfo");
            costInfoToInsert.Rows.Clear();

            foreach (var item in costCodeList)
            {
                var subList = list.Where(a => a.GetValue("CostUnitCode") == item);

                var costUnitTable = this.SQLDB.ExecuteDataTable("select * from S_EP_CostUnit where Code = '" + item + "'");
                if (costUnitTable.Rows.Count == 0) continue;
                var costUnitDic = FormulaHelper.DataRowToDic(costUnitTable.Rows[0]);

                var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", costUnitDic.GetValue("CBSNodeID"));
                if (cbsNodeDic == null)
                    continue;

                var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", cbsNodeDic.GetValue("FullID")));

                var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                                       && c["SubjectType"].ToString() == SubjectType.HRCost.ToString());

                if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.HRCost.ToString() + "】的节点");

                foreach (var sub in subList)
                {
                    var userInfoDT = hrHelper.ExecuteDataTable("select * from T_Employee where Code = '" + sub.GetValue("UserInfoCode").Trim() + "'");
                    if (userInfoDT.Rows.Count == 0)
                    {
                        continue;
                    }
                    DataRow userDr = userInfoDT.Rows[0];

                    //excel表内的重复行跳过
                    int existInExcelCount = costInfoToInsert.Select(string.Format("UserID = '{0}' and CBSNodeID = '{1}'", userDr["UserID"], subjectNode["ID"])).Count();
                    if (existInExcelCount > 0)
                        continue;

                    var existedDT = this.SQLDB.ExecuteDataTable(string.Format(@"select * from S_EP_CostInfo where UserID = '{0}'
                                                   and BelongYear = '{1}' and BelongMonth = '{2}' and CBSNodeID = '{3}' and CostType = '{4}' and ExpenseType = '{5}'",
                                                   userDr["UserID"], belongYear, belongMonth, subjectNode["ID"], SubjectType.HRCost.ToString(), subjectNode["ExpenseType"]));

                    DataRow newCostInfoDr = costInfoToInsert.NewRow();
                    //同样数据直接赋值
                    if (existedDT.Rows.Count > 0)
                    {
                        newCostInfoDr = existedDT.Rows[0];
                    }
                    else
                    {
                        costInfoToInsert.Rows.Add(newCostInfoDr);
                        newCostInfoDr["ID"] = FormulaHelper.CreateGuid();
                        newCostInfoDr["CreateUser"] = CurrentUserInfo.UserID;
                        newCostInfoDr["CreateUserName"] = CurrentUserInfo.UserName;
                        newCostInfoDr["CreateDate"] = DateTime.Now;
                    }

                    newCostInfoDr["Name"] = subjectNode["Name"];
                    newCostInfoDr["Code"] = subjectNode["Code"];
                    newCostInfoDr["CostType"] = SubjectType.HRCost.ToString();
                    newCostInfoDr["CBSInfoID"] = costUnitDic.GetValue("CBSInfoID");
                    newCostInfoDr["CostUnitID"] = costUnitDic.GetValue("ID");
                    newCostInfoDr["CBSFullCode"] = subjectNode["FullCode"];
                    newCostInfoDr["CBSNodeID"] = subjectNode["ID"];
                    newCostInfoDr["CBSNodeFullID"] = subjectNode["FullID"];

                    newCostInfoDr["CostDate"] = DateTime.Now;
                    newCostInfoDr["BelongYear"] = belongYear;
                    newCostInfoDr["BelongMonth"] = belongMonth;

                    if (!String.IsNullOrEmpty(belongMonth))
                    {
                        var nBelongMonth = Convert.ToInt32(belongMonth);
                        newCostInfoDr["BelongQuarter"] = (nBelongMonth - 1) / 3 + 1;
                    }

                    newCostInfoDr["UnitPrice"] = sub["TotalPrice"];
                    newCostInfoDr["Quantity"] = 1;
                    newCostInfoDr["TotalPrice"] = sub["TotalPrice"];
                    newCostInfoDr["SubjectCode"] = subjectNode["SubjectCode"];
                    newCostInfoDr["ExpenseType"] = subjectNode["ExpenseType"];

                    newCostInfoDr["BelongDept"] = subjectNode["ChargerDept"];
                    newCostInfoDr["BelongDeptName"] = subjectNode["ChargerDeptName"];
                    newCostInfoDr["BelongUser"] = subjectNode["ChargerUser"];
                    newCostInfoDr["BelongUserName"] = subjectNode["ChargerUserName"];


                    newCostInfoDr["UserID"] = userDr["UserID"];
                    newCostInfoDr["UserName"] = userDr["Name"];
                    newCostInfoDr["UserDept"] = userDr["DeptID"];
                    newCostInfoDr["UserDeptName"] = userDr["DeptName"];

                    newCostInfoDr["State"] = IncomeState.Finish.ToString();
                    newCostInfoDr["Status"] = IncomeState.Finish.ToString();

                    newCostInfoDr["ModifyUser"] = CurrentUserInfo.UserID;
                    newCostInfoDr["ModifyUserName"] = CurrentUserInfo.UserName;
                    newCostInfoDr["ModifyDate"] = DateTime.Now;

                    //是更新
                    if (existedDT.Rows.Count > 0)
                    {
                        var dic = FormulaHelper.DataRowToDic(newCostInfoDr);
                        dic.UpdateDB(this.SQLDB, "S_EP_CostInfo", dic.GetValue("ID"));
                    }
                }
            }
            this.SQLDB.BulkInsertToDB(costInfoToInsert, "S_EP_CostInfo");

            return Json("Success");
        }
    }
}
