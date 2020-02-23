using Common.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DataInitTool
{
    public class GlobalData
    {
        public static GlobalData CreateGlobalData(string excelFilePath)
        {
            try
            {
                #region EXCEL模板校验
                string templatePath = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel.xlsx";
                if (String.IsNullOrEmpty(templatePath))
                {
                    throw new Exception("模板文件路径为空，无法获取全局数据");
                }
                if (!File.Exists(templatePath))
                {
                    throw new Exception("没有找到指定选择路径的模板文件，无法读取数据");
                }
                if (String.IsNullOrEmpty(excelFilePath))
                {
                    throw new Exception("EXCEL文件路径为空，无法获取全局数据");
                }
                if (!System.IO.File.Exists(excelFilePath))
                {
                    throw new Exception("没有找到指定选择路径的EXCEL 文件，无法读取数据");
                }
                #endregion

                var golalData = new GlobalData();

                golalData.ErrorExcelPath = Path.GetDirectoryName(excelFilePath) + "\\" + Path.GetFileNameWithoutExtension(excelFilePath) + "_校验错误.xls";
                var k = 1;
                while (File.Exists(golalData.ErrorExcelPath))
                {
                    golalData.ErrorExcelPath = Path.GetDirectoryName(excelFilePath) + "\\" + Path.GetFileNameWithoutExtension(excelFilePath) + "_校验错误_" + k + ".xls";
                    k++;
                }

                golalData.ImportErrorExcelPath = Path.GetDirectoryName(excelFilePath) + "\\" + Path.GetFileNameWithoutExtension(excelFilePath) + "_导入错误.xls";
                k = 1;
                while (File.Exists(golalData.ImportErrorExcelPath))
                {
                    golalData.ImportErrorExcelPath = Path.GetDirectoryName(excelFilePath) + "\\" + Path.GetFileNameWithoutExtension(excelFilePath) + "_导入错误_" + k + ".xls";
                    k++;
                }


                #region 装载数据库访问对象
                for (int i = 0; i < System.Configuration.ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    var conn = System.Configuration.ConfigurationManager.ConnectionStrings[i];
                    if (golalData.DBList.ContainsKey(conn.Name))
                        continue;
                    golalData.DBList.Add(conn.Name, SQLHelper.Create(conn));
                }
                #endregion
                golalData.FileBuffer = null;
                using (var fileStream = new FileStream(excelFilePath, FileMode.Open))
                {
                    var fileSize = fileStream.Length;
                    golalData.FileBuffer = new byte[fileSize];
                    fileStream.Read(golalData.FileBuffer, 0, (int)fileSize);
                    fileStream.Close();
                }
                if (golalData.FileBuffer == null)
                {
                    throw new Exception("读取了空的文件");
                }


                IImporter importer = new AsposeExcelImporter();
                ExcelData data = importer.Import(golalData.FileBuffer, templatePath);
                var errors = new List<CellErrorInfo>();
                if (data.Tables.Count == 0) throw new Exception("模版配置不正确");
                var config = _loadConfigFile();
                golalData.UserInfoID = config.GetValue("DefaultUser");
                golalData.UserInfoName = config.GetValue("DefaultUserName");
                var templateList = JsonHelper.ToList(config.GetValue("TemplateConfig"));
                for (int i = 0; i < data.Tables.Count; i++)
                {
                    var sheetTable = data.Tables[i];
                    if (golalData.Data.ContainsKey(sheetTable.TableName))
                    {
                        continue;
                    }
                    var tableConfig = templateList.FirstOrDefault(c => c["SheetName"].ToString() == sheetTable.TableName);
                    if (tableConfig == null)
                    {
                        throw new Exception("【" + sheetTable.TableName + "】没有找到相关的配置定义信息，无法导入");
                    }
                    var importData = new ImportTable();
                    importData.SheetName = sheetTable.TableName;
                    importData.TableName = tableConfig.GetValue("TableName");
                    importData.SheetData = sheetTable;
                    if (!golalData.DBList.ContainsKey(tableConfig.GetValue("DB")) || golalData.DBList[tableConfig.GetValue("DB")] == null)
                    {
                        throw new Exception("【" + sheetTable.TableName + "】的数据库定义没有找到，请确认链接字符串中有【" + tableConfig.GetValue("DB") + "】的数据库链接配置");
                    }

                    #region 初始化字段配置信息（包含EXCEL字段定义和配置文件字段定义)
                    var requiredFields = tableConfig.GetValue("Required").Split(',');
                    var uniqueFields = tableConfig.GetValue("Unique").Split(',');
                    importData.DB = golalData.DBList[tableConfig.GetValue("DB")];
                    var selectorFields = JsonHelper.ToList(tableConfig.GetValue("SelectorFields"));
                    foreach (var selectorConfig in selectorFields)
                    {
                        var selectorField = selectorConfig.GetValue("FieldName");
                        if (String.IsNullOrEmpty(selectorField))
                        {
                            throw new Exception("【" + sheetTable.TableName + "】关联数据定义中，有字段名为空的配置，请修改配置");
                        }
                        if (sheetTable.Structure.Cells.Count(c => c.FieldName == selectorConfig.GetValue("FieldName")) == 0)
                        {
                            throw new Exception(String.Format("【{1}】关联数据定义中，字段【{0}】在EXCEL模板中不存在，请检查模板"
                                , selectorField, sheetTable.TableName ));
                        }
                    }
                    foreach (var cellConfig in sheetTable.Structure.Cells)
                    {
                        var fieldConfig = new FieldConfig();
                        fieldConfig.FieldName = cellConfig.FieldName;
                        if (cellConfig.isDate)
                        {
                            fieldConfig.DataType = "DateTime";
                        }
                        else if (cellConfig.isDecimal)
                        {
                            fieldConfig.DataType = "Decimal";
                        }
                        if (!String.IsNullOrEmpty(cellConfig.EnumKey))
                        {
                            fieldConfig.IsEnum = true;
                            fieldConfig.EnumKey = cellConfig.EnumKey;
                        }
                        else if (cellConfig.FieldClass == "User")
                        {
                            fieldConfig.IsUser = true;
                        }
                        else if (cellConfig.FieldClass == "Dept")
                        {
                            fieldConfig.IsOrg = true;
                        }
                        fieldConfig.IsRequired = requiredFields.Contains(cellConfig.FieldName);
                        fieldConfig.IsUnique = uniqueFields.Contains(cellConfig.FieldName);
                        var selector = selectorFields.FirstOrDefault(c => c["FieldName"].ToString() == cellConfig.FieldName);
                        if (selector != null)
                        {
                            fieldConfig.IsRelateField = true;
                            fieldConfig.RelateFieldName = selector.GetValue("RelateFieldName");
                            fieldConfig.RelateSheetName = selector.GetValue("SheetName");
                            fieldConfig.RelateValue = selector.GetValue("ReturnValue");
                        }
                        importData.FieldConfigs.Add(fieldConfig);
                    }
                    #endregion

                    string sql = "select '' RowIndex,* from {0}";
                    importData.ImportDataTable = importData.DB.ExecuteDataTable(String.Format(sql, importData.TableName));
                    golalData.Data.Add(sheetTable.TableName, importData);
                }
                return golalData;
            }
            catch (Exception exp)
            {
                LogWriter.Error(exp, exp.Message);
                throw exp;
            }
        }

        static Dictionary<string, object> _loadConfigFile()
        {
            string configFile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Settings.json";
            if (!System.IO.File.Exists(configFile))
            {
                throw new Exception("没有找到配置定义文件，初始化定义失败");
            }
            var configJSON = string.Empty;
            configJSON = File.ReadAllText(configFile);
            if (String.IsNullOrEmpty(configJSON))
            {
                throw new Exception("配置信息没有任何内容，初始化定义失败");
            }
            var config = JsonHelper.ToObject(configJSON);
            return config;
        }

        static GlobalData()
        {

        }

        public string UserInfoID
        {
            get;
            set;
        }

        public string UserInfoName
        { get; set; }

        public byte[] FileBuffer
        {
            get;
            set;
        }

        public string ErrorExcelPath
        {
            get;
            set;
        }

        public string ImportErrorExcelPath
        {
            get;
            set;
        }

        DataTable _userDt = null;
        public DataTable UserDt
        {
            get
            {
                if (_userDt == null)
                {
                    if (!this.DBList.ContainsKey("Base") || this.DBList["Base"] == null)
                    {
                        throw new Exception("没有找到BASE数据库的配置链接，无法获取用户信息");
                    }
                    var db = this.DBList["Base"];
                    _userDt = db.ExecuteDataTable("select ID,Code,Name from S_A_User");
                }
                return _userDt;
            }
        }

        DataTable _orgDt = null;
        public DataTable OrgDt
        {
            get
            {
                if (_orgDt == null)
                {
                    if (!this.DBList.ContainsKey("Base") || this.DBList["Base"] == null)
                    {
                        throw new Exception("没有找到BASE数据库的配置链接，无法获取组织信息");
                    }
                    var db = this.DBList["Base"];
                    _orgDt = db.ExecuteDataTable("select ID,FullID,Name from S_A_Org where Type!='Post'");
                }
                return _orgDt;
            }
        }

        DataTable _staticEnumItemDt = null;
        public DataTable StaticEnumItems
        {
            get
            {
                if (_staticEnumItemDt == null)
                {
                    if (!this.DBList.ContainsKey("Base") || this.DBList["Base"] == null)
                    {
                        throw new Exception("没有找到BASE数据库的配置链接，无法获取枚举信息");
                    }
                    var db = this.DBList["Base"];
                    _staticEnumItemDt = db.ExecuteDataTable(@"select S_M_EnumItem.Code as value,S_M_EnumItem.Name as text,S_M_EnumDef.Code as EnumKey from S_M_EnumItem
left join S_M_EnumDef on S_M_EnumItem.EnumDefID=S_M_EnumDef.ID");

                    if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TableEnumKeys"]))
                    {
                        var tableEnumKeys = System.Configuration.ConfigurationManager.AppSettings["TableEnumKeys"];
                        var tableEnumKeyList = tableEnumKeys.Split(',');
                        foreach (var tableEnumKey in tableEnumKeyList)
                        {
                            var dt = db.ExecuteDataTable(String.Format(@"select * from S_M_EnumDef where Type='Table' and Code='{0}'", tableEnumKey.Trim()));
                            if (dt.Rows.Count == 0)
                            {
                                continue;
                            }
                            try
                            {
                                var insDB = this.DBList[dt.Rows[0]["ConnName"].ToString()];
                                var enumDt = insDB.ExecuteDataTable(dt.Rows[0]["Sql"].ToString() + " " + dt.Rows[0]["OrderBy"].ToString());
                                foreach (DataRow enumRow in enumDt.Rows)
                                {
                                    var newRow = _staticEnumItemDt.NewRow();
                                    newRow["EnumKey"] = tableEnumKey;
                                    newRow["value"] = enumRow["value"];
                                    newRow["text"] = enumRow["text"];
                                    _staticEnumItemDt.Rows.Add(newRow);
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
                return _staticEnumItemDt;
            }
        }

        Dictionary<string, ImportTable> _Data = new Dictionary<string, ImportTable>();
        public Dictionary<string, ImportTable> Data
        {
            get
            {
                return _Data;
            }
        }

        Dictionary<string, SQLHelper> _dbList = new Dictionary<string, SQLHelper>();
        public Dictionary<string, SQLHelper> DBList
        {
            get
            {
                return _dbList;
            }
        }

    }

    public class FieldConfig
    {
        public string FieldName
        {
            get;
            set;
        }

        public bool IsRequired
        {
            get;
            set;
        }

        string _dataType = "String";
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public bool IsUser
        {
            get;
            set;
        }

        public bool IsOrg
        {
            get;
            set;
        }

        public bool IsEnum
        {
            get;
            set;
        }

        public string EnumKey
        {
            get;
            set;
        }

        public bool IsUnique
        {
            get;
            set;
        }

        public bool IsRelateField
        {
            get;
            set;
        }

        public string RelateSheetName
        {
            get;
            set;
        }

        public string RelateFieldName
        {
            get;
            set;
        }

        public string RelateValue
        {
            get;
            set;
        }
    }
}