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
    public class IncomeSubmitRevertController : ExpensesFormController<S_EP_IncomeSubmitRevert>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            DateTime incomeSubmitEndDate = DateTime.Now;
            DateTime.TryParse(dic.GetValue("IncomeSubmitEndDate"), out incomeSubmitEndDate);
            CostFO.ValidatePeriodIsClosed(incomeSubmitEndDate);
        }

        protected override void OnFlowEnd(S_EP_IncomeSubmitRevert data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
