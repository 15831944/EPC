using Formula;
using Formula.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEMSzsowAPI.Helper
{
    public static class HttpHelper
    {
        public static string Get(string requestUrl)
        {
            var response= Get("", requestUrl, "");
            return response.Content;
        }

        public static byte[] GetBytes(string requestUrl)
        {
            var response = Get("", requestUrl, "");
            return response.RawBytes;
        }

        public static RestSharp.IRestResponse GetResponse(string requestUrl)
        {
            return Get("", requestUrl, "");
        }

        public static RestSharp.IRestResponse Get(string serverUrl, string requestUrl, string referUrl)
        {
            var restClient = new RestSharp.RestClient(serverUrl);
            var request = new RestSharp.RestRequest(requestUrl, RestSharp.Method.GET);
            request.AddHeader("Referer", referUrl);//指定UrlRefer

            // 追加登录Cookie
            //var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name, true);
            //request.AddCookie(FormsAuthentication.FormsCookieName, cookie.Value);
            var response = restClient.Execute(request);
            if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
            {
                LogWriter.Info(string.Format("StatusCode:{0},ResponseStatus:{1},ErrorMessage:{2},serverUrl:{3},requestUrl:{4}",
                    response.StatusCode, response.ResponseStatus, response.ErrorMessage, serverUrl, requestUrl));
            }
            return response;
        }
        
        public static string Post(string apiBaseUri, string requestUrl, object data = null)
        {
            var restClient = new RestSharp.RestClient(apiBaseUri);
            var request = new RestSharp.RestRequest(requestUrl, RestSharp.Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(data);

            // 追加登录Cookie
            //var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name, true);
            //request.AddCookie(FormsAuthentication.FormsCookieName, cookie.Value);
            request.Timeout = 60000;
            var response = restClient.Execute(request);
            if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
            {
                LogWriter.Info(string.Format("StatusCode:{0},ResponseStatus:{1},ErrorMessage:{2},apiBaseUri:{3},requestUrl:{4}",
                    response.StatusCode, response.ResponseStatus, response.ErrorMessage, apiBaseUri, requestUrl));
            }
            return response.Content;
        }

        public static string Post(string requestUrl, object data = null)
        {
            return Post("", requestUrl, data);
        }

        public static T Post<T>(string serverUrl, string requestUrl, object data = null) where T : class
        {
            var content = Post(serverUrl, requestUrl, data);
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
