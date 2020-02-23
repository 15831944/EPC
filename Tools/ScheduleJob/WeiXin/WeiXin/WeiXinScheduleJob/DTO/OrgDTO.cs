using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinScheduleJob.DTO
{
    public class OrgDTO
    {
        public string misid; //mis部门id
        public int id; //创建的部门id
        public string name; //部门名称
        public int parentid; //父亲部门id。根部门为1
        public int order; //在父部门中的次序值。order值大的排序靠前。值范围是[0, 2^32)
        public int enable; //启用/禁用。1表示启用，0表示禁用
        public int isDeleted;
    }

    public class UserDTO
    {
        public string userid; //成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节 

        public string deptid;//成员当前部门
        public string id;
        public string workNo;
        public string Weixin;
        public int isDelete;
        public string telephone;//座机

        public string position; //职务信息。长度为0~128个字符
        public string name; //成员名称。长度为1~64个字符 
        public string english_name; //英文名。长度为1-64个字节，由字母、数字、点(.)、减号(-)、空格或下划线(_)组成 
        public string mobile; //手机号码。企业内必须唯一，mobile/email二者不能同时为空 
        public int[] department; //成员所属部门id列表,不超过20个 
        public int gender; //性别。1表示男性，2表示女性 
        public string email; //邮箱。长度不超过64个字节，且为有效的email格式。企业内必须唯一，mobile/email二者不能同时为空 
        public int enable; //启用/禁用成员。1表示启用成员，0表示禁用成员 
        public int status; //激活状态: 1=已激活，2=已禁用，4=未激活 
    }
}
