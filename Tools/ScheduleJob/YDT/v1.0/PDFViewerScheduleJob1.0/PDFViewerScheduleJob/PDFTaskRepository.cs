using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace PDFViewer
{
    public class PDFTaskRepository : IPDFTaskRepository
    {
        public IList<PDFTask> GetAllTasks()
        {
            var tasks = Get<List<PDFTask>>("GetAllTasks");

            return tasks;
        }

        public PDFTask GetTask(string ID)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);

            var task = Get<PDFTask>("GetTask", dic);
            return task;
        }

        public void AddTask(PDFTask task)
        {
            var strTask = JsonConvert.SerializeObject(task);
            var dic = new Dictionary<string, object>();
            dic.Add("task", strTask);

            var taskId = Get<string>("AddTask", dic);
        }

        public void DeleteTask(string ID)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);

            var taskId = Get<string>("DeleteTask", dic);
        }

        public void StartTask(string ID)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);

            var taskId = Get<string>("StartTask", dic);
        }

        public void EndTask(string ID, string status)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);
            dic.Add("status", status);

            var taskId = Get<string>("EndTask", dic);
        }

        public void UpdatePDFFileID(string ID, string pdfFileID, int pdfPageCount, bool isSplit)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);
            dic.Add("pdfFileID", pdfFileID);
            dic.Add("pdfPageCount", pdfPageCount);
            dic.Add("isSplit", isSplit);

            var taskId = Get<string>("UpdatePDFFileID", dic);
        }

        public void UpdateSWFFileID(string ID, string swfFileID)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);
            dic.Add("swfFileID", swfFileID);

            var taskId = Get<string>("UpdateSWFFileID", dic);
        }

        public void UpdateSnapFileID(string ID, string snapFileID)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", ID);
            dic.Add("snapFileID", snapFileID);

            var taskId = Get<string>("UpdateSnapFileID", dic);
        }


        private T Get<T>(string requestUrl, IDictionary<string, object> data = null)
        {
            return Get<T>("http://localhost:8050/PDFViewer/TaskAPI", requestUrl, data);
        }

        private T Get<T>(string serverUrl, string requestUrl, IDictionary<string, object> data = null)
        {
            T result = default(T);

            var restClient = new RestSharp.RestClient(serverUrl);
            var request = new RestSharp.RestRequest(requestUrl, RestSharp.Method.POST);
            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    request.AddParameter(key, data[key]);
                }
            }

            var response = restClient.Execute(request);

            result = JsonConvert.DeserializeObject<T>(response.Content);

            return result;
        }
    }
}
