using Formula;
using Formula.Exceptions;
using Market.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market.Areas.MarketUI.Controllers
{
    public class T_B_BondApplyController : MarketFormContorllor<T_B_BondApply>
    {
        protected override void OnFlowEnd(T_B_BondApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            base.OnFlowEnd(entity, taskExec, routing);
            if (!BusinessEntities.Set<S_P_MarketClue>().Any(a => a.ID == entity.Project))
                throw new BusinessException("未找到id为【" + entity.Project + "】的前期项目信息");

            S_B_Bond bond = new S_B_Bond();
            FormulaHelper.UpdateEntity<S_B_Bond>(bond, entity.ToDic(), false);
            bond.ID = entity.ID;
            bond.State = "NoReturn";
            BusinessEntities.Set<S_B_Bond>().Add(bond);
        }
    }
}