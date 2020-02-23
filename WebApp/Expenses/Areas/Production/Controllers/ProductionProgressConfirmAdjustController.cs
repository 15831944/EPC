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
    public class ProductionProgressConfirmAdjustController : ExpensesFormController<S_EP_ProductionProgressConfirmAdjustInfo>
    {
        protected override void OnFlowEnd(S_EP_ProductionProgressConfirmAdjustInfo data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
