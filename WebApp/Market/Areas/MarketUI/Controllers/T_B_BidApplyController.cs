using Market.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formula.Helper;
using Config.Logic;

namespace Market.Areas.MarketUI.Controllers
{
    public class T_B_BidApplyController : MarketFormContorllor<T_B_BidApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var bid = this.BusinessEntities.Set<S_B_Bid>().Find(dic.GetValue("Project"));
            if (bid != null)
                throw new Formula.Exceptions.BusinessValidationException("【" + dic.GetValue("ProjectName") + "】已经登记了投标信息");
        }
        protected override void OnFlowEnd(T_B_BidApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            base.OnFlowEnd(entity, taskExec, routing);
            if (entity.Agree == "True")
                entity.Submit();
        }

    }
}
