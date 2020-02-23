using System;
using System.ComponentModel;

namespace Monitor.Logic.DTO
{
    /// <summary>
    /// 合同
    /// </summary>
    public class ContractDTO
    {
        /// <summary>
        /// 合同名
        /// </summary>
        public string Name { get; set; }  

        /// <summary>
        /// 合同代码
        /// </summary>
        public string Code { get; set; } 

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ContractRMBAmount { get; set; }

        /// <summary>
        /// 开票
        /// </summary>
        public decimal SummaryInvoiceValue { get; set; }

        /// <summary>
        /// 到款
        /// </summary>
        public decimal SummaryReceiptValue { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 起止时间
        /// </summary>
        public string EndDate { get; set; } 

        /// <summary>
        /// 销售责任人
        /// </summary>
        public string HeadOfSalesName { get; set; } 

        /// <summary>
        /// 生产责任人
        /// </summary>
        public string ProduceMasterName  { get; set; } 

        /// <summary>
        /// 签订部门
        /// </summary>
        public string SignDept { get; set; }
    }
}
