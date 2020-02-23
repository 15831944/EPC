using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;

namespace Expenses.Areas.Cost.Controllers
{
    public class WorkHourSubmitRevertController : ExpensesFormController<S_EP_WorkHourSubmitRevert>
    {

        protected override void OnFlowEnd(S_EP_WorkHourSubmitRevert data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
