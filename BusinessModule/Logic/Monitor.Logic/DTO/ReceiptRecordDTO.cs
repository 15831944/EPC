using System.ComponentModel;

namespace Monitor.Logic.DTO
{
    public class ReceiptRecordDTO
    {
        /// <summary>
        /// 到款时间
        /// </summary>
        [Description("到款时间")]
        public string ArrivedDate { get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        [Description("到款金额")]
        public decimal ArrivedMoney { get; set; }

        /// <summary>
        /// 收款项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 到款ID
        /// </summary>
        public string ID { get; set; }
    }
}
