using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using OEMSzsowAPI.Model;
using OEMSzsowAPI.Helper;
using OEMSzsowAPI.Common;
using Formula.Helper;
using Formula;
using Config;
using System.Data;

namespace OEMSzsowAPI.ApiLogic
{
    public class ProjectAreaData : BaseApi
    {
        SQLHelper _BusinessSQLHelper;
        public SQLHelper BusinessSQLHelper
        {
            get
            {
                if (_BusinessSQLHelper == null)
                    _BusinessSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _BusinessSQLHelper;
            }
        }

        public ProjectAreaData(S_OEM_TaskList task)
            : base(task)
        { }

        public override void GetDataLogic(string businessID)
        {
            if (string.IsNullOrEmpty(businessID))
                return;
            var api = this.BaseServerUrl + "Project/GetAreaData?projectId=" + businessID;
            //请求
            this.Task.RequestUrl = api;
            var response = HttpHelper.Get(api);
            this.Task.Response = response;
        }

        public override void AfterGetData(Dictionary<string, object> data)
        {
            if (this.Task.BusinessType != BusinessType.GetData.ToString())
                return;
            var projectInfoID = this.Task.BusinessID;
            var productDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_E_Product where ProjectInfoID='" + projectInfoID + "'");
            var versionDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_E_ProductVersion where ProjectInfoID='" + projectInfoID + "'");
            var dbsDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_D_DBS where ProjectInfoID='" + projectInfoID + "' and DBSType='OEMMapping'");
            var documentDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_D_Document where ProjectInfoID='" + projectInfoID + "'");
            var documentVersisonDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_D_DocumentVersion where DocumentID in ( select ID from S_D_Document where ProjectInfoID='" + projectInfoID + "')");
            StringBuilder sb = new StringBuilder();
            StringBuilder filesb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            var folders = JsonHelper.ToList(data.GetValue("folders"));//目录集合
            var files = JsonHelper.ToList(data.GetValue("files"));//文件集合
            var oemFolderIDs = new List<string>();
            #region 其他资料逻辑 生成DBS
            //生成DBS 及 Document
            var oemDBSRoot = dbsDt.Select("DBSCode='OEM_Szsow'").FirstOrDefault();
            if (oemDBSRoot == null)
                errorList.Add(new Dictionary<string, string>() { { "Msg", "同步DBS失败，未找到编号为OEM_Szsow的DBS文件夹" } });
            else
            {
                var rootFolder = folders.FirstOrDefault(a => a.GetValue("FolderType") == Convert.ToInt32(FolderTypeEnum.Project).ToString());
                if (rootFolder == null)
                    errorList.Add(new Dictionary<string, string>() { { "Msg", "同步DBS失败，未找FolderType=1的公共区文件夹（根节点）" } });
                else
                {
                    var allFolderList = folders.Where(a => a.GetValue("FolderType") == Convert.ToInt32(FolderTypeEnum.Folder).ToString()).ToList();
                    var childrenFolder = allFolderList.Where(a => a.GetValue("PID") == rootFolder.GetValue("ID")).ToList();
                    foreach (var item in childrenFolder)
                        createDbs(item, allFolderList, oemDBSRoot, dbsDt, sb);//递归生成DBS目录及下层文件

                    //删除逻辑
                    oemFolderIDs = allFolderList.Select(a => a.GetValue("ID")).ToList();
                    var epmIDs = dbsDt.AsEnumerable().Select(c => c.Field<string>("ID")).ToList();
                    epmIDs.Remove(oemDBSRoot["ID"].ToString());
                    var deleteIDs = epmIDs.Where(a => !oemFolderIDs.Contains(a)).ToArray();
                    if (deleteIDs.Length > 0)
                    {
                        string dbsDelSql = "delete from S_D_DBS where id in ('" + string.Join("','", deleteIDs) + "')";
                        sb.AppendLine(dbsDelSql);
                    }
                }

            }
            #endregion
            #region 成果绑定附件 及生成 document数据
            foreach (var item in files)
            {
                if (string.IsNullOrEmpty(item.GetValue("file")))
                {
                    errorList.Add(new Dictionary<string, string>() { { "Msg", "file为null" } });
                    continue;
                }
                var file = JsonHelper.ToObject(item.GetValue("file"));

                #region 生成document数据
                if (oemFolderIDs.Contains(file.GetValue("FolderID")))
                {
                    var dbsRow = dbsDt.Select("ID='" + file.GetValue("FolderID") + "'").FirstOrDefault();
                    if (dbsRow == null)
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FolderID", file.GetValue("FolderID") }, { "Msg", "生成document数据失败，dbsRow为null" } });
                    }
                    else
                    {
                        var fileID = file.GetValue("ID");
                        var documentRow = documentDt.Select("ID='" + fileID + "'").FirstOrDefault();
                        if (documentRow == null)
                        {
                            documentRow = documentDt.NewRow();
                            documentDt.Rows.Add(documentRow);
                            documentRow["ID"] = fileID;
                            documentRow["ProjectInfoID"] = projectInfoID;
                            documentRow["DBSID"] = dbsRow["ID"];
                            documentRow["DBSFullID"] = dbsRow["FullID"];
                            documentRow["State"] = "Normal";
                            documentRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("CreateDate"));
                            documentRow["CreateUser"] = file.GetValue("LockUserName");
                            documentRow["CreateUserID"] = file.GetValue("LockUserID");
                            documentRow["Version"] = 1;
                        }
                        var documentName = file.GetValue("Name");
                        var documentMD5Code = file.GetValue("MD5");

                        //当md5不同时才进入文件队列，调用文件下载服务
                        if (documentRow["FontFile"] == null || documentRow["FontFile"] == DBNull.Value || documentRow["FontFile"].ToString() != documentMD5Code)
                        {
                            #region 生成待同步文件队列

                            if (!string.IsNullOrEmpty(documentMD5Code) && !string.IsNullOrEmpty(documentName))
                            {
                                string fileTaskSql = @" insert into S_OEM_TaskFileList(OEMCode,BusinessCode,BusinessID,CreateTime,MD5Code,FileName) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}')";
                                fileTaskSql = string.Format(fileTaskSql, Task.OEMCode, Task.BusinessCode + ".document", fileID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), documentMD5Code, documentName.Replace("'", "''"));
                                filesb.AppendLine(fileTaskSql);
                            }
                            #endregion
                        }

                        documentRow["Name"] = documentName;
                        documentRow["FontFile"] = documentMD5Code;
                        //documentRow["Code"] = file.GetObject("Serial");
                        //documentRow["MainFiles"] = file.GetObject("Serial");
                        documentRow["ModifyUserID"] = file.GetValue("LockUserID");
                        documentRow["ModifyUser"] = file.GetValue("LockUserName");
                        documentRow["ModifyDate"] = DataHelper.FormatTime(file.GetValue("LockedDate"));
                        string documentSql = SQLHelper.CreateUpdateSql("S_D_Document", documentRow);
                        sb.AppendLine(documentSql);

                        //documentVersion
                        var documentVersionRow = documentVersisonDt.Select("DocumentID='" + fileID + "'").FirstOrDefault();
                        if (documentVersionRow == null)
                        {
                            documentVersionRow = documentVersisonDt.NewRow();
                            documentVersisonDt.Rows.Add(documentVersionRow);
                            documentVersionRow["ID"] = GuidHelper.CreateGuid();
                            documentVersionRow["DocumentID"] = fileID;
                        }
                        foreach (DataColumn col in documentVersisonDt.Columns)
                        {
                            if (col.ColumnName == "ID") continue;
                            if (productDt.Columns.Contains(col.ColumnName))
                                documentVersionRow[col] = documentRow[col.ColumnName];
                        }
                        string versionSql = SQLHelper.CreateUpdateSql("S_D_DocumentVersion", documentVersionRow);
                        sb.AppendLine(versionSql);
                    }
                }
                #endregion

                #region 成果绑定附件
                var sourceFileId = item.GetValue("sourceFileId");
                var sourceMd5 = item.GetValue("sourceMd5");
                if (string.IsNullOrEmpty(sourceFileId) || string.IsNullOrEmpty(sourceMd5))
                    continue;
                if (string.IsNullOrEmpty(item.GetValue("attachInfo")))
                {
                    errorList.Add(new Dictionary<string, string>() { { "sourceFileId", sourceFileId }, { "Msg", "attachInfo为null" } });
                    continue;
                }
                var frameInfo = JsonHelper.ToObject(item.GetValue("attachInfo"));
                var drawingNo = frameInfo.GetValue("图号");
                var ext = file.GetValue("Extension");
                var fileName = file.GetValue("Name");
                var md5Code = file.GetValue("MD5");
                var productRow = productDt.Select("SwfFile='" + sourceFileId + "' and ShotSnap='" + sourceMd5 + "'  and Code='" + drawingNo + "' ").FirstOrDefault();
                if (productRow == null)
                    continue;
                //当md5不同时才进入文件队列，调用文件下载服务
                var PlotFileDic = new Dictionary<string, string>() { };
                if (productRow["PlotFile"] != null && productRow["PlotFile"] != DBNull.Value && !string.IsNullOrEmpty(productRow["PlotFile"].ToString()))
                    PlotFileDic = JsonHelper.ToObject<Dictionary<string, string>>(productRow["PlotFile"].ToString());
                if (productRow["PlotFile"] == null || productRow["PlotFile"] == DBNull.Value || PlotFileDic.GetValue(ext) != md5Code)
                {
                    var detailID = productRow["ID"].ToString();
                    PlotFileDic.SetValue(ext, md5Code);
                    productRow["PlotFile"] = JsonHelper.ToJson(PlotFileDic);
                    sb.AppendLine(SQLHelper.CreateUpdateSql("S_E_Product", productRow));
                    var versionRow = versionDt.Select("ProductID='" + detailID + "'").FirstOrDefault();
                    versionRow["PlotFile"] = productRow["PlotFile"];
                    sb.AppendLine(SQLHelper.CreateUpdateSql("S_E_ProductVersion", versionRow));
                    #region 生成待同步文件队列

                    if (!string.IsNullOrEmpty(md5Code) && !string.IsNullOrEmpty(fileName))
                    {
                        string fileTaskSql = @" insert into S_OEM_TaskFileList(OEMCode,BusinessCode,BusinessID,CreateTime,MD5Code,FileName) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}')";
                        fileTaskSql = string.Format(fileTaskSql, Task.OEMCode, Task.BusinessCode + ext, detailID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), md5Code, fileName.Replace("'", "''"));
                        filesb.AppendLine(fileTaskSql);
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            if (errorList.Count > 0)
                this.Task.ErrorMsg = JsonHelper.ToJson(errorList);
            if (sb.Length > 0)
                this.BusinessSQLHelper.ExecuteNonQuery(sb.ToString());
            if (filesb.Length > 0)
                this.BaseSQLHelper.ExecuteNonQuery(filesb.ToString());
        }

        private void createDbs(Dictionary<string, object> folder, List<Dictionary<string, object>> allFolderList,
            DataRow parentDBS, DataTable dbsDt, StringBuilder sb)
        {
            var folderID = folder.GetValue("ID");
            var dbsRow = dbsDt.Select("ID='" + folderID + "'").FirstOrDefault();
            if (dbsRow == null)
            {
                dbsRow = dbsDt.NewRow();
                dbsDt.Rows.Add(dbsRow);
                dbsRow["ID"] = folderID;
                dbsRow["ProjectInfoID"] = parentDBS["ProjectInfoID"];
                dbsRow["DBSType"] = parentDBS["DBSType"];
                dbsRow["ParentID"] = parentDBS["ID"];
                dbsRow["FullID"] = parentDBS["FullID"].ToString() + "." + folderID;
                dbsRow["InheritAuth"] = parentDBS["InheritAuth"];
                dbsRow["IsPublic"] = parentDBS["IsPublic"];
                dbsRow["CreateDate"] = DataHelper.FormatTime(folder.GetValue("CreateDate"));
            }
            dbsRow["Name"] = folder.GetValue("Name");
            //dbsRow["DBSCode"] = dbsRow["Name"];
            dbsRow["SortIndex"] = folder.GetObject("Serial");
            dbsRow["ArchiveFolder"] = parentDBS["ArchiveFolder"];
            dbsRow["ArchiveFolderName"] = parentDBS["ArchiveFolderName"];
            string dbsSql = SQLHelper.CreateUpdateSql("S_D_DBS", dbsRow);
            sb.AppendLine(dbsSql);

            var childrenFolder = allFolderList.Where(a => a.GetValue("PID") == folderID).ToList();
            //递归调用
            foreach (var item in childrenFolder)
                createDbs(item, allFolderList, dbsRow, dbsDt, sb);//递归生成DBS目录及下层文件
        }

        public static void test()
        {
            var prjid = "a8530114-ce01-46f0-bccc-a0253c4dbe47";
            var api = "http://www.szsow.com/api/goodway/project/getareadata?projectId=" + prjid;
            //var api = "http://192.168.199.126/webapi/goodway/project/getareadata?projectid=" + prjid;
            var response = HttpHelper.Get(api);
            var rtn = JsonHelper.ToObject<Dictionary<string, object>>(response);
            var appendData = JsonHelper.ToObject<Dictionary<string, object>>(rtn.GetValue("AppendData"));
            new ProjectAreaData(new S_OEM_TaskList() { BusinessID = prjid, BusinessType = BusinessType.GetData.ToString() }).AfterGetData(appendData);
        }
    }
}
