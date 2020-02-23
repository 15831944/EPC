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
using Config;
using Newtonsoft.Json;
using Formula.ImportExport;

namespace Expenses.Areas.Cost.Controllers
{
    public class WorkHourSubmitController : ExpensesFormController<S_EP_WorkHourSubmit>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                //判定当月的成本信息是否被锁定，如果被锁定则不允许再在锁定月份上报工时
                CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(dic.GetValue("BelongYear")), Convert.ToInt32(dic.GetValue("BelongMonth")), 1), "无法再在该月份上报工时");
                //不能重复上报
                //if (String.IsNullOrEmpty(dic.GetValue("CostUnitID")))
                //{
                //    throw new Formula.Exceptions.BusinessValidationException("必须指定项目");
                //}
                //string submitSql = @"select count(1) from S_EP_WorkHourSubmit where BelongYear = {0} and BelongMonth = {1} and CostUnitID = '{2}' and State != 'Removed'";
                //var resObj = this.SQLDB.ExecuteScalar(string.Format(submitSql, dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"), dic.GetValue("CostUnitID")));
                //if (resObj != null && (int)resObj > 0)
                //{
                //    throw new Formula.Exceptions.BusinessValidationException(string.Format("该项目在{0}年{1}月已经上报过，不能重复上报"));
                //}
            }
        }
        
        protected override void OnFlowEnd(S_EP_WorkHourSubmit data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult GetSubmitList(string UnitID)
        {
            var data = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_WorkHourSubmit WHERE CostUnitID='{0}'", UnitID));
            return Json(data);
        }

        public JsonResult GetUserPriceInfo(string UserInfo, string belongYear, string belongMonth)
        {
            var userList = JsonHelper.ToList(UserInfo);
            if (String.IsNullOrEmpty(belongYear) | String.IsNullOrEmpty(belongMonth))
                return Json(userList);
            foreach (var user in userList)
            {
                var result = CostFO.GetUserUnitPrice(user.GetValue("UserInfo"), Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth));
                user.SetValue("UnitPrice", result);
            }
            return Json(userList);
        }

        public JsonResult LoadUserWorkHourFromHR(string belongYear, string belongMonth)
        {
            CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), 1), "无法再在该月份上报工时");

            var hrSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            string sql = @"select UserDeptID,UserDeptName,
                         UserID as UserInfo, 
                         UserName as UserInfoName,
                         ProjectID as CostUnitInfo,
                         ProjectName as CostUnitInfoName,
                         sum(isnull(WorkHourValue,0)) as WorkHour
                         from S_W_UserWorkHour
                         where BelongYear = {0} and BelongMonth = {1} group by UserID,UserName,ProjectID,ProjectName,UserDeptID,UserDeptName";
            var dt = hrSqlDB.ExecuteDataTable(string.Format(sql, belongYear, belongMonth));
            var list = FormulaHelper.DataTableToListDic(dt);
            var costCodeList = list.GroupBy(a => new { CostUnitInfo = a.GetValue("CostUnitInfo"), CostUnitInfoName = a.GetValue("CostUnitInfoName") }).Select(a => a.Key);

            foreach (var item in costCodeList)
            {
                var costUnitDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName from S_EP_CostUnit
                                                             inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID 
                                                             where S_EP_CostUnit.ID = '" + item.CostUnitInfo + "'");
                if (costUnitDT.Rows.Count == 0)
                {
                    continue;
                }
                DataRow dr = costUnitDT.Rows[0];

                var workHourSubmitDic = new Dictionary<string, object>();
                workHourSubmitDic.SetValue("ID", FormulaHelper.CreateGuid());
                workHourSubmitDic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                workHourSubmitDic.SetValue("CreateUser", CurrentUserInfo.UserName);
                workHourSubmitDic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                workHourSubmitDic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                workHourSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourSubmitDic.SetValue("ModifyDate", DateTime.Now);

                workHourSubmitDic.SetValue("FlowPhase", "End");
                workHourSubmitDic.SetValue("CostUnitID", dr["ID"]);
                workHourSubmitDic.SetValue("CostUnitInfo", dr["ID"]);
                workHourSubmitDic.SetValue("CostUnitInfoName", dr["Name"]);
                workHourSubmitDic.SetValue("Code", dr["Code"]);
                workHourSubmitDic.SetValue("ChargerUser", dr["ChargerUser"]);
                workHourSubmitDic.SetValue("ChargerUserName", dr["ChargerUserName"]);
                workHourSubmitDic.SetValue("ChargerDept", dr["ChargerDept"]);
                workHourSubmitDic.SetValue("ChargerDeptName", dr["ChargerDeptName"]);
                workHourSubmitDic.SetValue("BelongYear", belongYear);
                workHourSubmitDic.SetValue("BelongMonth", belongMonth);
                workHourSubmitDic.SetValue("State", "Finish");

                var detailList = list.Where(a => a.GetValue("CostUnitInfo") == item.CostUnitInfo);

                var index = 0;
                var detailDicList = new List<Dictionary<string, object>>();
                foreach (var detail in detailList)
                {
                    var workHourSubmitDetailDic = new Dictionary<string, object>();
                    detailDicList.Add(workHourSubmitDetailDic);

                    workHourSubmitDetailDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourSubmitDetailDic.SetValue("S_EP_WorkHourSubmitID", workHourSubmitDic.GetValue("ID"));
                    workHourSubmitDetailDic.SetValue("SortIndex", index);
                    workHourSubmitDetailDic.SetValue("UserInfo", detail["UserInfo"]);
                    workHourSubmitDetailDic.SetValue("UserInfoName", detail["UserInfoName"]);
                    workHourSubmitDetailDic.SetValue("WorkHour", detail["WorkHour"]);
                    workHourSubmitDetailDic.SetValue("UserDeptInfo", detail["UserDeptID"]);
                    workHourSubmitDetailDic.SetValue("UserDeptInfoName", detail["UserDeptName"]);
                    index++;
                }
                workHourSubmitDic.SetValue("WorkHourDetail", JsonHelper.ToJson(detailDicList));
                workHourSubmitDic.InsertDB(this.SQLDB, "S_EP_WorkHourSubmit", workHourSubmitDic.GetValue("ID"));
                foreach (var detailDic in detailDicList)
                {
                    detailDic.InsertDB(this.SQLDB, "S_EP_WorkHourSubmit_WorkHourDetail");
                }

                S_EP_WorkHourSubmit submit = new S_EP_WorkHourSubmit(workHourSubmitDic);
                submit.Push();
            }

            return Json("");
        }

        public JsonResult GetUserWorkHourFromHR(string BelongYear, string BelongMonth, string CostUnitInfo)
        {
            var hrSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            string sql = @"select 
                         UserID as UserInfo, 
                         UserName as UserInfoName,
                         UserDeptID as UserDeptInfo,
                         UserDeptName as UserDeptInfoName,
                         sum(isnull(WorkHourValue,0)) as WorkHour
                         from S_W_UserWorkHour where BelongYear = {0} and BelongMonth = {1} and ProjectID = '{2}'
                         group by UserID,UserName,UserDeptID,UserDeptName";
            var dt = hrSqlDB.ExecuteDataTable(string.Format(sql, BelongYear, BelongMonth, CostUnitInfo));
            return Json(dt);
        }

        public JsonResult ValidateRevert(string Data)
        {
            var item = JsonHelper.ToObject(Data);
            var belongYear = item.GetValue("BelongYear");
            var belongMonth = item.GetValue("BelongMonth");
            if (String.IsNullOrEmpty(belongYear)) throw new Formula.Exceptions.BusinessValidationException("没有指定年份的工时填报单，无法撤销");
            if (String.IsNullOrEmpty(belongMonth)) throw new Formula.Exceptions.BusinessValidationException("没有指定月份的工时填报单，无法撤销");
            CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), 1), "无法撤销工时填报信息");

            //已经计算过单价情况下(存在CostInfo)不能撤销
            string costInfoExistSql = @"select count(*) from S_EP_WorkHourUser 
                                      inner join S_EP_CostInfo on S_EP_CostInfo.RelateID = S_EP_WorkHourUser.ID
                                      inner join S_EP_WorkHourDepartSubmit_WorkHourDetail on S_EP_WorkHourDepartSubmit_WorkHourDetail.ID = S_EP_WorkHourUser.RelateID
                                      where S_EP_WorkHourDepartSubmit_WorkHourDetail.S_EP_WorkHourDepartSubmitID = '{0}'";
            var countObj = this.SQLDB.ExecuteScalar(string.Format(costInfoExistSql, item.GetValue("ID")));
            if (countObj != null && (int)countObj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("工时已经计入人工成本无法撤销");
            }

            return Json("");
        }

        public JsonResult CheckLockAccount(string belongYear, string belongMonth)
        {
            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_LockAccount WHERE BelongYear={0} and BelongMonth={1} and State='Finish'", belongYear, belongMonth));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经关账锁定，无法导入工时信息", belongYear, belongMonth));
            }
            return Json("true");
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

            var costCodeList = list.Select(a => a.GetValue("Code").Trim()).Distinct();
            var baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            foreach (var item in costCodeList)
            {
                var costUnitDT = this.SQLDB.ExecuteDataTable(@"select S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName from S_EP_CostUnit
                                                             inner join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID 
                                                             where S_EP_CostUnit.Code = '" + item.Trim() + "'");
                if (costUnitDT.Rows.Count == 0)
                {
                    continue;
                }
                DataRow dr = costUnitDT.Rows[0];

                var workHourSubmitDic = new Dictionary<string, object>();
                workHourSubmitDic.SetValue("ID", FormulaHelper.CreateGuid());
                workHourSubmitDic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                workHourSubmitDic.SetValue("CreateUser", CurrentUserInfo.UserName);
                workHourSubmitDic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                workHourSubmitDic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                workHourSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourSubmitDic.SetValue("ModifyDate", DateTime.Now);

                workHourSubmitDic.SetValue("FlowPhase", "End");
                workHourSubmitDic.SetValue("CostUnitID", dr["ID"]);
                workHourSubmitDic.SetValue("CostUnitInfo", dr["ID"]);
                workHourSubmitDic.SetValue("CostUnitInfoName", dr["Name"]);
                workHourSubmitDic.SetValue("Code", dr["Code"]);
                workHourSubmitDic.SetValue("ChargerUser", dr["ChargerUser"]);
                workHourSubmitDic.SetValue("ChargerUserName", dr["ChargerUserName"]);
                workHourSubmitDic.SetValue("ChargerDept", dr["ChargerDept"]);
                workHourSubmitDic.SetValue("ChargerDeptName", dr["ChargerDeptName"]);
                workHourSubmitDic.SetValue("BelongYear", belongYear);
                workHourSubmitDic.SetValue("BelongMonth", belongMonth);
                workHourSubmitDic.SetValue("State", "Finish");

                var detailList = list.Where(a => a.GetValue("Code") == item);

                var index = 0;
                var detailDicList = new List<Dictionary<string, object>>();
                foreach (var detail in detailList)
                {
                    var userInfoDT = baseHelper.ExecuteDataTable("select * from S_A_User where Code = '" + detail.GetValue("UserInfoCode").Trim() + "'");
                    if (userInfoDT.Rows.Count == 0)
                    {
                        continue;
                    }
                    DataRow userDr = userInfoDT.Rows[0];

                    var workHourSubmitDetailDic = new Dictionary<string, object>();
                    detailDicList.Add(workHourSubmitDetailDic);

                    workHourSubmitDetailDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourSubmitDetailDic.SetValue("S_EP_WorkHourSubmitID", workHourSubmitDic.GetValue("ID"));
                    workHourSubmitDetailDic.SetValue("SortIndex", index);
                    workHourSubmitDetailDic.SetValue("UserInfo", userDr["ID"]);
                    workHourSubmitDetailDic.SetValue("UserInfoName", userDr["Name"]);
                    workHourSubmitDetailDic.SetValue("WorkHour", detail["WorkHour"]);
                    workHourSubmitDetailDic.SetValue("UserDeptInfo", userDr["DeptID"]);
                    workHourSubmitDetailDic.SetValue("UserDeptInfoName", userDr["DeptName"]);
                    index++;
                }
                workHourSubmitDic.SetValue("WorkHourDetail", JsonHelper.ToJson(detailDicList));
                workHourSubmitDic.InsertDB(this.SQLDB, "S_EP_WorkHourSubmit", workHourSubmitDic.GetValue("ID"));
                foreach (var detailDic in detailDicList)
                {
                    detailDic.InsertDB(this.SQLDB, "S_EP_WorkHourSubmit_WorkHourDetail");
                }

                S_EP_WorkHourSubmit submit = new S_EP_WorkHourSubmit(workHourSubmitDic);
                submit.Push();
            }

            return Json("Success");
        }
    }
}
