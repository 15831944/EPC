using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;

namespace Market.Areas.MarketUI.Controllers
{
    public class SPContractReviewController : MarketFormContorllor<T_SP_ContractReview>
    {
        protected override void OnFlowEnd(T_SP_ContractReview entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            this.MarketSQLDB.ExecuteNonQuery(String.Format("Update S_SP_SupplierContract set State='{0}' where ID='{1}' and State='{2}'", "WaitSign",
                entity.SPContractInfo, "Regist"));
        }
    }
}
