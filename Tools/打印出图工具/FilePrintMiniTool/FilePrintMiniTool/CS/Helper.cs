using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;

namespace FilePrintMiniTool.CS
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class Helper
    {
        public static Dictionary<string,string> GetPaperSizes()
        {
            string tmp = ConfigurationManager.AppSettings["EPMPaperSize"];
            Dictionary<string, string> res = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(tmp))
            {
               res = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmp);
            }
            else
            {
                Helper.WriteLog("未在配置文件中找到EPMPaperSize");
            }
            return res;
        }
        public static string GetConfigValue(EnumApi apiEnum)
        {
            string result = "";
            switch (apiEnum)
            {
                case EnumApi.GetPublishDetail:
                    result = ConfigurationManager.AppSettings["EPMApi_GetPublishDetail"];
                    break;
                case EnumApi.UpdatePrintConfig:
                    result = ConfigurationManager.AppSettings["EPMApi_UpdatePrintConfig"];
                    break;
                case EnumApi.Print:
                    result = ConfigurationManager.AppSettings["EPMApi_Print"];
                    break;
                default:
                    throw new Exception("GetApiUri无对应EnumApi处理分支");
            }

            return result;
        }

        public static void CallApi(EnumApi api, UploadDataCompletedEventHandler strHandler, object obj)
        {
            using (WebClient client = new WebClient())
            {
                #region 消息头
                client.Headers["Type"] = "Post";
                client.Headers.Add("Content-Type", ConfigurationManager.AppSettings["Content-Type"]);
                client.Encoding = Encoding.UTF8;               
                #endregion

                #region PostData
                string postData = JsonConvert.SerializeObject(obj);
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                client.Headers.Add("ContentLength", postData.Length.ToString());
                #endregion

                #region 回调处理
                client.UploadDataCompleted += strHandler;
                #endregion
               
                string uriString = GetConfigValue(api);
                if (!string.IsNullOrEmpty(uriString))
                    client.UploadDataAsync(new Uri(GetConfigValue(api)), bytes);
            }
        } 

        /// <summary>
        /// 将文件拷贝至本地路径
        /// </summary>
        /// <param name="localTmpFileName">文件本地路径</param>
        /// <param name="fileExt">文件扩展名</param>
        /// <param name="fileId">文件id</param>
        /// <returns>错误信息</returns>
        public static string CopyFile(ref string localTmpFileName, string fileId, string fileExt = "")
        {
            string errorInfo = "";
            try
            {
                int fileLength = OuterService.GetSing().GetFileSize(fileId);
                if (fileLength == 0)
                {
                    errorInfo = "文件不存在";
                    return "";
                }

                byte[] fileBytes = OuterService.GetSing().GetFileBytes(fileId, 0, fileLength);
                
                if (!string.IsNullOrEmpty(fileExt))
                {
                    localTmpFileName = GetTempFileNameWithExt(fileExt);
                }
                else
                {
                    localTmpFileName = Path.GetTempFileName();
                }

                using (FileStream fsWrite = new FileStream(localTmpFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fsWrite.Write(fileBytes, 0, fileBytes.Length);
                }
            }
            catch(Exception ex)
            {
                //文件服务器异常直接退出
                if (ex.GetType() == typeof(SoapException) || ex.GetType() == typeof(WebException))
                {
                    errorInfo = "无法从(FileStore)获得文件";                   
                } 
                else
                {
                    errorInfo = ex.Message;
                }
                Helper.WriteLog(ex.Message);
            }

            return errorInfo;
        }

        public static string CopyFile(string localPath, string fileId)
        {
            string errorInfo = "";
            try
            {
                int fileLength = OuterService.GetSing().GetFileSize(fileId);
                if (fileLength == 0)
                {
                    errorInfo = "文件不存在";
                    return "";
                }

                byte[] fileBytes = OuterService.GetSing().GetFileBytes(fileId, 0, fileLength);

                using (FileStream fsWrite = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fsWrite.Write(fileBytes, 0, fileBytes.Length);
                }
            }
            catch (Exception ex)
            {
                //文件服务器异常直接退出
                if (ex.GetType() == typeof(SoapException) || ex.GetType() == typeof(WebException))
                {
                    errorInfo = "无法从(FileStore)获得文件";
                }
                else
                {
                    errorInfo = ex.Message;
                }
                Helper.WriteLog(ex.Message);
            }

            return errorInfo;
        }

        public static int GetFileSize(string fileId)
        {
            return OuterService.GetSing().GetFileSize(fileId);
        }

        public static string GetFileFullPath(string fileId)
        {
            try
            {
                return OuterService.GetSing().GetFileFullPath(fileId);
            }
            catch (Exception ex)
            {                
                Helper.WriteLog(ex.Message);
            }
            return "";
        }

        public static string TestCopyFile(ref string localTmpFileName, string path)
        {
            string errorInfo = "";
            try
            {
                
                localTmpFileName = Path.GetTempFileName();
                File.Copy(path, localTmpFileName, true);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                Helper.WriteLog(ex.Message);
            }

            return errorInfo;
        }

        public static string GetTempFileNameWithExt(string ext)
        {
            string tempPath = Path.GetTempPath();
            return tempPath + Guid.NewGuid().ToString().Replace("-", "") + ext;
        }

        public static bool DelFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Helper.WriteLog("文件删除失败,路径:" + path + ",错误信息:" + ex.Message);
                return false;
            }

            return true;
        }

        public static double InchToMM(double inch)
        {
            return inch * 25.4;
        }

        public static double MMToInch(double mm)
        {
            return mm / 25.4;
        }

        /// <summary>
        /// 下载文件(显示进度)
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="filename"></param>
        /// <param name="prog"></param>
        public static void DownloadFile(ref string localTmpFileName, string URL, string fileExt, System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }

                localTmpFileName = GetTempFileNameWithExt(fileExt);
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(localTmpFileName, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 打开本地文件
        /// </summary>
        /// <param name="filename"></param>
        public static void OpenLocalFile(string filename)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo();
            sInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ThreadPool.QueueUserWorkItem(delegate(object o) { Process p = System.Diagnostics.Process.Start(filename); });
        }

        /// <summary>
        /// 写入日志方法
        /// </summary>
        /// <param name="msg">记录信息</param>
        public static void WriteLog(string msg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            } 
        }

        //public static string DetectFileExt(string path)
        //{
        //    FileStream fs = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //    BinaryReader r = new BinaryReader(fs);
        //    string bx = " ";
        //    byte buffer;
        //    try
        //    {
        //        buffer = r.ReadByte();
        //        bx = buffer.ToString();
        //        buffer = r.ReadByte();
        //        bx += buffer.ToString();
        //    }
        //    catch (Exception exc)
        //    {
        //        Helper.WriteLog(exc.Message);
        //    }
        //    r.Close();
        //    fs.Close();

        //    Dictionary<string, string> dic = MyExtentions.ToDictionary(typeof(EnumFileExt), true);
        //    if (dic.ContainsValue(bx))
        //    {
        //        return dic.FirstOrDefault(a => a.Value == bx).Key;
        //    }

        //    //error
        //    return "";
        //}
    }
}
