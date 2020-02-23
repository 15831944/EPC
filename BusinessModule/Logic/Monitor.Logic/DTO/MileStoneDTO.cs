using System;
namespace Monitor.Logic.DTO
{
    public class MileStoneDTO
    {
        /// <summary>
        /// 里程碑名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanFinishDate { get; set; }

        /// <summary>
        /// 时间完成时间
        /// </summary>
        public DateTime? FactFinishDate { get; set; }

        /// <summary>
        /// 延期状态
        /// </summary>
        public string State { get; set; }
    }
}
