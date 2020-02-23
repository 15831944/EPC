using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Newtonsoft.Json;
using Formula;
using Config;
using Config.Logic;

namespace Market.Logic.Domain
{
    public partial class S_SP_Payment
    {
        public void Validate()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_SP_SupplierContract.SingleOrDefault(d => d.ID == this.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.ContractName + "】的分包合同信息，无法进行收款操作");
            var contractInvoiceAmmout = 0M;
            //获取所属合同除本付款外的所有付款
            if (entities.S_SP_Payment.Any(d => d.Contract == this.Contract && d.ID != this.ID))
                contractInvoiceAmmout = Convert.ToDecimal(entities.S_SP_Payment.
                    Where(d => d.Contract == this.Contract && d.ID != this.ID).Sum(d => d.PaymentValue));
            var sumInvoiceValue = contractInvoiceAmmout + Convert.ToDecimal(this.PaymentValue);
            if (sumInvoiceValue > contract.ContractValue)
                throw new Formula.Exceptions.BusinessException("累计付款金额【" + sumInvoiceValue + "】不能大于合同总金额【" + contract.ContractValue + "】");
        }

        public void Save()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_SP_SupplierContract.SingleOrDefault(d => d.ID == this.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.ContractName + "】的分包合同信息，无法进行开票操作");
            if (String.IsNullOrEmpty(this.Supplier))
            {
                this.Supplier = contract.Supplier;
                this.SupplierName = contract.SupplierName;
            }
            if (this.PaymentDate.HasValue)
            {
                var date = Convert.ToDateTime(this.PaymentDate);
                this.BelongYear = date.Year;
                this.BelongMonth = date.Month;
                this.BelongQuarter = MarketHelper.GetQuarter(date);
            }
            contract.SummaryData();
            this.SychorToCostInfo();
           // this.SychorToSEPCostInfo();
        }

        public void SychorToCostInfo()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_SP_SupplierContract.SingleOrDefault(d => d.ID == this.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.ContractName + "】的分包合同信息，无法进行收款操作");
            var cost = entities.S_FC_CostInfo.FirstOrDefault(d => d.RelateID == this.ID);
            bool isNew = false;
            if (cost == null)
            {
                cost = entities.S_FC_CostInfo.Create();
                cost.ID = FormulaHelper.CreateGuid();
                if (this.PaymentDate.HasValue)
                {
                    cost.BelongMonth = this.PaymentDate.Value.Month;
                    cost.BelongQuarter = (this.PaymentDate.Value.Month - 1) / 3 + 1;
                    cost.BelongYear = this.PaymentDate.Value.Year;
                    cost.CostDate = this.PaymentDate.Value;
                }
                else
                {
                    cost.BelongMonth = DateTime.Now.Month;
                    cost.BelongQuarter = (DateTime.Now.Month - 1) / 3 + 1;
                    cost.BelongYear = DateTime.Now.Year;
                    cost.CostDate = DateTime.Now;
                }
                cost.CostType = "Payment";
                cost.ProjectType = "Production";
                cost.Quantity = 0;
                cost.SubjectCode = "Payment";
                cost.SubjectName = "分包付款";
                cost.UnitPrice = 0;
                cost.RelateID = this.ID;
                entities.S_FC_CostInfo.Add(cost);
                isNew = true;
            }
            cost.CostUserDeptID = contract.SupplierDept;
            cost.CostUserDeptName = contract.SupplierDeptName;
            cost.CostUserID = this.CreateUserID;
            cost.CostUserName = this.CreateUser;
            cost.CostValue = this.PaymentValue.HasValue ? this.PaymentValue.Value : 0;
            var project = entities.Set<S_I_Project>().FirstOrDefault(d => d.ID == contract.ProjectInfo);
            if (project != null)
            {
                cost.ProjectChargerUserID = project.ChargerUser;
                cost.ProjectChargerUserName = project.ChargerUserName;
                cost.ProjectClass = project.ProjectClass;
                cost.ProjectCode = project.Code;
                cost.ProjectDeptID = project.ChargerDept;
                cost.ProjectDeptName = project.ChargerDeptName;
                cost.ProjectID = project.ID;
                cost.ProjectName = project.Name;
            }
            cost.FormID = this.PaymentApplyID;
            var oaDb = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
            var marketDic = cost.ToDic();
            if (isNew)
                marketDic.InsertDB(oaDb, "S_FC_CostInfo");
            else
                marketDic.UpdateDB(oaDb, "S_FC_CostInfo", cost.ID);
        }

    }
}
