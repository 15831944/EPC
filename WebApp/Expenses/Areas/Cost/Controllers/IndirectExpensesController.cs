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

namespace Expenses.Areas.Cost.Controllers
{
    public class IndirectExpensesController : ExpensesFormController<S_EP_IndirectExpenses>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var applyValue = 0m;
            if (!decimal.TryParse(dic.GetValue("ApplyValue"), out applyValue))
            {
                throw new Formula.Exceptions.BusinessValidationException("金额格式不正确！");
            }
            if (applyValue <= 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("金额必须大于0！");
            }

            var applyDate = DateTime.Now.Date;
            if (!DateTime.TryParse(dic.GetValue("ApplyDate"), out applyDate))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择申请日期！");
            }
            CostFO.ValidatePeriodIsClosed(applyDate);

            dic.SetValue("BelongYear", applyDate.Year.ToString());
            dic.SetValue("BelongQuarter", ((applyDate.Month + 2) / 3).ToString());
            dic.SetValue("BelongMonth", applyDate.Month.ToString());

            if (isNew)
            {
                dic.SetValue("Source", "Input");
            }
            else
            {
                if (dic.GetValue("Source") == "Reimbursement")
                {
                    throw new Formula.Exceptions.BusinessValidationException("报销单费用不能修改！");
                }

                //是否已分摊
                if (ValidateCostApportion(dic.GetValue("ID")) != null)
                {
                    var apportionValue = Convert.ToDecimal(dic.GetValue("ApportionValue"));
                    if (applyValue < apportionValue)
                    {
                        throw new Formula.Exceptions.BusinessValidationException(string.Format("金额【{0}】必须大于已分摊金额【{1}】！",
                            applyValue, apportionValue));
                    }
                }

            }
        }

        public override JsonResult Delete()
        {
            if (string.IsNullOrWhiteSpace(Request["ListData"]))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择要删除的数据！");
            }
            var listData = JsonHelper.ToList(Request["ListData"]);
            var sql = string.Empty;
            foreach (var dic in listData)
            {
                //报销单
                if (dic.GetValue("Source") == "Reimbursement")
                {
                    throw new Formula.Exceptions.BusinessValidationException("关联报销单的数据不能删除！");
                }
                //是否已分摊
                if (ValidateCostApportion(dic.GetValue("ID")) != null)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("【{0}】【{1}月份】的【{2}】已分摊，不能删除！", 
                        dic.GetValue("ChargerUserName"), dic.GetValue("BelongMonth"), dic.GetValue("SubjectName")));
                }

                sql += dic.CreateDeleteSql("S_EP_IndirectExpenses");
            }

            if (!string.IsNullOrEmpty(sql))
            SQLDB.ExecuteNonQuery(sql);

            return Json("");
        }

        private object ValidateCostApportion(string relateID)
        {
            var sql = string.Format(@"select sum(CostApportion_Detail.AdjustValue) ApportionValue, RelateID 
from (select * from S_EP_CostApportion_Detail where RelateID='') CostApportion_Detail
inner join S_EP_CostApportion on S_EP_CostApportion.ID = CostApportion_Detail.S_EP_CostApportionID 
where S_EP_CostApportion.FlowPhase = 'End' and S_EP_CostApportion.State = 'Finish' group by RelateID", relateID);
            return SQLDB.ExecuteScalar(sql);
        }


        #region Excel导入
        public JsonResult ValidateData()
        {
            var i = 0;
            var belongYear = Request["BelongYear"];
            var belongMonth = Request["BelongMonth"];
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
            var sqlDept = "select ID,Name from S_A_Org where Name='{0}' ";
            var sqlSubject = "select ID,Name from S_EP_DefineSubject where Code='{0}' ";

            DataTable dt = new DataTable();
            decimal applyValue = 0m;
            var errors = excelData.Vaildate(e =>
            {
                switch (e.FieldName)
                {
                    case "ApplyValue":
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
                            dt = SQLDB.ExecuteDataTable(string.Format(sqlSubject, e.Value));
                            if (dt.Rows.Count == 0)
                            {
                                e.IsValid = false;
                                e.ErrorText = "科目编号不存在";
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

                    case "ChargerDeptName":
                        if (!string.IsNullOrWhiteSpace(e.Value))
                        {
                            dt = BaseSQLDB.ExecuteDataTable(string.Format(sqlDept, e.Value));
                            if (dt.Rows.Count == 0)
                            {
                                e.IsValid = false;
                                e.ErrorText = "部门信息不存在";
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
            var sqlUser = "select ID,Name from S_A_User where Code='{0}' ";
            var sqlDept = "select ID,Name from S_A_Org where Name='{0}' ";
            var sqlSubject = "select ID,Name from S_EP_DefineSubject where Code='{0}' ";
            var sqlInsert = string.Empty;
            var sqlUpdate = string.Empty;
            DataTable dt = new DataTable();
            var dic = new Dictionary<string, object>();
            foreach (var item in list)
            {
                dic.Clear();               

                try
                {
                    dt = InfrasDB.ExecuteDataTable(string.Format(sqlSubject, item.GetValue("SubjectCode")));
                    dic.SetValue("Subject", dt.Rows[0]["ID"]);

                    dt = BaseSQLDB.ExecuteDataTable(string.Format(sqlUser, item.GetValue("UserCode")));
                    dic.SetValue("ChargerUser", dt.Rows[0]["ID"]);
                    dic.SetValue("ChargerUserName", dt.Rows[0]["Name"]);

                    dt = BaseSQLDB.ExecuteDataTable(string.Format(sqlDept, item.GetValue("ChargerDeptName")));
                    dic.SetValue("ChargerDept", dt.Rows[0]["ID"]);
                    dic.SetValue("ChargerDeptName", item.GetValue("ChargerDeptName"));
                }
                catch (Exception)
                {
                    throw new Formula.Exceptions.BusinessValidationException("信息有误，无法导入！");
                }

                var existedDT = this.SQLDB.ExecuteDataTable(string.Format("select * from S_EP_IndirectExpenses where ChargerUser = '{0}' and ChargerDept = '{1}' and BelongYear = '{2}' and BelongMonth = '{3}' and Subject = '{4}'",
                                                   dic.GetValue("ChargerUser"), dic.GetValue("ChargerDept"), belongYear, belongMonth, dic.GetValue("Subject")));

                if (existedDT.Rows.Count > 0)
                {
                    dic = FormulaHelper.DataRowToDic(existedDT.Rows[0]);
                }
                else
                {
                    dic.SetValue("ID", FormulaHelper.CreateGuid());
                    dic.SetValue("CreateDate", createDate.ToString("yyyy-MM-dd"));
                    dic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                    dic.SetValue("CreateUser", CurrentUserInfo.UserName);
                }
                
                dic.SetValue("ModifyDate", createDate.ToString("yyyy-MM-dd"));
                dic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                dic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                dic.SetValue("ApplyValue", Convert.ToDecimal(item.GetValue("ApplyValue")));
                dic.SetValue("ApplyDate", string.Format("{0}-{1}-15", belongYear, belongMonth));
                dic.SetValue("BelongYear", belongYear);
                dic.SetValue("BelongQuarter", (belongMonth + 2) / 3);
                dic.SetValue("BelongMonth", belongMonth);
                dic.SetValue("Source", "Import");

                //是更新
                if (existedDT.Rows.Count > 0)
                {
                    sqlUpdate = dic.CreateUpdateSql(SQLDB, "S_EP_IndirectExpenses", dic.GetValue("ID"));
                }
                else
                {
                    sqlInsert += dic.CreateInsertSql(SQLDB, "S_EP_IndirectExpenses", dic.GetValue("ID"));
                }
                
            }
            string allSql = sqlInsert + " " + sqlUpdate;
            if (!string.IsNullOrEmpty(allSql))
            {
                SQLDB.ExecuteNonQuery(allSql);
            }
            return Json("Success");
        }

        #endregion

    }
}
