using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinScheduleJob
{
    public class AppSettingService
    {
        public static string GetAppSettings(string key)
        {
            return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings[key]);
        }

        public static string apiUrl
        {
            get
            {
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiURL"];
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    return baseUrl;
                }
                else
                {
                    throw new Exception("请配置名称为 ApiURL 的appSettings配置节！");
                }
            }
        }

        public static string corpid
        {
            get
            {
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["corpid"];
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    return baseUrl;
                }
                else
                {
                    throw new Exception("请配置名称为 corpid 的appSettings配置节！");
                }
            }
        }

        public static string agentId
        {
            get
            {
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["agentId"];
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    return baseUrl;
                }
                else
                {
                    throw new Exception("请配置名称为 agentId 的appSettings配置节！");
                }
            }
        }

        public static string usecorpsecret
        {
            get
            {
                var usecorpsecret = System.Configuration.ConfigurationManager.AppSettings["usecorpsecret"];
                if (!string.IsNullOrWhiteSpace(usecorpsecret))
                {
                    return usecorpsecret;
                }
                else
                {
                    throw new Exception("请配置名称为 usecorpsecret 的appSettings配置节！");
                }
            }
        }

        public static string addresscorpsecret
        {
            get
            {
                var addresscorpsecret = System.Configuration.ConfigurationManager.AppSettings["addresscorpsecret"];
                if (!string.IsNullOrWhiteSpace(addresscorpsecret))
                {
                    return addresscorpsecret;
                }
                else
                {
                    throw new Exception("请配置名称为 addresscorpsecret 的appSettings配置节！");
                }
            }
        }

        public static string IsMisSyscOrg
        {
            get
            {
                var isMisSyscOrg = System.Configuration.ConfigurationManager.AppSettings["IsMisSyscOrg"];
                if (!string.IsNullOrWhiteSpace(isMisSyscOrg))
                {
                    return isMisSyscOrg;
                }
                else
                {
                    throw new Exception("请配置名称为 IsMisSyscOrg 的appSettings配置节！");
                }
            }
        }

        public static string MailboxSuffix
        {
            get
            {
                var mailboxSuffix = System.Configuration.ConfigurationManager.AppSettings["MailboxSuffix"];
                if (!string.IsNullOrWhiteSpace(mailboxSuffix))
                {
                    return mailboxSuffix;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string AuthOrgRoot
        {
            get
            {
                var authOrgRoot = System.Configuration.ConfigurationManager.AppSettings["AuthOrgRoot"];
                if (!string.IsNullOrWhiteSpace(authOrgRoot))
                {
                    return authOrgRoot;
                }
                else
                {
                    return "1";
                }
            }
        }

        public static string MsgsBeginTime
        {
            get
            {
                var msgsBeginTime = System.Configuration.ConfigurationManager.AppSettings["MsgsBeginTime"];
                if (!string.IsNullOrWhiteSpace(msgsBeginTime))
                {
                    return msgsBeginTime;
                }
                else
                {
                    throw new Exception("请配置名称为 MsgsBeginTime 的appSettings配置节！");
                }
            }
        }

        public static string IsOnlyPush
        {
            get
            {
                var isOnlyPush = System.Configuration.ConfigurationManager.AppSettings["IsOnlyPush"];
                if (!string.IsNullOrWhiteSpace(isOnlyPush))
                {
                    return isOnlyPush;
                }
                else
                {
                    throw new Exception("请配置名称为 IsOnlyPush 的appSettings配置节！");
                }
            }
        }
        public static string SYNFlag
        {
            get
            {
                var sYNFlag = System.Configuration.ConfigurationManager.AppSettings["SYNFlag"];
                if (!string.IsNullOrWhiteSpace(sYNFlag))
                {
                    return sYNFlag;
                }
                else
                {
                    throw new Exception("请配置名称为 SYNFlag 的appSettings配置节！");
                }
            }
        }
        public static string SYNUser
        {
            get
            {
                var SYNUser = System.Configuration.ConfigurationManager.AppSettings["SYNUser"];
                if (!string.IsNullOrWhiteSpace(SYNUser))
                {
                    return SYNUser;
                }
                else
                {
                    throw new Exception("请配置名称为 SYNUser 的appSettings配置节！");
                }
            }
        }
        public static string SYNDept
        {
            get
            {
                var sYNDept = System.Configuration.ConfigurationManager.AppSettings["SYNDept"];
                if (!string.IsNullOrWhiteSpace(sYNDept))
                {
                    return sYNDept;
                }
                else
                {
                    throw new Exception("请配置名称为 SYNDept 的appSettings配置节！");
                }
            }
        }
        public static string MsgsSendTime
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MsgsSendTime"];
            }
        }

        public static string MsgsSendURL
        {
            get
            {
                var msgsSendURL = System.Configuration.ConfigurationManager.AppSettings["MsgsSendURL"];
                if (!string.IsNullOrWhiteSpace(msgsSendURL))
                {
                    return msgsSendURL;
                }
                else
                {
                    throw new Exception("请配置名称为 MsgsSendURL 的appSettings配置节！");
                }
            }
        }

        public static string WeiXinUserCount
        {
            get
            {
                var weiXinUserCount = System.Configuration.ConfigurationManager.AppSettings["WeiXinUserCount"];
                if (!string.IsNullOrWhiteSpace(weiXinUserCount))
                {
                    return weiXinUserCount;
                }
                else
                {
                    throw new Exception("请配置名称为 WeiXinUserCount 的appSettings配置节！");
                }
            }
        }

        public static string IsRegularPush
        {
            get
            {
                var isRegularPush = System.Configuration.ConfigurationManager.AppSettings["IsRegularPush"];
                if (!string.IsNullOrWhiteSpace(isRegularPush))
                {
                    return isRegularPush;
                }
                else
                {
                    throw new Exception("请配置名称为 IsRegularPush 的appSettings配置节！");
                }
            }
        }

        public static string IsCreateProjectGroup
        {
            get
            {
                var isCreateProjectGroup = System.Configuration.ConfigurationManager.AppSettings["IsCreateProjectGroup"];
                if (!string.IsNullOrWhiteSpace(isCreateProjectGroup))
                {
                    return isCreateProjectGroup;
                }
                else
                {
                    throw new Exception("请配置名称为 IsCreateProjectGroup 的appSettings配置节！");
                }
            }
        }

        public static string OrgNameField
        {
            get
            {
                var orgNameField = System.Configuration.ConfigurationManager.AppSettings["OrgNameField"];
                if (!string.IsNullOrWhiteSpace(orgNameField))
                {
                    return orgNameField;
                }
                else
                {
                    throw new Exception("请配置名称为 OrgNameField 的appSettings配置节！");
                }
            }
        }

        public static string IsUpdateExistUser
        {
            get
            {
                var isUpdateExistUser = System.Configuration.ConfigurationManager.AppSettings["IsUpdateExistUser"];
                if (!string.IsNullOrWhiteSpace(isUpdateExistUser))
                {
                    return isUpdateExistUser;
                }
                else
                {
                    throw new Exception("请配置名称为 IsUpdateExistUser 的appSettings配置节！");
                }
            }
        }

        public static string WithOutOrgField
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WithOutOrgField"];
            }
        }
    }
}
