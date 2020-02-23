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
    /// 分包合同实体对象
    /// </summary>
    public partial class S_SP_SupplierContract
    {
        decimal _invoiceValue = -1;
        /// <summary>
        /// 累计开票金额
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal InvoiceValue
        {
            get {
                if (_invoiceValue < 0)
                {
                    if (this.S_SP_Invoice.Count == 0)
                        _invoiceValue = 0;
                    else
                    {
                        var sumValue = this.S_SP_Invoice.Sum(d => d.Amount);
                        _invoiceValue = sumValue.HasValue ? sumValue.Value : 0m;
                    }
                }
                return _invoiceValue;
            }
        }

        decimal _paymentValue = -1;
        /// <summary>
        /// 累计付款金额
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal PaymentValue
        {
            get
            {
                if (_paymentValue < 0)
                {
                    if (this.S_SP_Payment.Count == 0)
                        _paymentValue = 0;
                    else
                    {
                        var sumValue = this.S_SP_Payment.Sum(d => d.PaymentValue);
                        _paymentValue = sumValue.HasValue ? sumValue.Value : 0;
                    }
                }
                return _paymentValue;
            }
        }

        /// <summary>
        /// 保存分包合同信息
        /// </summary>
        public void Save()
        {
            if (this.SignDate.HasValue)
            {
                var date = Convert.ToDateTime(this.SignDate);
                this.BelongYear = date.Year;
                this.BelongMonth = date.Month;
                this.BelongQuarter = MarketHelper.GetQuarter(date);
                if (this.State == "WaitSign" || this.State == "Regist")
                {
                    this.State = "Sign";
                }
            }
            this.SychProperties();
            this.SummaryData();
        }

        /// <summary>
        /// 当合同名称或合同编号变更时，同步相关数据
        /// </summary>
        public void SychProperties()
        {
            var marketDB = this.GetMarketSqlHelper();
            string updateSql = @" update S_SP_PaymentPlan set SupplierContractName='{1}' where SupplierContract='{0}';
 update S_SP_Payment set ContractName='{1}' where Contract='{0}';
 update S_SP_Invoice set SupplierContractName='{1}' where SupplierContract='{0}';";
            updateSql = String.Format(updateSql, this.Name, this.ID);
            marketDB.ExecuteNonQuery(updateSql);
        }

        /// <summary>
        /// 汇总计算合同的累计开票和付款金额
        /// </summary>
        public void SummaryData()
        {
            this.SumSPInvoiceValue = this.InvoiceValue;
            this.SumPaymentValue = this.PaymentValue;
        }
    }
}
