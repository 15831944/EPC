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
    public class CustomerRequestReviewController : MarketFormContorllor<T_CP_CustomerRequestReview>
    {
        protected override void OnFlowEnd(T_CP_CustomerRequestReview entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
            {
                if (entity.CanDo == "1")
                {
                    var clue = BusinessEntities.Set<S_P_MarketClue>().Create();
                    clue.ID = Formula.FormulaHelper.CreateGuid();
                    clue.CustomerInfoName = entity.CustomerInfoName;
                    clue.CustomerInfo = entity.CustomerInfo;
                    clue.Name = entity.ProjectName;
                    clue.Content = entity.CustomerRequire;
                    clue.State = ClueState.Tracking.ToString();
                    clue.CreateDate = DateTime.Now;
                    BusinessEntities.Set<S_P_MarketClue>().Add(clue);                   
                    clue.Save();
                }
            }
            this.BusinessEntities.SaveChanges();
        }
    }
}
