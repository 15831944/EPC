using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Formula;
using Market.Logic.Domain;


namespace Market.Logic
{
    public class MarketIndexView
    {
        SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Market);
        MarketEntities marketEntities = FormulaHelper.GetEntities<MarketEntities>();

        public MarketIndexView()
        {
            string sql = @"select  isnull(Sum(isnull(ContractRMBAmount,0)-isnull(SumReceiptValue,0)-isnull(SummaryBadDebtValue,0)),0)
from dbo.S_C_ManageContract where IsSigned='{0}' ";
            this.CurrentContractValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, ContractIsSigned.Signed.ToString())));

            sql = @"select  isnull(Sum(Amount),0) from dbo.S_C_Receipt where BelongYear='{0}'";
            this.CurrentYearReceiptValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, DateTime.Now.Year.ToString())));

            sql = @"select  isnull(Sum(isnull(SumInvoiceValue,0)-isnull(SumReceiptValue,0)-isnull(SumBadDebtValue,0)),0) from dbo.S_C_ManageContract";
            this.CurrentReceivable = Convert.ToDecimal(sqlHelper.ExecuteScalar(sql));

            sql = @"select  isnull(Sum(PlanReceiptValue),0)  from S_C_PlanReceipt where BelongYear='{0}' and State='{1}'";
            this.CurrentYearPlanReceiptValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, DateTime.Now.Year,PlanReceiptState.UnReceipt.ToString())));

            sql = @"select isnull(Sum(ContractRMBAmount),0) from S_C_ManageContract where  IsSigned='{0}' and BelongYear='{1}'";
            this.CurrentYearSignContractValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, ContractIsSigned.Signed.ToString(), DateTime.Now.Year)));

            sql = "select   isnull(Sum(ContractRMBAmount),0) from S_C_ManageContract where  IsSigned='{0}' ";
            this.CurrentUnSignContractValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, ContractIsSigned.UnSigned.ToString())));
            
            sql = "select isnull(Sum(Amount),0)  from dbo.S_C_Receipt where BelongYear='{0}' and BelongMonth ='{1}'";
            this.CurrentMonthReceiptValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, DateTime.Now.Year,DateTime.Now.Month)));

            sql = "select isnull(Sum(PlanReceiptValue),0) from dbo.S_C_PlanReceipt where BelongYear='{0}' and BelongMonth ='{1}' and State='{2}'";
            this.CurrentMonthPlanReceiptValue = Convert.ToDecimal(sqlHelper.ExecuteScalar(String.Format(sql, DateTime.Now.Year, DateTime.Now.Month, PlanReceiptState.UnReceipt.ToString())));
        }

        /// <summary>
        /// 当年已签合同额
        /// </summary>
        public decimal CurrentYearSignContractValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前待签合同额
        /// </summary>
        public decimal CurrentUnSignContractValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前剩余合同额（合同金额-已到款金额）
        /// </summary>
        public decimal CurrentContractValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当年实际收款
        /// </summary>
        public decimal CurrentYearReceiptValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前应收账款（开票总额-到款总额）
        /// </summary>
        public decimal CurrentReceivable
        {
            get;
            set;
        }

        /// <summary>
        /// 当年计划收款总额
        /// </summary>
        public decimal CurrentYearPlanReceiptValue
        {
            get;
            set;
        }

        public decimal CurrentMonthPlanReceiptValue
        { get; set; }

        public decimal CurrentMonthReceiptValue
        { get; set; }

    }
}
