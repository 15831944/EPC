using System;

namespace JPush.Api.Common.Resp
{
   public class APIConnectionException:Exception
    {
        public APIConnectionException(String message):base(message)
        {
            
        }
    }
}
