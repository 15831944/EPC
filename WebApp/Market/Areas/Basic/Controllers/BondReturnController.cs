using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
using Config.Logic;
using Base.Logic.Domain;

namespace Market.Areas.Basic.Controllers
{
    public class BondReturnController : MarketFormContorllor<S_B_BondReturn>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var returnAmount = 0m;
            decimal.TryParse(dic.GetValue("ReturnAmount"), out returnAmount);
            if (returnAmount <= 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("归还金额必须大于0！");
            }

            if (isNew)
            {
                var sql = string.Format(@"select Bond.ID as BondID,
ISNULL(Bond.Amount,0) as Amount,
ISNULL(BondReturn.ReturnAmountTotal,0) as LastReturnAmountTotal
from (select * from S_B_Bond where ID='{0}') Bond
outer apply(select top 1 * from S_B_BondReturn a where Bond.ID=a.BondID order by a.CreateDate desc) BondReturn ",
dic.GetValue("BondID"));
                var dt = MarketSQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("保证金申请不存在！");
                }
                else
                {
                    var dicLast = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    var amount = Convert.ToDecimal(dicLast.GetValue("Amount"));
                    var lastReturnAmountTotal = Convert.ToDecimal(dicLast.GetValue("LastReturnAmountTotal"));
                    var returnAmountTotal = lastReturnAmountTotal + returnAmount;
                    if (returnAmountTotal > amount)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("累计还款金额不能大于保证金金额！");
                    }

                    var performanceAmount = amount - returnAmountTotal;

                    dic.SetValue("LastReturnAmountTotal", Math.Round(lastReturnAmountTotal, 2).ToString());
                    dic.SetValue("ReturnAmountTotal", Math.Round(returnAmountTotal, 2).ToString());
                    dic.SetValue("PerformanceAmount", Math.Round(performanceAmount, 2).ToString());
                }

            }


        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var amount = Convert.ToDecimal(dic.GetValue("Amount"));
            var returnAmountTotal = Convert.ToDecimal(dic.GetValue("ReturnAmountTotal"));
            var status = "Return";
            if (returnAmountTotal == amount)
            {
                status = "Settle";
            }
            var sql = string.Format(@"update S_B_Bond set ReturnAmountTotal=(select ISNULL(sum(ReturnAmount),0) from S_B_BondReturn where BondID='{0}'), State='{1}' 
where ID='{0}' ", dic.GetValue("BondID"), status);
            MarketSQLDB.ExecuteNonQuery(sql);
        }

        public JsonResult GetReturnList(string BondID)
        {
            string sql = String.Format(@"select * from S_B_BondReturn where BondID='{0}' order by CreateDate ", BondID);
            var data = this.MarketSQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

    }
}
