using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    /// <summary>
    /// Excel模板的配置信息
    /// </summary>
    public class ExcelConfig
    {
        private IExcelMetadataStorage storage = new DefaultExcelMetadataStorage();
        public ExcelConfig(string templatePath)
        {
            Tables = new List<TableConfig>();
            this.TemplatePath = templatePath;
            this.Metadata = storage.GetMetadataByPath(templatePath);
        }
        /// <summary>
        /// 表格，需要循环导入导出的数据（Sheet）
        /// </summary>
        public IList<TableConfig> Tables { get; set; }

        /// <summary>
        /// Excel模板的物理路径
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// Excel模板的元数据信息
        /// </summary>
        public ExcelMetadata Metadata { get; set; }
    }
}
