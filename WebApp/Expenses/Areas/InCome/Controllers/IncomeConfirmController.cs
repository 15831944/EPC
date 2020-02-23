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
    public class IncomeConfirmController : ExpensesFormController<S_EP_RevenueInfo>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                if (!dic.ContainsKey("BelongYear") || String.IsNullOrEmpty(dic["BelongYear"]))
                {
                    throw new Formula.Exceptions.BusinessValidationException("确认收入时，必须指定所属年份");
                }
                if (!dic.ContainsKey("BelongMonth") || String.IsNullOrEmpty(dic["BelongMonth"]))
                {
                    throw new Formula.Exceptions.BusinessValidationException("确认收入时，必须指定所属月份");
                }

                if (Convert.ToInt32(this.SQLDB.ExecuteScalar(String.Format("select count(ID) from S_EP_RevenueInfo where BelongYear='{0}' and BelongMonth='{1}' and State<>'Removed'",
                      dic["BelongYear"],
                      dic["BelongMonth"]))) > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(dic["BelongYear"] + "年" + dic["BelongMonth"] + "月已经有确认收入，不能重复进行收入确认");
                }

                //根据会计期间来获取收入确认的基准日期
                var inComeDate = new DateTime(Convert.ToInt32(dic["BelongYear"]), Convert.ToInt32(dic["BelongMonth"]), 1);
                string defineMonthPeriodSql = "select * from S_EP_DefineMonthPeriod where Year = {0} and Month = {1}";
                var defineMonthPeriodDt = this.InfrasDB.ExecuteDataTable(string.Format(defineMonthPeriodSql, Convert.ToInt32(dic["BelongYear"]), Convert.ToInt32(dic["BelongMonth"])));
                if (defineMonthPeriodDt.Rows.Count > 0 && defineMonthPeriodDt.Rows[0]["StartDate"] != null && defineMonthPeriodDt.Rows[0]["StartDate"] != DBNull.Value)
                {
                    inComeDate = Convert.ToDateTime(defineMonthPeriodDt.Rows[0]["StartDate"]);
                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException("无法获取会计期间");
                }
                dic["IncomeDate"] = inComeDate.ToString();

                if (!String.IsNullOrEmpty(dic["BelongMonth"].ToString()))
                {
                    var nBelongMonth = Convert.ToInt32(dic["BelongMonth"].ToString());
                    dic["BelongQuarter"] = ((nBelongMonth - 1) / 3 + 1).ToString();
                }
            }
        }

        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                string incomeConfirmID = dr["ID"].ToString();
                string detailSql = @"select S_EP_RevenueInfo_Detail.*,isnull(adjust.AdjustCount,0) AdjustCount from S_EP_RevenueInfo_Detail 
left join 
(select count(*) AdjustCount,RevenueInfoDetailID 
from S_EP_RevenueAdjustInfo_Detail inner join S_EP_RevenueAdjustInfo on S_EP_RevenueAdjustInfo.ID = S_EP_RevenueAdjustInfo_Detail.S_EP_RevenueAdjustInfoID
where S_EP_RevenueAdjustInfo.FlowPhase = 'End' and S_EP_RevenueAdjustInfo_Detail.AdjustValue != 0
group by RevenueInfoDetailID) adjust
on adjust.RevenueInfoDetailID = S_EP_RevenueInfo_Detail.ID
where S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID = '" + incomeConfirmID + "'";
                var detailDT = this.SQLDB.ExecuteDataTable(detailSql);
                dr["Detail"] = JsonHelper.ToJson(detailDT);
            }
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var ID in Ids)
            {
                var dic = this.GetDataDicByID("S_EP_RevenueInfo", ID);
                var revInfo = new S_EP_RevenueInfo(dic);
                revInfo.Remove();
            }
        }

        public JsonResult CalAccrualncome()
        {
            string belongYearStr = GetQueryString("BelongYear");
            string belongMonthStr = GetQueryString("BelongMonth");
            if (!string.IsNullOrEmpty(belongYearStr)
                && !string.IsNullOrEmpty(belongMonthStr))
            {
                var fo = Formula.FormulaHelper.CreateFO<IncomeFo>();
                var result = fo.Calculate(belongYearStr, belongMonthStr);
                return Json(result);
            }
            else
            {
                return Json("");
            }
        }

        protected override void OnFlowEnd(S_EP_RevenueInfo data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
