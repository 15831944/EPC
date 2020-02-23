using System.ComponentModel;

namespace Monitor.Logic.DTO
{
    public class SectorDistributionDTO
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Description("部门名称")]
        public string Name { get; set; }

        /// <summary>
        /// 延期项目数
        /// </summary>
        [Description("延期项目数")]
        public int DelayProject { get; set; }

        /// <summary>
        /// 在建项目数-延期项目数
        /// </summary>
        [Description("在建项目数-延期项目数")]
        public int ExecuteProject { get; set; }
    }
}
