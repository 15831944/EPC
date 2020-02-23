
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WeiXinScheduleJob.DTO;

namespace WeiXinScheduleJob
{
    public class BaseHttpClient
    { 
        protected static string contentType = "application/json;charset=UTF-8";

	    protected const int RESPONSE_OK = 200;
        //设置读取超时时间
        private const int DEFAULT_SOCKET_TIMEOUT = (30 * 1000); // milliseconds

        /// <summary>
        /// HTTP 验证
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> Authorization()
        {
            return null;
        }


        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="method">请求方式</param>
        /// <param name="url">请求链接</param>
        /// <param name="reqParams">请求参数</param>
        /// <returns></returns>
        public static ResultDTO SendRequest(RequestMode method, String url, String reqParams)
        {
            HttpWebRequest myReq = null;
            HttpWebResponse response = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(reqParams))
                {
                    if (method == RequestMode.Get || method == RequestMode.Delete)
                    {
                        url += "?" + reqParams;
                    }
                }
                myReq = (HttpWebRequest) WebRequest.Create(url);
                myReq.Method = method.ToString();
                myReq.ReadWriteTimeout = DEFAULT_SOCKET_TIMEOUT;
                myReq.ContentType = contentType;

                ////权限验证
                //var auth = this.Authorization();
                //if (auth != null)
                //{
                //    foreach (var item in auth)
                //    {
                //        myReq.Headers.Add(item.Key, item.Value);
                //    }
                //}

                if (myReq.Method == "POST" || myReq.Method == "Put")
                {
                    byte[] bs = Encoding.UTF8.GetBytes(reqParams);
                    myReq.ContentLength = bs.Length;
                    using (Stream reqStream = myReq.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }
                }
                response = (HttpWebResponse) myReq.GetResponse();
                if (Equals(response.StatusCode, HttpStatusCode.OK) ||
                    Equals(response.StatusCode, HttpStatusCode.Created))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return new ResultDTO() { status = true, info = reader.ReadToEnd() };
                    }
                }
                return new ResultDTO() { status=false,info="失败"};
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    //HttpStatusCode errorCode = ((HttpWebResponse) e.Response).StatusCode;
                    //string statusDescription = ((HttpWebResponse)e.Response).StatusDescription;
                    using (StreamReader sr = new StreamReader(((HttpWebResponse) e.Response).GetResponseStream(),
                            Encoding.UTF8))
                    {
                        return new ResultDTO() { status = false, info = sr.ReadToEnd() }; 
                    }
                }
                return new ResultDTO() { status = false, info = e.Message };
         
            }
            //这里不再抓取非http的异常，如果异常抛出交给开发者自行处理
            //catch (System.Exception ex)
            //{
            //     String errorMsg = ex.Message;
            //     Debug.Print(errorMsg);
            //}
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (myReq != null)
                {
                    myReq.Abort();
                }
            }
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
