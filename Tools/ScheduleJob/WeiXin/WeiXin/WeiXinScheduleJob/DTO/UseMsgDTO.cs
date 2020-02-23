using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinScheduleJob.DTO
{
    public class UseMsgDTO
    {
        public List<UseDTO> Flow { get; set; }
        public UseDTO Msg { get; set; } 
    }

    public class UseDTO
    {
        public string AgentId { get; set; }
        public string UseCorpsecret { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string MenuID { get; set; }
        
    }
}
