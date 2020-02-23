using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Formula;
using Config;

namespace Market.Logic.Domain
{
    public partial class S_SP_Invoice
    {
        public void Save()
        {
            var entities = this.GetMarketContext();
            var contract = entities.S_SP_SupplierContract.SingleOrDefault(d => d.ID == this.SupplierContract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.SupplierContract + "】的分包合同信息，无法进行开票操作");
            if(String.IsNullOrEmpty(this.Supplier))
            {
                this.Supplier = contract.Supplier;
                this.SupplierContractName = contract.SupplierName;
            }
            if (this.InvoiceDate.HasValue)
            {
                var date = Convert.ToDateTime(this.InvoiceDate);
                this.BelongYear = date.Year;
                this.BelongMonth = date.Month;
                this.BelongQuarter = MarketHelper.GetQuarter(date);
            }
            if (String.IsNullOrEmpty(this.ManagerContract))
            {
                this.ManagerContract = contract.ManagerContract;
                this.ManagerContractName = contract.ManagerContractName;
            }

            var contractInvoiceAmmout = 0M;
            //获取所属合同除本发票外的所有发票的开票金额
            if (entities.S_SP_Invoice.Where(d => d.SupplierContract == this.SupplierContract && d.ID != this.ID).Count() > 0)
                contractInvoiceAmmout = Convert.ToDecimal(entities.S_SP_Invoice.
                    Where(d => d.SupplierContract == this.SupplierContract && d.ID != this.ID).Sum(d => d.Amount));
            var sumInvoiceValue = contractInvoiceAmmout + Convert.ToDecimal(this.Amount);
            if (sumInvoiceValue > contract.ContractValue)
                throw new Formula.Exceptions.BusinessException("累计开票金额【" + sumInvoiceValue + "】不能大于合同总金额【" + contract.ContractValue + "】");
            //contract.SummaryData();
        }

        public void Delete()
        {
            var sql = string.Format("select COUNT(ID) from S_EP_InvoiceConfirm where InvoiceID='{0}'  ", this.ID);
            var sqlDB =  SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var count = Convert.ToInt32(sqlDB.ExecuteScalar(sql));
            if (count > 0)
            {
                throw new Formula.Exceptions.BusinessException("发票已被计入委外成本，不能进行删除操作，如果要删除请撤销关联的委外成本");
            }

            var entities = this.GetMarketContext();
            var contract = entities.Set<S_SP_SupplierContract>().SingleOrDefault(d => d.ID == this.SupplierContract);
            if (contract == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + this.SupplierContract + "】的分包合同信息");
            if (entities.Set<S_SP_Payment_InvoiceRelation>().Count(a => a.InvoiceID == this.ID) > 0)
                throw new Formula.Exceptions.BusinessException("已经关联了付款的发票信息，无法进行删除操作");
            entities.Set<S_SP_Invoice>().Remove(this);
            contract.SummaryData();
        }
    }
}
