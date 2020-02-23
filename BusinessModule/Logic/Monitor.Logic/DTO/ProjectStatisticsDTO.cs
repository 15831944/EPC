using System.ComponentModel;

namespace Monitor.Logic.DTO
{
    public class ProjectStatisticsDTO
    {
        /// <summary>
        /// 项目总数
        /// </summary>
        [Description("项目总数")]
        public int TotalProject { get; set; }

        /// <summary>
        /// 在建项目数
        /// </summary>
        [Description("在建项目数")]
        public int ExecuteProject { get; set; }

        /// <summary>
        /// 延期项目数
        /// </summary>
        [Description("延期项目数")]
        public int DelayProject { get; set; }

        /// <summary>
        /// 剩余名义合同额
        /// </summary>
        [Description("剩余名义合同额")]
        public decimal RestMoney { get; set; }

        /// <summary>
        /// 累计名义合同额
        /// </summary>
        [Description("累计名义合同额")]
        public decimal TotalMoney { get; set; }
    }
}
