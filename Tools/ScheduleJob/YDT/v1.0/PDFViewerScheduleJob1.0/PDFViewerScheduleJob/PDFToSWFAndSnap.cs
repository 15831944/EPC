using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Quartz;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PDFViewer
{
    [Description("PDF转SWF和缩略图")]
    public class PDFToSWFAndSnap : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var taskId = "";
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
            {
                taskId = jobDataTaskId.ToString();
            }

            IPDFTaskRepository repo = IPDFTaskRepositoryFactory.GetRepository();
            var task = repo.GetTask(taskId);
            if (task != null)
            {
                // 修改开始执行时间
                repo.StartTask(task.ID);
                try
                {
                    var pdfFileId = "";
                    var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                    Regex r = new Regex(@"^[1-9]\d*_");
                    var mainFileName = r.Replace(Path.GetFileNameWithoutExtension(task.FileID), "", 1);

                    #region 转换PDF
                    byte[] pdfBuffer = null;
                    if (!string.IsNullOrEmpty(task.FileType))
                    {
                        if (task.FileType.ToLower() == "pdf")
                        {
                            pdfFileId = task.FileID;
                            pdfBuffer = FileStoreHelper.GetFile(pdfFileId);
                        }
                        else if (task.FileType.ToLower() == "docx" || task.FileType.ToLower() == "doc")
                        {
                            var pdfFileName = mainFileName + ".pdf";

                            // 获取文件
                            var butter = FileStoreHelper.GetFile(task.FileID);
                            pdfBuffer = FileConverter.Word2PDF(butter);
                            pdfFileId = FileStoreHelper.SaveFile(pdfBuffer, pdfFileName);
                        }
                        else if (task.FileType.ToLower() == "xlsx" || task.FileType.ToLower() == "xls")
                        {
                            var pdfFileName = mainFileName + ".pdf";

                            // 获取文件
                            var buffer = FileStoreHelper.GetFile(task.FileID);
                            pdfBuffer = FileConverter.Excel2PDF(buffer);
                            pdfFileId = FileStoreHelper.SaveFile(pdfBuffer, pdfFileName);
                        }
                        else if (task.FileType.ToLower() == "jpg")
                        {
                            // 获取文件
                            pdfBuffer = FileStoreHelper.GetFile(task.FileID);
                        }
                        else
                        {
                            throw new Exception("未知文件类型，没有相关的转换器");
                        }
                    }
                    else
                    {
                        throw new Exception("未定义文件类型");
                    }
                    #endregion

                    if (pdfBuffer == null)
                        throw new Exception("PDF文件不存在");

                    var pdfPageCount = task.PDFPageCount = FileConverter.GetPageCount(pdfBuffer);
                    //var isSplit = task.IsSplit = IsSplitPDF(pdfBuffer.Length, pdfPageCount);
                    var isSplit = false;
                    repo.UpdatePDFFileID(task.ID, pdfFileId, pdfPageCount, isSplit);

                    #region 生成SWF
                    var pdfFilePath = directoryPath + pdfFileId;
                    var swfFilePath = directoryPath + Path.GetFileNameWithoutExtension(task.FileID) + ".swf";
                    if (!File.Exists(pdfFilePath))
                    {
                        FileConverter.SaveFileBuffer(pdfBuffer, pdfFilePath);
                    }
                    FileConverter.PDF2SWF(pdfFilePath, swfFilePath, pdfPageCount, task.IsSplit);
                    repo.UpdateSWFFileID(task.ID, Path.GetFileNameWithoutExtension(task.FileID) + ".swf");
                    UploadSWFFiles(task.FileID, Path.GetFileNameWithoutExtension(task.FileID), pdfPageCount, task.IsSplit);
                    #endregion

                    #region 生成缩略图
                    var jpgFileId = directoryPath + mainFileName + ".jpg";
                    byte[] snapBuffer = FileConverter.ConvertToSnap(pdfBuffer, System.IO.Path.GetExtension(jpgFileId).Trim('.'));
                    string fsJpgFileID = FileStoreHelper.SaveFile(snapBuffer, mainFileName + ".jpg");
                    repo.UpdateSnapFileID(task.ID, fsJpgFileID);
                    #endregion

                    repo.EndTask(task.ID, "Success");
                }
                catch (Exception ex)
                {
                    // 记录日志

                    repo.EndTask(task.ID, "Error", ex.Message);

                    throw ex;
                }
            }
        }

        /// <summary>
        /// 判断是否需要分页生成的PDF
        /// </summary>
        /// <returns></returns>
        public bool IsSplitPDF(int fileSize, int pageCount)
        {
            var result = false;
            var configPageCount = ConfigurationManager.AppSettings["PDFSplit.PageCount"];
            var configFileSize = ConfigurationManager.AppSettings["PDFSplit.FileSize"]; // 单位是M
            if (!string.IsNullOrWhiteSpace(configPageCount) && !string.IsNullOrWhiteSpace(configFileSize))
            {
                var intPageCount = int.Parse(configPageCount);
                var intFileSize = int.Parse(configFileSize);
                result = pageCount >= intPageCount || (fileSize >= (intFileSize * 1024 * 1024) && pageCount > 6);
            }

            return result;
        }

        private void UploadSWFFiles(string relateId, string fileNameWithoutExtension, int pageCount, bool isSplit)
        {
            if (isSplit)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    string fileName = fileNameWithoutExtension + i.ToString() + ".swf";
                    SaveSWFFile(fileName, relateId);
                }
            }
            else
            {
                string fileName = fileNameWithoutExtension + ".swf";
                SaveSWFFile(fileName, relateId);
            }
        }

        private void SaveSWFFile(string fileName, string relateId)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;
            if (File.Exists(filePath))
            {
                byte[] data = FileConverter.GetFileBuffer(filePath);
                FileStoreHelper.SaveFile(data, fileName, relateId);
                File.Delete(filePath);
            }
        }
    }
}
