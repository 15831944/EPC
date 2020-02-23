using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using JPush.Api.Common;

namespace JPush.Api.Device
{
  public  class TagAliasResult:BaseResult
    {
        public List<String> tags;
        public String alias;

        public TagAliasResult()
        {
            tags = null;
            alias = null;
        }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
        public static TagAliasResult fromResponse(ResponseWrapper responseWrapper)
        {
            TagAliasResult tagAliasResult = new TagAliasResult();
            if (responseWrapper.isServerResponse())
            {
                tagAliasResult = JsonConvert.DeserializeObject<TagAliasResult>(responseWrapper.responseContent);
            }
            tagAliasResult.ResponseResult = responseWrapper;
            return tagAliasResult;
        }

    }
}
