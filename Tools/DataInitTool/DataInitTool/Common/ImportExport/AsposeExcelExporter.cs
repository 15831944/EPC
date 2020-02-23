using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
using System.IO;


namespace Common.Logic
{
    /// <summary>
    /// Excel导出数据，采用第三方Aspose组件
    /// </summary>
    /// <remarks>
    /// 模板的批注规则：【枚举】、【内容】
    ///     如果是枚举的单元格
    ///         【枚举】Project.Formulate
    ///         【内容】简易□    纸质□    胶装□   加厚□                  普装□    简精□    精装□   其它□
    ///             
    ///     如果是需要格式化的单元格
    ///         【内容】  说明：  {0}   页
    ///         
    ///     如果是日期单元格
    ///         设置单元格的日期格式（Excel自带功能）
    ///         
    /// 模板的导入字段格式为：&=$ProjectName，前后都不能有空格或其它任意字符
    /// </remarks>
    public class AsposeExcelExporter : IExporter
    {
        public byte[] Export(IDictionary<string, object> dicVariable, byte[] templateBuffer)
        {
            return Export(dicVariable, null, templateBuffer);
        }

        public byte[] Export(DataTable dt, byte[] templateBuffer)
        {
            var ds = new DataSet();
            if (dt != null)
            {
                ds.Tables.Add(dt);
            }
            return Export(null, ds, templateBuffer);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="templateBuffer"></param>
        /// <returns></returns>
        public byte[] Export(IDictionary<string, object> dicVariable, DataSet ds, byte[] templateBuffer)
        {
            // 创建一个workbookdesigner对象
            WorkbookDesigner designer = new WorkbookDesigner();

            // 设置报表模板
            designer.Workbook = new Workbook(new MemoryStream(templateBuffer));

            designer.Workbook.BuiltInDocumentProperties.Keywords = "exportExcel";
            designer.Workbook.BuiltInDocumentProperties.Company = "goodwaysoft";
            designer.Workbook.BuiltInDocumentProperties.RevisionNumber = int.Parse(DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Second.ToString());
            designer.Workbook.BuiltInDocumentProperties.Author = "E2Home";

            var worksheet = designer.Workbook.Worksheets[0];
            Cells cells = worksheet.Cells;
            var comments = worksheet.Comments;
            // 设置DataSet对象，可以是多张表
            if (ds != null)
                designer.SetDataSource(ParseDataSource(ds, designer.Workbook.Worksheets[0]));

            //设置变量对象
            if (dicVariable != null)
            {
                foreach (var key in dicVariable.Keys)
                {
                    designer.SetDataSource(key, dicVariable[key]);
                }
            }

            // 根据数据源处理生成报表内容
            designer.Process();

            // 替换书签，用于处理格式化内容的情况，如：共{0}页
            foreach (Comment c in comments)
            {
                var cellValue = cells[c.Row, c.Column].StringValue.Trim();
                var dic = ParseNote(c.Note);
                foreach (var keywords in dic.Keys)
                {

                }
            }
            worksheet.Comments.Clear();

            // 将报表内容保存为文件流（字节数组）
            var excelStream = new MemoryStream();
            designer.Workbook.Save(excelStream, SaveFormat.Excel97To2003);
            var buffer = excelStream.ToArray();
            excelStream.Close();

            return buffer;
        }

        /// <summary>
        /// 解析报表模板
        /// </summary>
        public byte[] ParseTemplate(IList<ColumnInfo> columns, string excelKey, string title)
        {
            if (columns == null)
                throw new Exception("模板列信息不能为NULL");

            return CreateTemplate(columns, excelKey, title);
        }

        /// <summary>
        /// 动态生产简化模板，用于动态导出列表信息
        /// </summary>
        /// <param name="columns">模板中的列信息描述</param>
        /// <returns></returns>
        public byte[] CreateTemplate(IList<ColumnInfo> columns, string excelKey, string title)
        {
            var workbook = new Workbook();
            var sheet = (Worksheet)workbook.Worksheets[0];

            Cells cells = sheet.Cells;
            sheet.FreezePanes(2, 1, 2, 0);//冻结第一行
            //sheet.Name = jsonObject.sheetName;//接受前台的Excel工作表名

            //为标题设置样式     
            var styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //题头样式 
            var styleHeader = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleHeader.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleHeader.Font.Name = "宋体";//文字字体 
            styleHeader.Font.Size = 14;//文字大小 
            styleHeader.Font.IsBold = true;//粗体 
            styleHeader.IsTextWrapped = true;//单元格内容自动换行 
            styleHeader.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            styleHeader.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //内容样式
            var styleContent = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleContent.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleContent.Font.Name = "宋体";//文字字体 
            styleContent.Font.Size = 12;//文字大小 
            styleContent.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            styleContent.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //表格列数 
            var columnCount = columns.Count;

            //生成行1 标题行    
            cells.Merge(0, 0, 1, columnCount);//合并单元格 
            cells[0, 0].PutValue(title);//填写内容 
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 25);

            //生成题头列行 
            for (int i = 0; i < columnCount; i++)
            {
                cells[1, i].PutValue(columns[i].ChineseName);
                cells[1, i].SetStyle(styleHeader);
            }
            cells.SetRowHeight(1, 23);

            //生成内容行，第三行起始。变量绑定表达式 &=[Data Source].[Field Name] 或者 &=DataSource.FieldName
            for (int k = 0; k < columnCount; k++)
            {
                // 变量绑定表达式 &=[Data Source].[Field Name] 或者 &=DataSource.FieldName
                var exp = string.Format("&=[{0}].[{1}]", columns[k].TableName, columns[k].FieldName);
                cells[2, k].PutValue(exp);

                // 设置时间单元格格式
                if (!string.IsNullOrWhiteSpace(columns[k].DateFormat))
                {
                    styleContent.Number = 14;
                    cells[2, k].SetStyle(styleContent);
                    styleContent.Number = 0;
                }
                else
                {
                    cells[2, k].SetStyle(styleContent);
                }
            }
            cells.SetRowHeight(2, 22);


            //添加制表日期
            //cells[2 + rowCount, columnCount - 1].PutValue("制表日期:" + DateTime.Now.ToShortDateString());
            sheet.AutoFitColumns();//让各列自适应宽度
            //sheet.AutoFitRows();//让各行自适应宽度

            // 设置唯一Key，用于校验模板是否被修改过
            //workbook.BuiltInDocumentProperties.Comments = string.Join(",", columns.Select(c => c.FieldName).ToArray());
            //workbook.BuiltInDocumentProperties.Version = int.Parse(DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString());

            // 是否生产临时文件供跟踪测试
            // workbook.Save(@"D:/Tmpl.xls");

            // 将报表内容保存为文件流（字节数组）
            var excelStream = new MemoryStream();
            workbook.Save(excelStream, SaveFormat.Excel97To2003);
            var buffer = excelStream.ToArray();
            excelStream.Close();

            // 写入模板的元数据
            //new DefaultExcelMetadataStorage().AddMetadata(excelKey, buffer, string.Join(",", columns.Select(c => c.FieldName).ToArray()));

            return buffer;
        }

        /// <summary>
        /// 解析数据源
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet ParseDataSource(DataSet ds, Worksheet cell)
        {
            var columns = System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.Items["__ColumnInfo"] as List<ColumnInfo> : null;
            if (columns != null)
            {
                var enumColumns = columns.FindAll(c => !string.IsNullOrWhiteSpace(c.EnumKey) || !string.IsNullOrWhiteSpace(c.DateFormat));
                foreach (var col in enumColumns)
                {
                    var dt = ds.Tables[col.TableName];
                    if (dt == null || dt.Rows.Count == 0)
                        continue;

                    //if (!string.IsNullOrWhiteSpace(col.DateFormat)) //日期格式字段改为字符串型
                    //{
                    //    dt.Columns[col.FieldName].ColumnName = col.FieldName + "_";
                    //    dt.Columns.Add(col.FieldName);
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        row[col.FieldName] = row[col.FieldName + "_"].ToString();
                    //    }
                    //}

                    foreach (DataRow row in dt.Rows)
                    {
                        var fieldValue = row[col.FieldName].ToString();

                        // 处理枚举值
                        if (!string.IsNullOrWhiteSpace(col.EnumKey) && col.EnumKey != "NULL")
                        {
                            // 考虑多选枚举的情况
                            List<string> enumList = new List<string>();
                            var enumValues = fieldValue.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (var enumValue in enumValues)
                            {
                                var enumItem = col.EnumDataSource.FirstOrDefault(item => item.Value == enumValue);
                                if (enumItem != null)
                                {
                                    enumList.Add(enumItem.Text);
                                }
                            }
                            fieldValue = string.Join(",", enumList.ToArray());

                        }


                        // 处理日期字段
                        if (!string.IsNullOrWhiteSpace(col.DateFormat))
                        {
                            fieldValue = ParseDateTime(fieldValue, col.DateFormat);
                            if (fieldValue == "")
                            {
                                row[col.FieldName] = DBNull.Value;
                            }
                            else
                            {
                                //row[col.FieldName] = fieldValue;
                                row[col.FieldName] = DateTime.Parse(fieldValue);
                            }
                        }
                        else
                        {
                            row[col.FieldName] = fieldValue;
                        }
                    }
                }
            }

            #region 解决导出的excel不能统计的问题

            if (columns != null)
            {
                var numColumns = columns.Where(t => t.DataType == "int" || t.DataType == "float" || t.DataType == "decimal").ToList();
                foreach (var col in numColumns)
                {
                    var dt = ds.Tables[col.TableName];
                    var dc = dt.Columns[col.FieldName];
                    string newColumnName = dc.ColumnName;
                    dc.ColumnName = "_" + dc.ColumnName;

                    DataColumn newCol = null;
                    if (col.DataType == "int")
                    {
                        newCol = dt.Columns.Add(newColumnName, typeof(int));
                    }
                    else if (col.DataType == "float")
                    {
                        newCol = dt.Columns.Add(newColumnName, typeof(float));
                    }
                    else if (col.DataType == "decimal")
                    {
                        newCol = dt.Columns.Add(newColumnName, typeof(decimal));
                    }
                    if (newCol != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            row[newCol] = row[dc.ColumnName];
                        }
                        dt.Columns.Remove(dc);
                    }
                }
            }

            #endregion

            if (columns != null && cell.Comments.Count > 0)
            {
                DataTable dt = ds.Tables[columns[0].TableName];
                DataRow dr = dt.NewRow();
                DataColumn dc = new DataColumn("SUMMARYCOLUMN", typeof(string));
                dt.Columns.Add(dc);
                dt.Rows.Add(dr);
            }

            return ds;
        }

        // 解析日期格式
        public string ParseDateTime(string date, string format)
        {
            DateTime dt;
            if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out dt))
            {
                return dt.ToString(format);
            }

            return date ?? string.Empty;
        }

        /// <summary>
        /// 解析批注信息，获取单元格的元数据
        /// </summary>
        /// <param name="note">批注信息</param>
        /// <remarks>原理：找到所有模板关键字的位置，然后排序之后破开字符串。最后存入键值对中。</remarks>
        /// <returns></returns>
        public IDictionary<string, string> ParseNote(string note)
        {
            var dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(note))
            {
                // 获取所有模板关键字出现的位置
                var indexs = new List<int>();               
                indexs.Sort(); // 排序，从小到大

                // 将元数据内容存入键值对
                for (var i = 0; i < indexs.Count; i++)
                {
                    // 破开得到元数据内容项，如：【枚举】Type
                    var item = "";
                    if (i == indexs.Count - 1)
                    {
                        item = note.Substring(indexs[i]);
                    }
                    else
                    {
                        item = note.Substring(indexs[i], indexs[i + 1] - indexs[i]);
                    }

                    // 解析元数据到键值对中              
                }
            }

            return dic;
        }

    }
}
