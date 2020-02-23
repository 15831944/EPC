using Expenses.Logic.Domain;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
using Expenses.Logic;
using System.Data;
using Config;
using Formula.Helper;
using Workflow.Logic.Domain;
using Base.Logic.Domain;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionBatchSettleRevertController : ExpensesFormController<S_EP_ProductionSettleRevert>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {


        }
        protected override void OnFlowEnd(S_EP_ProductionSettleRevert data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateRevert(string SettleID)
        {
            var applyDic = this.GetDataDicByID("S_EP_ProductionBatchSettleApply", SettleID);
            if (applyDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到指定的产值确认结算单，无法撤销！");
            if (applyDic.GetValue("FlowPhase") != "End") throw new Formula.Exceptions.BusinessValidationException("此确认单流程为结束，无法撤销！");
            var sql = string.Format(@"select top 1 ID from S_EP_ProductionSettleRevert 
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
