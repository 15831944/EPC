using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.EnterpriseServices;
using Common.WebAPIKit;
using System.Configuration;
using System.Diagnostics;

namespace PDFViewer
{
    [Description("Form表单转换为PDF")]
    public class FormToPDF : IJob
    {
        private static string FormToWordApiUrl
        {
            get
            {
                var _FormToWordApiUrl = ConfigurationManager.AppSettings["FormToWordApiUrl"];
                if (!string.IsNullOrEmpty(_FormToWordApiUrl))
                {
                    if (!_FormToWordApiUrl.EndsWith("/"))
                        return _FormToWordApiUrl + "/";
                    else
                        return _FormToWordApiUrl;
                }
                else
                    return "";
            }
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                FormToPDFTaskRepository repo = new FormToPDFTaskRepository();
                repo.AddTask();

                var taskId = "";
                var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
                if (jobDataTaskId != null)
                    taskId = jobDataTaskId.ToString();

                FormToPDFTask task = repo.GetTask(taskId);
                
                while (task != null)
                {
                    try
                    {
                        repo.BeginTask(task.ID);
                        var uri = FormToWordApiUrl + string.Format("FormToWordAPI/{0}?TmplCode={1}", task.FormID, task.TempCode);
                        byte[] wordBuffer = WebApiClientHelper.DoFileRequest(uri);

                        StringBuilder fileNames = new StringBuilder();

                        //1. 转pdf
                        byte[] pdfFile = FileConverter.Word2PDF(wordBuffer);
                        int j = 0;
                        var length = "0KB";
                        if (pdfFile.Length < 1000)
                            length = pdfFile.Length + "B";
                        else if (pdfFile.Length >= 1000 && pdfFile.Length < 1000000)
                            length = pdfFile.Length / 1000 + "KB";
                        else
                            length = pdfFile.Length / 1000000 + "M";

                        var src = string.IsNullOrEmpty(ConfigurationManager.AppSettings["Src"]) ? "" : ConfigurationManager.AppSettings["Src"];

                        string pdfFileID = FileStoreHelper.SaveFile(pdfFile, task.FormID + ".pdf", src, true);

                        //2.转jpg
                        List<byte[]> results = FileConverter.Word2JPG(wordBuffer);
                        foreach (var item in results)
                        {
                            int p = 0;
                            length = "0KB";
                            if (item.Length < 1000)
                                length = item.Length + "B";
                            else if (item.Length >= 1000 && item.Length < 1000000)
                                length = item.Length / 1000 + "KB";
                            else
                                length = item.Length / 1000000 + "M";

                            string fileID = FileStoreHelper.SaveFile(item, task.FormID + "_" + (p++).ToString() + ".jpg", src, true);

                            fileNames.Append(fileID);
                            fileNames.Append("_");
                            fileNames.Append(length);
                            fileNames.Append(",");
                        }
                        repo.EndTask(task.ID, pdfFileID, fileNames.ToString().TrimEnd(','));

                        task = repo.GetTask();
                    }
                    catch (Exception ex)
                    {
                        repo.Log(task.ID, ex.Message);
                        LogWriter.Error("[出错记录]："+task.ID+" [错误信息]："+ex.Message);
                        continue;
                    }
                }
                
            }
            catch (Exception e)
            {
                LogWriter.Error(e.Message);
            }
            finally
            {
                //防止和浏览转图程序关闭进程冲突，不能在转图浏览服务器上同时部署
                CloseExit();
                System.Environment.Exit(0);
            }
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
            catch (Exception ex)
            {
                LogWriter.Info("关闭office进程失败！ " + ex.StackTrace);
            }
            System.Environment.Exit(0);
        }
    }
}
