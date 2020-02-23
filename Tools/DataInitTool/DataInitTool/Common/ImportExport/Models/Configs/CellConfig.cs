using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    /// <summary>
    /// 单元格的配置信息
    /// </summary>
    public class CellConfig
    {
        /// <summary>
        /// 行索引值
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 列索引值
        /// </summary>
        public int ColIndex { get; set; }

        /// <summary>
        /// 对应的字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 日期型？
        /// </summary>
        public bool isDate { get; set; }

        /// <summary>
        /// 数值型？
        /// </summary>
        public bool isDecimal { get; set; }

        /// <summary>
        /// 枚举Key
        /// </summary>
        public string EnumKey { get; set; }

        /// <summary>
        /// 业务字段类型
        /// </summary>
        public string FieldClass { get; set; }

        /// <summary>
        /// 业务字段ID
        /// </summary>
        public string FieldID { get; set; }

    }
}
