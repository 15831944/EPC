using System.ComponentModel;

namespace Monitor.Logic.Enum
{
    public enum DateEnum
    {
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year,

        /// <summary>
        /// 本年后
        /// </summary>
        [Description("本年后")]
        AfterYear,

        /// <summary>
        /// 季
        /// </summary>
        [Description("季")]
        Quarter,

        /// <summary>
        /// 本季后
        /// </summary>
        [Description("本季后")]
        AfterQuarter,

        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month,

        /// <summary>
        /// 本月后
        /// </summary>
        [Description("本月后")]
        AfterMonth,

        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week
    }
}
