using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quartz;
using OEMSzsowAPI.Helper;
using Config;
using Config.Logic;
using OEMSzsowAPI.Model;
using System.Configuration;
using OEMSzsowAPI.Common;
using System.Threading;
using System.Data;
using Formula;

namespace OEMSzsowAPI
{
    public class FileMain : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log4NetConfig.Configure();
            try
            {
                Start();
            }
            catch (Exception e)
            {
                LogWriter.Info(string.Format("同步程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Message));
                LogWriter.Error(e, e.Message);
            }
        }

        private void Start()
        {
            SQLHelper baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            SQLHelper businessHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            //获取同步的文件数据
            //读取队列表
            var list = baseHelper.ExecuteList<S_OEM_TaskFileList>("select * from S_OEM_TaskFileList where OEMCode='Szsow' and SyncState is null or SyncState=''");

            LogWriter.Info(string.Format("同步文件开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //删除一周以前同步成功的数据
            baseHelper.ExecuteNonQuery(@"delete from S_OEM_TaskFileList where OEMCode='Szsow' and SyncState='Complate' 
                and CreateTime <= '" + DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            var api = ConfigurationManager.AppSettings["OEMDownloadURL"] ?? "";
            foreach (var task in list)
            {
                try
                {
                    task.SyncTime = DateTime.Now;
                    if (string.IsNullOrEmpty(task.RequestData))
                        task.RequestData = string.Empty;
                    if (string.IsNullOrEmpty(task.Response))
                        task.Response = string.Empty;
                    if (string.IsNullOrEmpty(task.ErrorMsg))
                        task.ErrorMsg = string.Empty;
                    #region 调用api下载文件
                    var paramStr = "?md5=" + task.MD5Code + "&filename=" + task.FileName;
                    var url = api + paramStr;
                    task.SyncState = SyncState.Start.ToString();
                    task.RequestUrl = url;
                    task.RequestData = paramStr;
                    var response = HttpHelper.GetResponse(url);
                    if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
                        throw new Exception(response.ErrorMessage);
                    var content = response.Content;
                    if (!string.IsNullOrEmpty(content) && content.StartsWith("{"))
                        throw new Exception(content);

                    var bs = response.RawBytes;
                    task.SyncState = SyncState.Complate.ToString();
                    task.Response = bs.ToString();
                    #endregion

                    #region 上传filestore
                    task.FsFileID = FileHelper.Instance.SaveFile(bs, task.FileName, "");
                    if (string.IsNullOrEmpty(task.FsFileID))
                        throw new Exception("上传FileStore失败");
                    #endregion

                    #region 绑定业务表FSFileID
                    var businessSql = string.Empty; ;
                    switch (task.BusinessCode.ToLower())
                    {
                        case "plan":
                        case "exchange":
                            businessSql = string.Format(" update S_P_MileStone_ProductDetail set FileID='{1}' where id='{0}'", task.BusinessID, task.FsFileID.Replace("'", "''"));
                            break;
                        case "projectareadata.dwg":
                            businessSql = string.Format(@" update S_E_Product set MainFile='{1}' where id='{0}'
                            update S_E_ProductVersion set MainFile='{1}' where ProductID='{0}'", task.BusinessID, task.FsFileID.Replace("'", "''"));
                            break;
                        case "projectareadata.pdf":
                            businessSql = string.Format(@" update S_E_Product set PdfFile='{1}' where id='{0}'
                            update S_E_ProductVersion set PdfFile='{1}' where ProductID='{0}'", task.BusinessID, task.FsFileID.Replace("'", "''"));
                            break;
                        case "projectareadata.document":
                            businessSql = string.Format(@" update S_D_Document set MainFiles='{1}' where id='{0}'
                            update S_D_DocumentVersion set MainFiles='{1}' where DocumentID='{0}'", task.BusinessID, task.FsFileID.Replace("'", "''"));
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(businessSql))
                        businessHelper.ExecuteNonQuery(businessSql);
                    #endregion

                    updateTask(baseHelper, task);
                }
                catch (Exception e)
                {
                    task.ErrorMsg = e.Message;
                    task.SyncState = SyncState.Error.ToString();
                    LogWriter.Info(string.Format("同步文件异常：{0}，错误：{1}", task.SyncTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), task.ErrorMsg.Replace("'", "''")));
                    LogWriter.Error(e, task.ErrorMsg);
                    updateTask(baseHelper, task);
                }
            }
            LogWriter.Info(string.Format("同步文件结束：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }
        
        private void updateTask(SQLHelper baseHelper,S_OEM_TaskFileList fileTask)
        {
            if (string.IsNullOrEmpty(fileTask.FsFileID))
                fileTask.FsFileID = string.Empty;
            var sql = "update S_OEM_TaskFileList set SyncState='{1}',SyncTime='{2}',RequestUrl='{3}',RequestData='{4}',Response='{5}',ErrorMsg='{6}',FsFileID='{7}' where ID='{0}'";
            sql = string.Format(sql, fileTask.ID, fileTask.SyncState, fileTask.SyncTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                fileTask.RequestUrl.Replace("'", "''"), fileTask.RequestData.Replace("'", "''"), fileTask.Response, fileTask.ErrorMsg.Replace("'", "''"), fileTask.FsFileID.Replace("'", "''"));
            baseHelper.ExecuteNonQuery(sql);
        }

        private void test()
        {
            var api = ConfigurationManager.AppSettings["OEMDownloadURL"] ?? "";
            string md5Code = "eae4b82e9ddef3d87621b9d2620c1e35";
            string fileName = "bug记录.txt";
            var paramStr = "?md5=" + md5Code + "&filename=" + fileName;
            api += paramStr;
            Console.WriteLine(api);
            var error = string.Empty;
            try
            {
                var response = HttpHelper.GetResponse(api);
                if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
                {
                    throw new Exception(response.ErrorMessage);
                }
                var content = response.Content;
                if (!string.IsNullOrEmpty(content) && content.StartsWith("{"))
                {
                    throw new Exception(content);
                }
                else
                {
                    var bs = response.RawBytes;
                    var fileId = FileHelper.Instance.SaveFile(bs, fileName, "");
                    Console.WriteLine(fileId);
                }

            }
            catch (Exception e)
            {
                error = e.Message;
            }
            if (!string.IsNullOrEmpty(error))
                Console.WriteLine(error);
            Console.ReadKey();
            //for (int i = 1; i <= 6 * 60; i++)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine(i);
            //}
        }
    }
}
