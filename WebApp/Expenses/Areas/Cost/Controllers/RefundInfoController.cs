using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Formula.Exceptions;
using Base.Logic.Domain;
using Workflow.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class RefundInfoController : ExpensesFormController<S_EP_UserRefund>
    {
        public JsonResult GetRefundList(string LoanID)
        {
            string sql = String.Format(@"select * from S_EP_UserRefund where LoanID='{0}' order by CreateDate ", LoanID);
            var data = this.SQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var refundValue = 0m;
            decimal.TryParse(dic.GetValue("RefundValue"), out refundValue);
            if (refundValue <= 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("还款金额必须大于0！");
            }
            

            if (isNew)
            {
                var sql = string.Format(@"select Loan.ID as LoanID,Loan.SerialNumber,Loan.Reason,Loan.ApplyUser,Loan.ApplyUserName,
ISNULL(Loan.FactValue,0) as FactValue,ISNULL(Refund.RefundValueTotal,0) as LastRefundValueTotal 
from (select * from S_EP_UserLoan where ID='{0}') Loan
outer apply(select top 1 * from S_EP_UserRefund a where Loan.ID=a.LoanID order by a.CreateDate desc) Refund ",
dic.GetValue("LoanID"));
                var dt = SQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("借款单不存在！");
                }
                else
                {
                    var dicLast = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    var factValue = Convert.ToDecimal(dicLast.GetValue("FactValue"));
                    var lastRefundValueTotal = Convert.ToDecimal(dicLast.GetValue("LastRefundValueTotal"));
                    var refundValueTotal = lastRefundValueTotal + refundValue;
                    if (refundValueTotal > factValue)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("累计还款金额不能大于借款金额！");
                    }

                    dic.SetValue("LastRefundValueTotal", Math.Round(lastRefundValueTotal, 2).ToString());
                    dic.SetValue("RefundValueTotal", Math.Round(refundValueTotal, 2).ToString());

                }

            }

        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var sql = string.Format(@"update S_EP_UserLoan set RefundValueTotal=(select ISNULL(sum(RefundValue),0) from S_EP_UserRefund where LoanID='{0}') 
where ID='{0}' ", dic.GetValue("LoanID"));
            SQLDB.ExecuteNonQuery(sql);

        }

        public JsonResult ValidateStart(string LoanID)
        {
            var sql = string.Format("select * from S_EP_UserRefund where LoanID='{0}' order by ID desc ", LoanID);
            var dt = SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return Json("");
            }

            var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
            var FactValue = Convert.ToDecimal(dic.GetValue("FactValue"));
            var RefundValueTotal = Convert.ToDecimal(dic.GetValue("RefundValueTotal"));
            if (RefundValueTotal > FactValue)
            {
                throw new Formula.Exceptions.BusinessValidationException("借款已还完！");
            }
            if (dic.GetValue("FlowPhase") == "Start")
            {
                return Json(new { ID = dic.GetValue("ID") });
            }

            return Json("");
        }

    }
}
