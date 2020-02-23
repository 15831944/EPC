using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using WeiXinScheduleJob.DTO;

namespace WeiXinScheduleJob
{
    public class WeiXinService
    {
        /// <summary>
        /// 访问凭证方法
        /// </summary>
        public static string GetToken(bool isUse = true, string usecorpsecret = "")
        {
            string token = "";
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.GETTOKEN, AppSettingService.corpid, isUse ? string.IsNullOrEmpty(usecorpsecret) ? AppSettingService.usecorpsecret : usecorpsecret : AppSettingService.addresscorpsecret));
            if (dto.status)
            {
                Dictionary<string, object> dic = JsonHelper.ToObject(dto.info.ToString());
                token = dic.GetValue(WeiXinConstant.ACCESS_TOKEN);
            }
            return token;
        }

        /// <summary>
        /// 获取企业微信部门信息
        /// </summary>
        private static Dictionary<string, object> GetDepartmentList()
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.DEPARTMENT_LIST, GetToken(), ""));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            else
                return null;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        public static List<OrgDTO> GetWeiXinOrg()
        {
            var dept = GetDepartmentList();
            if (dept != null)
            {
                var deptJson = dept.GetValue(WeiXinConstant.DEPARTMENT);
                if (!string.IsNullOrWhiteSpace(deptJson))
                {
                    return JsonHelper.ToObject<List<OrgDTO>>(deptJson);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取部门成员详情
        /// </summary>
        public static Dictionary<string, object> GetUserList()
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.USER_LIST, GetToken(), AppSettingService.AuthOrgRoot));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 根据userid获取成员信息
        /// </summary>
        public static Dictionary<string, object> GetUserInfoByUserID(string userID)
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.GETUSERBYUSERID, GetToken(), userID));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        public static Dictionary<string, object> GetUserSimplelist()
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.USER_SIMPLELIST, GetToken(), AppSettingService.AuthOrgRoot));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 创建成员
        /// </summary>
        public static object CreateUser(UserDTO userDTO)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.USER_CREATE, GetToken(false)), userDTO);
            SystemLog.WriteLogs(string.Format("创建成员:{0}:{1}", userDTO.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        public static object UpdateUser(UserDTO userDTO)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.USER_UPDATE, GetToken(false)), userDTO);
            SystemLog.WriteLogs(string.Format("更新成员:{0}:{1}", userDTO.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        public static object BatchDeleteUser(Dictionary<string, object> users)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.USER_BATCHDELETE, GetToken(false)), users);
            SystemLog.WriteLogs(string.Format("删除成员:{0}", dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public static object CreateDepartment(OrgDTO org)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.DEPARTMENT_CREATE, GetToken(false)), org);
            SystemLog.WriteLogs(string.Format("创建部门:{0}:{1}", org.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }


        /// <summary>
        /// 更新部门
        /// </summary>
        public static object UpdateDepartment(OrgDTO org)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.DEPARTMENT_UPDATE, GetToken(false)), org);
            SystemLog.WriteLogs(string.Format("更新部门:{0}:{1}", org.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public static object DeleteDepartment(OrgDTO org)
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.DEPARTMENT_DELETE, GetToken(false), org.id));
            SystemLog.WriteLogs(string.Format("删除部门:{0}:{1}", org.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 消息推送
        /// </summary>
        public static void sendMessage(MessageDTO messageDTO, string usecorpsecret)
        {
            if (!string.IsNullOrEmpty(messageDTO.totag))
            {
                ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.TAG_LIST, GetToken(true, usecorpsecret)));
                if (dto.status)
                {
                    var tagData = JsonHelper.ToObject(dto.info.ToString());
                    string tagJson = tagData.GetValue(WeiXinConstant.TAGLIST);
                    var tagLists = JsonHelper.ToObject<List<Dictionary<string, object>>>(tagJson);
                    int tagid = 12;
                    if (tagLists.Count > 0)
                        tagid = Convert.ToInt32(tagLists.FirstOrDefault().GetValue(WeiXinConstant.TAGID));
                    else
                        BaseHttpService.Post(string.Format(WeiXinConstant.TAG_CREATE, GetToken()), new TagDTO { Tagname = "goodway", Tagid = tagid });
                }
            }
            ResultDTO msgDto = BaseHttpService.Post(string.Format(WeiXinConstant.MESSAGE_SEND, GetToken(true, usecorpsecret)), messageDTO);
            SystemLog.WriteLogs(string.Format("消息发送:{0}", msgDto.info.ToString()));
        }
        /// <summary>
        /// 带标题消息推送
        /// </summary>
        public static void sendMessage(MessageNewsDTO messageDTO, string usecorpsecret)
        {
            if (!string.IsNullOrEmpty(messageDTO.totag))
            {
                ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.TAG_LIST, GetToken(true, usecorpsecret)));
                if (dto.status)
                {
                    var tagData = JsonHelper.ToObject(dto.info.ToString());
                    string tagJson = tagData.GetValue(WeiXinConstant.TAGLIST);
                    var tagLists = JsonHelper.ToObject<List<Dictionary<string, object>>>(tagJson);
                    int tagid = 12;
                    if (tagLists.Count > 0)
                        tagid = Convert.ToInt32(tagLists.FirstOrDefault().GetValue(WeiXinConstant.TAGID));
                    else
                        BaseHttpService.Post(string.Format(WeiXinConstant.TAG_CREATE, GetToken()), new TagDTO { Tagname = "goodway", Tagid = tagid });
                }
            }
            ResultDTO msgDto = BaseHttpService.Post(string.Format(WeiXinConstant.MESSAGE_SEND, GetToken(true, usecorpsecret)), messageDTO);
            SystemLog.WriteLogs(string.Format("消息发送:{0}", msgDto.info.ToString()));
        }
        /// <summary>
        /// 创建项目群组
        /// </summary>
        public static Dictionary<string, object> CreateProjectGroup(ProjectGroupDTO projectGroup)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.APPCHATCREATE, GetToken(true)), projectGroup);
            SystemLog.WriteLogs(string.Format("创建项目群组:{0}:{1}", projectGroup.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 查询项目群组
        /// </summary>
        public static Dictionary<string, object> GetProjectGroup(string chatid)
        {
            ResultDTO dto = BaseHttpService.Get(string.Format(WeiXinConstant.APPCHATGET, GetToken(true), chatid));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 修改项目群组
        /// </summary>
        public static Dictionary<string, object> UpdateProjectGroup(ProjectGroupDTO projectGroup)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.APPCHATUPDATE, GetToken(true)), projectGroup);
            SystemLog.WriteLogs(string.Format("修改项目群组:{0}:{1}", projectGroup.name, dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject(dto.info.ToString());
            return null;
        }

        /// <summary>
        /// 创建项目群组发消息
        /// </summary>
        public static object CreateProjectSend(ProjectMessageDTO projectMessage)
        {
            ResultDTO dto = BaseHttpService.Post(string.Format(WeiXinConstant.APPCHATSEND, GetToken(true)), projectMessage);
            SystemLog.WriteLogs(string.Format("创建项目群组发消息:{0}", dto.info.ToString()));
            if (dto.status)
                return JsonHelper.ToObject<object>(dto.info.ToString());
            return null;
        }
    }
}
