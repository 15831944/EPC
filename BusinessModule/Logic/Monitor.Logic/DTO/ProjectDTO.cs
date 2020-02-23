using System;
namespace Monitor.Logic.DTO
{
    public class ProjectDTO
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        public string PhaseName { get; set; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public string ChargeDeptName { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string ChargeUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime? DelegateDate { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanFinishDate { get; set; }

        /// <summary>
        /// 参与专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 项目范围及任务要求
        /// </summary>
        public string RangeAndTaskRequest { get; set; }
    }
}
