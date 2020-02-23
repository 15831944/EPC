using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using JPush.Api.Common;

namespace JPush.Api.Device
{
   public class TagListResult:BaseResult
    {
        public List<String> tags ;
        public TagListResult()
        {
            tags = null;
        }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public static TagListResult fromResponse(ResponseWrapper responseWrapper)
        {
            TagListResult tagListResult = new TagListResult();
            if (responseWrapper.isServerResponse())
            {
                tagListResult = JsonConvert.DeserializeObject<TagListResult>(responseWrapper.responseContent);
            }
            tagListResult.ResponseResult = responseWrapper;
            return tagListResult;
        }
    }
}
