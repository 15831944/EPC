using Common.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Logic
{
    public class BaseHttpService
    {
        public static string Post(string method, object obj, string url = null)
        {
            if (url == null)
                url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppConfig.IPPort + method;
            try
            {
                string data = JsonHelper.ToJson(obj);
                return BaseHttpClient.SendRequest(RequestMode.Post, url, data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string Delete(string method, List<object> list, string url = null)
        {
            if (url == null)
                url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppConfig.IPPort + method;
            try
            {
                string data = JsonHelper.ToJson(list);
                return BaseHttpClient.SendRequest(RequestMode.Delete, url, data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string Get(string method, string reqParams, bool isDocUrl = false, string url = null)
        {
            if (url == null)
                url = method.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? method : AppConfig.IPPort + method;
            try
            {
                return BaseHttpClient.SendRequest(RequestMode.Get, url, reqParams);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static QuerySealStrategy_Return GetCASealList()
        {
            var querySealStrategy = new QuerySealStrategy();
            var querySealStrategy_return = BaseHttpService.Post(GlobalData.QuerySealStrategyStr, querySealStrategy);
            return JsonHelper.ToObject<QuerySealStrategy_Return>(querySealStrategy_return);
        }

        public static ServerMoreSign_Return PostSign(ServerMoreSign sign)
        {
            var serverMoreSign_Return = BaseHttpService.Post(GlobalData.ServerMoreSignStr, sign);
            return JsonHelper.ToObject<ServerMoreSign_Return>(serverMoreSign_Return);
        }
    }
}
