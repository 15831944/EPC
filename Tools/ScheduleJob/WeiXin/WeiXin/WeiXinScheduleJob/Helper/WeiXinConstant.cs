using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinScheduleJob
{
    public class WeiXinConstant
    {
        /// <summary>
        /// 访问凭证
        /// </summary>
        public const string ACCESS_TOKEN = "access_token";

        /// <summary>
        /// ID
        /// </summary>
        public const string ID = "id";

        /// <summary>
        /// 部门
        /// </summary>
        public const string DEPARTMENT = "department";

        /// <summary>
        /// 上级部门
        /// </summary>
        public const string PARENTID = "parentid";

        /// <summary>
        /// 获取标签
        /// </summary>
        public const string TAGLIST = "taglist";

        /// <summary>
        /// 标签ID
        /// </summary>
        public const string TAGID = "tagid";

        /// <summary>
        /// NAME
        /// </summary>
        public const string NAME = "name";

        /// <summary>
        /// 用户列表
        /// </summary>
        public const string USERLIST = "userlist";

        /// <summary>
        /// 内部用户列表
        /// </summary>
        public const string MISUSERLIST = "misuserlist";

        /*****************************************以下是方法*************************************/

        /// <summary>
        /// 访问凭证方法
        /// </summary>
        public const string GETTOKEN = "/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

        /// <summary>
        /// 获取部门列表方法
        /// </summary>
        public const string DEPARTMENT_LIST = "/cgi-bin/department/list?access_token={0}&id={1}";

        /// <summary>
        /// 创建部门
        /// </summary>
        public const string DEPARTMENT_CREATE = "/cgi-bin/department/create?access_token={0}";

        /// <summary>
        /// 删除部门
        /// </summary>
        public const string DEPARTMENT_DELETE = "/cgi-bin/department/delete?access_token={0}&id={1}";

        /// <summary>
        /// 更新部门
        /// </summary>
        public const string DEPARTMENT_UPDATE = "/cgi-bin/department/update?access_token={0}";

        /// <summary>
        /// 获取部门成员详情方法
        /// </summary>
        public const string USER_LIST = "/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child=1";

        /// <summary>
        /// 获取标签列表方法
        /// </summary>
        public const string TAG_LIST = "/cgi-bin/tag/list?access_token={0}";

        /// <summary>
        /// 创建标签方法
        /// </summary>
        public const string TAG_CREATE = "/cgi-bin/tag/create?access_token={0}";

        /// <summary>
        /// 消息推送方法
        /// </summary>
        public const string MESSAGE_SEND = "/cgi-bin/message/send?access_token={0}";


        /// <summary>
        /// 获取部门成员方法
        /// </summary>
        public const string USER_SIMPLELIST = "/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child=1";

        /// <summary>
        /// 创建成员
        /// </summary>
        public const string USER_CREATE = "/cgi-bin/user/create?access_token={0}";

        /// <summary>
        /// 更新成员
        /// </summary>
        public const string USER_UPDATE = "/cgi-bin/user/update?access_token={0}";

        /// <summary>
        /// 批量删除成员
        /// </summary>
        public const string USER_BATCHDELETE = "/cgi-bin/user/batchdelete?access_token={0}";

        /// <summary>
        /// 网页授权登录获取code
        /// </summary>
        public const string AUTHORIZE = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&agentid={2}&state=0&connect_redirect=1#wechat_redirect";

        /// <summary>
        /// 根据code获取成员信息
        /// </summary>
        public const string GETUSERINFO = "/cgi-bin/user/getuserinfo?access_token={0}&code={1}";

        /// <summary>
        /// 根据userid获取成员信息
        /// </summary>
        public const string GETUSERBYUSERID = "/cgi-bin/user/get?access_token={0}&userid={1}";

        /// <summary>
        /// 创建项目群组 
        /// </summary>
        public const string APPCHATCREATE = "/cgi-bin/appchat/create?access_token={0}";

        /// <summary>
        /// 查询项目群组 
        /// </summary>
        public const string APPCHATGET = "/cgi-bin/appchat/get?access_token={0}&chatid={1}";

        /// <summary>
        /// 修改项目群组 
        /// </summary>
        public const string APPCHATUPDATE = "/cgi-bin/appchat/update?access_token={0}";

        /// <summary>
        /// 创建项目群组发消息 
        /// </summary>
        public const string APPCHATSEND = "/cgi-bin/appchat/send?access_token={0}";

    }
}
