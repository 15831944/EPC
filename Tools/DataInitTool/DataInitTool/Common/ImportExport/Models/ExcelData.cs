using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.Logic
{
    /// <summary>
    /// Excel导入的数据
    /// </summary>
    public class ExcelData
    {
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        /// <param name="config"></param>
        public void InitConfig(ExcelConfig config)
        {
            Tables = new List<ExcelTableInfo>();
            foreach (var tableConfig in config.Tables)
            {
                Tables.Add(new ExcelTableInfo(tableConfig));
            }
        }

        /// <summary>
        /// 表格数据
        /// </summary>
        public IList<ExcelTableInfo> Tables { get; set; }

        /// <summary>
        /// 获取一个数据表
        /// </summary>
        /// <param name="tableName">表名称，如果没有设置，则为第一个表格的数据</param>
        /// <returns></returns>
        public DataTable GetDataTable(string tableName = "")
        {
            DataTable data = null;
            if (Tables.Count > 0)
            {
                // 构建表结构
                var table = Tables[0];
                if (!string.IsNullOrWhiteSpace(tableName))
                {
                    table = Tables.FirstOrDefault(t => t.TableName == tableName);
                }

                if (table != null)
                {
                    data = table.ToDataTable();
                }
            }

            return data;
        }

        public List<Dictionary<string, string>> GetList(string tableName)
        {
            List<Dictionary<string, string>> list = null;
            var table = Tables.FirstOrDefault(t => t.TableName == tableName);
            list = table.ToList();
            return list;
        }
    }
}
