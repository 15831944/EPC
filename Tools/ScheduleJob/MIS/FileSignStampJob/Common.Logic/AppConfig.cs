using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    public class AppConfig
    {
        private static Configuration _config;
        private static string _dllPath = string.Empty;

        //DLL所在路径
        public static string DllPath { get { return _dllPath; } }

        public static void InitContext(bool isCAD = false)
        {
            string strDLLPath = string.Empty;
            string strConfigPath = string.Empty;

            //获取程序DLL路径
            strDLLPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            strDLLPath = strDLLPath.Substring(8);
            if (!string.IsNullOrEmpty(strDLLPath))
            {
                strDLLPath = strDLLPath.Substring(0, strDLLPath.LastIndexOf("/", StringComparison.Ordinal));
                strConfigPath = Directory.GetFiles(strDLLPath, "*.config").First();
            }

            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = strConfigPath;
            AppConfig._config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);

            AppConfig._dllPath = strDLLPath;
        }

        public static ConnectionStringSettings GetConnectionStrings(string key)
        {
            if (_config == null)
                AppConfig.InitContext();

            return _config.ConnectionStrings.ConnectionStrings[key];
        }

        private static string GetAppSettings(string key)
        {
            if (_config == null)
                AppConfig.InitContext();

            return _config.AppSettings.Settings[key].Value;
        }

        private static bool SetAppSettings(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                    config.AppSettings.Settings[key].Value = value;
                else
                    config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region FileStore服务器
        private static string _fileStore;
        /// <summary>
        /// FileStore服务器
        /// </summary>
        public static string FileStore
        {
            get
            {
                if (string.IsNullOrEmpty(_fileStore))
                {
                    _fileStore = GetAppSettings("FileStore");
                }
                return _fileStore;
            }
        }
        #endregion


        private static string _signState;
        /// <summary>
        /// 获取任务条件：成果的SignState
        /// </summary>
        public static string SignState
        {
            get
            {
                if (string.IsNullOrEmpty(_signState))
                {
                    _signState = GetAppSettings("SignState");
                }
                return _signState;
            }
        }
        /// <summary>
        /// 是否进行签名
        /// </summary>
        public static bool IsSign
        {
            get
            {
                return GetAppSettings("IsSign").ToLower() == "true";
            }
        }
        /// <summary>
        /// 是否进行盖章
        /// </summary>
        public static bool IsStamp
        {
            get
            {
                return GetAppSettings("IsStamp").ToLower() == "true";
            }
        }
        /// <summary>
        /// 是否进行CA签章
        /// </summary>
        public static bool IsCA
        {
            get
            {
                return GetAppSettings("IsCA").ToLower() == "true";
            }
        }

        #region 客户端盖CA章时间戳相关
        private static string _tsa_url;
        /// <summary>
        /// 时间戳验证URL
        /// </summary>
        public static string TSA_URL
        {
            get
            {
                if (string.IsNullOrEmpty(_tsa_url))
                {
                    _tsa_url = GetAppSettings("TSA_URL");
                }
                return _tsa_url;
            }
        }

        private static string _tsa_accnt;
        /// <summary>
        /// 时间戳验证账号
        /// </summary>
        public static string TSA_ACCNT
        {
            get
            {
                if (string.IsNullOrEmpty(_tsa_accnt))
                {
                    _tsa_accnt = GetAppSettings("TSA_ACCNT");
                }
                return _tsa_accnt;
            }
        }

        private static string _tsa_passw;
        /// <summary>
        /// 时间戳验证密码
        /// </summary>
        public static string TSA_PASSW
        {
            get
            {
                if (string.IsNullOrEmpty(_tsa_passw))
                {
                    _tsa_passw = GetAppSettings("TSA_PASSW");
                }
                return _tsa_passw;
            }
        }
        #endregion

        #region 上海CA一体机相关
        /// <summary>
        /// 是否使用一体机盖章
        /// </summary>
        public static bool IsUseCAMachine
        {
            get
            {
                return GetAppSettings("UseCAMachine").ToLower() == "true";
            }
        }

        private static string _ipPort;
        /// <summary>
        /// 一体机服务器IP端口
        /// </summary>
        public static string IPPort
        {
            get
            {
                if (string.IsNullOrEmpty(_ipPort))
                {
                    _ipPort = GetAppSettings("IPPort");
                }
                return _ipPort;
            }
        }

        private static string _api_key;
        public static string API_KEY
        {
            get
            {
                if (string.IsNullOrEmpty(_api_key))
                {
                    _api_key = GetAppSettings("API_Key");
                }
                return _api_key;
            }
        }

        private static string _api_secret;
        public static string API_SECRET
        {
            get
            {
                if (string.IsNullOrEmpty(_api_secret))
                {
                    _api_secret = GetAppSettings("API_Secret");
                }
                return _api_secret;
            }
        }

        /// <summary>
        /// 是否同步图章
        /// </summary>
        public static bool IsSynchSeal
        {
            get
            {
                return GetAppSettings("SynchSeal").ToLower() == "true";
            }
        }

        /// <summary>
        /// 上次同步图章时间
        /// </summary>
        public static DateTime LastSyncTime
        {
            get
            {
                string _date = GetAppSettings("LastSyncTime");
                DateTime _dt = default(DateTime);
                DateTime.TryParse(_date, out _dt);
                return _dt;
            }
            set
            {
                SetAppSettings("LastSyncTime", value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        /// <summary>
        /// 同步图章时间间隔(单位：小时)
        /// </summary>
        public static int TimeInterval
        {
            get
            {
                var ti = GetAppSettings("TimeInterval");
                if (string.IsNullOrEmpty(ti))
                    return 24;
                else
                    return int.Parse(ti);
            }
        }

        public static string Alpha
        {
            get
            {
                var al = GetAppSettings("alpha");
                if (string.IsNullOrEmpty(al))
                    return "10";
                else
                    return al;
            }
        }
        #endregion
    }
}
