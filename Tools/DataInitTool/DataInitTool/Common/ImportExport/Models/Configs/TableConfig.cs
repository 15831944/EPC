using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    /// <summary>
    /// 表格，表示需要循环导入的配置信息，EndCode属性表示循环终止的字符。
    /// </summary>
    public class TableConfig
    {
        public TableConfig()
        {
            Cells = new List<CellConfig>();
            FieldNames = new List<string>();
        }

        /// <summary>
        /// 单元格
        /// </summary>
        public IList<CellConfig> Cells { get; set; }

        /// <summary>
        /// 单元格
        /// </summary>
        public IList<string> FieldNames { get; set; }

        /// <summary>
        /// 循环的开始行号
        /// </summary>
        public int StartRowIndex { get; set; }

        /// <summary>
        /// 循环的开始列号
        /// </summary>
        public int StartColIndex { get; set; }

        /// <summary>
        /// 结束标识
        /// </summary>
        public string EndCode { get; set; }

        /// <summary>
        /// 字段所属表的名称
        /// </summary>
        public string TableName { get; set; }

        public CellConfig GetCellConfig(string CellName)
        {
            foreach (var cell in Cells)
            {
                if (cell.FieldName == CellName)
                    return cell;
            }
            return null;
        }
    }
}
