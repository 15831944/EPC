using Expenses.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.Logic.Domain;
using Config.Logic;
using Formula;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionInputController : ExpensesFormController<S_EP_ProductionInput>
    {
        public JsonResult GetCommunalValue()
        {
            string sql = @"select  isnull((select isnull(Sum(ProductionValue),0) as CommunalValue from [dbo].[S_EP_CBSNode]
where NodeType='Communal'),0)-
isnull((select Sum(UseValue) as UseValue from (
select distinct UseValue,ApplyFormID from S_EP_ProductionCBSChangeLog) ChangeLog),0) as CommunalValue";
            var obj = this.SQLDB.ExecuteScalar(sql);
            return Json(new { result = Convert.ToDecimal(obj) });
        }

        protected override void OnFlowEnd(S_EP_ProductionInput data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

    }
}
