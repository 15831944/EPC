using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Data;

namespace Expenses.Areas.InCome.Controllers
{
    public class IncomeSubmitController : ExpensesFormController<S_EP_IncomeSubmit>
    {
        public JsonResult GetSubmitList(string UnitID)
        {
            string sql = @"SELECT S_EP_IncomeSubmitRevert.ID as RevertID,
isnull(S_EP_IncomeSubmit.LastScale,0)/100 as LastScale,isnull(S_EP_IncomeSubmit.CurrentScale,0)/100 as CurrentScale,
isnull(S_EP_IncomeSubmit.TotalScale,0)/100 as TotalScale,S_EP_IncomeSubmit.*
 FROM S_EP_IncomeSubmit
 left join S_EP_IncomeSubmitRevert on S_EP_IncomeSubmit.ID = S_EP_IncomeSubmitRevert.S_EP_IncomeSubmitID 
 WHERE S_EP_IncomeSubmit.IncomeUnitID='{0}' ORDER BY ID ";
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, UnitID));
            return Json(dt);
        }

        public JsonResult ValidateSubmit(string UnitID)
        {
            string sql = String.Format("SELECT COUNT(ID) FROM S_EP_IncomeSubmit WHERE IncomeUnitID='{0}' AND FlowPhase<>'{1}'", UnitID, "End");
            var obj = this.SQLDB.ExecuteScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("存在尚未完成审批的进度申报，请等待审批完成后再进行申报");
            }
            return Json("");
        }


        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            CostFO.ValidatePeriodIsClosed(DateTime.Now);

            #region  累计和当期相互根据逻辑计算赋予默认值(前提是有一个数据为空)
            var lastScale = String.IsNullOrEmpty(dic.GetValue("LastScale")) ? 0m : Convert.ToDecimal(dic.GetValue("LastScale"));
            var totalScale = 0m;
            var currentScale = 0m;
            if (String.IsNullOrEmpty(dic.GetValue("TotalScale")) && !String.IsNullOrEmpty(dic.GetValue("CurrentScale")))
            {
                currentScale = Convert.ToDecimal(dic.GetValue("CurrentScale"));
                totalScale = currentScale + lastScale;
                dic.SetValue("TotalScale", totalScale.ToString());
            }
            else if (!String.IsNullOrEmpty(dic.GetValue("TotalScale")) && String.IsNullOrEmpty(dic.GetValue("CurrentScale")))
            {
                totalScale = Convert.ToDecimal(dic.GetValue("TotalScale"));
                currentScale = Convert.ToDecimal(dic.GetValue("TotalScale")) + lastScale;
                dic.SetValue("CurrentScale", currentScale.ToString());
            }
            #endregion
        }

        protected override void OnFlowEnd(S_EP_IncomeSubmit data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public void ValidateRevert(string NodeID)
        {
            //只能逐步撤销
            {
                string existSql = @"select count(*) from S_EP_IncomeSubmit where 
                                  IncomeUnitID = (select IncomeUnitID from S_EP_IncomeSubmit where ID = '{0}')
                                  and FlowPhase = 'End' and ID > '{0}'";
                var countObj = this.SQLDB.ExecuteScalar(string.Format(existSql, NodeID));
                if (countObj != null && (int)countObj > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
                }
            }

            //如果有包含收入节点，以收入节点的收入百分比来判断是否被收入确认
            {
                var incomeSubmitDT = this.SQLDB.ExecuteDataTable(string.Format("SELECT * FROM S_EP_IncomeSubmit WHERE ID = '{0}'", NodeID));
                if (incomeSubmitDT.Rows.Count > 0)
                {
                    var row = incomeSubmitDT.Rows[0];
                    decimal allIncomeScale = 0;
                    decimal.TryParse(row["TotalScale"].ToString(), out allIncomeScale);
                    if (allIncomeScale == 0) return;

                    string revenueSql = @"select max(TotalScale) from S_EP_RevenueInfo_Detail
                                        inner join S_EP_RevenueInfo on S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID where S_EP_RevenueInfo.State != 'Removed' and IncomeUnitID = '{0}'";
                    var revenueObj = this.SQLDB.ExecuteScalar(string.Format(revenueSql, row["IncomeUnitID"].ToString()));
                    if (revenueObj != null)
                    {
                        decimal revenue = 0;
                        decimal.TryParse(revenueObj.ToString(), out revenue);
                        if (revenue == 0) return;
                        if (revenue >= allIncomeScale)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("申报的节点已经已经确认过收入，无法撤销");
                        }
                    }
                }
            }
        }
    }
}
