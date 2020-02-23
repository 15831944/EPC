using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
namespace Expenses.Areas.Build.Controllers
{
    public class CBSInfoCancelController : ExpensesFormController<S_EP_CBSInfoCancel>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            base.BeforeSave(dic, formInfo, isNew);
            Dictionary<string, object> dicObjList = new Dictionary<string, object>();
            foreach (var item in dic)
            {
                dicObjList.Add(item.Key, item.Value);
            }
            S_EP_CBSInfoCancel data = new S_EP_CBSInfoCancel(dicObjList);
            data.Validate();
        }
        protected override void OnFlowEnd(S_EP_CBSInfoCancel data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
