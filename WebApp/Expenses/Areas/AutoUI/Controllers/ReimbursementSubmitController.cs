using Base.Logic.Domain;
using Config;
using Config.Logic;
using Expenses.Areas.Cost.Controllers;
using Expenses.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Logic;

namespace Expenses.Areas.AutoUI.Controllers
{
    public class ReimbursementSubmitController : Expenses.Areas.Cost.Controllers.ReimbursementSubmitController
    {
        #region 流程相关
        string sqlFlowExecList = @"
select S_WF_InsTaskExec.ID as ID
,S_WF_InsTaskExec.CreateTime as CreateTime
,TaskUserID
,TaskUserName
,ExecUserID
,ExecUserName
,ExecTime
,ExecComment
,S_WF_InsTaskExec.Type as Type
,S_WF_InsTask.ID as TaskID
,TaskName
,TaskCategory
,TaskSubCategory
,SendTaskUserNames
,FlowName
,FlowCategory
,FlowSubCategory
,case when '{1}'='EN' then isnull(S_WF_InsDefStep.NameEN, S_WF_InsDefStep.Name) else S_WF_InsDefStep.Name end as StepName
,S_WF_InsDefStep.ID as StepID
,ExecRoutingIDs
,ExecRoutingName
,S_WF_InsFlow.InsDefFlowID
,S_WF_InsTask.DoBackRoutingID
,S_WF_InsTask.OnlyDoBack
,S_WF_InsTaskExec.InsTaskID
,S_WF_InsTaskExec.ApprovalInMobile
from S_WF_InsTaskExec
right join S_WF_InsTask on InsTaskID=S_WF_InsTask.ID
join S_WF_InsFlow on S_WF_InsTask.InsFlowId=S_WF_InsFlow.ID
join S_WF_InsDefStep on InsDefStepID=S_WF_InsDefStep.ID
where FormInstanceID='{0}' and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='')
order by isnull(S_WF_InsTaskExec.CreateTime,S_WF_InsTask.CreateTime),S_WF_InsTaskExec.ID
";

        public ActionResult TraceGrid()
        {
            return View();
        }

        public JsonResult GetFlowExecList(string id, MvcAdapter.QueryBuilder qb)
        {
            if (string.IsNullOrEmpty(id))
                return Json("[]");
            var LGID = FormulaHelper.GetCurrentLGID();
            string sql = string.Format(sqlFlowExecList, id, LGID);

            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Workflow");

            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            if (dt.Rows.Count == 0)
                return Json(dt);

            string insDefFlowID = dt.Rows[0]["InsDefFlowID"].ToString();
            sql = string.Format("select ID,Name from S_WF_InsDefRouting where InsDefFlowID='{0}'", insDefFlowID);
            DataTable dtRouting = sqlHelper.ExecuteDataTable(sql, qb);

            dt.Columns.Add("UseTime");
            dt.Columns.Add("TaskUserDept");

            var userService = FormulaHelper.GetService<IUserService>();

            foreach (DataRow row in dt.Rows)
            {
                string ExecRoutingIDs = row["ExecRoutingIDs"].ToString().Trim(',');
                if (!string.IsNullOrEmpty(ExecRoutingIDs) && row["ExecRoutingName"].ToString() == "")
                {
                    row["ExecRoutingName"] = dtRouting.AsEnumerable().SingleOrDefault(c => c["ID"].ToString() == ExecRoutingIDs.Split(',').LastOrDefault())["Name"];
                }
                //处理打回和直送操作的名称
                if (string.IsNullOrEmpty(ExecRoutingIDs) && row["ExecRoutingName"].ToString() == "" && row["ExecTime"].ToString() != "")
                {
                    if (row["Type"].ToString() == TaskExecType.Normal.ToString() || row["Type"].ToString() == TaskExecType.Delegate.ToString())
                    {
                        if (row["DoBackRoutingID"].ToString() != "")
                            row["ExecRoutingName"] = "驳回";
                        if (row["OnlyDoBack"].ToString() == "1")
                            row["ExecRoutingName"] = "送驳回人";
                    }
                    else if (row["Type"].ToString() == TaskExecType.Circulate.ToString())
                    {
                        row["ExecRoutingName"] = "阅毕";
                    }
                    else if (row["Type"].ToString() == TaskExecType.Ask.ToString())
                    {
                        row["ExecRoutingName"] = "阅毕";
                    }

                }

                string CreateTime = row["CreateTime"].ToString();
                string ExecTime = row["ExecTime"].ToString();
                if (!string.IsNullOrEmpty(ExecTime))
                {
                    if (!string.IsNullOrEmpty(row["ApprovalInMobile"].ToString()))
                    {
                        switch (row["ApprovalInMobile"].ToString())
                        {
                            case "1":
                                row["ApprovalInMobile"] = "";
                                break;
                            case "2":
                                row["ApprovalInMobile"] = "钉钉";
                                break;
                            case "3":
                                row["ApprovalInMobile"] = "移动通";
                                break;
                            case "5":
                                row["ApprovalInMobile"] = "微信";
                                break;
                            default:
                                row["ApprovalInMobile"] = "PC";
                                break;
                        }
                    }
                    else
                    {
                        row["ApprovalInMobile"] = "PC";
                    }
                    var span = DateTime.Parse(ExecTime) - DateTime.Parse(CreateTime);
                    row["UseTime"] = string.Format("{0}小时{1}分", span.Days * 24 + span.Hours, span.Minutes == 0 ? 1 : span.Minutes);
                }
                if (row["TaskUserID"].ToString() != "")
                {
                    var ogUser = userService.GetUserInfoByID(row["TaskUserID"].ToString());
                    if (ogUser != null)
                    {
                        row["TaskUserDept"] = ogUser.UserOrgName;
                    }
                }
                else
                {
                    row["TaskUserName"] = "";
                    row["ExecUserName"] = "";
                }
                //操作意见取最新回复
                var entities = FormulaHelper.GetEntities<BaseEntities>();
                string routingID = row["ID"].ToString();
                string execComment = row["ExecComment"].ToString();
                var msgBody = entities.Set<S_S_MsgBody>().Where(c => c.FlowMsgID == routingID).OrderByDescending(c => c.SendTime);
                if (msgBody.Count() > 0)
                {
                    execComment = msgBody.First().Content;
                }
                row["ExecComment"] = execComment;
            }

            return Json(dt);
        }
        #endregion
        #region 浮动窗体内容
        #region 个人
        public ActionResult ApplyUserRelateInfo()
        {
            string applyUserID = GetQueryString("ApplyUserID");
            var hrDB = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var officeDB = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
            var currentUserDT = hrDB.ExecuteDataTable(string.Format(@"select S_A_User.*,S_A_UserImg.Picture,unitPrice.ResourceCode from S_A_User 
left join S_A_UserImg on S_A_UserImg.UserID = S_A_User.ID
left join 
(
select unitPrice.* from EPM_HR..S_W_UserUnitPrice unitPrice inner join
(select max(ID) MaxID, UserID 
from EPM_HR..S_W_UserUnitPrice where PriceType = 'Budget' group by UserID) latestUnitPrice
on unitPrice.ID = latestUnitPrice.MaxID
) unitPrice
on unitPrice.UserID = S_A_User.ID where S_A_User.ID = '{0}'", applyUserID));

            if(currentUserDT.Rows.Count > 0)
            {
                var currentUserDic = FormulaHelper.DataRowToDic(currentUserDT.Rows[0]);
                 //人名
                ViewBag.UserName = currentUserDic.GetValue("Name");
                //头像
                ViewBag.UserPic = currentUserDic.GetValue("Picture");
                //资源等级
                ViewBag.UserResourceCode = currentUserDic.GetValue("ResourceCode");
                //个人借款
                var loanValueObj = officeDB.ExecuteScalar(string.Format(@"select sum(isnull(FactValue,0)) FactValue,ApplyUser
                                         from T_F_LoanApply where ApplyUser = '{0}' and FlowPhase = 'End' and ApplyUser = '{0}' group by ApplyUser", applyUserID));

                ViewBag.UserLoanValue = "0";
                if (loanValueObj != null && loanValueObj != DBNull.Value)
                {
                    ViewBag.UserLoanValue = string.Format("{0:N}",((decimal)loanValueObj));
                }

                //未还金额
                var restLoanValueObj = officeDB.ExecuteScalar(string.Format(@"select sum(isnull(FactValue,0)) FactValue,ApplyUser
                                         from T_F_LoanApply where ApplyUser = '{0}' and FlowPhase = 'End' and ApplyUser = '{0}'
                                         and (TravelApply is null or TravelApply = '') group by ApplyUser", applyUserID));

                ViewBag.UserRestLoanValue = "0";
                if (restLoanValueObj != null && restLoanValueObj != DBNull.Value)
                {
                    ViewBag.UserRestLoanValue = string.Format("{0:N}",((decimal)restLoanValueObj));
                }

                //当月借款
                var monthLoanValueObj = officeDB.ExecuteScalar(string.Format(@"select sum(isnull(FactValue,0)) FactValue,ApplyUser
                                         from T_F_LoanApply where ApplyUser = '{0}' and FlowPhase = 'End' and ApplyUser = '{0}' 
                                         and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate) group by ApplyUser", applyUserID));

                ViewBag.UserMonthLoanValue = "0";
                if (monthLoanValueObj != null && monthLoanValueObj != DBNull.Value)
                {
                    ViewBag.UserMonthLoanValue = string.Format("{0:N}",((decimal)monthLoanValueObj));
                }

                //当月冲账
                var monthCzValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and ReimbursementClass = '冲账' and year(getdate()) = year(ApplyDate)
                                      and month(getdate()) = month(ApplyDate)) tmp where ApplyUser = '{0}'", applyUserID));

                ViewBag.UserMonthCzValue = "0";
                if (monthCzValueObj != null && monthCzValueObj != DBNull.Value)
                {
                    ViewBag.UserMonthCzValue = string.Format("{0:N}",((decimal)monthCzValueObj));
                }

                //当月报销
                var monthBxValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      and month(getdate()) = month(ApplyDate)) tmp where ApplyUser = '{0}'", applyUserID));

                ViewBag.UserMonthBxValue = "0";
                if (monthBxValueObj != null && monthBxValueObj != DBNull.Value)
                {
                    ViewBag.UserMonthBxValue = string.Format("{0:N}",((decimal)monthBxValueObj));
                }

                //当年借款
                var yearLoanValueObj = officeDB.ExecuteScalar(string.Format(@"select sum(isnull(FactValue,0)) FactValue,ApplyUser
                                         from T_F_LoanApply where ApplyUser = '{0}' and FlowPhase = 'End' and ApplyUser = '{0}' 
                                         and year(getdate()) = year(ApplyDate) group by ApplyUser", applyUserID));

                ViewBag.UserYearLoanValue = "0";
                if (yearLoanValueObj != null && yearLoanValueObj != DBNull.Value)
                {
                    ViewBag.UserYearLoanValue = string.Format("{0:N}",((decimal)yearLoanValueObj));
                }
                //当年冲账
                var yearCzValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and ReimbursementClass = '冲账' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ApplyUser = '{0}'", applyUserID));

                ViewBag.UserYearCzValue = "0";
                if (yearCzValueObj != null && yearCzValueObj != DBNull.Value)
                {
                    ViewBag.UserYearCzValue = string.Format("{0:N}",((decimal)yearCzValueObj));
                }

                //当年报销
                var yearBxValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ApplyUser = '{0}'", applyUserID));

                ViewBag.UserYearBxValue = "0";
                if (yearBxValueObj != null && yearBxValueObj != DBNull.Value)
                {
                    ViewBag.UserYearBxValue = string.Format("{0:N}",((decimal)yearBxValueObj));
                }
            }
           
            return View();
        }

        public JsonResult GetApplyUserData()
        {
            string applyUserID = GetQueryString("ApplyUserID");
            var chartData = GetApplyUserReimbursementChartData(applyUserID);
            var pieData = GetApplyUserSubjectPieData(applyUserID);
            return Json(new { chartData = JsonHelper.ToJson(chartData), pieData = JsonHelper.ToJson(pieData) });
        }

        /// <summary>
        /// 月度费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string,object> GetApplyUserReimbursementChartData(string applyUserID)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(ApplyValue) ApplyValue, BelongMonth from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue,month(ApplyDate) BelongMonth from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) 
                                      ) tmp where ApplyUser = '{0}' group by BelongMonth", applyUserID));

            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("BelongMonth",typeof(string)));
            newDt.Columns.Add(new DataColumn("ApplyValue", typeof(decimal)));
            for (int i = 1; i <= 12; i++)
            {
                var resRow = newDt.NewRow();
                resRow["BelongMonth"] = i.ToString() + "月";
                resRow["ApplyValue"] = 0;

                var row = sourceDt.Select("BelongMonth=" + i).FirstOrDefault();
                if (row != null && row["ApplyValue"] != null && row["ApplyValue"] != DBNull.Value)
                {
                    resRow["ApplyValue"] = (decimal)row["ApplyValue"];
                }
                newDt.Rows.Add(resRow);
            }

            var columnChart = HighChartHelper.CreateColumnChart("", newDt, "BelongMonth", new string[] { "报销费用" }, new string[] { "ApplyValue" });
             return columnChart.Render();
        }

        /// <summary>
        /// 科目费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetApplyUserSubjectPieData(string applyUserID)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(isnull(ApplyValue,0)) valueField, SubjectName nameField from S_EP_ReimbursementApply_Details detail
                                                       left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                                                       where apply.FlowPhase = 'End' and ApplyUser = '{0}' group by SubjectName", applyUserID));

            var chart = HighChartHelper.CreatePieChart("", "", sourceDt);
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }
        /// <summary>
        /// 当月报销明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetApplyUserReimbursementDetail(string applyUserID, QueryBuilder qb)
        {
            string sql = @"select detail.* from S_EP_ReimbursementApply_Details detail
                         left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                         where apply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate) and ApplyUser = '{0}'";
            DataTable dt = this.SQLDB.ExecuteDataTable(string.Format(sql, applyUserID), qb);      
            return Json(dt);
        }
        #endregion
        #region 部门
        public ActionResult ApplyUserDepartInfo()
        {
            string applyDeptID = GetQueryString("ApplyDeptID");
            if (!string.IsNullOrEmpty(applyDeptID))
            {
                //当月累计费用
                var monthBxValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      and month(getdate()) = month(ApplyDate)) tmp where ApplyDept = '{0}'", applyDeptID));

                ViewBag.DeptMonthBxValue = "0";
                if (monthBxValueObj != null && monthBxValueObj != DBNull.Value)
                {
                    ViewBag.DeptMonthBxValue = string.Format("{0:N}",((decimal)monthBxValueObj));
                }

                //当年累计费用
                var yearBxValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(ApplyValue) ApplyValue from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ApplyDept = '{0}'", applyDeptID));

                ViewBag.DeptYearBxValue = "0";
                if (yearBxValueObj != null && yearBxValueObj != DBNull.Value)
                {
                    ViewBag.DeptYearBxValue = string.Format("{0:N}",((decimal)yearBxValueObj));
                }
            }
            
            return View();
        }

        public JsonResult GetDeptData()
        {
            string applyDeptID = GetQueryString("ApplyDeptID");
            var chartData = GetDeptReimbursementChartData(applyDeptID);
            var pieData = GetDeptSubjectPieData(applyDeptID);
            return Json(new { chartData = JsonHelper.ToJson(chartData), pieData = JsonHelper.ToJson(pieData) });
        }

        /// <summary>
        /// 月度费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetDeptReimbursementChartData(string applyDeptID)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(ApplyValue) ApplyValue,BelongMonth from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue,month(ApplyDate) BelongMonth from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ApplyDept = '{0}' group by BelongMonth", applyDeptID));

            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("BelongMonth", typeof(string)));
            newDt.Columns.Add(new DataColumn("ApplyValue", typeof(decimal)));
            for (int i = 1; i <= 12; i++)
            {
                var resRow = newDt.NewRow();
                resRow["BelongMonth"] = i.ToString() + "月";
                resRow["ApplyValue"] = 0;

                var row = sourceDt.Select("BelongMonth=" + i).FirstOrDefault();
                if (row != null && row["ApplyValue"] != null && row["ApplyValue"] != DBNull.Value)
                {
                    resRow["ApplyValue"] = (decimal)row["ApplyValue"];
                }
                newDt.Rows.Add(resRow);
            }

            var columnChart = HighChartHelper.CreateColumnChart("", newDt, "BelongMonth", new string[] { "报销费用" }, new string[] { "ApplyValue" });
            return columnChart.Render();
        }

        /// <summary>
        /// 科目费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetDeptSubjectPieData(string applyDeptID)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(isnull(ApplyValue,0)) valueField, SubjectName nameField from S_EP_ReimbursementApply_Details detail
                                                       left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                                                       where apply.FlowPhase = 'End' and ApplyDept = '{0}' group by SubjectName", applyDeptID));

            var chart = HighChartHelper.CreatePieChart("", "", sourceDt);
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }

        /// <summary>
        /// 当月报销明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDeptReimbursementDetail(string applyDeptID, QueryBuilder qb)
        {
            string sql = @"select detail.* from S_EP_ReimbursementApply_Details detail
                         left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                         where apply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate) and ApplyDept = '{0}'";
            DataTable dt = this.SQLDB.ExecuteDataTable(string.Format(sql, applyDeptID), qb);
            return Json(dt);
        }
        #endregion
        #region 公司
        public ActionResult CompanyRelateInfo()
        {
            //当月收款
            string monthReceiptSql = @"select sum(isnull(Amount,0)) Amount from S_C_Receipt where year(getdate()) = BelongYear and month(getdate()) = BelongMonth";
            var monthReceiptObj = this.SQLDB.ExecuteScalar(monthReceiptSql);

            ViewBag.MonthReceiptValue = "0";
            if (monthReceiptObj != null && monthReceiptObj != DBNull.Value)
            {
                ViewBag.MonthReceiptValue = string.Format("{0:N}",((decimal)monthReceiptObj));
            }
            
            //当月合同额
            string monthContractSql = @"select sum(isnull(ContractValue,0)) ContractValue from S_C_ManageContract where year(getdate()) = year(SignDate) and month(getdate()) = month(SignDate)";
            var monthContractObj = this.SQLDB.ExecuteScalar(monthContractSql);

            ViewBag.MonthContractValue = "0";
            if (monthContractObj != null && monthContractObj != DBNull.Value)
            {
                ViewBag.MonthContractValue = string.Format("{0:N}",((decimal)monthContractObj));
            }

            //当年收款
            string yearReceiptSql = @"select sum(isnull(Amount,0)) Amount from S_C_Receipt where year(getdate()) = BelongYear";
            var yearReceiptObj = this.SQLDB.ExecuteScalar(yearReceiptSql);

            ViewBag.YearReceiptValue = "0";
            if (yearReceiptObj != null && yearReceiptObj != DBNull.Value)
            {
                ViewBag.YearReceiptValue = string.Format("{0:N}",((decimal)yearReceiptObj));
            }

            //当年合同额
            string yearContractSql = @"select sum(isnull(ContractValue,0)) ContractValue from S_C_ManageContract where year(getdate()) = year(SignDate)";
            var yearContractObj = this.SQLDB.ExecuteScalar(yearContractSql);

            ViewBag.YearContractValue = "0";
            if (yearContractObj != null && yearContractObj != DBNull.Value)
            {
                ViewBag.YearContractValue = string.Format("{0:N}",((decimal)yearContractObj));
            }

            return View();
        }

        public JsonResult GetCompanyData()
        {
            var chartData = GetCompanyReimbursementChartData();
            var pieData = GetCompanySubjectPieData();
            return Json(new { chartData = JsonHelper.ToJson(chartData), pieData = JsonHelper.ToJson(pieData) });
        }

        /// <summary>
        /// 月度费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetCompanyReimbursementChartData()
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(ApplyValue) ApplyValue,BelongMonth from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue,month(ApplyDate) BelongMonth from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp group by BelongMonth"));

            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("BelongMonth", typeof(string)));
            newDt.Columns.Add(new DataColumn("ApplyValue", typeof(decimal)));
            for (int i = 1; i <= 12; i++)
            {
                var resRow = newDt.NewRow();
                resRow["BelongMonth"] = i.ToString() + "月";
                resRow["ApplyValue"] = 0;

                var row = sourceDt.Select("BelongMonth=" + i).FirstOrDefault();
                if (row != null && row["ApplyValue"] != null && row["ApplyValue"] != DBNull.Value)
                {
                    resRow["ApplyValue"] = (decimal)row["ApplyValue"];
                }
                newDt.Rows.Add(resRow);
            }

            var columnChart = HighChartHelper.CreateColumnChart("", newDt, "BelongMonth", new string[] { "报销费用" }, new string[] { "ApplyValue" });
            return columnChart.Render();
        }

        /// <summary>
        /// 科目费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetCompanySubjectPieData()
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(isnull(ApplyValue,0)) valueField, SubjectName nameField from S_EP_ReimbursementApply_Details detail
                                                       left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                                                       where apply.FlowPhase = 'End' group by SubjectName"));

            var chart = HighChartHelper.CreatePieChart("", "", sourceDt);
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }

        /// <summary>
        /// 当月报销明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCompanyReimbursementDetail(QueryBuilder qb)
        {
            string sql = @"select detail.* from S_EP_ReimbursementApply_Details detail
                         left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                         where apply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate)";
            var dt = this.SQLDB.ExecuteDataTable(string.Format(sql), qb);
            return Json(dt);
        }
        #endregion 
        #region 项目
        public ActionResult ProjectRelateInfo()
        {
            return View();
        }

        public JsonResult GetProjectData()
        {
            string projectInfo = GetQueryString("ProjectInfo");
            var chartData = GetProjectReimbursementChartData(projectInfo);
            var pieData = GetProjectSubjectPieData(projectInfo);
            return Json(new {
                chartData = JsonHelper.ToJson(chartData),
                pieData = JsonHelper.ToJson(pieData) });
        }

        /// <summary>
        /// 项目预算执行情况明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProjectBudgetDetail(string projectInfo, QueryBuilder qb)
        {
            string sql = @"select detail.*,(isnull(detail.TotalValue,0) - isnull(detail.CostValue,0)) RestValue
from S_EP_BudgetVersion_Detail detail inner join  
(select S_EP_BudgetVersion.* from S_EP_BudgetVersion inner join
(select max(VersionNumber) maxVersionNumber,BudgetUnitID from S_EP_BudgetVersion where FlowPhase = 'End' group by BudgetUnitID) maxVersion
on S_EP_BudgetVersion.VersionNumber = maxVersion.maxVersionNumber and S_EP_BudgetVersion.BudgetUnitID = maxVersion.BudgetUnitID) budgetVersion
on budgetVersion.ID = detail.S_EP_BudgetVersionID
left join S_EP_CBSNode on budgetVersion.ProjectInfoID = S_EP_CBSNode.ProjectInfoID
left join S_EP_CostUnit on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID
where S_EP_CostUnit.ID = '{0}'";
            var dt = this.SQLDB.ExecuteDataTable(string.Format(sql, projectInfo), qb);
            return Json(dt);
        }

        /// <summary>
        /// 月度费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetProjectReimbursementChartData(string projectInfo)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(ApplyValue) ApplyValue,BelongMonth from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue,month(ApplyDate) BelongMonth from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ProjectInfo = '{0}' group by BelongMonth", projectInfo));

            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("BelongMonth", typeof(string)));
            newDt.Columns.Add(new DataColumn("ApplyValue", typeof(decimal)));
            for (int i = 1; i <= 12; i++)
            {
                var resRow = newDt.NewRow();
                resRow["BelongMonth"] = i.ToString() + "月";
                resRow["ApplyValue"] = 0;

                var row = sourceDt.Select("BelongMonth=" + i).FirstOrDefault();
                if (row != null && row["ApplyValue"] != null && row["ApplyValue"] != DBNull.Value)
                {
                    resRow["ApplyValue"] = (decimal)row["ApplyValue"];
                }
                newDt.Rows.Add(resRow);
            }

            var columnChart = HighChartHelper.CreateColumnChart("", newDt, "BelongMonth", new string[] { "报销费用" }, new string[] { "ApplyValue" });
            return columnChart.Render();
        }

        /// <summary>
        /// 科目费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetProjectSubjectPieData(string projectInfo)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(isnull(ApplyValue,0)) valueField, SubjectName nameField from S_EP_ReimbursementApply_Details detail
                                                       left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                                                       where apply.FlowPhase = 'End' and ProjectInfo = '{0}' group by SubjectName", projectInfo));

            var chart = HighChartHelper.CreatePieChart("", "", sourceDt);
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }

        /// <summary>
        /// 当月报销明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProjectReimbursementDetail(string projectInfo, QueryBuilder qb)
        {
            string sql = @"select detail.* from S_EP_ReimbursementApply_Details detail
                         left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                         where apply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate) and ProjectInfo = '{0}'";
            var dt = this.SQLDB.ExecuteDataTable(string.Format(sql, projectInfo), qb);
            return Json(dt);
        }
        #endregion
        #region 合同
        public ActionResult ContractRelateInfo()
        {
            string projectInfo = GetQueryString("ProjectInfo");//成本单元ID
            string sql = @"select ContractInfoID,S_EP_CBSInfo.ID CBSInfoID from S_EP_CBSInfo
                        left join S_EP_CostUnit on S_EP_CBSInfo.ID = S_EP_CostUnit.CBSInfoID where S_EP_CostUnit.ID = '" + projectInfo + "'";
            var tmpDt = this.SQLDB.ExecuteDataTable(sql);
            ViewBag.ContractInfoID = "";
            if (tmpDt.Rows.Count > 0)
            {
                var row = tmpDt.Rows[0];
                string contractID = row["ContractInfoID"].ToString();
                ViewBag.ContractInfoID = contractID;

                var costUnitDt = this.SQLDB.ExecuteDataTable("select * from S_EP_CostUnit where CBSInfoID = '" + row["CBSInfoID"].ToString() + "'");
                var arr = costUnitDt.AsEnumerable().Select(a => a["ID"].ToString());
                ViewBag.CostUnitArr = string.Join("','", arr);

                //合同额
                var contractAmountValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select isnull(ContractRMBAmount,0) ContractRMBAmount
                                         from S_C_ManageContract where ID ='{0}'", contractID.ToString()));

                ViewBag.ContractAmountValue = "0";
                if (contractAmountValueObj != null && contractAmountValueObj != DBNull.Value)
                {
                    ViewBag.ContractAmountValue = string.Format("{0:N}",((decimal)contractAmountValueObj));
                }

                //当月收款
                var monthContractReceiptValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(isnull(Amount,0)) Amount from S_C_Receipt
                where BelongYear = year(getdate()) and BelongMonth = month(getdate()) and ContractInfoID = '{0}'", contractID.ToString()));

                ViewBag.MonthContractReceiptValue = "0";
                if (monthContractReceiptValueObj != null && monthContractReceiptValueObj != DBNull.Value)
                {
                    ViewBag.MonthContractReceiptValue = string.Format("{0:N}",((decimal)monthContractReceiptValueObj));
                }

                //当年收款
                var yearContractReceiptValueObj = this.SQLDB.ExecuteScalar(string.Format(@"select sum(isnull(Amount,0)) Amount from S_C_Receipt
                where BelongYear = year(getdate()) and ContractInfoID = '{0}'", contractID.ToString()));

                ViewBag.YearContractReceiptValue = "0";
                if (yearContractReceiptValueObj != null && yearContractReceiptValueObj != DBNull.Value)
                {
                    ViewBag.YearContractReceiptValue = string.Format("{0:N}",((decimal)yearContractReceiptValueObj));
                }
            }            

            return View();
        }

        public JsonResult GetContractData()
        {
            string costUnitArr = GetQueryString("CostUnitArr");
            var chartData = GetContractReimbursementChartData(costUnitArr);
            var pieData = GetContractSubjectPieData(costUnitArr);
            return Json(new
            {
                chartData = JsonHelper.ToJson(chartData),
                pieData = JsonHelper.ToJson(pieData)
            });
        }

        /// <summary>
        /// 月度费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetContractReimbursementChartData(string costUnitArr)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(ApplyValue) ApplyValue,BelongMonth from
                                      (select S_EP_ReimbursementApply.*,detail.ApplyValue,month(ApplyDate) BelongMonth from S_EP_ReimbursementApply
                                      left join 
                                      (select sum(isnull(ApplyValue,0)) ApplyValue,S_EP_ReimbursementApplyID 
                                      from S_EP_ReimbursementApply_Details group by S_EP_ReimbursementApplyID) detail
                                      on detail.S_EP_ReimbursementApplyID = S_EP_ReimbursementApply.ID                                       
                                      where S_EP_ReimbursementApply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate)
                                      ) tmp where ProjectInfo in ('{0}') group by BelongMonth", costUnitArr));

            DataTable newDt = new DataTable();
            newDt.Columns.Add(new DataColumn("BelongMonth", typeof(string)));
            newDt.Columns.Add(new DataColumn("ApplyValue", typeof(decimal)));
            for (int i = 1; i <= 12; i++)
            {
                var resRow = newDt.NewRow();
                resRow["BelongMonth"] = i.ToString() + "月";
                resRow["ApplyValue"] = 0;

                var row = sourceDt.Select("BelongMonth=" + i).FirstOrDefault();
                if (row != null && row["ApplyValue"] != null && row["ApplyValue"] != DBNull.Value)
                {
                    resRow["ApplyValue"] = (decimal)row["ApplyValue"];
                }
                newDt.Rows.Add(resRow);
            }

            var columnChart = HighChartHelper.CreateColumnChart("", newDt, "BelongMonth", new string[] { "报销费用" }, new string[] { "ApplyValue" });
            return columnChart.Render();
        }

        /// <summary>
        /// 科目费用分布
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetContractSubjectPieData(string costUnitArr)
        {
            var sourceDt = this.SQLDB.ExecuteDataTable(string.Format(@"select sum(isnull(ApplyValue,0)) valueField, SubjectName nameField from S_EP_ReimbursementApply_Details detail
                                                       left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                                                       where apply.FlowPhase = 'End' and ProjectInfo in ('{0}') group by SubjectName", costUnitArr));

            var chart = HighChartHelper.CreatePieChart("", "", sourceDt);
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }

        /// <summary>
        /// 当月报销明细
        /// </summary>
        /// <returns></returns>
        public JsonResult GetContractReimbursementDetail(string costUnitArr, QueryBuilder qb)
        {
            string sql = @"select detail.* from S_EP_ReimbursementApply_Details detail
                         left join S_EP_ReimbursementApply apply on apply.ID = detail.S_EP_ReimbursementApplyID
                         where apply.FlowPhase = 'End' and year(getdate()) = year(ApplyDate) and month(getdate()) = month(ApplyDate) and ProjectInfo in ('{0}')";
            var dt = this.SQLDB.ExecuteDataTable(string.Format(sql, costUnitArr),qb);
            return Json(dt);
        }
        #endregion
        #endregion
    }
}
