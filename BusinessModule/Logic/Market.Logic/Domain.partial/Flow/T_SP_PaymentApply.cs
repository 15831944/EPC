using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Newtonsoft.Json;
using Formula;

namespace Market.Logic.Domain
{
    public partial class T_SP_PaymentApply
    {
        public void Validate()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_SP_SupplierContract.SingleOrDefault(d => d.ID == this.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到名称为【" + this.ContractName + "】的分包合同信息，无法进行开票操作");
            var contractPaymentAmmout = 0M;
            //获取所属合同除本付款外的所有付款
            if (entities.S_SP_Payment.Any(d => d.Contract == this.Contract))
                contractPaymentAmmout = Convert.ToDecimal(entities.S_SP_Payment.
                    Where(d => d.Contract == this.Contract).Sum(d => d.PaymentValue));
            var sumPaymentValue = contractPaymentAmmout + Convert.ToDecimal(this.ApplyValue);
            if (sumPaymentValue > contract.ContractValue)
                throw new Formula.Exceptions.BusinessException("累计付款金额【" + sumPaymentValue + "】不能大于合同总金额【" + contract.ContractValue + "】");
        }

        public void SavePayment()
        {
            var entities = this.GetMarketContext();
            var date = this.PaymentDate.HasValue ? this.PaymentDate.Value : DateTime.Now;
            var paymentInfo = new S_SP_Payment();
            paymentInfo.ID = Formula.FormulaHelper.CreateGuid();
            paymentInfo.Contract = this.Contract;
            paymentInfo.ContractName = this.ContractName;
            paymentInfo.Supplier = this.Supplier;
            paymentInfo.SupplierName = this.SupplierName;
            paymentInfo.PaymentValue = this.PaymentValue;
            paymentInfo.BillValue = this.BillValue;
            paymentInfo.PaymentWithoutBill = this.PaymentWithoutBill;
            paymentInfo.PaymentDate = this.PaymentDate;
            paymentInfo.BelongYear = date.Year;
            paymentInfo.BelongMonth = date.Month;
            paymentInfo.BelongQuarter = MarketHelper.GetQuarter(date);
            paymentInfo.PaymentUser = this.PaymentUser;
            paymentInfo.PaymentUserName = this.PaymentUserName;
            paymentInfo.Remark = this.Remark;
            paymentInfo.PaymentUserName = this.PaymentUserName;
            paymentInfo.CreateUserID = this.CreateUserID;
            paymentInfo.CreateUser = this.CreateUser;
            paymentInfo.CreateDate = DateTime.Now;
            paymentInfo.ModifyUser = this.ModifyUser;
            paymentInfo.ModifyUserID = this.ModifyUserID;
            paymentInfo.ModifyDate = DateTime.Now;
            paymentInfo.PaymentCode = this.SerialNumber;
            paymentInfo.PaymentApplyID = this.ID;
            paymentInfo.ManagerContract = this.ManagerContract;
            paymentInfo.ManagerContractName = this.ManagerContractName;
            entities.S_SP_Payment.Add(paymentInfo);
            foreach (var item in this.T_SP_PaymentApply_InvoiceRelation.ToList())
            {
                var relation = new S_SP_Payment_InvoiceRelation();
                relation.ID = item.ID;
                relation.InvoiceNo = item.InvoiceNo;
                relation.InvoiceID = item.InvoiceID;
                relation.InvoiceRelationValue = item.InvoiceRelationValue;
                relation.RemainInvoiceValue = item.RemainInvoiceValue;
                relation.RelationValue = item.RelationValue;
                relation.InvoiceUnit = item.InvoiceUnit;
                relation.S_SP_Payment = paymentInfo;
                paymentInfo.S_SP_Payment_InvoiceRelation.Add(relation);
            }

            foreach (var item in this.T_SP_PaymentApply_AcceptanceBill.ToList())
            {
                var relation = new S_SP_Payment_AcceptanceBill();
                relation.ID = item.ID;
                relation.AcceptanceBillID = item.AcceptanceBillID;
                relation.SerialNumber = item.SerialNumber;
                relation.CustomerName = item.CustomerName;
                relation.BillDate = item.BillDate;
                relation.Amount = item.Amount;
                relation.S_SP_Payment = paymentInfo;
                paymentInfo.S_SP_Payment_AcceptanceBill.Add(relation);
                //修改承兑汇票支付状态
                entities.Set<S_C_AcceptanceBill>().Find(item.AcceptanceBillID).State = "AlreadyPaid";
            }
            paymentInfo.Save();
        }

        public void ToEPCost()
        {
            var db = this.GetMarketSqlHelper();
            var obj = db.ExecuteScalar(String.Format(@" select isnull(value,'') from S_EP_DefineParams where Code='{1}'", "SubContractConfirmMethod"));
            if (obj == null || obj == DBNull.Value)
                return;
            if (obj.ToString().ToLower() != "PaymentApply") return;




        }
    }
}
