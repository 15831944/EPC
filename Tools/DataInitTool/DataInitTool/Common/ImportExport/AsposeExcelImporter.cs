using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Data;

namespace Common.Logic
{
    /// <summary>
    /// Excel导入数据，采用第三方Aspose组件
    /// </summary>
    public class AsposeExcelImporter : IImporter
    {
        public ExcelData Import(byte[] excelFile, string templatePath)
        {
            return Import(excelFile, GetExcelConfig(templatePath));
        }

        public ExcelData Import(byte[] excelFile, ExcelConfig config)
        {
            var data = new ExcelData();
            data.InitConfig(config);
            var workbook = new Workbook(new MemoryStream(excelFile));
            if (data.Tables.Count != workbook.Worksheets.Count)
                throw new Exception("模板的Sheet个数与导入的Sheet个数不符，请使用模板进行数据导入");
            // 填充表格数据，需要循环导入的数据
            for (int k = 0; k < workbook.Worksheets.Count; k++)
            {
                var cells = workbook.Worksheets[k].Cells;
                var comments = workbook.Worksheets[k].Comments;
                var thisTableName = workbook.Worksheets[k].Name;

                var table = data.Tables.FirstOrDefault(a => a.TableName == thisTableName);
                var startRowIndex = table.Structure.StartRowIndex;
                // 循环行
                for (int i = startRowIndex; i <= cells.MaxDataRow + 1; i++)
                {
                    // 判断是否结束
                    if (IsEndRow()) break;

                    bool hasRow = false;//是否有行

                    var row = new ExcelRowInfo(i);
                    // 循环列
                    foreach (var cell in table.Structure.Cells)
                    {
                        // 获取导入单元格的值
                        var value = cells[i, cell.ColIndex].StringValue.Trim();

                        if (!string.IsNullOrEmpty(value))//任何一个单元格有数据，则有行
                            hasRow = true;

                        var cellInfo = new ExcelCellInfo(cell);
                        cellInfo.Value = value.Replace("'", "''");
                        row.Cells.Add(cellInfo);
                    }

                    if (hasRow) //有行数据则加入
                        table.Rows.Add(row);
                }
            }

            return data;
        }

        /// <summary>
        /// 判断是否为最后一行
        /// </summary>
        /// <returns></returns>
        private bool IsEndRow()
        {
            return false;
        }

        public ExcelConfig GetExcelConfig(string templatePath)
        {
            var config = new ExcelConfig(templatePath);
            Workbook workbook = new Workbook(new MemoryStream(config.Metadata.FileBuffer));
            for (int k = 0; k < workbook.Worksheets.Count; k++)
            {
                var cells = workbook.Worksheets[k].Cells;
                var comments = workbook.Worksheets[k].Comments;
                var table = new TableConfig();
                config.Tables.Add(table);
                table.TableName = workbook.Worksheets[k].Name;
                
                //if (GlobalData.TableConfigDic.ContainsKey(table.TableName))
                //    GlobalData.TableConfigDic[table.TableName] = table;
                //else
                //    GlobalData.TableConfigDic.Add(table.TableName, table);

                //循环行
                for (int i = 0; i < cells.MaxDataRow + 1; i++)
                {
                    // 循环列
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        string s = cells[i, j].StringValue.Trim();

                        var fieldName = ParseFieldName(s);
                        if (!string.IsNullOrWhiteSpace(fieldName))
                        {
                            table.StartRowIndex = i;
                            table.StartColIndex = j;
                            var note = string.Empty;
                            var comment = comments[i, j];
                            if (comment != null)
                            {
                                note = comment.Note;
                            }

                            var cellConfig = GetCellInfo(s, i, j, note);
                            table.Cells.Add(cellConfig);
                            table.FieldNames.Add(cellConfig.FieldName);
                        }
                    }
                }
            }
            return config;
        }

        /// <summary>
        /// 判断是否为绑定表达式，判断依据为：以&=开头的，但是不能以&=&=开头（这是公式）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsExp(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.StartsWith("&=") && !value.StartsWith("&=&=");
        }

        /// <summary>
        /// 获取单元格的信息
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private CellConfig GetCellInfo(string value, int row, int column, string CellComment)
        {
            CellConfig cellConfig = null;
            if (IsExp(value))
            {
                cellConfig = new CellConfig();
                cellConfig.ColIndex = column;
                cellConfig.RowIndex = row;
                cellConfig.FieldName = ParseFieldName(value);
                cellConfig.isDate = ParseType(value, "DataType_Date");
                cellConfig.isDecimal = ParseType(value, "DataType_Decimal");
                cellConfig.FieldClass = ParseFieldClass(value);
                if (cellConfig.FieldClass == "Enum")
                {
                    cellConfig.EnumKey = ParseFieldValue(value, true);
                    cellConfig.FieldID = string.Empty;

                }
                else
                {
                    cellConfig.EnumKey = string.Empty;
                    cellConfig.FieldID = ParseFieldValue(value, false);
                }
            }
            return cellConfig;
        }

        /// <summary>
        /// 从表达式中解析表名称
        /// </summary>
        /// <returns></returns>
        private string ParseFieldName(string exp)
        {
            if (IsExp(exp))
            {
                var arr = exp.Replace("&=", "").Split('.');
                return arr[0].Trim("[]".ToCharArray());
            }
            return string.Empty;
        }

        private bool ParseType(string exp, string typeMode)
        {
            if (IsExp(exp))
            {
                var arr = exp.Replace("&=", "").Split('.');
                if (arr.Length > 1)
                    return arr[1].Trim("[]".ToCharArray()).StartsWith(typeMode);
            }
            return false;
        }
        private string ParseFieldClass(string exp)
        {
            if (IsExp(exp))
            {
                var arr = exp.Replace("&=", "").Split('.');
                if (arr.Length > 1)
                {
                    var fieldValue = arr[1].Trim("[]".ToCharArray());
                    var arr2 = fieldValue.Split('_');
                    if (arr2.Length > 2)
                        return arr2[1];
                    else
                        return string.Empty;
                }
            }
            return string.Empty;
        }
        private string ParseFieldValue(string exp, bool isEnum)
        {
            if (IsExp(exp))
            {
                var arr = exp.Replace("&=", "").Split('.');
                if (arr.Length > 1)
                {
                    var fieldValue = arr[1].Trim("[]".ToCharArray());
                    var arr2 = fieldValue.Split('_');
                    if (arr2.Length > 2)
                        if (isEnum)
                            return arr2[2].Replace(":", ".");
                        else
                            return arr2[2];
                }
            }
            return string.Empty;
        }
    }
}
