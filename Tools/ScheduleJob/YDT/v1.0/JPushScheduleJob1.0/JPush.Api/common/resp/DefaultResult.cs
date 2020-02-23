using System.Net;

namespace JPush.Api.Common.Resp
{
    public  class DefaultResult:BaseResult
    {

        public static DefaultResult fromResponse(ResponseWrapper responseWrapper)
        {
            DefaultResult result = null;

            if (responseWrapper.isServerResponse())
            {
                result = new DefaultResult();
            }

            result.ResponseResult=responseWrapper;

            return result;
        }
        public override bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;
            }
            return false;
        }
    }
}
