using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.EnterpriseServices;
using Common.WebAPIKit;
using System.Configuration;

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
            FormToPDFTaskRepository repo = new FormToPDFTaskRepository();
            repo.AddTask();
            
            var taskId = "";
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
                taskId = jobDataTaskId.ToString();

            FormToPDFTask task = repo.GetTask(taskId);
            try
            {
                while (task != null)
                {
                    repo.BeginTask(task.ID);
                    var uri = FormToWordApiUrl + string.Format("FormToWordAPI/{0}?TmplCode={1}", task.FormID, task.TempCode);
                    byte[] wordBuffer = WebApiClientHelper.DoFileRequest(uri);
                    byte[] pdfBuffer = FileConverter.Word2JPG(wordBuffer);
                    var length = "0KB";
                    if (pdfBuffer.Length < 1000)
                    {
                        length = pdfBuffer.Length + "B";
                    }
                    else if (pdfBuffer.Length >= 1000 && pdfBuffer.Length < 1000000)
                        length = pdfBuffer.Length / 1000 + "KB";
                    else
                        length = pdfBuffer.Length / 1000000 + "M";

                    string pdfFileID = FileStoreHelper.SaveFile(pdfBuffer, task.FormID + ".jpg");
                    repo.EndTask(task.ID, pdfFileID + "_" + length);
                    
                    task = repo.GetTask();
                }
            }
            catch (Exception ex)
            {
                repo.Log(task.ID, ex.Message);
            }
        }
    }
}
