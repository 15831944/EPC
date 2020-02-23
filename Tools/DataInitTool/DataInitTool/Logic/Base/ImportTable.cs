using Common.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataInitTool
{
    public partial class ImportTable : IValidation
    {
        public string TableName
        {
            get;
            set;
        }

        public string SheetName
        {
            get;
            set;
        }

        public DataTable ImportDataTable
        {
            get;
            set;
        }

        public ExcelTableInfo SheetData
        {
            get;
            set;
        }

        public SQLHelper DB
        {
            get;
            set;
        }

        List<FieldConfig> fields = new List<FieldConfig>();
        public List<FieldConfig> FieldConfigs
        {
            get { return fields; }
        }

        List<CellErrorInfo> FillImportData(GlobalData gData)
        {
            var result = new List<CellErrorInfo>();
            if (this.ImportDataTable == null)
                throw new Exception("输入数据内容为空，无法填充导入数据");
            if (this.SheetData == null)
                throw new Exception("Excel表格数据为空，无法填充导入数据");
            for (int i = 0; i < this.SheetData.Rows.Count; i++)
            {
                var sheetRow = this.SheetData.Rows[i];
                bool validatePass = true;
                var newRow = this.ImportDataTable.NewRow();
                newRow["RowIndex"] = sheetRow.RowIndex;
                newRow["ID"] = FormulaHelper.CreateGuid();
                for (int m = 0; m < this.FieldConfigs.Count; m++)
                {
                    var fieldConfig = this.FieldConfigs[m];
                    var cellValue = sheetRow[fieldConfig.FieldName].Trim();
                    #region 必填校验
                    if (fieldConfig.IsRequired && String.IsNullOrEmpty(cellValue))
                    {
                        result.Add(new CellErrorInfo
                        {
                            TableName = this.SheetName,
                            ColIndex = m,
                            RowIndex = sheetRow.RowIndex,
                            ErrorText = string.Format("该单元格必填")
                        });
                        validatePass = false;
                    }
                    #endregion

                    #region 枚举并校验填充
                    if (!String.IsNullOrWhiteSpace(cellValue) && fieldConfig.IsEnum && !string.IsNullOrEmpty(fieldConfig.EnumKey))
                    {
                        var enumItems = gData.StaticEnumItems.AsEnumerable().Where(c => c["EnumKey"] != DBNull.Value
                        && c["EnumKey"].ToString() == fieldConfig.EnumKey).ToList();
                        var value = string.Empty;
                        var valueItems = cellValue.Replace("，", ",").Split(',');
                        var likeText = new List<string>();
                        foreach (var item in enumItems)
                        {
                            if (valueItems.Contains(item["text"].ToString())) { value += item["value"].ToString() + ","; }
                            foreach (var vitem in valueItems)
                            {
                                if (item["text"].ToString().IndexOf(vitem) >= 0) likeText.Add(item["text"].ToString());
                            }
                        }
                        if (string.IsNullOrEmpty(value))
                        {
                            result.Add(new CellErrorInfo
                            {
                                TableName = this.SheetName,
                                ColIndex = m,
                                RowIndex = sheetRow.RowIndex,
                                ErrorText = string.Format("枚举项[{0}]不正确，可能为{1}", cellValue, string.Join(",", likeText.Distinct()))
                            });
                            validatePass = false;
                        }
                        else
                        {
                            cellValue = value.TrimEnd(',');
                        }
                    }
                    #endregion

                    #region 唯一性校验
                    if (fieldConfig.IsUnique && !String.IsNullOrEmpty(cellValue) && this.ImportDataTable.Columns.Contains(fieldConfig.FieldName))
                    {
                        if (this.ImportDataTable.AsEnumerable().Count(c => c[fieldConfig.FieldName] != DBNull.Value
                            && c[fieldConfig.FieldName].ToString() == cellValue)>0)
                        {
                            result.Add(new CellErrorInfo
                            {
                                TableName = this.SheetName,
                                ColIndex = m,
                                RowIndex = sheetRow.RowIndex,
                                ErrorText = string.Format("存在重复数据")
                            });
                            validatePass = false;
                        }
                    }
                    #endregion


                    if (this.ImportDataTable.Columns.Contains(fieldConfig.FieldName))
                    {
                        if (fieldConfig.DataType.ToLower() == "datetime")
                        {
                            if (!String.IsNullOrEmpty(cellValue))
                            {
                                newRow[fieldConfig.FieldName] = Convert.ToDateTime(cellValue);
                            }
                        }
                        else if (fieldConfig.DataType.ToLower() == "decimal")
                        {
                            if (!String.IsNullOrEmpty(cellValue))
                            {
                                newRow[fieldConfig.FieldName] = Convert.ToDecimal(cellValue);
                            }
                        }
                        else
                        {
                            newRow[fieldConfig.FieldName] = cellValue;
                        }
                    }

                    #region 关联数据校验并填充
                    if (!String.IsNullOrWhiteSpace(cellValue) && fieldConfig.IsRelateField && !String.IsNullOrEmpty(fieldConfig.RelateSheetName))
                    {

                        DataTable relationImportDt = null;
                        if (fieldConfig.RelateSheetName == "User" || fieldConfig.RelateSheetName == "用户")
                        {
                            #region 校验用户信息
                            if (String.IsNullOrEmpty(fieldConfig.RelateFieldName))
                            {
                                fieldConfig.RelateFieldName = "Name";
                            }
                            relationImportDt = gData.UserDt;
                            if (!relationImportDt.Columns.Contains(fieldConfig.RelateFieldName))
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "用户信息表中不包含列【" + fieldConfig.RelateFieldName + "】"
                                });
                                validatePass = false;
                                continue;
                            }
                            var unitCellValues = cellValue.Split(',');
                            foreach (var item in unitCellValues)
                            {
                                var users = relationImportDt.AsEnumerable().FirstOrDefault(c => c[fieldConfig.RelateFieldName] != DBNull.Value
                                && c[fieldConfig.RelateFieldName].ToString() == item);
                                if (users == null)
                                {
                                    result.Add(new CellErrorInfo
                                    {
                                        TableName = this.SheetName,
                                        ColIndex = m,
                                        RowIndex = sheetRow.RowIndex,
                                        ErrorText = "用户不存在"
                                    });
                                    validatePass = false;
                                    continue;
                                }
                            }
                            #endregion
                        }
                        else if (fieldConfig.RelateSheetName == "Dept" || fieldConfig.RelateSheetName == "部门")
                        {
                            #region 组织机构
                            relationImportDt = gData.OrgDt;
                            if (String.IsNullOrEmpty(fieldConfig.RelateFieldName))
                            {
                                fieldConfig.RelateFieldName = "Name";
                            }
                            if (!relationImportDt.Columns.Contains(fieldConfig.RelateFieldName))
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "部门信息表中不包含列【" + fieldConfig.RelateFieldName + "】"
                                });
                                validatePass = false;
                                continue;
                            }
                            var unitCellValues = cellValue.Split(',');
                            foreach (var item in unitCellValues)
                            {
                                var orgs = relationImportDt.AsEnumerable().FirstOrDefault(c => c[fieldConfig.RelateFieldName] != DBNull.Value
                                 && c[fieldConfig.RelateFieldName].ToString() == item);
                                if (orgs == null)
                                {
                                    result.Add(new CellErrorInfo
                                    {
                                        TableName = this.SheetName,
                                        ColIndex = m,
                                        RowIndex = sheetRow.RowIndex,
                                        ErrorText = "部门不存在"
                                    });
                                    validatePass = false;
                                    continue;
                                }
                            }
                            #endregion
                        }
                        else if (String.IsNullOrEmpty(fieldConfig.RelateFieldName))
                        {
                            result.Add(new CellErrorInfo
                            {
                                TableName = this.SheetName,
                                ColIndex = m,
                                RowIndex = sheetRow.RowIndex,
                                ErrorText = "关联字段为空【" + fieldConfig.RelateFieldName + "】"
                            });
                            validatePass = false;
                            continue;
                        }
                        else
                        {
                            #region 选择器校验
                            if (!gData.Data.ContainsKey(fieldConfig.RelateSheetName))
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "对应的工作簿【" + fieldConfig.RelateSheetName + "】没有找到"
                                });
                                validatePass = false;
                                continue;
                            }
                            var relateSheetImportData = gData.Data[fieldConfig.RelateSheetName];
                            if (relateSheetImportData == null)
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "对应的工作簿【" + fieldConfig.RelateSheetName + "】没有找到"
                                });
                                validatePass = false;
                                continue;
                            }
                            if (String.IsNullOrWhiteSpace(fieldConfig.RelateValue))
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "没有设置返回值，请检查配置信息"
                                });
                                validatePass = false;
                                continue;
                            }
                            relationImportDt = relateSheetImportData.ImportDataTable;
                            if (!relationImportDt.Columns.Contains(fieldConfig.RelateFieldName))
                            {
                                result.Add(new CellErrorInfo
                                {
                                    TableName = this.SheetName,
                                    ColIndex = m,
                                    RowIndex = sheetRow.RowIndex,
                                    ErrorText = "数据表【" + relateSheetImportData.TableName + "】不包含列【" + fieldConfig.RelateFieldName + "】，请检查配置或Excel模板"
                                });
                                validatePass = false;
                                continue;
                            }
                            var unitCellValues = cellValue.Split(',');
                            foreach (var item in unitCellValues)
                            {
                                var count = relationImportDt.AsEnumerable().
                                    Count(c => c[fieldConfig.RelateFieldName] != DBNull.Value && c[fieldConfig.RelateFieldName].ToString() == item);
                                if (count == 0)
                                {
                                    result.Add(new CellErrorInfo
                                    {
                                        TableName = this.SheetName,
                                        ColIndex = m,
                                        RowIndex = sheetRow.RowIndex,
                                        ErrorText = "数据表【" + relateSheetImportData.TableName + "】找不到数据【" + item + "】"
                                    });
                                    validatePass = false;
                                    continue;
                                }
                                if (count > 1)
                                {
                                    result.Add(new CellErrorInfo
                                    {
                                        TableName = this.SheetName,
                                        ColIndex = m,
                                        RowIndex = sheetRow.RowIndex,
                                        ErrorText = "数据表【" + relateSheetImportData.TableName + "】中【" + fieldConfig.FieldName + "】数据不唯一"
                                    });
                                    validatePass = false;
                                    continue;
                                }
                            }
                            #endregion
                        }
                        if (!validatePass) continue;
                        var cellValueList = cellValue.Split(',');
                        var returnParams = fieldConfig.RelateValue.Replace(",", ";").Split(';');
                        foreach (var returnParam in returnParams)
                        {
                            var fieldInfos = returnParam.Split(':');
                            var targetField = fieldInfos[0];
                            var sourceField = fieldInfos[1];
                            var targerValue = "";
                            foreach (var item in cellValueList)
                            {
                                var relateRow = relationImportDt.AsEnumerable().
                                    FirstOrDefault(c => c[fieldConfig.RelateFieldName] != DBNull.Value && c[fieldConfig.RelateFieldName].ToString().Trim() == item.Trim());
                                if (relateRow == null)
                                {
                                    continue;
                                }
                                targerValue += relateRow[sourceField] != null &&
                                    relateRow[sourceField] != DBNull.Value && !String.IsNullOrEmpty(relateRow[sourceField].ToString())
                                    ? relateRow[sourceField].ToString() + "," : "";
                            }
                            newRow[targetField] = targerValue.TrimEnd(',');
                        }
                    }
                    #endregion

                }
                if (!validatePass) { continue; }
                this.ImportDataTable.Rows.Add(newRow);
              
            }
            return result;
        }

        public List<CellErrorInfo> ValidateSheet(GlobalData gData)
        {
            var result = this.SheetData.Vaildate(e =>
              {
                  var fieldConfig = this.FieldConfigs.FirstOrDefault(c => c.FieldName == e.FieldName);
                  if (fieldConfig == null)
                  {
                      e.IsValid = false;
                      e.ErrorText = "没有找到字段的定义信息，验证失败";
                  }
                  else if (!String.IsNullOrWhiteSpace(e.Value) && fieldConfig.DataType.ToLower() == "datetime")
                  {
                      #region 校验是否日期格式
                      var isDate = false;
                      var list = e.Value.Replace("/", "-").Replace(".", "-").Split('-');
                      if (list.Length == 3)
                      {
                          var tmp = list[0].PadLeft(4, '0') + "-" + list[1].PadLeft(2, '0') + "-" + list[2].PadLeft(2, '0');
                          Regex reg = new Regex("(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)");
                          isDate = reg.IsMatch(tmp);
                          if (int.Parse(list[0]) % 4 == 0 && int.Parse(list[0]) % 100 != 0 || int.Parse(list[0]) % 400 == 0)
                          {
                              if (int.Parse(list[1]) == 2 && int.Parse(list[2]) == 29)
                                  isDate = true;
                          }
                      }
                      else
                          isDate = false;
                      #endregion
                      if (!isDate)
                      {
                          e.IsValid = false;
                          e.ErrorText = "此单元格请输入日期，日期格式yyyy/MM/dd或yyyy-MM-dd或yyyy.MM.dd";
                      }
                  }
                  else if (!String.IsNullOrWhiteSpace(e.Value) && fieldConfig.DataType.ToLower() == "decimal")
                  {
                      #region 校验数字
                      Regex reg = new Regex(@"^(-?\d+)(\.\d+)?$");
                      if (!reg.IsMatch(e.Value))
                      {
                          e.IsValid = false;
                          e.ErrorText = "此单元格请输入非零数字";
                      }
                      #endregion
                  }
              }).ToList();
            if (result.Count > 0)
                return result;
            var errors = FillImportData(gData);
            result.AddRange(errors);
            return result;
        }

        public List<CellErrorInfo> Import(GlobalData gData)
        {
            var result = new List<CellErrorInfo>();
            foreach (DataRow row in this.ImportDataTable.Rows)
            {
                try
                {
                    BeforeInsert(row, this.SheetName, this.TableName);
                    var columns = row.Table.Columns;
                    string insertSQL = @"
if not exists (select ID from " + this.TableName + @" where ID='" + row["ID"].ToString() + @"')
 insert into " + this.TableName + "({1}) values ({0})";
                    string insertValue = string.Empty;
                    string columnNames = string.Empty;
                    foreach (DataColumn column in columns)
                    {
                        if (column.ColumnName == "RowIndex")
                        {
                            continue;
                        }
                        #region 填充默认属性
                        if (row[column.ColumnName] == null || row[column.ColumnName] == DBNull.Value || String.IsNullOrWhiteSpace(row[column.ColumnName].ToString()))
                        {
                            if (column.ColumnName == "CreateUser" || column.ColumnName == "CreateUserName")
                            {
                                row[column.ColumnName] = gData.UserInfoName;
                            }
                            else if (column.ColumnName == "CreateUserID")
                            {
                                row[column.ColumnName] = gData.UserInfoID;
                            }
                            else if (column.ColumnName == "CreateDate")
                            {
                                row[column.ColumnName] = DateTime.Now;
                            }
                        }
                        if (column.DataType != typeof(String) &&
                            (row[column.ColumnName] == null || row[column.ColumnName] == DBNull.Value
                            || String.IsNullOrWhiteSpace(row[column.ColumnName].ToString())
                            ))
                        {
                            continue;
                        }
                        #endregion
                        insertValue += "'" + row[column.ColumnName].ToString() + "',";
                        columnNames += column.ColumnName + ",";
                    }
                    if (String.IsNullOrWhiteSpace(insertValue) || String.IsNullOrWhiteSpace(columnNames))
                        continue;
                    this.DB.ExecuteNonQuery(String.Format(insertSQL, insertValue.TrimEnd(','), columnNames.TrimEnd(',')));
                    AfterInsert(row, this.SheetName, this.TableName);
                }
                catch (Exception exp)
                {
                    LogWriter.Error(exp);
                    if (row["RowIndex"] != null && row["RowIndex"] != DBNull.Value && !String.IsNullOrEmpty(row["RowIndex"].ToString()))
                    {
                        result.Add(new CellErrorInfo
                        {
                            TableName = this.SheetName,
                            ColIndex = 0,
                            RowIndex = Convert.ToInt32(row["RowIndex"]),
                            ErrorText = exp.Message
                        });
                    }
                }
            }
            return result;
        }

        partial void BeforeInsert(DataRow row, string SheetName, string TableName);

        partial void AfterInsert(DataRow row, string SheetName, string TableName);
    }
}
