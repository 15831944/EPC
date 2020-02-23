using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Config;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using MvcAdapter;
using Formula;
using Expenses.Logic.BusinessFacade;

namespace Expenses.Areas.InCome.Controllers
{
    public class IncomeAdjustController : ExpensesFormController<S_EP_RevenueAdjustInfo>
    {
        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            foreach (var detailDic in detailList)
            {
                string revenueInfoDetailID = detailDic.GetValue("RevenueInfoDetailID");
                DataInterfaceFo.ValidateDataSyn("S_EP_RevenueInfo_Detail", revenueInfoDetailID);
            }
        }
        protected override void OnFlowEnd(S_EP_RevenueAdjustInfo data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
