using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using System.Web;

namespace JPushTask
{
    public class HttpHelper
    {
        #region 变量定义
        private CookieContainer cc = new CookieContainer();
        private string contentType = "application/x-www-form-urlencoded";
        private string accept = "*/*";
        private string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        private Encoding encoding = Encoding.GetEncoding("utf-8");
        private int maxTry = 300;
        private int currentTry = 0;
        public IWebProxy Proxy;
        #endregion
        #region 变量赋值
        /// <summary> 
        /// Cookie
        /// </summary> 
        public CookieContainer CookieContainer
        {
            get
            {
                return cc;
            }
        }

        /// <summary> 
        /// 语言
        /// </summary> 
        /// <value></value> 
        public Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        public int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(10, 600));
            }
        }

        public int MaxTry
        {
            get
            {
                return maxTry;
            }
            set
            {
                maxTry = value;
            }
        }
        #endregion

        #region 获取HTML
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">post 提交的字符串</param>
        /// <param name="isPost">是否是post</param>
        /// <param name="cookieContainer">CookieContainer</param>
        /// <returns>html </returns>
        public string GetHtml(string url, Hashtable hashData, bool isPost, CookieContainer cookieContainer, string refurl)
        {
            string postData = ParsToString(hashData);

            ServicePointManager.Expect100Continue = false;
            if (string.IsNullOrEmpty(postData))
            {
                return GetHtml(url, cookieContainer);
            }

            Thread.Sleep(NetworkDelay);//等待

            currentTry++;

            HttpWebRequest httpWebRequest = null;

            HttpWebResponse httpWebResponse = null;
            try
            {
                byte[] byteRequest = Encoding.Default.GetBytes(postData);

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (Proxy != null)
                    httpWebRequest.Proxy = Proxy;
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = refurl;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.ContentLength = byteRequest.Length;

                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                currentTry = 0;

                httpWebRequest.Abort();
                httpWebResponse.Close();
                foreach (Cookie cookie in httpWebResponse.Cookies)
                {
                    cookieContainer.Add(cookie);
                }

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                //if (currentTry <= maxTry)
                //{
                //    GetHtml(url, postData, isPost, cookieContainer);
                //}
                //currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return "error";
            }
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">CookieContainer</param>
        /// <returns>HTML</returns>
        public string GetHtml(string url, CookieContainer cookieContainer)
        {
            Thread.Sleep(NetworkDelay);

            currentTry++;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (Proxy != null)
                    httpWebRequest.Proxy = Proxy;
                httpWebRequest.CookieContainer = cookieContainer;
                // httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                //currentTry--;

                //httpWebRequest.Abort();
                //httpWebResponse.Close();

                foreach (Cookie cookie in httpWebResponse.Cookies)
                {
                    cookieContainer.Add(cookie);
                }

                return html;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                //if (currentTry <= maxTry)
                //{
                //    GetHtml(url, cookieContainer);
                //}

                //currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return "";
            }
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>HTML</returns>
        public string GetHtml(string url)
        {
            return GetHtml(url, cc);
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">提交的字符串</param>
        /// <param name="isPost">是否是POST</param>
        /// <returns>HTML</returns>
        public string GetHtml(string url, Hashtable postData, bool isPost)
        {
            return GetHtml(url, postData, isPost, cc, url);
        }
        /// <summary>
        /// 获取字符流
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="cookieContainer">cookieContainer</param>
        /// <returns>Stream</returns>
        public Stream GetStream(string url, CookieContainer cookieContainer)
        {
            //Thread.Sleep(delay); 

            currentTry++;
            HttpWebRequest httpWebRequest = null;

            HttpWebResponse httpWebResponse = null;

            try
            {

                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (Proxy != null)
                    httpWebRequest.Proxy = Proxy;
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = maxTry;
                httpWebRequest.Referer = url;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                currentTry--;
                //httpWebRequest.Abort(); 
                //httpWebResponse.Close(); 
                foreach (Cookie cookie in httpWebResponse.Cookies)
                {
                    cookieContainer.Add(cookie);
                }

                return responseStream;
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + e.Message);
                //Console.ForegroundColor = ConsoleColor.White;

                //if (currentTry <= maxTry)
                //{
                //    GetHtml(url, cookieContainer);
                //}

                //currentTry--;

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return null;
            }
        }
        #endregion


        private string ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }


        public string httpPost(String httpUrl, String data, int connectTimeout)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(httpUrl);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                httpWebRequest.Timeout = connectTimeout;

                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;


                byte[] byteRequest = Encoding.Default.GetBytes(data);
                httpWebRequest.ContentLength = byteRequest.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                return result;
            }
            catch (Exception e)
            {
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                return "";
            }
        }

    }
}
