using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using Common.WebAPIKit.Commlib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace Common.WebAPIKit
{
    public enum EnuHttpMethod
    {
        Get,
        Put,
        Post,
        Delete
    }

    /// <summary>
    /// 这是自定义的RESTFul Web API的客户端
    /// 通过Custom-Auth-Name和Custom-Auth-Key来识别用户身份
    /// </summary>
    public static class WebApiClientHelper
    {
        public static string Account { get; set; }
        public static string Token { get; set; }

        private static void MakePrincipleHeader(WebRequest reqMsg, string strUri)
        {
            Guid guid = Guid.NewGuid();//为每次请求都生成一个不同的标识
            strUri = InternalHelper.GetEffectiveUri(strUri);
            string strToEncrypt = Md5.MD5Encode(strUri + guid) + " " + guid;
            string strTheAuthKey = Des.Encode(strToEncrypt, Md5.MD5TwiceEncode(Token ?? ""));
            reqMsg.Headers.Add(Consts.HTTP_HEADER_AUTH_USER, Account);
            reqMsg.Headers.Add(Consts.HTTP_HEADER_AUTH_KEY, strTheAuthKey);

            reqMsg.Headers.Add(HttpRequestHeader.AcceptEncoding, "utf-8");
        }

        public static string MakeConfidentialMessage(string strToEnc)
        {
            return Des.Encode(strToEnc, Md5.MD5TwiceEncode(Token));
        }

        public static string DoJsonRequest(string strUri, EnuHttpMethod method, IUriConvertable queryCondition = null, long tick = 0)
        {
            return DoJsonRequest<object>(strUri, method, null, queryCondition: queryCondition, tick: tick);
        }

        public static string DoJsonRequest<T>(string strUri, EnuHttpMethod method, T objToSend, IUriConvertable queryCondition = null, long tick = 0) where T : class
        {
            string strToRequest = strUri;

            if (tick != 0 && (method == EnuHttpMethod.Put || method == EnuHttpMethod.Delete))
                strToRequest += "?UpdateTicks=" + tick.ToString(CultureInfo.InvariantCulture);

            if (queryCondition != null && method == EnuHttpMethod.Get)
                strToRequest += queryCondition.QueryString;

            WebRequest requestMsg = WebRequest.Create(strToRequest);
            MakePrincipleHeader(requestMsg, strToRequest);
            requestMsg.ContentType = "application/json";
            requestMsg.Proxy = null;

            switch (method)
            {
                case EnuHttpMethod.Delete:
                    requestMsg.Method = "DELETE";
                    break;
                case EnuHttpMethod.Put:
                    requestMsg.Method = "PUT";
                    break;
                case EnuHttpMethod.Post:
                    requestMsg.Method = "POST";
                    break;
                default: //EnuHttpMethod.Get:
                    requestMsg.Method = "GET";
                    break;
            }

            if (objToSend != null)
            {
                //IsoDateTimeConverter dt = new IsoDateTimeConverter();
                //dt.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                //string postData = JsonConvert.SerializeObject(objToSend, dt);
                byte[] byteArray;
                if (typeof(T).Name == "List`1")
                {
                    IsoDateTimeConverter dt = new IsoDateTimeConverter();
                    dt.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                    StringBuilder sb = new StringBuilder();
                    var listObj = (IEnumerable)objToSend;
                    sb.Append("DataMetaType=List&");
                    int i = 0;
                    foreach (var item in listObj)
                    {
                        sb.Append("List" + (i++).ToString());
                        sb.Append("=");
                        sb.Append(JsonConvert.SerializeObject(item, dt));
                        sb.Append("&");
                    }
                    sb.Append("DataMetaCount=" + i.ToString());
                    byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                }
                else
                    byteArray = Encoding.UTF8.GetBytes(ParseToString<T>(objToSend));
                requestMsg.ContentType = "application/x-www-form-urlencoded";
                requestMsg.ContentLength = byteArray.Length;
                Stream dataStream = requestMsg.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            HttpWebResponse response = null;
            string responseFromServer = "";
            try
            {
                using (response = (HttpWebResponse)requestMsg.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                responseFromServer = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                throw new ClientException(responseFromServer, ex, response.StatusCode);
            }
            return responseFromServer;
        }

        private static string ParseToString<T>(T obj)
        {
            StringBuilder sb = new StringBuilder();
            var objType = typeof(T);
            if (objType.Name.StartsWith("Dict"))
            {
                var list = obj as Dictionary<string, string>;
                foreach (var item in list)
                {
                    sb.Append("&");
                    sb.Append(item.Key);
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(item.Value));
                }
            }
            else
            {
                var props = objType.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    var propVal = p.GetValue(obj, null);
                    if (propVal == null)
                        continue;
                    sb.Append("&");
                    sb.Append(p.Name);
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(propVal.ToString()));
                }
            }
            return sb.ToString().TrimStart('&');
        }

        public static string DoFileRequest(string strUri, EnuHttpMethod method, string filePath, long tick = 0)
        {
            string strToRequest = strUri;

            if (tick != 0)
                strToRequest += "?UpdateTicks=" + tick.ToString(CultureInfo.InvariantCulture);

            WebRequest requestMsg = WebRequest.Create(strToRequest);
            MakePrincipleHeader(requestMsg, strToRequest);

            switch (method)
            {
                case EnuHttpMethod.Delete:
                    requestMsg.Method = "DELETE";
                    break;
                case EnuHttpMethod.Put:
                case EnuHttpMethod.Post:
                    requestMsg.Method = "POST";

                    break;
                default: //EnuHttpMethod.Get:
                    requestMsg.Method = "GET";
                    break;
            }

            if (method == EnuHttpMethod.Post || method == EnuHttpMethod.Put)
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    throw new ClientException("本地文件不存在，不能读取！");

                FileStream fileStream = null;
                Stream requestStream = null;
                try
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    var fileName = GetFileName(filePath);

                    string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                    requestMsg.ContentType = "multipart/form-data;boundary=" + boundary;
                    requestMsg.Headers.Add("FileName", HttpUtility.UrlEncode(fileName));//额外自定义文件名放在头中
                    requestMsg.ContentLength = fileStream.Length;

                    requestStream = requestMsg.GetRequestStream();

                    byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                             (int)fileStream.Length))];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        requestStream.Write(buffer, 0, bytesRead);

                    fileStream.Close();

                }
                catch (Exception e)
                {
                    if (requestStream != null) requestStream.Close();
                    if (fileStream != null) fileStream.Close();
                    throw new ClientException("本地文件异常，不能读取！");
                }
            }
            else
                requestMsg.ContentType = "application/json";



            try
            {
                using (HttpWebResponse response = (HttpWebResponse)requestMsg.GetResponse())
                {
                    if (method == EnuHttpMethod.Post || method == EnuHttpMethod.Put)
                    {
                        //返回上传之后的文件名
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            if (requestMsg != null) requestMsg.Abort();
                            return responseFromServer;
                        }
                    }
                    else
                    {
                        //返回文件流
                        using (Stream stream = response.GetResponseStream())
                        {
                            if (File.Exists(filePath))
                                throw new ClientException("本地文件已经存在，不能覆盖！");
                            using (FileStream fs = File.Create(filePath))
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    fs.Write(buffer, 0, bytesRead);
                                }
                                fs.Close();
                            }
                        }
                        return "";

                    }
                }
            }
            catch (IOException e)
            {
                throw new ClientException("本地文件写失败！");
            }
            catch (WebException ex)
            {
                if (requestMsg != null) requestMsg.Abort();
                var response = (HttpWebResponse)ex.Response;
                var msg = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                throw new ClientException(msg, ex, response.StatusCode);
            }

        }

        public static byte[] DoFileRequest(string strUri, long tick = 0)
        {
            string strToRequest = strUri;

            if (tick != 0)
                strToRequest += "?UpdateTicks=" + tick.ToString(CultureInfo.InvariantCulture);

            WebRequest requestMsg = WebRequest.Create(strToRequest);
            MakePrincipleHeader(requestMsg, strToRequest);

            requestMsg.Method = "GET";
            requestMsg.ContentType = "application/json";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)requestMsg.GetResponse())
                {
                    //返回文件流
                    using (Stream stream = response.GetResponseStream())
                    {
                        MemoryStream ms = new MemoryStream();
                        int readByte;
                        while ((readByte = stream.ReadByte()) != -1)
                        {
                            ms.WriteByte(((byte)readByte));
                        }
                        return ms.ToArray();
                    }
                }
            }
            catch (IOException e)
            {
                throw new ClientException("本地文件写失败！");
            }
            catch (WebException ex)
            {
                if (requestMsg != null) requestMsg.Abort();
                var response = (HttpWebResponse)ex.Response;
                var msg = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                throw new ClientException(msg, ex, response.StatusCode);
            }

        }

        private static string GetFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            int pos = filePath.LastIndexOf('\\');
            if (pos <= 0 || pos == filePath.Length - 1)
                return null;

            return filePath.Substring(pos + 1);
        }
    }
}
