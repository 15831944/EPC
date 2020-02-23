using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterfaceSyn.Common;
using System.Data;
using System.Windows.Forms;

namespace DataInterfaceSyn
{
    public class TargetTableStore
    {
        public List<TableModel> TargetTableModels { get; private set; }
        public TargetTableStore()
        {
            TargetTableModels = new List<TableModel>();
        }

        public void Prepare()
        {
            ReLoadTargetTableModels();
        }

        public void SaveFile()
        {
            string path = Application.StartupPath + "\\tableDefaultValue.dddff";
            DataManager.Save(path, TargetTableModels);
        }

        public void ReadFile()
        {
            string path = Application.StartupPath + "\\tableDefaultValue.dddff";
            TargetTableModels = DataManager.Load(path) as List<TableModel>;
        }

        private void ReLoadTargetTableModels()
        {
            List<TableModel> fileDataList = new List<TableModel>();
            string fileName = Application.StartupPath + "\\tableDefaultValue.dddff";
            if (System.IO.File.Exists(fileName))
            {
                fileDataList = DataManager.Load(fileName) as List<TableModel>;
            }
            List<TableModel> dataBaseList = GetTargetTables();
            foreach (var dataBaseModel in dataBaseList)
            {
                var fileData = fileDataList.FirstOrDefault(a => a.TableName == dataBaseModel.TableName);
                if (fileData != null)
                {
                    foreach (var dataBaseModelCol in dataBaseModel.Columns)
                    {
                        var fileDataCol = fileData.Columns.FirstOrDefault(a => a.Name == dataBaseModelCol.Name);
                        if (fileDataCol != null)
                        {
                            dataBaseModelCol.DefaultValue = fileDataCol.DefaultValue;
                        }
                    }
                }
            }

            TargetTableModels.AddRange(dataBaseList);
        }

        public IEnumerable<TableModel> GetMainTargetTables()
        {
            return TargetTableModels.Where(a => a.IsMain);
        }

        public IEnumerable<TableModel> GetSubTargetTables(string mainTargetTableName)
        {
            return TargetTableModels.Where(a => a.ForeignTableName == mainTargetTableName);
        }

        public TableModel GetTargetTableModel(string mainTargetTableName)
        {
            return TargetTableModels.FirstOrDefault(a => a.TableName == mainTargetTableName);
        }

        public string GetExcuteSql(Dictionary<string, object> sourceDic, XmlTableMap tableMap)
        {
            var sqlHelper = SQLHelper.CreateSqlHelper(EnumConn.DataInterface);
            string sql = "";
            string id = sourceDic.GetValue("ID");
            var targetDt = sqlHelper.ExecuteDataTable(string.Format("select top 1 * from {0} where id = '{1}'", tableMap.TargetTable, id));

            
            //检查targetDt是否有ModifyDate,如果存在则修复sourceDic(不存在该字段就补,该字段数据为空就补当前时间)
            FixSourceDicModifyDate(sourceDic, targetDt);

            if (targetDt.Rows.Count == 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in targetDt.Columns)
                {
                    dic.SetValue(dc.ColumnName, sourceDic.GetValue(dc.ColumnName));
                }
                foreach (var map in tableMap.SpecialFieldMaps)
                {
                    dic.SetValue(map.TargetField, sourceDic.GetValue(map.SourceField));
                }

                ////设置外键值
                //var tableModel = GetTargetTableModel(tableMap.TargetTable);
                //if (tableModel != null && !tableModel.IsMain)
                //{
                //    //没有映射到外键字段值得时候再取
                //    if (string.IsNullOrEmpty(dic.GetValue(tableModel.ForeignKeyField)))
                //    {
                //        if (string.IsNullOrEmpty(sourceDic.GetValue("ID")))
                //            LogWriter.Info(string.Format("【{0}】的查询结果必须包含ID,否则其子表的外键字段无法赋值", tableMap.SourceTable));

                //        dic.SetValue(tableModel.ForeignKeyField, "");
                //    }
                //}

                //由TableModel进行字段默认值进行补充
                SetDefaultValueFromWebConfigSetting(dic, targetDt.Columns, tableMap.TargetTable);

                dic.SetValue("SynDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                sql = dic.CreateInsertSql(sqlHelper, tableMap.TargetTable, dic.GetValue("ID"));
            }
            else
            {
                DataRow targetDr = targetDt.Rows[0];
                DateTime targetModifyDate = DateTime.MinValue;
                DateTime sourceModifyDate = DateTime.MaxValue;

                //如果目标存在ModifyDate才取值，并判断是否更新，否则总是更新
                if (targetDt.Columns.Contains("ModifyDate"))
                {
                    DateTime.TryParse(targetDr["ModifyDate"].ToString(), out targetModifyDate);
                    DateTime.TryParse(sourceDic["ModifyDate"].ToString(), out sourceModifyDate);
                }                
               
                //旧数据要更新
                if (targetModifyDate < sourceModifyDate)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    foreach (DataColumn dc in targetDt.Columns)
                    {
                        //源无此列，则赋值目标
                        if (!sourceDic.ContainsKey(dc.ColumnName))
                        {
                            dic.SetValue(dc.ColumnName, targetDr[dc.ColumnName]);
                        }
                        else
                        {
                            dic.SetValue(dc.ColumnName, sourceDic.GetValue(dc.ColumnName));
                        }
                    }
                    foreach (var map in tableMap.SpecialFieldMaps)
                    {
                        dic.SetValue(map.TargetField, sourceDic.GetValue(map.SourceField));
                    }

                    //由TableModel进行字段默认值进行补充
                    SetDefaultValueFromWebConfigSetting(dic, targetDt.Columns, tableMap.TargetTable);

                    dic.SetValue("SynDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    sql = dic.CreateUpdateSql(sqlHelper, tableMap.TargetTable, dic.GetValue("ID"));
                }
            }
            return sql;
        }

        private void FixSourceDicModifyDate(Dictionary<string, object> sourceDic, DataTable targetDt)
        {
            if (!targetDt.Columns.Contains("ModifyDate"))
            {
                return;
            }

            //如果都存在ModifyDate则进行数据是否更新校验
            if (string.IsNullOrEmpty(sourceDic.GetValue("ModifyDate")))
            {
                sourceDic.SetValue("ModifyDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }

        private void SetDefaultValueFromWebConfigSetting(Dictionary<string, object> targetDic, DataColumnCollection dcc, string tableName)
        {
            TableModel targetTable = TargetTableModels.FirstOrDefault(a => a.TableName == tableName);
            if (targetTable != null)
            {
                foreach (var col in targetTable.Columns)
                {
                    //有该列，但又没有赋值过
                    if (!string.IsNullOrEmpty(col.DefaultValue) 
                        && string.IsNullOrEmpty(targetDic.GetValue(col.Name))
                        && dcc.Contains(col.Name))
                    {
                        targetDic.SetValue(col.Name, col.DefaultValue);
                    }
                }
            }
        }

        public bool ExcuteSql(Dictionary<string, object> sourceDic, XmlTableMap tableMap)
        {
            string sql = GetExcuteSql(sourceDic, tableMap);
            if(!string.IsNullOrEmpty(sql))
            {
                try
                {
                    SQLHelper.CreateSqlHelper(EnumConn.DataInterface).ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    LogWriter.Info(string.Format("在对表{0}进行增改数据操作的时候出错,数据为【{1}】,错误内容为{2}", tableMap.TargetTable, JsonHelper.ToJson(sourceDic), ex.Message));
                    return false;
                }
            }
            
            return true;
        }

        public static List<TableModel> GetTargetTables()
        {
            List<TableModel> resList = new List<TableModel>();

            var sqlHelper = SQLHelper.CreateSqlHelper(EnumConn.DataInterface);
            //表名、主表名、外键列名
            string sql = @"select subTable.name TableName,mainTable.MainTableName ForeignTableName,t2.name ForeignKeyField
                         from sysobjects subTable
                         left join 
                         (select sysobjects.name MainTableName, fk.fkeyid fkeyid from sysobjects 
                         inner join sysforeignkeys fk on sysobjects.id = fk.rkeyid) mainTable
                         on subTable.id = mainTable.fkeyid
                         left join SYS.FOREIGN_KEY_COLUMNS t1 on subTable.id = t1.parent_object_id
                         left join SYS.SYSCOLUMNS t2 on t2.id = t1.parent_object_id AND t1.parent_column_id=t2.colid
                         where subTable.XType='U'";
            var dt = sqlHelper.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                TableModel tm = new TableModel();
                tm.TableName = dr["TableName"].ToString();
                tm.ForeignTableName = dr["ForeignTableName"].ToString();
                tm.ForeignKeyField = dr["ForeignKeyField"].ToString();
                resList.Add(tm);

                string colSql = @"SELECT 
                                Name = A.NAME,
                                IsPK = CASE WHEN EXISTS(SELECT 1 FROM SYSOBJECTS WHERE XTYPE='PK' AND PARENT_OBJ=A.ID AND NAME IN (
                                SELECT NAME FROM SYSINDEXES WHERE INDID IN(
                                SELECT INDID FROM SYSINDEXKEYS WHERE ID=A.ID AND COLID=A.COLID))) THEN 'true' ELSE 'false' END,
                                ValueType = B.NAME,
                                AllowEmpty = CASE WHEN A.ISNULLABLE=1 THEN 'true 'ELSE 'false' END 
                                FROM 
                                SYSCOLUMNS A
                                LEFT JOIN SYSTYPES B ON A.XUSERTYPE=B.XUSERTYPE
                                INNER JOIN SYSOBJECTS D ON A.ID=D.ID AND D.XTYPE='U ' --AND D.NAME<>'DTPROPERTIES'
                                LEFT JOIN SYSCOMMENTS E ON A.CDEFAULT=E.ID
                                LEFT JOIN sys.extended_properties G ON A.ID=G.major_id AND A.COLID=G.minor_id   
                                LEFT JOIN sys.extended_properties F ON D.ID=F.major_id AND F.minor_id=0
                                where D.NAME='{0}'
                                ORDER BY A.ID,A.COLORDER ";

                var colDt = sqlHelper.ExecuteDataTable(string.Format(colSql, tm.TableName));

                foreach (DataRow colDr in colDt.Rows)
                {
                    TableModelColumn col = new TableModelColumn();
                    tm.Columns.Add(col);

                    col.Name = colDr["Name"].ToString();
                    col.IsPK = colDr["IsPK"].ToString().ToLower() == "true";
                    col.AllowEmpty = colDr["AllowEmpty"].ToString().ToLower() == "true";
                    col.ValueType = colDr["ValueType"].ToString();
                }
            }

            return resList;
        }
    }
}
