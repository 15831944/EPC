using Expenses.Logic.Domain;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
using Expenses.Logic;

namespace Expenses.Areas.InCome.Controllers
{
    public class ProgressConfirmRevertController : ExpensesFormController<S_EP_ProgressConfirmRevert>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            string datetTimeStr = dic.GetValue("FactEndDate");
            DateTime dateTime = Convert.ToDateTime(datetTimeStr);
            CostFO.ValidatePeriodIsClosed(dateTime);
        }
        protected override void OnFlowEnd(S_EP_ProgressConfirmRevert data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
