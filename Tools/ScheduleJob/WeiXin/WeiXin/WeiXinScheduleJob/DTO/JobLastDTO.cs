using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinScheduleJob.DTO
{
    public class JobLastDTO
    {
        public DateTime MsgLastTime { get; set; } //消息最后发送时间
        public DateTime FlowLastTime { get; set; } //流程最后发送时间
    }
}
