
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeiXinScheduleJob.DTO;

namespace WeiXinScheduleJob
{
    public class BaseHttpService
    {
        public static ResultDTO Post(string method, object obj)
        {
            ResultDTO resultDTO = new ResultDTO() { status = false, info = false };
            string url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppSettingService.apiUrl + method;
            try
            {
                string data = JsonHelper.ToJson(obj);
                resultDTO = BaseHttpClient.SendRequest(RequestMode.Post, url, data);
            }
            catch (Exception e) { }
            return resultDTO;
        }

        public static ResultDTO Delete(string method, List<object> list)
        {
            ResultDTO resultDTO = new ResultDTO() { status = false, info = false };
            string url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppSettingService.apiUrl + method;
            try
            {
                string data = JsonHelper.ToJson(list);
                resultDTO = BaseHttpClient.SendRequest(RequestMode.Delete, url, data);
            }
            catch (Exception e) { }
            return resultDTO;
        }

        public static ResultDTO Get(string method, string reqParams = "")
        {
            ResultDTO resultDTO = new ResultDTO() { status = false, info = false };
            string url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppSettingService.apiUrl + method;
            try
            {
                resultDTO = BaseHttpClient.SendRequest(RequestMode.Get, url, reqParams);
            }
            catch (Exception e) { }
            return resultDTO;
        }

    }
}
