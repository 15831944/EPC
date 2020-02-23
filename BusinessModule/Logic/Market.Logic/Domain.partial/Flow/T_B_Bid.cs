using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class T_B_BidApply
    {
        public void Submit()
        {
            var user = Formula.FormulaHelper.GetUserInfo();
            var marketEntities = this.GetMarketContext();

            ////登记一条投标数据
            ////增加一条经营项目数据
            //S_P_MarketClue clue = new S_P_MarketClue();
            //clue.ID = Formula.FormulaHelper.CreateGuid();
            //clue.Address = this.Address;
            //clue.CompanyID = this.CompanyID;
            //clue.CreateDate = DateTime.Now;
            //clue.CreateUser = user.UserName;
            //clue.CreateUserID = user.UserID;
            //clue.DeptInfo = this.DeptInfo;
            //clue.DeptInfoName = this.DeptInfoName;
            //clue.Name = this.ProjectName;
            //clue.OrgID = user.UserOrgID;
            //clue.ProjectClass = this.BusinessClass;
            //clue.State = ClueState.Tracking.ToString();
            //clue.CustomerInfo = this.BusinessOwner;
            //clue.CustomerInfoName = this.BusinessOwnerName;
            //clue.CustomerInfoSub = this.BusinessOwner;
            //clue.CustomerInfoSubName = this.BusinessOwnerName;
            //clue.EngineeringInfo = this.EngineeringInfo;
            //clue.EngineeringInfoName = this.EngineeringInfoName;
            //clue.Save(this);
            //marketEntities.Set<S_P_MarketClue>().Add(clue);
            S_B_Bid bid = new S_B_Bid();
            Formula.FormulaHelper.UpdateEntity<S_B_Bid>(bid, this.ToDic(), false);
            bid.ID = this.Project;
            marketEntities.Set<S_B_Bid>().Add(bid);
            marketEntities.SaveChanges();
        }

        public void Validate()
        {
 
        }
    }
}
