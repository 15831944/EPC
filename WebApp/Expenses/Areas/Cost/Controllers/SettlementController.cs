using Config;
using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
using Formula.Helper;
using Expenses.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class SettlementController : ExpensesFormController<S_EP_Settlement>
    {
        public JsonResult GetCBSNodeRelateData()
        {
            string cbsFullID = GetQueryString("CBSNodeFullID");
            string costUnitID = GetQueryString("CostUnit");
            var cbsNodeDt = this.SQLDB.ExecuteDataTable(String.Format(@"SELECT ID CBSNodeID,ParentID CBSNodeParentID,
                                                       FullID CBSFullID,NodeType,'' Remark,Name,CostValue,SortIndex
                                                       FROM S_EP_CBSNode WHERE FULLID LIKE '{0}%'", cbsFullID));
            var settleDateObj = this.SQLDB.ExecuteScalar("select max(SettleDate) from S_EP_Settlement where CostUnit = '" + costUnitID + "'");


            return Json(new { CBSNodes = JsonHelper.ToJson(cbsNodeDt), MaxSettleDate = settleDateObj });
        }

        protected override void OnFlowEnd(S_EP_Settlement data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
