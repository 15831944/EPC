namespace Monitor.Logic.DTO
{
    public class ContractStatisticsDTO
    {
        /// <summary>
        /// 合同ID、负责人ID、客户ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 合同名称、负责人名称、客户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 合同数
        /// </summary>
        public int Count { get; set; }
    }
}
