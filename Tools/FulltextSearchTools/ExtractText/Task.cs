using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using EsHelper;
using ExtractTextHelper;
using Config.Logic;
using Formula.Helper;

namespace ExtractText
{
    public class Task
    {
        const string conStrTemplate = "Data Source={0};User ID={1};PWD={2};Initial Catalog={3};Persist Security Info=False;Pooling=true;Min Pool Size=50;Max Pool Size=500;MultipleActiveResultSets=true";

        public void Convert()
        {
            #region 创建esHelper、configHelper

            var esUrl = ConfigurationManager.AppSettings["EsUrl"];
            if (string.IsNullOrEmpty(esUrl))
            {
                LogWriter.Error(string.Format("文字提取程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "配置文件缺少EsUrl"));
                return;
            }
            var esHelper = EsBaseHelper.CreateEsHelper(esUrl);
            try
            {
                if (!esHelper.ExistsIndex(EsConst.defaultEsFileIndex))
                    esHelper.CreateIndex<EsFile>(EsConst.defaultEsFileIndex);//创建es索引
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("文字提取程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
                return;
            }
            esHelper.DefaultIndex = EsConst.defaultEsFileIndex;//设置es默认索引

            SQLHelper configHelper = SQLHelper.CreateSqlHelper("DocConfigCntString");

            #endregion

            string spaceSql = "select * from dbo.S_DOC_Space ";
            var spaceDt = configHelper.ExecuteDataTable(spaceSql);

            string nodeConfigSQL = @"select NodeID ConfigID,SpaceID,AttrField Field,DataType,InputType,EnumKey,FulltextProp,AttrSort from S_DOC_NodeAttr
                                        where FulltextProp='True' 
                                        union
                                        select FileID ConfigID,SpaceID,FileAttrField Field,DataType,InputType,EnumKey,FulltextProp,AttrSort from S_DOC_FileAttr
                                        where FulltextProp='True' 
                                        order by AttrSort ";
            var propFieldDt = configHelper.ExecuteDataTable(nodeConfigSQL);
            var enumFieldDt = propFieldDt.Select("EnumKey is not null");
            var enumDic = new Dictionary<string, DataTable>();
            var enumService = Formula.FormulaHelper.GetService<Formula.IEnumService>();
            foreach (var item in enumFieldDt)
            {
                var enumKey = item["EnumKey"].ToString();
                if (enumDic.ContainsKey(enumKey)) continue;
                var enumDt = enumService.GetEnumTable(enumKey);
                enumDic.Add(enumKey, enumDt);
            }
            foreach (DataRow space in spaceDt.Rows)
            {
                try
                {
                    string constr = String.Format(conStrTemplate, space["Server"].ToString()
                        , space["UserName"].ToString(), space["Pwd"].ToString(), space["DbName"].ToString());
                    SQLHelper sqlHepler = new SQLHelper(constr);
                    string sql = @"select S_Attachment.*,S_FileInfo.ConfigID from S_Attachment 
left join S_FileInfo on S_FileInfo.ID = S_Attachment.FileID
where S_Attachment.State='Normal' and CurrentVersion='True' and S_FileInfo.State='Published'
and ((MainFile is not null and MainFile!='' ) or  (PDFFile is not null and PDFFile!='' ))
order by ID";
                    DataTable dt = sqlHepler.ExecuteDataTable(sql);
                    var SpaceID = space["ID"].ToString();
                    int i = 1;
                    foreach (DataRow attItem in dt.Rows)
                    {
                        string logSql = string.Empty;
                        var Id = SQLHelper.CreateGuid();
                        string mainFile = string.Empty;
                        if (attItem["PDFFile"] != DBNull.Value && attItem["PDFFile"] != null && !string.IsNullOrEmpty(attItem["PDFFile"].ToString()))
                            mainFile = attItem["PDFFile"].ToString();
                        if (string.IsNullOrEmpty(mainFile))
                        {
                            if (attItem["MainFile"] != DBNull.Value && attItem["MainFile"] != null && !string.IsNullOrEmpty(attItem["MainFile"].ToString()))
                                mainFile = attItem["MainFile"].ToString();
                        }
                        var FileID = attItem["FileID"].ToString();
                        var NodeID = attItem["NodeID"].ToString();
                        var ConfigID = attItem["ConfigID"].ToString();
                        var AttrID = attItem["ID"].ToString();
                        string FormatLogSql = @"INSERT INTO S_DOC_FulltextSearchConvertLog
                                                            ([ID],[FsFileID],[AttrID] ,[FileID] ,[NodeID] ,[SpaceID] ,[CreateDate] ,[ConvertState],[ErrorMeesage])
                                                            VALUES ('" + Id + "' ,'" + mainFile.Replace("'", "''") + "','" + AttrID + "','" + FileID + "' ,'" + NodeID + "' ,'" + SpaceID + "' ,'" + DateTime.Now + "','{0}','{1}')";
                        string updateSql = "update S_Attachment Set State='Finish' where ID='" + attItem["ID"].ToString() + "'";
                        Console.WriteLine("文件信息：" + mainFile + " " + DateTime.Now.ToString());
                        var ext = GetFileExt(mainFile).ToLower();
                        try
                        {
                            string content = string.Empty;
                            //string fullPath = service.GetFileFullPath(mainFile);
                            var file = FileStoreHelper.GetFile(mainFile);
                            //Console.WriteLine("文件路径：" + fullPath);

                            #region 提取文字

                            switch (ext)
                            {
                                case "docx":
                                case "doc": content = ExtractHelper.GetWordText(file); break;
                                case "xls":
                                case "xlsx": content = ExtractHelper.GetExcelText(file); break;
                                case "pdf": content = ExtractHelper.GetPdfText_Itextsharp(file); break;
                                case "txt": content = ExtractHelper.GetTxtText(file); break;
                                default:
                                    {
                                        Console.WriteLine(ext + "文件跳过");
                                        sqlHepler.ExecuteNonQuery(updateSql);
                                        Console.Write(ext + " 格式不对跳过" + i.ToString() + "/" + dt.Rows.Count);
                                        i++;
                                        continue;
                                    }
                            }
                            Console.WriteLine("获取信息内容完成");
                            #endregion
                            #region 属性json、全路径json
                            var fileDt = sqlHepler.ExecuteDataTable(@"select * from S_FileInfo where id='" + FileID + "'");
                            if (fileDt.Rows.Count == 0)
                                throw new Exception(string.Format("没有找到ID为【{0}】的S_FileInfo的记录", FileID));
                            var nodeFullID = fileDt.Rows[0]["FullNodeID"].ToString();
                            var nodeDt = sqlHepler.ExecuteDataTable(@"select * from S_NodeInfo where id in ('" + nodeFullID.Replace(".", "','") + "')");
                            nodeDt.PrimaryKey = new DataColumn[] { nodeDt.Columns["ID"] };

                            string propertyJson = string.Empty;//属性，空格分隔的string，暂时不存Json
                            string nodePathJson = string.Empty;//全路径Json
                            var nodePathList = new List<Dictionary<string, string>>();
                            nodePathList.Add(new Dictionary<string, string>() { { "id", SpaceID }, { "name", space["Name"].ToString() }, { "type", "space" } });

                            foreach (var nid in nodeFullID.Split('.'))
                            {
                                var nodeRow = nodeDt.Rows.Find(nid);
                                if (nodeRow == null) continue;
                                //目录属性
                                var propString = GetPropStrings(propFieldDt,nodeRow,enumDic);
                                propertyJson += propString;
                                
                                //全路径
                                string nname = nodeRow["Name"].ToString();
                                nodePathList.Add(new Dictionary<string, string>() { { "id", nid }, { "name", nname }, { "type", "node" } });
                            }
                            //文件节点目录
                            nodePathList.Add(new Dictionary<string, string>() { { "id", SpaceID }, { "name", fileDt.Rows[0]["Name"].ToString() }, { "type", "file" } });
                            nodePathJson = JsonHelper.ToJson(nodePathList);
                            //文件属性
                            var filePropString = GetPropStrings(propFieldDt, fileDt.Rows[0], enumDic);
                            propertyJson += filePropString;
                            propertyJson = propertyJson.TrimEnd();
                            #endregion
                            #region 插入Es

                            var esFile = new EsFile();
                            esFile.Id = FileID;
                            esFile.SpaceID = SpaceID;
                            esFile.NodeID = NodeID;
                            esFile.FileID = FileID;
                            esFile.AttrID = AttrID;
                            esFile.ConfigID = ConfigID;
                            if (!string.IsNullOrEmpty(mainFile))
                                esFile.FsFileID = System.Convert.ToInt32(mainFile.Split('_')[0]);
                            esFile.Title = GetFileName(mainFile);
                            esFile.Content = content;
                            esFile.PropertyJson = propertyJson;
                            esFile.FullPathJson = nodePathJson;
                            if (attItem["CreateDate"] != null && attItem["CreateDate"] != DBNull.Value)
                                esFile.FileCreateDate = System.Convert.ToDateTime(attItem["CreateDate"]);
                            else
                                esFile.FileCreateDate = DateTime.Now; //归档日期
                            if (attItem["CreateUserName"] != null && attItem["CreateUserName"] != DBNull.Value)
                                esFile.FileCreateUser = attItem["CreateUserName"].ToString(); //归档人
                            esFile.SecretLevel = string.Empty;//密级
                            esHelper.AddDocument(esFile);
                            Console.WriteLine("更新Es数据完成");
                            #endregion

                            sqlHepler.ExecuteNonQuery(updateSql);
                            logSql = String.Format(FormatLogSql, "Success", "");
                            configHelper.ExecuteNonQuery(logSql);
                            Console.Write(" 成功" + i.ToString() + "/" + dt.Rows.Count);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            logSql = String.Format(FormatLogSql, "Error", ex.Message);
                            configHelper.ExecuteNonQuery(logSql);
                            LogWriter.Error(ex, string.Format("文字提取程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
                            Console.WriteLine("失败跳过" + i.ToString() + "/" + dt.Rows.Count);
                            i++;
                            continue;
                        }
                    }
                }
                catch (Exception exp)
                {
                    LogWriter.Error(exp, string.Format("文字提取程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), exp.Message));

                    Console.WriteLine(exp.InnerException);
                    continue;
                }
            }
        }

        private static string GetPropStrings(DataTable propFieldDt, DataRow dataRow, Dictionary<string, DataTable> enumDic)
        {
            var filedDt = propFieldDt.Select("ConfigID='" + dataRow["ConfigID"].ToString() + "'");
            string propValues = string.Empty;
            foreach (var fieldRow in filedDt)
            {
                var field = fieldRow["Field"].ToString();
                if (string.IsNullOrEmpty(field)) continue;
                if (!dataRow.Table.Columns.Contains(field)) continue;
                var value = dataRow[field].ToString();
                if (string.IsNullOrEmpty(value)) continue;
                if (fieldRow["DataType"].ToString() == "DateTime")
                    value = System.Convert.ToDateTime(value).ToShortDateString();
                if (fieldRow["EnumKey"] != null && fieldRow["EnumKey"] != DBNull.Value && !string.IsNullOrEmpty(fieldRow["EnumKey"].ToString()))
                {
                    //替换枚举
                    var enumKey = fieldRow["EnumKey"].ToString();
                    var enumDt = enumDic[enumKey];
                    var _row = enumDt.Select("value='" + value + "'");
                    if (_row.Length > 0)
                        value = _row[0]["text"].ToString();
                }
                propValues += value + " ";
            }
            return propValues;
        }

        #region Fs扩展
        public static string GetFileExt(string FsFileID)
        {
            if (!string.IsNullOrEmpty(FsFileID))
            {
                var fileName = "";
                var fileArray = FsFileID.Split('_');
                if (fileArray.Length >= 2)
                    fileName = FsFileID.Substring(FsFileID.IndexOf('_') + 1);
                else
                    fileName = FsFileID;

                if (fileName.LastIndexOf('.') > -1)
                {
                    var strs = fileName.Substring(fileName.LastIndexOf('.') + 1).Split('_');
                    return strs[0];
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static string GetFileName(string FsFileID)
        {
            if (!string.IsNullOrEmpty(FsFileID))
            {
                var fileName = "";
                var fileArray = FsFileID.Split('_');
                if (fileArray.Length > 2)
                {
                    fileName = FsFileID.Substring(FsFileID.IndexOf('_') + 1);
                }
                else if (fileArray.Length == 2)
                    return fileArray[1];
                else
                    return FsFileID;

                if (fileName.LastIndexOf('_') > fileName.LastIndexOf('.'))
                {
                    var strs = fileName.Substring(0, fileName.LastIndexOf('_'));
                    return strs;
                }
                else if (fileName.LastIndexOf('.') > -1)
                    return fileName;
                else
                    return FsFileID;
            }
            else
                return "";
        }
        #endregion

    }
}
