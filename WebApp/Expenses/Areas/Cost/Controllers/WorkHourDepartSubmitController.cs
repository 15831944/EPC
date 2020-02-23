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
    public class WorkHourDepartSubmitController : ExpensesFormController<S_EP_WorkHourDepartSubmit>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                CostFO.ValidatePeriodIsClosed(new DateTime(Convert.ToInt32(dic["BelongYear"]), Convert.ToInt32(dic["BelongMonth"]), 1), "无法再在该月份上报工时");
                //不能重复上报
                //if (String.IsNullOrEmpty(dic.GetValue("DepartInfo")))
                //{
                //    throw new Formula.Exceptions.BusinessValidationException("必须指定部门");
                //}
                //string submitSql = @"select count(1) from S_EP_WorkHourDepartSubmit where BelongYear = {0} and BelongMonth = {1} and DepartInfo = '{2}' and State != 'Removed'";
                //var resObj = this.SQLDB.ExecuteScalar(string.Format(submitSql, dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"), dic.GetValue("DepartInfo")));
                //if (resObj != null && (int)resObj > 0)
                //{
                //    throw new Formula.Exceptions.BusinessValidationException(string.Format("该部门在{0}年{1}月已经上报过，不能重复上报"));
                //}
            }
        }

        protected override void OnFlowEnd(S_EP_WorkHourDepartSubmit data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
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
                var actualResult = CostFO.GetUserUnitPrice(user.GetValue("UserInfo"), Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth),true);
                user.SetValue("UnitPrice", result);
                user.SetValue("ActualUnitPrice", actualResult);
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
            var departNameList = list.GroupBy(a => new { DepartID = a.GetValue("UserDeptID"), DepartName = a.GetValue("UserDeptName") }).Select(a => a.Key);

            foreach (var item in departNameList)
            {
                var workHourDepartSubmitDic = new Dictionary<string, object>();
                workHourDepartSubmitDic.SetValue("ID", FormulaHelper.CreateGuid());
                workHourDepartSubmitDic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                workHourDepartSubmitDic.SetValue("CreateUser", CurrentUserInfo.UserName);
                workHourDepartSubmitDic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                workHourDepartSubmitDic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                workHourDepartSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourDepartSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourDepartSubmitDic.SetValue("ModifyDate", DateTime.Now);

                workHourDepartSubmitDic.SetValue("FlowPhase", "End");
                workHourDepartSubmitDic.SetValue("DepartInfo", item.DepartID);
                workHourDepartSubmitDic.SetValue("DepartInfoName", item.DepartName);
                workHourDepartSubmitDic.SetValue("BelongYear", belongYear);
                workHourDepartSubmitDic.SetValue("BelongMonth", belongMonth);
                workHourDepartSubmitDic.SetValue("State", "Finish");

                var detailList = list.Where(a => a.GetValue("UserDeptID") == item.DepartID);

                var index = 0;
                var detailDicList = new List<Dictionary<string, object>>();
                foreach (var detail in detailList)
                {
                    var workHourDepartSubmitDetailDic = new Dictionary<string, object>();
                    detailDicList.Add(workHourDepartSubmitDetailDic);

                    workHourDepartSubmitDetailDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourDepartSubmitDetailDic.SetValue("S_EP_WorkHourDepartSubmitID", workHourDepartSubmitDic.GetValue("ID"));
                    workHourDepartSubmitDetailDic.SetValue("SortIndex", index);
                    workHourDepartSubmitDetailDic.SetValue("UserInfo", detail["UserInfo"]);
                    workHourDepartSubmitDetailDic.SetValue("UserInfoName", detail["UserInfoName"]);
                    workHourDepartSubmitDetailDic.SetValue("WorkHour", detail["WorkHour"]);
                    workHourDepartSubmitDetailDic.SetValue("CostUnitInfo", detail["CostUnitInfo"]);
                    workHourDepartSubmitDetailDic.SetValue("CostUnitInfoName", detail["CostUnitInfoName"]);
                    index++;
                }
                workHourDepartSubmitDic.SetValue("WorkHourDetail", JsonHelper.ToJson(detailDicList));
                workHourDepartSubmitDic.InsertDB(this.SQLDB, "S_EP_WorkHourDepartSubmit", workHourDepartSubmitDic.GetValue("ID"));
                foreach (var detailDic in detailDicList)
                {
                    detailDic.InsertDB(this.SQLDB, "S_EP_WorkHourDepartSubmit_WorkHourDetail");
                }

                S_EP_WorkHourDepartSubmit submit = new S_EP_WorkHourDepartSubmit(workHourDepartSubmitDic);
                submit.Push();
            }
            return Json("");
        }

        public JsonResult GetUserWorkHourFromHR(string BelongYear, string BelongMonth, string DepartInfo)
        {
            var hrSqlDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            string sql = @"select 
                         UserID as UserInfo, 
                         UserName as UserInfoName,
                         ProjectID as CostUnitInfo,
                         ProjectName as CostUnitInfoName,
                         sum(isnull(WorkHourValue,0)) as WorkHour
                         from S_W_UserWorkHour where BelongYear = {0} and BelongMonth = {1} and UserDeptID = '{2}'
                         group by UserID,UserName,ProjectID,ProjectName";
            var dt = hrSqlDB.ExecuteDataTable(string.Format(sql, BelongYear, BelongMonth, DepartInfo));
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
                if (e.FieldName == "DepartName" && String.IsNullOrEmpty(e.Value.Trim()))
                {
                    if (String.IsNullOrEmpty(e.Value.Trim()))
                    {
                        e.IsValid = false;
                        e.ErrorText = "部门名称不能为空";
                    }
                    else
                    {
                        var obj = baseHelper.ExecuteScalar("select count(*) from S_A_Org where Name = '" + e.Value.Trim() + "'");
                        if (obj == null || (int)obj == 0)
                        {
                            e.ErrorText = "未找到该部门";
                            e.IsValid = false;
                        }
                    }
                }
                else if (e.FieldName == "Code")
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

            var departNameList = list.Select(a => a.GetValue("DepartName").Trim()).Distinct();
            var baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            foreach (var item in departNameList)
            {
                var idObj = baseHelper.ExecuteScalar("select top 1 ID from S_A_Org where Name = '" + item + "'");
                if (idObj == null || idObj == DBNull.Value)
                {
                    continue;
                }

                var workHourDepartSubmitDic = new Dictionary<string, object>();
                workHourDepartSubmitDic.SetValue("ID", FormulaHelper.CreateGuid());
                workHourDepartSubmitDic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                workHourDepartSubmitDic.SetValue("CreateUser", CurrentUserInfo.UserName);
                workHourDepartSubmitDic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                workHourDepartSubmitDic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                workHourDepartSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourDepartSubmitDic.SetValue("CreateDate", DateTime.Now);
                workHourDepartSubmitDic.SetValue("ModifyDate", DateTime.Now);

                workHourDepartSubmitDic.SetValue("FlowPhase", "End");
                workHourDepartSubmitDic.SetValue("DepartInfo", idObj);
                workHourDepartSubmitDic.SetValue("DepartInfoName", item);
                workHourDepartSubmitDic.SetValue("BelongYear", belongYear);
                workHourDepartSubmitDic.SetValue("BelongMonth", belongMonth);
                workHourDepartSubmitDic.SetValue("State", "Finish");

                var detailList = list.Where(a => a.GetValue("DepartName") == item);

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

                    var costUnitDT = this.SQLDB.ExecuteDataTable(@"select * from S_EP_CostUnit where Code = '" + detail.GetValue("Code").Trim() + "'");
                    if (costUnitDT.Rows.Count == 0)
                    {
                        continue;
                    }
                    DataRow costUnitDr = costUnitDT.Rows[0];

                    var workHourDepartSubmitDetailDic = new Dictionary<string, object>();
                    detailDicList.Add(workHourDepartSubmitDetailDic);

                    workHourDepartSubmitDetailDic.SetValue("ID", FormulaHelper.CreateGuid());
                    workHourDepartSubmitDetailDic.SetValue("S_EP_WorkHourDepartSubmitID", workHourDepartSubmitDic.GetValue("ID"));
                    workHourDepartSubmitDetailDic.SetValue("SortIndex", index);
                    workHourDepartSubmitDetailDic.SetValue("UserInfo", userDr["ID"]);
                    workHourDepartSubmitDetailDic.SetValue("UserInfoName", userDr["Name"]);
                    workHourDepartSubmitDetailDic.SetValue("WorkHour", detail["WorkHour"]);
                    workHourDepartSubmitDetailDic.SetValue("CostUnitInfo", costUnitDr["ID"]);
                    workHourDepartSubmitDetailDic.SetValue("CostUnitInfoName", costUnitDr["Name"]);
                    index++;
                }
                workHourDepartSubmitDic.SetValue("WorkHourDetail", JsonHelper.ToJson(detailDicList));
                workHourDepartSubmitDic.InsertDB(this.SQLDB, "S_EP_WorkHourDepartSubmit", workHourDepartSubmitDic.GetValue("ID"));
                foreach (var detailDic in detailDicList)
                {
                    detailDic.InsertDB(this.SQLDB, "S_EP_WorkHourDepartSubmit_WorkHourDetail");
                }

                S_EP_WorkHourDepartSubmit submit = new S_EP_WorkHourDepartSubmit(workHourDepartSubmitDic);
                submit.Push();
            }

            return Json("Success");
        }
    }
}
