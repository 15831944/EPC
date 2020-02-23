using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class T_C_CreditNoteApply
    {
        /// <summary>
        /// 开票
        /// </summary>
        public void Submit()
        {
            var user = Formula.FormulaHelper.GetUserInfo();
            var marketEntities = this.GetMarketContext();
            if (this.State != InvoiceApplyState.Wait.ToString())
                throw new Formula.Exceptions.BusinessException("只能对流程通过且未进行开票动作的发票进行开票操作");
            var invoice = new S_C_Invoice();
            marketEntities.S_C_Invoice.Add(invoice);
            invoice.ID = this.ID;
            invoice.PayerUnitIDName = this.PayerUnitName;
            invoice.PayerUnit = this.PayerUnitName;
            invoice.PayerUnitID = this.PayerUnit;
            invoice.Amount = -Convert.ToDecimal(this.Amount);//红冲票对应收款为相反数
            var customerInfo = marketEntities.S_F_Customer.FirstOrDefault(c => c.ID == this.PayerUnit);
            if (customerInfo == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的客户信息，无法开票");
            }
            invoice.CustomerFullID = customerInfo.FullID;
            invoice.ContractInfoID = this.Contract;           
            invoice.InvoiceDate = DateTime.Now;
            invoice.CreateDate = DateTime.Now;
            invoice.CreateUser = user.UserName;
            invoice.CreateUserID = user.UserID;
            invoice.InvoiceMaker = user.UserName;
            invoice.InvoiceCode = this.InvoiceCode;
            invoice.InvoiceType = Market.Logic.InvoiceType.CreditNote.ToString();
            invoice.InvoiceMakerID = user.UserID;
            invoice.InvoiceMakerIDName = user.UserName;
            invoice.TaxRate = this.TaxRate;
            invoice.BankName = this.BankName;
            invoice.BankAccount = this.BankAccount;
            invoice.TaxAccount = this.TaxAccount;
            invoice.InvoiceName = this.InvoiceName;
            invoice.State = InvoiceState.Normal.ToString();
            invoice.Save();//  invoice.Amount = -Convert.ToDecimal(this.Amount);//红冲票对应收款为相反数 save没问题!


            foreach (var item in this.T_C_CreditNoteApply_Detail.ToList())
            {
                var relationValue = item.CreditValue.HasValue ? item.CreditValue.Value : 0;
                var entities = this.GetMarketContext();
                var plan = entities.S_C_ManageContract_ReceiptObj.SingleOrDefault(d => d.ID == item.PlanReceiptID);
                if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.PlanReceiptID + "】的计划收款对象，无法进行关联操作");

                RelateToReceiptObject(invoice, plan, relationValue);
            }
            this.State = InvoiceApplyState.Complete.ToString();
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
            if (contract.InvoiceValue < amount)
                throw new Formula.Exceptions.BusinessException("红冲金额不能大于合同已开票金额【" + Math.Round(contract.InvoiceValue, 2) + "】");
        }

        /// <summary>
        /// 将发票对象关联到计划收款对象
        /// </summary>
        /// <param name="receiptObj">计划收款对象</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于计划收款金额。当默认为0，且offset为true,关联金额则取发票和计划收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToReceiptObject(S_C_Invoice invoice, S_C_ManageContract_ReceiptObj receiptObj, decimal relateValue = 0, bool offSet = false)
        {
            var marketEntites = invoice.GetMarketContext();
            marketEntites.S_C_Invoice_ReceiptObject.Delete(d => d.ReceiptObjectID == receiptObj.ID && d.S_C_InvoiceID == invoice.ID);
            var relation = marketEntites.S_C_Invoice_ReceiptObject.Create();
            relation.ID = FormulaHelper.CreateGuid();
            relation.S_C_InvoiceID = invoice.ID;
            relation.ReceiptObjectID = receiptObj.ID;
            relation.ContractInfoID = invoice.ContractInfoID;
            relation.ModifyDate = DateTime.Now;
            var userInfo = FormulaHelper.GetUserInfo();
            relation.ModifyUserName = userInfo.UserName;
            relation.ModifyUserID = userInfo.UserID;
            relation.S_C_Invoice = invoice;
            if (relateValue == 0)
                relateValue = Convert.ToDecimal(receiptObj.ReceiptValue);

            //if (!offSet)
            {
                if (relateValue > this.Amount)//不用invoice.Amount因为invoice.Amount已经是负数
                    throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于发票金额");

                if (relateValue > receiptObj.FactInvoiceValue)
                    throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】对应到发票的金额，不能高于已开票金额");
            }
            //else
            //{
            //    if (relateValue > receiptObj.ReceiptValue)
            //        relateValue = Convert.ToDecimal(receiptObj.ReceiptValue);
            //    if (relateValue > this.Amount)
            //        relateValue = Convert.ToDecimal(this.Amount);
            //}

            relation.RelationValue = -relateValue;
            marketEntites.S_C_Invoice_ReceiptObject.Add(relation);
            receiptObj.SummaryInvoiceValue();
        }
    }
}
