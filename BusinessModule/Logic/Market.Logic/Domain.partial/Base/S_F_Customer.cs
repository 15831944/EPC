using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class S_F_Customer
    {
        partial void onDelete()
        {
            var marketEntities = this.GetMarketContext();
            if (marketEntities.S_C_ManageContract.Count(d => d.PartyA == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("客户【" + this.Name + "】已经签订过合同，无法进行删除操作");
            if (marketEntities.S_C_Invoice.Count(d => d.PayerUnitID == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("客户【" + this.Name + "】已经开具过发票，无法进行删除操作");
            if (marketEntities.S_C_Receipt.Count(d => d.CustomerID == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("客户【" + this.Name + "】已经有收款，无法进行删除操作");
            if (marketEntities.S_I_Project.Count(d => d.Customer == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("客户【" + this.Name + "】已经有项目，无法进行删除操作");
        }

        /// <summary>
        /// 更新客户冗余属性（目前只同步更新客户名称和客户编号）
        /// 主要更新结构数发票信息表，合同信息表，收款信息表中的客户名称和客户编号
        /// </summary>
        public void SychCustomerProperties()
        {
            if (this.ID != null && this.ID != "")
            {
                var marketDB = this.GetMarketSqlHelper();
                string updateSql = @"
update S_C_Receipt set CustomerName='{0}' where CustomerID='{1}';
update S_C_Invoice set PayerUnit='{0}' where PayerUnitID='{1}';
update S_C_ManageContract set PartyAName='{0}' where PartyA='{1}';
update S_C_PlanReceipt set CustomerName='{0}',CustomerCode='{3}' where CustomerID='{1}';
update S_F_Customer set ParentName='{0}' where Parent='{1}';
update S_F_Customer_Complain set CustomerInfoName='{0}' where CustomerInfo='{1}';
update S_F_Customer_BigEvent set CustomerInfoName='{0}' where CustomerInfo='{1}';
update S_F_Customer_Demand set CustomerInfoName='{0}' where CustomerInfo='{1}';
update S_F_Customer_ReturnVisit set CustomerInfoName='{0}' where CustomerInfo='{1}';
update S_I_Project set CustomerName='{0}' where Customer='{1}';
update S_P_MarketClue set CustomerInfoName='{0}' where CustomerInfo='{1}';
";
                var name = this.Name.Replace("'", "''");
                var parentname = this.ParentName;
                if (!string.IsNullOrEmpty(this.ParentName))
                    parentname = parentname.Replace("'", "''");
                else
                    parentname = "";
                updateSql = String.Format(updateSql, name, this.ID, this.JuridicalPerson, this.SerialNumber, this.Parent, parentname, Config.SQLHelper.CreateSqlHelper(Config.ConnEnum.Project).DbName);
                marketDB.ExecuteNonQuery(updateSql);
            }
        }

        /// <summary>
        /// 校验客户信息填写是否正确
        /// </summary>
        public void ValidateCustomerInfo()
        {
            var entities = this.GetMarketContext();
            if (!string.IsNullOrEmpty(this.Code))
            {
                bool codeIsExist = entities.Set<S_F_Customer>().Where(p => p.ID != this.ID).Any(p => p.Code == this.Code);
                if (codeIsExist == true)
                    throw new Formula.Exceptions.BusinessException(string.Format("客户编码【{0}】已存在，请重新输入！", this.Code));
            }
            if (!string.IsNullOrEmpty(this.ShortName))
            {
                bool shortNameIsExist = entities.Set<S_F_Customer>().Any(p => p.ID != this.ID && p.ShortName == this.ShortName);
                if (shortNameIsExist == true)
                    throw new Formula.Exceptions.BusinessException(string.Format("客户简称【{0}】已存在，请重新输入！", this.ShortName));
            }
        }

    }
}
