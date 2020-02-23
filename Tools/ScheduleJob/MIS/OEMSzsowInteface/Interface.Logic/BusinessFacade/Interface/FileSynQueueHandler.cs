using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class FileSynQueueHandler
    {
        #region 公共属性

        SQLHelper _SQLHelpeProject;
        public SQLHelper SQLHelpeProject
        {
            get
            {
                if (_SQLHelpeProject == null)
                    _SQLHelpeProject = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _SQLHelpeProject;
            }
        }

        SQLHelper _SQLHelperInterface;
        public SQLHelper SQLHelperInterface
        {
            get
            {
                if (_SQLHelperInterface == null)
                    _SQLHelperInterface = SQLHelper.CreateSqlHelper("SzsowInteface");
                return _SQLHelperInterface;
            }
        }

        string _BaseStorageUrl;
        public string BaseStorageUrl
        {
            get
            {
                if (_BaseStorageUrl == null)
                {
                    _BaseStorageUrl = ConfigurationManager.AppSettings["OEMStorageURL"] ?? "";
                    if (!_BaseStorageUrl.EndsWith("/"))
                        _BaseStorageUrl = _BaseStorageUrl + "/";
                }
                return _BaseStorageUrl;
            }
        }

        string _BaseServerUrl;
        public string BaseServerUrl
        {
            get
            {
                if (_BaseServerUrl == null)
                {
                    _BaseServerUrl = ConfigurationManager.AppSettings["OEMSyncURL"] ?? "";
                    if (!_BaseServerUrl.EndsWith("/"))
                        _BaseServerUrl = _BaseServerUrl + "/";
                }
                return _BaseServerUrl;
            }
        }
        #endregion

        public void ExecuteQueue()
        {
            var queueData = this.SQLHelperInterface.ExecuteObject<I_FileSynQueue>("select top 1 * from I_FileSynQueue where SynState='" + SynState.New.ToString() + "' order by CreateTime");
            while (queueData != null)
            {
                try
                {
                    StringBuilder interfaceSb = new StringBuilder();
                    if (queueData.SynType == SynType.Download.ToString())
                    {
                        queueData.RequestUrl = this.BaseStorageUrl + "Download?md5=" + queueData.MD5Code + "&filename=" + queueData.FileName;
                        #region 调用api下载文件
                        var response = HttpHelper.GetResponse(queueData.RequestUrl);
                        if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
                            throw new Exception(response.ErrorMessage);

                        var bs = response.RawBytes;
                        if (bs.Length == 0)
                        {
                            var content = response.Content;
                            if (!string.IsNullOrEmpty(content) && content.StartsWith("{"))
                                throw new Exception(content);
                            throw new Exception("没有获得正确得文件流数据");
                        }

                        #endregion
                        #region 上传filestore
                        queueData.FsFileID = FileStoreHelper.UploadFile(queueData.FileName, bs);
                        if (string.IsNullOrEmpty(queueData.FsFileID))
                            throw new Exception("上传FileStore失败：" + queueData.FsFileID);
                        #endregion
                        ExecuteDownload(queueData, interfaceSb);
                    }
                    else
                    {
                        queueData.RequestUrl = this.BaseStorageUrl + "UploadFileMobile";
                        #region 下载filestore文件
                        var bs = FileStoreHelper.GetFile(queueData.FsFileID);
                        if (bs.Length == 0)
                            throw new Exception("从FileStore下载失败：" + queueData.FsFileID);
                        #endregion
                        #region 调用api上传文件
                        if (string.IsNullOrEmpty(queueData.FileName))
                            queueData.FileName = GetFileName(queueData.FsFileID);
                        queueData.Response = HttpHelper.PostFile(queueData.RequestUrl, bs, queueData.FileName);
                        #endregion
                        ExecuteUpload(queueData, interfaceSb, bs.Length);
                    }
                    ComplateSync(queueData, interfaceSb);
                }
                catch (Exception e)
                {
                    ErrorSync(queueData, e.Message);
                }
                queueData = this.SQLHelperInterface.ExecuteObject<I_FileSynQueue>("select top 1 * from I_FileSynQueue where SynState='" + SynState.New.ToString() + "' order by CreateTime");
            }
        }

        public void TestUplaod(string filepath)
        {
            var api = this.BaseStorageUrl + "UploadFileMobile";
            var fs =new System.IO.FileStream(filepath, System.IO.FileMode.Open);
             //获取文件大小
             long size = fs.Length;
             byte[] bs = new byte[size];
             //将文件读到byte数组中
             fs.Read(bs, 0, bs.Length);
             fs.Close();
             var response = HttpHelper.PostFile(api, bs, System.IO.Path.GetFileName(filepath));
             Console.WriteLine(response);
             Console.ReadLine();
        }

        #region 私有方法

        #region 下载逻辑处理

        private void ExecuteDownload(I_FileSynQueue queueData, StringBuilder interfaceSb)
        {
            StringBuilder projectSb = new StringBuilder();
            if (queueData.RelateTable == "S_P_MileStone_ProductDetail")
            {
                #region 获取提资数据
                var milestoneDetailSql = string.Format(" update S_P_MileStone_ProductDetail set FileID='{1}' where id='{0}'", queueData.RelateID, queueData.FsFileID.Replace("'", "''"));
                projectSb.AppendLine(milestoneDetailSql);

                if (queueData.RelateType.ToLower() == "plan")
                {
                    //同步绑定S_E_Product 的文件数据
                    var productAttrInfo = JsonHelper.ToObject(queueData.RequestData);
                    var signatured = productAttrInfo.GetValue("Signatured");//是否签名，如果签名则md5 为签名的pdf文件
                    var sourceFileId = productAttrInfo.GetValue("SourceFileId");
                    var ext = productAttrInfo.GetValue("Extension");
                    var drawingNo = productAttrInfo.GetValue("DrawingNo");
                    if (!string.IsNullOrEmpty(drawingNo))
                    {
                        var field = "MainFile";
                        if (ext.ToLower() == ".pdf")
                        {
                            if (signatured.ToLower() == "true")
                                field = "SignPdfFile";
                            else
                                field = "PdfFile";
                        }
                        var productSql = string.Format(@"
                            update S_E_Product set {0}='{1}' where swffile='{2}' and code='{3}'
                            update S_E_ProductVersion set {0}='{1}' where swffile='{2}' and code='{3}'", field, queueData.FsFileID.Replace("'", "''"), sourceFileId, drawingNo);
                        projectSb.AppendLine(productSql);
                    }
                }
                #endregion
            }
            else
            {
                //I_FlowAuditProduct、I_FlowChangeProduct、I_FlowSignProduct、I_FlowExchangeProduct四张流程表数据
                var interfaceSql = string.Format(" update {2} set FsFileID='{1}' where id='{0}'", queueData.RelateID, queueData.FsFileID.Replace("'", "''"), queueData.RelateTable);
                interfaceSb.AppendLine(interfaceSql);
                if (queueData.RelateTable == "I_FlowExchangeProduct")
                {
                    //计划内 同时更新S_P_MileStone_ProductDetail数据
                    var milestoneDetailSql = string.Format(" update S_P_MileStone_ProductDetail set FileID='{1}' where id='{0}'", queueData.RelateID, queueData.FsFileID.Replace("'", "''"));
                    projectSb.AppendLine(milestoneDetailSql);
                }
            }
            if (projectSb.Length > 0)
                this.SQLHelpeProject.ExecuteNonQuery(projectSb.ToString());
        }
        #endregion

        #region 上传逻辑处理

        private void ExecuteUpload(I_FileSynQueue queueData, StringBuilder sb, long filesize)
        {
            var rtn = JsonHelper.ToObject(queueData.Response);
            var errorStr = string.Empty;

            if (rtn.GetValue("success").ToLower() != "true")
            {
                if (!string.IsNullOrEmpty(rtn.GetValue("error")))
                    errorStr = string.Format("error：{0}", rtn.GetValue("error"));
                else
                    errorStr = queueData.Response;
            }

            var md5 = rtn.GetValue("md5");
            //业务处理
            switch (queueData.RelateTable)
            {
                case "S_D_InputDocument": ExecuteInputDocument(queueData, md5, sb, filesize); break;
                default: break;
            }
            queueData.SynTime = DateTime.Now;
        }

        private void ExecuteInputDocument(I_FileSynQueue queueData, string md5, StringBuilder sb, long filesize)
        {
            var sql = string.Empty;

            var sourceData = JsonHelper.ToObject(queueData.RequestData);
            var param = new InputDocumentRequestData();
            param.fileMd5 = md5;
            param.fileId = sourceData.GetValue("ID");
            param.fileName = sourceData.GetValue("Name");
            param.folderId = sourceData.GetValue("InputID");
            param.fileSize = filesize;
            param.fileExtension = GetFileExt(sourceData.GetValue("Files"));
            param.userId = sourceData.GetValue("CreateUserID");
            var synUser = GlobalData.UserList.FirstOrDefault(a => a.ID == param.userId);
            if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                param.userId = synUser.SynID;
            sql = @"
if exists(select 1 from I_DataSynQueue where SynType='{0}' and RelateID='{2}' and SynState ='" + SynState.New.ToString() + @"')
    update I_DataSynQueue set CreateTime = '{4}', RequestData='{5}',  RequestUrl='{6}' where SynType='{0}' and RelateID='{2}' and SynState ='" + SynState.New.ToString() + @"'
else
    insert into I_DataSynQueue(SynType,RelateTable,RelateID,RelateType,CreateTime,RequestData,RequestUrl,SynState) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','" + SynState.New.ToString() + "')";
            sql = string.Format(sql, SynType.Save.ToString(), "S_D_InputDocument", param.fileId, ""
                , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<InputDocumentRequestData>(param).Replace("'", "''"), this.BaseServerUrl + "file/add");
            sb.AppendLine(sql);
        }

        #endregion

        #region filestore文件名相关方法

        private string GetFileExt(string fileID)
        {
            if (!string.IsNullOrEmpty(fileID))
            {
                var fileName = "";
                var fileArray = fileID.Split('_');
                if (fileArray.Length >= 2)
                    fileName = fileID.Substring(fileID.IndexOf('_') + 1);
                else
                    fileName = fileID;

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

        private string GetFileName(string fileID)
        {
            if (!string.IsNullOrEmpty(fileID))
            {
                var fileArray = fileID.Split('_');
                if (fileArray.Length > 1)
                    return fileArray[1];
                else
                    return fileID;
            }
            else
                return "";
        }
        #endregion

        #region 队列相关方法

        /// <summary>
        /// 接口正常返回处理
        /// </summary>
        /// <param name="queueData"></param>
        private void ComplateSync(I_FileSynQueue queueData, StringBuilder sb)
        {
            sb.AppendLine(@" delete from I_FileSynQueue where ID='" + queueData.ID.ToString() + "'");
            this.SQLHelperInterface.ExecuteNonQuery(sb.ToString());
        }

        /// <summary>
        /// 接口错误处理
        /// </summary>
        /// <param name="queueData"></param>
        /// <param name="msg"></param>
        private void ErrorSync(I_FileSynQueue queueData, string msg)
        {
            queueData.SynState = SynState.Error.ToString();
            queueData.SynTime = DateTime.Now;
            queueData.ErrorMsg = msg;
            if (string.IsNullOrEmpty(queueData.Response)) queueData.Response = string.Empty;
            if (string.IsNullOrEmpty(queueData.ErrorMsg)) queueData.ErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(queueData.FsFileID)) queueData.FsFileID = string.Empty;
            string sql = @" update I_FileSynQueue set SynTime = '{1}', Response='{2}',ErrorMsg='{3}' ,SynState='{4}' ,RequestUrl='{5}'
                ,FsFileID='{6}' where ID='{0}'";
            sql = string.Format(sql, queueData.ID.ToString(), queueData.SynTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                , queueData.Response.Replace("'", "''"), queueData.ErrorMsg.Replace("'", "''"), queueData.SynState.ToString()
                , queueData.RequestUrl.Replace("'", "''"), queueData.FsFileID.Replace("'", "''"));
            this.SQLHelperInterface.ExecuteNonQuery(sql);
        }
        #endregion

        #endregion
    }
}
