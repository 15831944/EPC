using Formula.Exceptions;
using Market.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Market.Areas.MarketUI.Controllers
{
    public class T_B_BidProcessApplyController : MarketFormContorllor<T_B_BidProcessApply>
    {
        protected override void OnFlowEnd(T_B_BidProcessApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            base.OnFlowEnd(entity, taskExec, routing);
            S_B_Bid bid = BusinessEntities.Set<S_B_Bid>().Find(entity.BidID);
            if (bid == null)
            {
                throw new BusinessException("id为【" + entity.BidID + "】投标数据为空");
            }
            bid.BidFile = entity.BidFile;
            bid.BidClearFile = entity.BidClearFile;
            bid.BidOtherFile = entity.BidOtherFile;
            BusinessEntities.SaveChanges();
        }
    }
}
