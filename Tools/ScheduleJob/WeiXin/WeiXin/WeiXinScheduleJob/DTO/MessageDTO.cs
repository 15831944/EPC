using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinScheduleJob.DTO
{
    public class MessageDTO
    {
        public string touser; //成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向该企业应用的全部成员发送
        public string toparty; //部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        public string totag; //标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        public string msgtype; //消息类型，此时固定为：text

        public int agentid; //企业应用的id，整型。可在应用的设置页面查看
        public MessageTextDTO text; //消息内容，最长不超过2048个字节
        public int safe; //表示是否是保密消息，0表示否，1表示是，默认0
    }

    public class MessageTextDTO
    {
        public string content; //推送内容
    }
}
