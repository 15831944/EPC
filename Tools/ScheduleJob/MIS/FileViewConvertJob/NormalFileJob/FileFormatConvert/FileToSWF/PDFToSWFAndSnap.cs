using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Quartz;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Common.Logic.DTO;
using Common.Logic.Enum;
using Common.Logic;
using System.Diagnostics;

namespace FileFormatConvert
{
    [Description("非dwg文件转PDF和SWF和缩略图")]
    public class PDFToSWFAndSnap : IJob
    {

        public PDFToSWFAndSnap()
        {
            AppConfig.InitContext();
            Log4NetConfig.ConfigureFile();
        }

        public string ErrInfo = "<br>  记录时间：{0}<br>  相关序号：{1}<br>  文件ID：{2}<br>  错误描述：{3}<br>  详细信息：{4}";


        public void Execute(IJobExecutionContext context)
        {
            var taskId = "";
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
            {
                taskId = jobDataTaskId.ToString();
            }
            FileRepository repo = new FileRepository();
            FileTask task = null;
            while ((task = repo.GetTask(taskId)) != null)
            {
                // 修改开始执行时间
                repo.StartTask(task.ID);
                try
                {
                    if (!IsConvertFormat(task.ExtName))
                        throw new Exception("未知文件类型，没有相关的转换器！");

                    string viewMode = AppConfig.GetAppSettings("ViewMode");
                    if (viewMode != ViewModeDTO.TilePicViewer.ToString())
                    {
                        string folderPath = OfficeHelper.GetFolderPath(task.ID);
                        var pdfFilePath = Path.Combine(folderPath, task.ID + ".pdf");
                        var swfFilePath = Path.Combine(folderPath, task.ID + ".swf");
                        var snapFilePath = Path.Combine(folderPath, task.ID + ".png");

                        #region 1.转换PDF

                        byte[] pdfBuffer = null;
                      
                        if (!string.IsNullOrEmpty(task.ExtName))
                        {
                            if (task.ExtName.ToLower() == "pdf")
                            {
                                pdfBuffer = FileStoreHelper.GetFile(task.Name);
                            }
                            else if (task.ExtName.ToLower() == "docx" || task.ExtName.ToLower() == "doc")
                            {
                                // 获取文件
                                var butter = FileStoreHelper.GetFile(task.Name);
                                pdfBuffer = FileConverter.Word2PDF(butter, pdfFilePath, task.ExtName.ToLower());
                            }
                            else if (task.ExtName.ToLower() == "xlsx" || task.ExtName.ToLower() == "xls")
                            {
                                // 获取文件
                                var buffer = FileStoreHelper.GetFile(task.Name);
                                pdfBuffer = FileConverter.Excel2PDF(buffer);
                            }
                            else if (task.ExtName.ToLower() == "txt")
                            {
                                // 获取文件
                                var buffer = FileStoreHelper.GetFile(task.Name);
                                pdfBuffer = FileConverter.Txt2PDF(buffer);
                            }
                            else if (task.ExtName.ToLower() == "jpg" || task.ExtName.ToLower() == "jpeg" || task.ExtName.ToLower() == "png")
                            {
                                // 获取文件
                                var buffer = FileStoreHelper.GetFile(task.Name);
                                pdfBuffer = FileConverter.Img2PDF(buffer);
                            }
                            else if (task.ExtName.ToLower() == "tif" || task.ExtName.ToLower() == "tiff")
                            {
                                // 获取文件
                                var buffer = FileStoreHelper.GetFile(task.Name);
                                pdfBuffer = FileConverter.Tif2PDF(buffer);
                            }
                            else
                            {
                                throw new Exception("未知文件类型，没有相关的转换器！");
                            }
                        }
                        else
                        {
                            throw new Exception("未定义文件类型！");
                        }

                        if (pdfBuffer == null)
                            throw new Exception("PDF文件不存在！");

                        #endregion

                        #region 2.生成SWF


                        var pdfPageCount = FileConverter.GetPageCount(pdfBuffer);
                        if (!File.Exists(pdfFilePath))
                        {
                            FileConverter.SaveFileBuffer(pdfBuffer, pdfFilePath);
                            FileConverter.PDF2SWF(pdfFilePath, swfFilePath, pdfPageCount);
                        }
                        #endregion

                        #region 3.生成缩略图
                        byte[] snapBuffer = FileConverter.ConvertToSnap(pdfBuffer, "pdf");
                        FileConverter.SaveFileBuffer(snapBuffer, snapFilePath);
                        #endregion

                        repo.EndTask(task.ID, ResultStatus.Success);
                    }
                    else
                    {
                        //1.获取文件
                        byte[] bytes = FileStoreHelper.GetFile(task.ID);
                        string filePath = OfficeHelper.GetFolderPath(task.ID, "Files");
                        FileStoreHelper.SaveFileBuffer(bytes, Path.Combine(filePath, task.Name));
                        //2.转图
                        var imgDTO = OfficeHelper.InitDTO(task.Name, bytes.Length, task.ID);
                        FileConverter fileConverter = new FileConverter();
                        var result = fileConverter.Exec(imgDTO);
                        if (result == null || !result.status)
                            throw new Exception("转图失败！");
                        //3.生成记录文件
                        imgDTO.Versions[0].ImageZoomLevel = 18;
                        imgDTO.Versions[0].HighHeightUnit = result.HighHeightUnit;
                        OfficeHelper.WriteJsonFile(imgDTO);
                        //4.设置数据库完成
                        repo.EndTask(task.ID, ResultStatus.Success);
                        //5.删除原始文件
                        if (File.Exists(imgDTO.Versions[0].FullPath))
                            File.Delete(imgDTO.Versions[0].FullPath);
                    }

                }
                catch (Exception ex)
                {
                    // 记录日志
                    repo.EndTask(task.ID, ResultStatus.Error);
                    LogWriter.Info(string.Format(ErrInfo, DateTime.Now.ToString(), task.ID, task.Name, ex.Message, ex.StackTrace));
                }
            }
            
            //新增处理关联word、excel进程关闭问题 2019-5-29
            CloseExit();
        }

        private void CloseExit()
        {
            try
            {
                Process[] current = Process.GetProcesses();
                //遍历与当前进程名称相同的进程列表
                foreach (Process process in current)
                {
                    //如果实例已经存在则kill当前进程
                    string pName = process.ProcessName.ToUpper();

                    if (pName.Equals("WINWORD") || pName.Equals("EXCEL"))
                    {
                        process.Kill(); 
                    }
                }
            }
            catch(Exception ex) {
                LogWriter.Info("关闭office进程失败！ "+ ex.StackTrace);
            }
            System.Environment.Exit(0);
        }

        private bool IsConvertFormat(string extName)
        {
            bool isTrue = false;
            string[] formats = { "pdf", "png", "jpg", "jpeg", "gif", "doc", "docx", "xls", "xlsx","tif" ,"tiff"};
            foreach (var item in formats)
            {
                if (item.ToLower() == extName.Trim())
                {
                    isTrue = true;
                    break;
                }
            }
            return isTrue;
        }

        protected byte[] GetFileData(string fielPath)
        {
            FileStream fs = new FileStream(fielPath, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源
                    fs.Close();
                }
            }
        }

    }

    /// <summary>
    /// 输出结果
    /// </summary>
    public class ResultDTO
    {
        public bool status; //状态
        public string info; //详情
        public int HighHeightUnit; //层级
    }

    /// <summary>
    /// 转换类型
    /// </summary>
    public enum ViewModeDTO
    {
        None, //无浏览
        SWFViewer, //SWF游览
        PDFViewer, //PDF游览
        TilePicViewer //TilePic浏览
    } 
}
