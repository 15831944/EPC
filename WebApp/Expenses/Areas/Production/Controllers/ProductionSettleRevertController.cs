using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using Expenses.Logic.Domain;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionSettleRevertController : ExpensesFormController<S_EP_ProductionSettleSingleRevert>
    {
        protected override void OnFlowEnd(S_EP_ProductionSettleSingleRevert data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateRevert(string SettleID)
        {
            var applyDic = this.GetDataDicByID("S_EP_ProductionSettleApply", SettleID);
            if (applyDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到指定的产值确认结算单，无法撤销！");
            if (applyDic.GetValue("FlowPhase") != "End") throw new Formula.Exceptions.BusinessValidationException("此确认单流程为结束，无法撤销！");
            var sql = string.Format(@"select top 1 ID from S_EP_ProductionSettleSingleRevert 
where SettleID='{0}' and FlowPhase<>'End' ", SettleID);
            var currentID = this.SQLDB.ExecuteScalar(sql);
            if (currentID != null && currentID != DBNull.Value && !String.IsNullOrEmpty(currentID.ToString()))
            {
                return Json(new { ID = currentID.ToString() });
            }
            return Json("");
        }
    }
}
