using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class T_C_InvoiceMakeUpApply
    {
        /// <summary>
        /// 开票
        /// </summary>
        public void Submit()
        {             
            var user = Formula.FormulaHelper.GetUserInfo();
            var marketEntities = this.GetMarketContext();

            var receipt = marketEntities.Set<S_C_Receipt>().Find(this.ReceiptID);
            if(receipt == null)
                throw new Formula.Exceptions.BusinessException("未找到ID为【" + this.ReceiptID + "】的收款项,无法完成补开流程");
            if (this.State != InvoiceApplyState.Wait.ToString())
                throw new Formula.Exceptions.BusinessException("只能对流程通过且未进行开票动作的发票进行开票操作");
            var invoice = new S_C_Invoice();
            marketEntities.S_C_Invoice.Add(invoice);
            invoice.PayerUnitIDName = this.PayerUnitName;
            invoice.PayerUnit = this.PayerUnitName;
            invoice.PayerUnitID = this.PayerUnit;
            invoice.Amount = Convert.ToDecimal(this.Amount);
            var customerInfo = marketEntities.S_F_Customer.FirstOrDefault(c => c.ID == this.PayerUnit);
            if (customerInfo == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的客户信息，无法开票");
            }
            invoice.CustomerFullID = customerInfo.FullID;
            invoice.ContractInfoID = this.Contract;
            invoice.InvoiceType = this.InvoiceType;
            invoice.InvoiceDate = DateTime.Now;
            invoice.CreateDate = DateTime.Now;
            invoice.CreateUser = user.UserName;
            invoice.CreateUserID = user.UserID;
            invoice.InvoiceMaker = user.UserName;
            invoice.InvoiceCode = this.InvoiceCode;
            invoice.InvoiceMakerID = user.UserID;
            invoice.InvoiceMakerIDName = user.UserName;         
            invoice.BankName = this.BankName;
            invoice.BankAccount = this.BankAccount;
            invoice.TaxAccount = this.TaxAccount;
            invoice.InvoiceName = this.InvoiceName;
            invoice.TaxRate = this.TaxRate;
            invoice.ClearAmount = this.ClearValue;
            invoice.TaxesAmount = this.TaxValue;
            invoice.TaxName = this.TaxName;
            invoice.State = InvoiceState.Normal.ToString();
            invoice.Save();
            foreach (var item in this.T_C_InvoiceMakeUpApply_Detail.ToList())
            {
                var relationValue = item.ApplyInvoiceAmount.HasValue ? item.ApplyInvoiceAmount.Value : 0;
                invoice.RelateToReceiptObject(item.PlanReceiptID, relationValue);
            }

            this.State = InvoiceApplyState.Complete.ToString();
            receipt.RelateToInvoice(invoice);
        }

        public void Delete()
        {
            if (this.FlowPhase != "Start" && !String.IsNullOrEmpty(this.FlowPhase))
                throw new Formula.Exceptions.BusinessException("已经提交审批的记录，不能进行删除操作");
        }

        public void Validate()
        {
            var marketEntities = this.GetMarketContext();
            var contract = marketEntities.S_C_ManageContract.SingleOrDefault(d => d.ID == this.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("必须选择一个合同才能进行开票申请操作");
            var amount = this.Amount.HasValue ? this.Amount.Value : 0;
            if (contract.RemainInvoiceValue < amount)
                throw new Formula.Exceptions.BusinessException("开票金额不能大于本合同剩余可开票金额【" + Math.Round(contract.RemainInvoiceValue, 2) + "】");

            var needAmount = this.NeedAmount ?? 0;
            if ((this.Amount ?? 0) > needAmount)
            {
                throw new Formula.Exceptions.BusinessException("开票金额不能大于需补开的金额【" + Math.Round(needAmount, 2) + "】");
            }
        }
    }
}
