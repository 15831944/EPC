using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Common.WebAPIKit.Commlib
{
    /// <summary>
    /// 用于客户端或局部的异常
    /// </summary>
    public class ClientException : Exception
    {
        public HttpStatusCode ExceptionCode { get; set; }

        /// <param name="message">对用户友好的信息</param>
        /// <param name="exceptionCode">code类型</param>
        public ClientException(string message, HttpStatusCode exceptionCode = HttpStatusCode.Gone)
            : base(message)
        {
            ExceptionCode = exceptionCode;
        }

        /// <param name="message">对用户友好的信息</param>
        /// <param name="exceptionCode">code类型</param>
        /// <param name="inner">系统异常（用于记录）</param>
        public ClientException(string message, Exception inner, HttpStatusCode exceptionCode = HttpStatusCode.Gone)
            : base(message, inner)
        {
            ExceptionCode = exceptionCode;
        }

    }
}
