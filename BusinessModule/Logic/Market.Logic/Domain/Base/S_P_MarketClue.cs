using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;
using Config;
using Config.Logic;

namespace Market.Logic.Domain
{
    /// <summary>
    /// 前期项目模型
    /// </summary>
    public partial class S_P_MarketClue
    {

        public void Save(T_B_BidApply tb = null)
        {
            var marketEntities = this.GetMarketContext();
            var user = Formula.FormulaHelper.GetUserInfo();

            var customer = marketEntities.S_F_Customer.SingleOrDefault(d => d.ID == this.CustomerInfo);
            if (customer != null)
            {
                if (String.IsNullOrEmpty(this.Province))
                    this.Province = customer.Province;
                if (String.IsNullOrEmpty(this.Country))
                    this.Country = customer.Country;
                if (String.IsNullOrEmpty(this.City))
                    this.City = customer.City;
            }

            //登记投标信息
            S_B_Bid bid = new S_B_Bid();
            if(tb != null)
            {
                FormulaHelper.UpdateEntity<S_B_Bid>(bid, tb.ToDic(), false);
            }
            else
            {
                bid.EngineeringInfo = this.EngineeringInfo;
                bid.EngineeringInfoName = this.EngineeringInfoName;
                bid.BusinessClass = this.ProjectClass;
                bid.ProjectManager = this.TechCharger;
                bid.ProjectManagerName = this.TechChargerName;
                bid.DeptInfo = this.DeptInfo;
                bid.DeptInfoName = this.DeptInfoName;
                bid.BusinessOwner = this.CustomerInfo;
                bid.BusinessOwnerName = this.CustomerInfoName;
                bid.Project = this.ID;
                bid.ProjectName = this.Name;
            }
            bid.ID = this.ID;
            marketEntities.Set<S_B_Bid>().Add(bid);
        }

        public void Delete()
        {
            var marketEntities = this.GetMarketContext();
            if (marketEntities.S_I_Project.Count(d => d.MakertClueID == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("【" + this.Name + "】已成功立项，无法进行删除操作");
        }
    }
}
