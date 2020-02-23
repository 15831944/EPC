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
using Formula.Exceptions;
using Newtonsoft.Json;
using Formula.ImportExport;
using Config;

namespace Expenses.Areas.Cost.Controllers
{
    public class WorkHourUserController : ExpensesFormController<S_EP_WorkHourUser>
    {
        public JsonResult ThisDelete(string listIDs)
        {
            string costInfoSql = "select * from S_EP_CostInfo where RelateID in ('{0}')";
            var costInfoDT = this.SQLDB.ExecuteDataTable(string.Format(costInfoSql, listIDs));
            if (costInfoDT.Rows.Count > 0)
            {
                var dic = FormulaHelper.DataRowToDic(costInfoDT.Rows[0]);
                throw new BusinessException(string.Format("{0}年{1}月项目【{2}】对应人员【{3}】的工时已经计入成本不能删除。",
                    dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"), dic.GetValue("CostUnitInfoName"), dic.GetValue("UserInfoName")));
            }

            string deleteSql = string.Format("delete S_EP_WorkHourUser where ID in ('{0}')", listIDs);
            this.SQLDB.ExecuteNonQuery(deleteSql);
            return Json("");
        }       

        public JsonResult GetCostUnitDetailList(string CostUnitInfo, string BelongYear, string BelongMonth)
        {
            string costUnitDetailSql = @"select S_EP_WorkHourUser.*,TotalPrice from S_EP_WorkHourUser
            left join S_EP_CostInfo on S_EP_CostInfo.RelateID = S_EP_WorkHourUser.ID where CostUnitInfo = '{0}' and S_EP_WorkHourUser.BelongYear = {1} and S_EP_WorkHourUser.BelongMonth = {2}";
            var resDT = this.SQLDB.ExecuteDataTable(string.Format(costUnitDetailSql, CostUnitInfo, BelongYear, BelongMonth));
            return Json(resDT);
        }

        public JsonResult GetDepartDetailList(string DepartInfo, string BelongYear, string BelongMonth)
        {
            string costUnitDetailSql = @"select S_EP_WorkHourUser.*,TotalPrice from S_EP_WorkHourUser
            left join S_EP_CostInfo on S_EP_CostInfo.RelateID = S_EP_WorkHourUser.ID where UserDeptInfo = '{0}' and S_EP_WorkHourUser.BelongYear = {1} and S_EP_WorkHourUser.BelongMonth = {2}";
            var resDT = this.SQLDB.ExecuteDataTable(string.Format(costUnitDetailSql, DepartInfo, BelongYear, BelongMonth));
            return Json(resDT);
        }

        public JsonResult AdjustTotalValue(string ids, string totalValue)
        {
            var costInfoDT = this.SQLDB.ExecuteDataTable(string.Format("select * from S_EP_CostInfo where id in ('{0}')", ids.Replace(",", "','")));
            var costInfoDicList = FormulaHelper.DataTableToListDic(costInfoDT);
            decimal dTotalValue = 0;
            decimal.TryParse(totalValue, out dTotalValue);
            foreach (var dic in costInfoDicList)
            {
                CostFO.ValidatePeriodIsClosed(new DateTime((int)dic["BelongYear"], (int)dic["BelongMonth"], 1), "不能调整成本");
                dic.SetValue("TotalPrice", totalValue);
                decimal dWorkHour = 0;
                decimal.TryParse(dic.GetValue("Quantity"), out dWorkHour);
                if (dWorkHour != 0)
                {
                    decimal unitPrice = dTotalValue / dWorkHour;
                    dic.SetValue("UnitPrice", unitPrice.ToString("0.00"));
                }
                dic.UpdateDB(this.SQLDB, "S_EP_CostInfo", dic.GetValue("ID"));
            }

            return Json("");
        }       

        public JsonResult LoadUserWorkHourFromHR(string belongYear, string belongMonth)
        {
            CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), 1), "无法再在该月份汇总工时");

            string excuteSql = "";
            excuteSql += LoadProductionUserWorkHourFromHR(belongYear, belongMonth);
            excuteSql += LoadManageUserWorkHourFromHR(belongYear, belongMonth);
            if (!string.IsNullOrEmpty(excuteSql))
            {
                this.SQLDB.ExecuteNonQuery(excuteSql);
            }

            return Json("");
        }

        private string LoadProductionUserWorkHourFromHR(string belongYear, string belongMonth)
        {
            var hrSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            string sql = @"select UserDeptID,UserDeptName,
                         UserID as UserInfo, 
                         UserName as UserInfoName,
                         ProjectID as CostUnitInfo,
                         ProjectName as CostUnitInfoName,
                         sum(isnull(WorkHourValue,0)) as WorkHour
                         from S_W_UserWorkHour
                         where BelongYear = {0} and BelongMonth = {1} and WorkHourType = 'Production' group by UserID,UserName,ProjectID,ProjectName,UserDeptID,UserDeptName";
            var dt = hrSqlDB.ExecuteDataTable(string.Format(sql, belongYear, belongMonth));
            var list = FormulaHelper.DataTableToListDic(dt);
            string excuteSql = "";
            foreach (var item in list)
            {
                var costUnitDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName from S_EP_CostUnit
                                                             inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID 
                                                             where S_EP_CostUnit.ID = '" + item.GetValue("CostUnitInfo") + "'");

                if (costUnitDT.Rows.Count == 0) continue;

                //成本单元dic
                var costUnitDic = new Dictionary<string, object>();
                if (costUnitDT.Rows.Count > 0)
                {
                    costUnitDic = FormulaHelper.DataRowToDic(costUnitDT.Rows[0]);
                }

                string existWorkHourSql = string.Format(@"select * from S_EP_WorkHourUser where BelongYear = {0}
                                          and BelongMonth = {1} and UserInfo = '{2}' and IsProduction = 'true' and 
                                          CostUnitInfo = '{3}'", belongYear, belongMonth, item.GetValue("UserInfo"), item.GetValue("CostUnitInfo"));

                var existWorkHourDt = this.SQLDB.ExecuteDataTable(existWorkHourSql);
                var workHourUserDic = new Dictionary<string, object>();
                bool isUpdate = true;
                if (existWorkHourDt.Rows.Count > 0)
                {
                    workHourUserDic = FormulaHelper.DataRowToDic(existWorkHourDt.Rows[0]);
                }
                else
                {
                    isUpdate = false;
                    workHourUserDic.SetValue("CostUnitInfo", costUnitDic["ID"]);
                    workHourUserDic.SetValue("CostUnitInfoName", costUnitDic["Name"]);
                    workHourUserDic.SetValue("IsProduction", "true");

                    workHourUserDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourUserDic.SetValue("CreateUser", CurrentUserInfo.UserID);
                    workHourUserDic.SetValue("CreateUserName", CurrentUserInfo.UserName);
                    workHourUserDic.SetValue("CreateDate", DateTime.Now);
                    workHourUserDic.SetValue("BelongYear", belongYear);
                    workHourUserDic.SetValue("BelongMonth", belongMonth);
                    workHourUserDic.SetValue("UserInfo", item["UserInfo"]);
                    workHourUserDic.SetValue("UserInfoName", item["UserInfoName"]);
                    workHourUserDic.SetValue("UserDeptInfo", item["UserDeptID"]);
                    workHourUserDic.SetValue("UserDeptInfoName", item["UserDeptName"]);
                }

                workHourUserDic.SetValue("ModifyUser", CurrentUserInfo.UserID);
                workHourUserDic.SetValue("ModifyUserName", CurrentUserInfo.UserName);
                workHourUserDic.SetValue("ModifyDate", DateTime.Now);
                workHourUserDic.SetValue("WorkHour", item.GetValue("WorkHour"));

                if (isUpdate)
                {
                    excuteSql += workHourUserDic.CreateUpdateSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
                else
                {
                    excuteSql += workHourUserDic.CreateInsertSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
            }
            return excuteSql;
        }

        private string LoadManageUserWorkHourFromHR(string belongYear, string belongMonth)
        {
            var hrSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            string sql = @"select UserDeptID,UserDeptName,
                         UserID as UserInfo, 
                         UserName as UserInfoName,
                         ProjectID as CostUnitInfo,
                         ProjectName as CostUnitInfoName,
                         sum(isnull(WorkHourValue,0)) as WorkHour
                         from S_W_UserWorkHour
                         where BelongYear = {0} and BelongMonth = {1} and WorkHourType != 'Production' group by UserID,UserName,ProjectID,ProjectName,UserDeptID,UserDeptName";
            var dt = hrSqlDB.ExecuteDataTable(string.Format(sql, belongYear, belongMonth));
            var list = FormulaHelper.DataTableToListDic(dt);
            string excuteSql = "";
            foreach (var item in list)
            {
                var costUnitDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName from S_EP_CostUnit
                                                             inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID 
                                                             where S_EP_CostUnit.ID = '" + item.GetValue("CostUnitInfo") + "'");

                //成本单元dic
                var costUnitDic = new Dictionary<string, object>();
                if (costUnitDT.Rows.Count > 0)
                {
                    costUnitDic = FormulaHelper.DataRowToDic(costUnitDT.Rows[0]);
                }

                string existWorkHourSql = string.Format(@"select * from S_EP_WorkHourUser where BelongYear = {0}
                                          and BelongMonth = {1} and UserInfo = '{2}' and IsProduction = 'false'", belongYear, belongMonth, item.GetValue("UserInfo"));
                if (costUnitDic.Keys.Count > 0)
                {
                    existWorkHourSql += " and CostUnitInfo = '" + costUnitDic.GetValue("ID") + "'";
                }

                var existWorkHourDt = this.SQLDB.ExecuteDataTable(existWorkHourSql);
                var workHourUserDic = new Dictionary<string, object>();
                bool isUpdate = true;
                if (existWorkHourDt.Rows.Count > 0)
                {
                    workHourUserDic = FormulaHelper.DataRowToDic(existWorkHourDt.Rows[0]);
                }
                else
                {
                    isUpdate = false;
                    if (costUnitDic.Keys.Count > 0)
                    {
                        workHourUserDic.SetValue("CostUnitInfo", costUnitDic["ID"]);
                        workHourUserDic.SetValue("CostUnitInfoName", costUnitDic["Name"]);
                    }

                    workHourUserDic.SetValue("IsProduction", "false");

                    workHourUserDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourUserDic.SetValue("CreateUser", CurrentUserInfo.UserID);
                    workHourUserDic.SetValue("CreateUserName", CurrentUserInfo.UserName);
                    workHourUserDic.SetValue("CreateDate", DateTime.Now);
                    workHourUserDic.SetValue("BelongYear", belongYear);
                    workHourUserDic.SetValue("BelongMonth", belongMonth);
                    workHourUserDic.SetValue("UserInfo", item["UserInfo"]);
                    workHourUserDic.SetValue("UserInfoName", item["UserInfoName"]);
                    workHourUserDic.SetValue("UserDeptInfo", item["UserDeptID"]);
                    workHourUserDic.SetValue("UserDeptInfoName", item["UserDeptName"]);
                }

                workHourUserDic.SetValue("ModifyUser", CurrentUserInfo.UserID);
                workHourUserDic.SetValue("ModifyUserName", CurrentUserInfo.UserName);
                workHourUserDic.SetValue("ModifyDate", DateTime.Now);
                workHourUserDic.SetValue("WorkHour", item.GetValue("WorkHour"));

                if (isUpdate)
                {
                    excuteSql += workHourUserDic.CreateUpdateSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
                else
                {
                    excuteSql += workHourUserDic.CreateInsertSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
            }
            return excuteSql;
        }

        public JsonResult ValidateData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var excelData = JsonConvert.DeserializeObject<ExcelData>(tempdata["data"]);

            var baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            List<string> tmpCodes = new List<string>();
            var errors = excelData.Vaildate(e =>
            {
                if (e.FieldName == "Code")
                {
                    if (!String.IsNullOrEmpty(e.Value.Trim()))
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
                        var obj = baseHelper.ExecuteScalar("select count(*) from S_A_User where Code = '" + e.Value.Trim() + "'");
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
                else if (e.FieldName == "WorkHour")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "工时不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "工时格式不对";
                    }
                }
            });
            return Json(errors);
        }

        public JsonResult SaveExcelData()
        {
            string belongYearMonth = GetQueryString("BelongYearMonth");
            var arr = belongYearMonth.Split(',');
            string belongYear = arr[0];
            string belongMonth = arr[1];
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);
            
            var baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);

            string excuteSql = "";
            foreach (var item in list)
            {
                string costUnitCode = item["Code"].ToString().Trim();
                var costUnitDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName from S_EP_CostUnit
                                                             inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID 
                                                             where S_EP_CostUnit.Code = '" + costUnitCode + "'");
                
                //成本单元dic
                var costUnitDic = new Dictionary<string, object>();
                if (costUnitDT.Rows.Count > 0)
                {
                    costUnitDic = FormulaHelper.DataRowToDic(costUnitDT.Rows[0]);                    
                }                

                string userInfoCode = item["UserInfoCode"].ToString().Trim();
                var userDt = baseHelper.ExecuteDataTable("select * from S_A_User where Code = '" + userInfoCode + "'");
                //人员dic
                var userDic = FormulaHelper.DataRowToDic(userDt.Rows[0]);

                string existWorkHourSql = string.Format(@"select * from S_EP_WorkHourUser where BelongYear = {0}
                                          and BelongMonth = {1} and UserInfo = '{2}'", belongYear, belongMonth, userDic.GetValue("ID"));
                //生产项目工时
                if (costUnitDic.Keys.Count > 0)
                {
                    existWorkHourSql += " and IsProduction = 'true' and CostUnitInfo = '" + costUnitDic.GetValue("ID") + "'";
                }
                else
                {
                    existWorkHourSql += " and IsProduction = 'false'";
                }

                var existWorkHourDt = this.SQLDB.ExecuteDataTable(existWorkHourSql);
                var workHourUserDic = new Dictionary<string, object>();
                bool isUpdate = true;
                if (existWorkHourDt.Rows.Count > 0)
                {
                    workHourUserDic = FormulaHelper.DataRowToDic(existWorkHourDt.Rows[0]);
                }
                else
                {
                    isUpdate = false;
                    if (costUnitDic.Keys.Count > 0)
                    {
                        workHourUserDic.SetValue("CostUnitInfo", costUnitDic["ID"]);
                        workHourUserDic.SetValue("CostUnitInfoName", costUnitDic["Name"]);
                        workHourUserDic.SetValue("IsProduction", "true");
                    }
                    else
                    {
                        workHourUserDic.SetValue("IsProduction", "false");
                    }

                    workHourUserDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourUserDic.SetValue("CreateUser", CurrentUserInfo.UserID);
                    workHourUserDic.SetValue("CreateUserName", CurrentUserInfo.UserName);
                    workHourUserDic.SetValue("CreateDate", DateTime.Now);
                    workHourUserDic.SetValue("BelongYear", belongYear);
                    workHourUserDic.SetValue("BelongMonth", belongMonth);
                    workHourUserDic.SetValue("UserInfo", userDic["ID"]);
                    workHourUserDic.SetValue("UserInfoName", userDic["Name"]);
                    workHourUserDic.SetValue("UserDeptInfo", userDic["DeptID"]);
                    workHourUserDic.SetValue("UserDeptInfoName", userDic["DeptName"]);
                }
                
                workHourUserDic.SetValue("ModifyUser", CurrentUserInfo.UserID);
                workHourUserDic.SetValue("ModifyUserName", CurrentUserInfo.UserName);                
                workHourUserDic.SetValue("ModifyDate", DateTime.Now);  
                workHourUserDic.SetValue("WorkHour", item.GetValue("WorkHour"));

                if (isUpdate)
                {
                    excuteSql += workHourUserDic.CreateUpdateSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
                else
                {
                    excuteSql += workHourUserDic.CreateInsertSql(this.SQLDB, "S_EP_WorkHourUser", workHourUserDic.GetValue("ID"));
                }
            }

            if (!string.IsNullOrEmpty(excuteSql))
            {
                this.SQLDB.ExecuteNonQuery(excuteSql);
            }
            
            return Json("Success");
        } 
    }
}
