using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Logic
{
    public class CellValidationArgs
    {
        public CellValidationArgs()
        {
            this.IsValid = true;
        }

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
        /// 字段值
        /// </summary>
        public string Value { get; set; }

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

        /// <summary>
        ///  是否验证通过
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误提示文本
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// 所在数据行
        /// </summary>
        public DataRow Record { get; set; }

    }
}
