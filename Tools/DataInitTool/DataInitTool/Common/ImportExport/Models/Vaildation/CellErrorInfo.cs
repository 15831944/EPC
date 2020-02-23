using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    public class CellErrorInfo
    {
        /// <summary>
        /// 表格名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 行索引值
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 列索引值
        /// </summary>
        public int ColIndex { get; set; }

        /// <summary>
        /// 错误提示文本
        /// </summary>
        public string ErrorText { get; set; }
    }
}
