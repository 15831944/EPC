using System.ComponentModel;

namespace Monitor.Logic.DTO
{
    /// <summary>
    /// 合同——收款计划
    /// </summary>
    public class PlanReceiptDTO
    {
        /// <summary>
        /// 收款ID
        /// </summary>
        [Description("收款ID")]
        public string ID { get; set; }

        /// <summary>
        /// 收款计划名字
        /// </summary>
        [Description("收款计划名字")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Description("负责人")]
        public string FirstDutyManName { get; set; }

        /// <summary>
        /// 计划收款时间
        /// </summary>
        [Description("计划收款时间")]
        public string PlanReceiptDate { get; set; }

        /// <summary>
        /// 计划收款金额
        /// </summary>
        [Description("计划收款金额")]
        public decimal PlanReceiptValue { get; set; }

        /// <summary>
        /// 实际到款金额
        /// </summary>
        [Description("实际到款金额")]
        public decimal FactReceiptValue { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Description("状态")]
        public string State { get; set; }

    }
}
