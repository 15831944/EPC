﻿using JPush.Api.Push;
using System;
using System.Net;

namespace JPush.Api.Common
{
    public class APIRequestException:Exception
    {

        private ResponseWrapper responseRequest;
        public APIRequestException(ResponseWrapper responseRequest)
            : base(responseRequest.exceptionString)
        {
            this.responseRequest = responseRequest;
        }
        public HttpStatusCode Status
        {
            get
            {
                return this.responseRequest.responseCode; 
            }
        }
        public long MsgId
        {
            get
            {
                return responseRequest.jpushError.msg_id; 
            }
        }
        public int ErrorCode
        {
            get
            {
                return responseRequest.jpushError.error.code;
            }
        }

        public String ErrorMessage 
        {
            get
            {
                return responseRequest.jpushError.error.message;
            }
        }
        private JpushError ErrorObject()
        {
            return responseRequest.jpushError;
        }
        public int RateLimitQuota()
        {
            return responseRequest.rateLimitQuota;
        }
        public int RateLimitRemaining()
        {
            return responseRequest.rateLimitRemaining;
        }
        public int RateLimitReset()
        {
            return responseRequest.rateLimitReset;
        }
    }
    
}
