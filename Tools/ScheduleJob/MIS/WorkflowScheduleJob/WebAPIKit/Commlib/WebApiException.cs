using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Common.WebAPIKit.Commlib
{
    /// <summary>
    /// 用于服务器端的异常
    /// </summary>
    public class WebApiException : Exception
    {
        public HttpStatusCode ExceptionCode { get; set; }
        public WebApiException(Exception exOrg = null)
            : base(s_dictErrString[HttpStatusCode.InternalServerError], exOrg)
        {
            ExceptionCode = HttpStatusCode.InternalServerError;
        }

        public WebApiException(string strErrorMessage, Exception exOrg = null)
            : base(strErrorMessage, exOrg)
        {
            ExceptionCode = HttpStatusCode.InternalServerError;
        }

        public WebApiException(HttpStatusCode code, string strErrorMessage, Exception exOrg = null)
            : base(strErrorMessage, exOrg)
        {
            ExceptionCode = code;
        }

        //默认异常信息
        private static readonly Dictionary<HttpStatusCode, string> s_dictErrString =
            new Dictionary<HttpStatusCode, string>
            {
                {HttpStatusCode.InternalServerError, "服务器意外错误，请与管理员联系"},
                {HttpStatusCode.BadRequest, "请求参数不正确"},
                {HttpStatusCode.Unauthorized,"登录信息丢失，请刷新页面后重试"},
                {HttpStatusCode.Forbidden, "权限不足"},
                {HttpStatusCode.NotFound,"请求的条目不存在，请刷新页面后重试"},
                {HttpStatusCode.Conflict,"要操作的内容已经发生变动，请刷新页面后重试"},
                {HttpStatusCode.Gone,"本地文件或环境冲突"}
            };

        public static string GetDescriptionByCode(HttpStatusCode code)
        {
            return s_dictErrString[code];
        }
    }
}
