using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Formula;

namespace Market.Logic.Domain
{
    /// <summary>
    /// 发票对象模型
    /// </summary>
    public partial class S_C_Invoice
    {

        #region 公共属性

        decimal _recieptValue = -1;
        /// <summary>
        /// 关联收款金额（只读，如果未关联任何收款金额，则为0）
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal RecieptValue
        {
            get
            {
                if (_recieptValue == -1)
                {
                    _recieptValue = Convert.ToDecimal(this.S_C_InvoiceReceiptRelation.Sum(d => d.RelationValue));
                }
                return _recieptValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal MaxReceiptWriteOff
        {
            get
            {
                var writeOff = 0M;
                if (this.S_C_InvoiceReceiptRelation.Count > 0)
                    writeOff = Convert.ToDecimal(this.S_C_InvoiceReceiptRelation.Sum(d => d.RelationValue));
                var _maxReceiptWriteOff = Convert.ToDecimal(this.Amount) - writeOff;
                return _maxReceiptWriteOff;
            }
        }

        decimal _maxReceiptObjWriteOff = -1;
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal MaxReceiptObjWriteOff
        {
            get
            {
                if (this._maxReceiptObjWriteOff < 0)
                {
                    var writeOff = Convert.ToDecimal(this.S_C_Invoice_ReceiptObject.Sum(d => d.RelationValue));
                    _maxReceiptObjWriteOff = Convert.ToDecimal(this.Amount) - writeOff;
                }
                return _maxReceiptObjWriteOff;
            }
        }


        decimal _receivable = -1;
        /// <summary>
        /// 发票对象的应收款（只读）
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal Receivable
        {
            get
            {
                if (_receivable == -1)
                {
                    _receivable = Convert.ToDecimal(this.Amount) - this.RecieptValue;
                }
                return _receivable;
            }
        }

        #endregion

        #region 公共实例方法

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_C_ManageContract.SingleOrDefault(d => d.ID == this.ContractInfoID);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.ContractInfoID + "】的合同信息，无法进行开票操作");
            if (String.IsNullOrEmpty(this.ID)) this.ID = FormulaHelper.CreateGuid();
            if (String.IsNullOrEmpty(this.PayerUnitID))
            {
                this.PayerUnitID = contract.PartyA;
                this.PayerUnit = contract.PartyAName;
                this.PayerUnitIDName = contract.PartyAName;
            }
            var customer = entities.S_F_Customer.FirstOrDefault(a => a.ID == this.PayerUnitID);
            if (customer != null)
            {
                if (customer.S_F_Customer_BankAccounts.Count(c => c.BankAccount == this.BankAccount) == 0
                    && !String.IsNullOrEmpty(this.BankAccount) && !String.IsNullOrEmpty(this.BankName))
                {
                    var bankAccount = new S_F_Customer_BankAccounts();
                    bankAccount.BankName = this.BankName;
                    bankAccount.BankAccount = this.BankAccount;
                    bankAccount.BankAccountName = this.BankName;
                    customer.S_F_Customer_BankAccounts.Add(bankAccount);
                }
                customer.TaxAccounts = this.TaxAccount;
                if (string.IsNullOrEmpty(customer.InvoiceName))
                    customer.InvoiceName = this.InvoiceName;
            }
            if (String.IsNullOrEmpty(this.ReceviedUnitID))
            {
                this.ReceviedUnitID = contract.BusinessDept;
                this.ReceviedUnit = contract.BusinessDeptName;
            }
            this.ContractCode = contract.SerialNumber;
            this.ContractName = contract.Name;
            this.ContractInfoIDName = contract.Name;
            this.State = InvoiceState.Normal.ToString();
            this.BelongYear = this.InvoiceDate.HasValue ? this.InvoiceDate.Value.Year : DateTime.Now.Year;
            this.BelongMonth = this.InvoiceDate.HasValue ? this.InvoiceDate.Value.Year : DateTime.Now.Month;
            this.BelongQuarter = MarketHelper.GetQuarter(this.InvoiceDate.HasValue ? this.InvoiceDate.Value : DateTime.Now);
            var contractInvoiceAmmout = 0M;
            //获取所属合同除本发票外的所有发票的开票金额
            var invoceState = InvoiceState.Normal.ToString();
            if (entities.S_C_Invoice.Where(d => d.ContractInfoID == this.ContractInfoID && d.ID != this.ID && d.State == invoceState).Count() > 0)
                contractInvoiceAmmout = Convert.ToDecimal(entities.S_C_Invoice.
                    Where(d => d.ContractInfoID == this.ContractInfoID && d.ID != this.ID &&
                        d.State == invoceState).Sum(d => d.Amount));
            var sumInvoiceValue = contractInvoiceAmmout + Convert.ToDecimal(this.Amount);
            //限制总开票金额不能大于合同金额
            if (sumInvoiceValue > contract.ContractRMBAmount)
                throw new Formula.Exceptions.BusinessException("累计开票金额【" + sumInvoiceValue + "】不能大于合同总金额【" + contract.ContractRMBAmount + "】");
            this.CauculateTax();
            contract.SummaryInvoiceData();
        }

        /// <summary>
        /// 发票作废
        /// </summary>
        public void Invalid(bool validate = true)
        {
            if (validate)
            {
                if (this.S_C_InvoiceReceiptRelation.Sum(d => d.RelationValue) > 0)
                {
                    throw new Formula.Exceptions.BusinessException("合同为【" + this.ContractName + "】的发票已经有收款记录，无法进行作废操作");
                }
            }

            this.State = InvoiceState.Invalid.ToString();

            var entities = this.GetMarketContext();

            //删除与收款关联的数据
            foreach (var item in this.S_C_InvoiceReceiptRelation.ToList())
                entities.S_C_InvoiceReceiptRelation.Remove(item);

            //删除所有与收款项关联的数据
            foreach (var item in this.S_C_Invoice_ReceiptObject.ToList())
            {
                var receiptObject = item.S_C_ManageContract_ReceiptObj;
                entities.S_C_Invoice_ReceiptObject.Remove(item);
                //重新计算收款项的累计开票金额
                receiptObject.SummaryInvoiceValue();
            }
            //重新计算合同的累计开票金额
            this.S_C_ManageContract.SummaryInvoiceData();
        }

        /// <summary>
        /// 清除所有计划收款绑定内容
        /// </summary>
        public void ClearPlanRelation()
        {
            var marketEntites = this.GetMarketContext();
            marketEntites.S_C_Invoice_ReceiptObject.Delete(d => d.S_C_InvoiceID == this.ID);
            this.S_C_Invoice_ReceiptObject.Clear();
        }

        /// <summary>
        /// 清除所有收款绑定内容
        /// </summary>
        public void ClearReceiptRelation()
        {
            var marketEntites = this.GetMarketContext();
            marketEntites.S_C_InvoiceReceiptRelation.Delete(d => d.InvoiceID == this.ID);
            this.S_C_InvoiceReceiptRelation.Clear();
        }

        /// <summary>
        /// 删除发票方法
        /// </summary>
        /// <param name="isDestory">是否销毁删除（当参数为true时，不再进行逻辑校验。否则关联过收款的发票则不允许被删除）</param>
        public void Delete(bool isDestory = false)
        {
            onDelete();
            if (!isDestory && this.S_C_InvoiceReceiptRelation.Count > 0)
                throw new Formula.Exceptions.BusinessException("已经关联了收款的发票信息，无法进行删除操作");
            var entities = this.GetMarketContext();
            var contract = entities.S_C_ManageContract.SingleOrDefault(d => d.ID == this.ContractInfoID);
            foreach (var item in this.S_C_InvoiceReceiptRelation.ToList())
                entities.S_C_InvoiceReceiptRelation.Remove(item);
            foreach (var item in this.S_C_Invoice_ReceiptObject.ToList())
            {
                var receiptObject = item.S_C_ManageContract_ReceiptObj;
                entities.S_C_Invoice_ReceiptObject.Remove(item);
                receiptObject.SummaryInvoiceValue();
            }
            entities.S_C_Invoice.Remove(this);
            contract.SummaryInvoiceData();
            onDeleted();
        }

        /// <summary>
        /// 根据发票类型自动计算税金数据
        /// </summary>
        public void CauculateTax()
        {
            if (String.IsNullOrEmpty(this.InvoiceType)) return;
            var invoiceType = (InvoiceType)Enum.Parse(typeof(InvoiceType), this.InvoiceType);
            if (!this.TaxRate.HasValue)
                this.TaxRate = Convert.ToDecimal(invoiceType);
            if (this.TaxRate.HasValue && this.Amount.HasValue)
            {
                this.TaxesAmount = this.Amount / (1 + this.TaxRate) * this.TaxRate;
                this.ClearAmount = this.Amount - this.TaxesAmount;
            }
        }

        /// <summary>
        /// 将发票对象关联到计划收款对象
        /// </summary>
        /// <param name="planID">计划收款ID</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于收款金额。当默认为0，且offset为true,关联金额则取发票和收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToReceiptObject(string receiptObjectID, decimal relateValue = 0, bool offSet = false)
        {
            var entities = this.GetMarketContext();
            var plan = entities.S_C_ManageContract_ReceiptObj.SingleOrDefault(d => d.ID == receiptObjectID);
            if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + receiptObjectID + "】的计划收款对象，无法进行关联操作");
            this.RelateToReceiptObject(plan, relateValue, offSet);
        }

        /// <summary>
        /// 将发票对象关联到计划收款对象
        /// </summary>
        /// <param name="receiptObj">计划收款对象</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于计划收款金额。当默认为0，且offset为true,关联金额则取发票和计划收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToReceiptObject(S_C_ManageContract_ReceiptObj receiptObj, decimal relateValue = 0, bool offSet = false)
        {
            var marketEntites = this.GetMarketContext();
            marketEntites.S_C_Invoice_ReceiptObject.Delete(d => d.ReceiptObjectID == receiptObj.ID && d.S_C_InvoiceID == this.ID);
            var relation = marketEntites.S_C_Invoice_ReceiptObject.Create();
            relation.ID = FormulaHelper.CreateGuid();
            relation.S_C_InvoiceID = this.ID;
            relation.ReceiptObjectID = receiptObj.ID;
            relation.ContractInfoID = this.ContractInfoID;
            relation.ModifyDate = DateTime.Now;
            var userInfo = FormulaHelper.GetUserInfo();
            relation.ModifyUserName = userInfo.UserName;
            relation.ModifyUserID = userInfo.UserID;
            relation.S_C_Invoice = this;
            if (relateValue == 0)
                relateValue = Convert.ToDecimal(receiptObj.ReceiptValue);
            if (!offSet)
            {
                if (relateValue > this.Amount)
                    throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于发票金额");

                if (relateValue > receiptObj.ReceiptValue)
                    throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】对应到发票的金额，不能高于计划收款金额");
                var relationValue = receiptObj.S_C_Invoice_ReceiptObject.Where(d => d.S_C_InvoiceID != this.ID).Sum(d => d.RelationValue);
                var remainInvoiceValue = receiptObj.ReceiptValue - relationValue;
                if (relateValue > remainInvoiceValue)
                {
                    throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】对应到发票的金额，不能高于剩余可开票金额【" + remainInvoiceValue + "】");
                }
            }
            else
            {
                if (relateValue > receiptObj.ReceiptValue)
                    relateValue = Convert.ToDecimal(receiptObj.ReceiptValue);
                if (relateValue > this.Amount)
                    relateValue = Convert.ToDecimal(this.Amount);
            }
            relation.RelationValue = relateValue;
            marketEntites.S_C_Invoice_ReceiptObject.Add(relation);
            receiptObj.SummaryInvoiceValue();
        }

        /// <summary>
        /// 将发票对象关联到收款对象
        /// </summary>
        /// <param name="invoiceID">收款ID</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于收款金额。当默认为0，且offset为true,关联金额则取发票和收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToReciept(string recieptID, decimal relateValue = 0, bool offSet = false)
        {
            var entities = this.GetMarketContext();
            var reciept = entities.S_C_Receipt.SingleOrDefault(d => d.ID == recieptID);
            if (reciept == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + recieptID + "】的收款对象，无法进行关联操作");
            this.RelateToReciept(reciept, relateValue, offSet);
        }

        /// <summary>
        /// 将发票对象关联到收款对象
        /// </summary>
        /// <param name="invoiceID">收款对象</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于收款金额。当默认为0，且offset为true,关联金额则取发票和收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToReciept(S_C_Receipt reciept, decimal relateValue = 0, bool offSet = false)
        {
            var relation = this.S_C_InvoiceReceiptRelation.SingleOrDefault(d => d.ReceiptID == reciept.ID);
            if (relation == null)
            {
                relation = new S_C_InvoiceReceiptRelation();
                relation.ID = FormulaHelper.CreateGuid();
                this.S_C_InvoiceReceiptRelation.Add(relation);
            }

            if (relateValue == 0)
                relateValue = Convert.ToDecimal(reciept.Amount);

            //是否是以冲销帐方式来关联发票与收款的关系
            //如果不以冲销帐来关联，则关联金额不允许大于开票金额，也不允许大于收款金额
            //如果是以冲销帐方式来关联则根据小金额来关联发票
            if (!offSet)
            {
                if (relateValue > this.Amount)
                    throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于发票金额");
                if (relateValue > reciept.Amount)
                    throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于收款金额");
            }
            else
            {
                if (relateValue > reciept.Amount)
                    relateValue = Convert.ToDecimal(reciept.Amount);
                if (relateValue > this.Amount)
                    relateValue = Convert.ToDecimal(this.Amount);
            }
            relation.ReceiptID = reciept.ID;
            relation.ContractInfoID = this.ContractInfoID;
            relation.RelationValue = relateValue;
        }

        #endregion




        #region 分布扩展方法

        partial void onDelete();
        partial void onDeleted();

        #endregion

    }
}
