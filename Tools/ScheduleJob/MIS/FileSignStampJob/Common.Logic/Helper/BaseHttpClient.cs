using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Common.Logic
{
    public class BaseHttpClient
    {
        protected static string contentType = "application/json;charset=UTF-8";
        protected const int RESPONSE_OK = 200;
        //设置读取超时时间
        private const int DEFAULT_SOCKET_TIMEOUT = (30 * 1000);

        public static string SendRequest(RequestMode method, String url, String reqParams)
        {
            HttpWebRequest myReq = null;
            HttpWebResponse response = null;
            string responseFromServer = "";
            try
            {
                if (method == RequestMode.Get)
                {
                    url += "?" + reqParams;
                }
                myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method = method.ToString();
                myReq.ReadWriteTimeout = DEFAULT_SOCKET_TIMEOUT;
                myReq.ContentType = contentType;

                if (method == RequestMode.Post || method == RequestMode.Put || method == RequestMode.Delete)
                {
                    byte[] bs = Encoding.UTF8.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }
                response = (HttpWebResponse)myReq.GetResponse();
                if (Equals(response.StatusCode, HttpStatusCode.OK) ||
                    Equals(response.StatusCode, HttpStatusCode.Created))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                throw e;
            }
            return responseFromServer;
        }
    }

    //请求方式
    public enum RequestMode
    {
        Post,
        Delete,
        Get,
        Put
    }
}
