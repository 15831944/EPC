using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JPush.Api.Push.Notification
{
    public abstract class PlatformNotification
    {
        
        public const String ALERT = "alert";
        private const String EXTRAS = "extras";
        [JsonProperty]
        public String alert{get;protected set;}
        [JsonProperty]
        public Dictionary<String, object> extras { get; protected set; }

        public PlatformNotification()
        {
            this.alert = null;
            this.extras = null;
        }
      

    }
}
