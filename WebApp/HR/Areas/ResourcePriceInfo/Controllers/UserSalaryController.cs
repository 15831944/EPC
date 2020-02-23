using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using Formula;
using HR.Logic;
using HR.Logic.Domain;
using System.Data;
using MvcAdapter;
using System.Transactions;
using Newtonsoft.Json;
using Formula.ImportExport;

namespace HR.Areas.ResourcePriceInfo.Controllers
{
    public class UserSalaryController : HRController
    {
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
                if (e.FieldName == "UserCode")
                {
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "不能为空";
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
                else if (e.FieldName == "BasicSalary")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        //e.IsValid = false;
                        //e.ErrorText = "不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
                else if (e.FieldName == "Bonus")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        //e.IsValid = false;
                        //e.ErrorText = "不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
                else if (e.FieldName == "Benefit")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        //e.IsValid = false;
                        //e.ErrorText = "不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
                else if (e.FieldName == "FiveOne")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        //e.IsValid = false;
                        //e.ErrorText = "不能为空";
                    }
                    else if (!decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
                else if (e.FieldName == "OtherValue")
                {
                    decimal tmp = 0;
                    if (!String.IsNullOrEmpty(e.Value.Trim()) && !decimal.TryParse(e.Value.Trim(), out tmp))
                    {
                        e.IsValid = false;
                        e.ErrorText = "格式不对";
                    }
                }
                else if (e.FieldName == "TotalValue")
                {
                    decimal tmp = 0;
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        //e.IsValid = false;
                        //e.ErrorText = "不能为空";
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
            string belongYearMonth = GetQueryString("BelongYearMonth");
            var arr = belongYearMonth.Split(',');
            string belongYear = arr[0];
            string belongMonth = arr[1];
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);
            
            var salaryToInsertDt = this.SqlHelper.ExecuteDataTable("select top 1 * from S_W_UserSalary");
            salaryToInsertDt.Rows.Clear();
            foreach (var item in list)
            {
                var userInfoDT = this.SqlHelper.ExecuteDataTable("select * from T_Employee where Code = '" + item.GetValue("UserCode").Trim() + "'");
                if (userInfoDT.Rows.Count == 0)
                {
                    continue;
                }
                DataRow userDr = userInfoDT.Rows[0];

                //excel表内的重复行跳过
                int existInExcelCount = salaryToInsertDt.Select(string.Format("UserInfo = '{0}'", userDr["ID"])).Count();
                if (existInExcelCount > 0)
                    continue;

                var salaryDT = this.SqlHelper.ExecuteDataTable(string.Format("select * from S_W_UserSalary where UserInfo = '{0}' and BelongYear = '{1}' and BelongMonth = '{2}'",
                                                    userDr["UserID"], belongYear, belongMonth));

                var salaryRow = salaryToInsertDt.NewRow();
                //同样数据直接赋值
                if (salaryDT.Rows.Count > 0)
                {
                    salaryRow = salaryDT.Rows[0];
                }
                else
                {
                    salaryToInsertDt.Rows.Add(salaryRow);
                    salaryRow["ID"] = FormulaHelper.CreateGuid();
                    salaryRow["CreateDate"] = DateTime.Now;
                    salaryRow["CreateUserID"] = CurrentUserInfo.UserID;
                    salaryRow["CreateUser"] = CurrentUserInfo.UserName;
                }

                salaryRow["ModifyDate"] = DateTime.Now;
                salaryRow["ModifyUserID"] = CurrentUserInfo.UserID;
                salaryRow["ModifyUser"] = CurrentUserInfo.UserName;
                salaryRow["OrgID"] = CurrentUserInfo.UserOrgID;
                salaryRow["CompanyID"] = CurrentUserInfo.UserCompanyID;
                salaryRow["BelongYear"] = belongYear;
                salaryRow["BelongMonth"] = belongMonth;
                salaryRow["UserInfo"] = userDr["UserID"];
                salaryRow["UserInfoName"] = userDr["Name"];
                salaryRow["DepartInfo"] = userDr["DeptID"];
                salaryRow["DepartInfoName"] = userDr["DeptName"];
                decimal tmp = 0;
                decimal.TryParse(item["BasicSalary"].ToString(), out tmp);
                salaryRow["BasicSalary"] = tmp;
                decimal.TryParse(item["Bonus"].ToString(), out tmp);
                salaryRow["Bonus"] = tmp;
                decimal.TryParse(item["Benefit"].ToString(), out tmp);
                salaryRow["Benefit"] = tmp;
                decimal.TryParse(item["FiveOne"].ToString(), out tmp);
                salaryRow["FiveOne"] = tmp;
                decimal.TryParse(item["OtherValue"].ToString(), out tmp);
                salaryRow["OtherValue"] = tmp;
                decimal.TryParse(item["TotalValue"].ToString(), out tmp);
                salaryRow["TotalValue"] = tmp;

                //是更新
                if (salaryDT.Rows.Count > 0)
                {
                    var dic = FormulaHelper.DataRowToDic(salaryRow);
                    dic.UpdateDB(this.SqlHelper, "S_W_UserSalary", dic.GetValue("ID"));
                }
            }
            this.SqlHelper.BulkInsertToDB(salaryToInsertDt, "S_W_UserSalary");
            return Json("Success");
        }
    }
}
