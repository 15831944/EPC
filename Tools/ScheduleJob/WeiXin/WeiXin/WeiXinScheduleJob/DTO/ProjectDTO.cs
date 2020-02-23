using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinScheduleJob.DTO
{
    public class ProjectGroupDTO
    {
        public string name { get; set; } //群聊名
        public string owner { get; set; } //指定群主的id。如果不指定，系统会随机从userlist中选一人作为群主
        public List<string> userlist { get; set; } //群成员id列表。至少2人，至多500人
        public string chatid { get; set; } //群聊的唯一标志，不能与已有的群重复；字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z。如果不填，系统会随机生成群id
        public List<string> add_user_list { get; set; } //添加成员的id列表
        public List<string> del_user_list { get; set; } //踢出成员的id列表
        public string ProjectID { get; set; }
    }

    public class ProjectUserDTO
    {
        public string UserCode { get; set; }
        public bool IsProjectManager { get; set; }
    }

    public class ProjectMessageDTO
    {
        public string chatid { get; set; }
        public string msgtype { get; set; }
        public MessageTextDTO text { get; set; }
        public int safe { get; set; }
    }
}
