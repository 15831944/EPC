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
        private static bool _isCad = false;

        //是否cad运行环境
        public static bool IsCad { get { return _isCad; } }

        public static void InitContext(bool isCAD = false)
        {
            string strDLLPath = "";
            if (!isCAD)
            {
                //获取web路径
                strDLLPath = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
                strDLLPath = strDLLPath.Substring(8);
                AppConfig._config = ConfigurationManager.OpenExeConfiguration(strDLLPath);
            }
            else
            {
                //获取程序DLL路径
                strDLLPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                strDLLPath = strDLLPath.Substring(8);
                if (!string.IsNullOrEmpty(strDLLPath))
                {
                    strDLLPath = strDLLPath.Substring(0, strDLLPath.LastIndexOf("/", StringComparison.Ordinal));
                    strDLLPath = Directory.GetFiles(strDLLPath, "*.config").First();
                }

                ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
                filemap.ExeConfigFilename = strDLLPath;
                AppConfig._config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);

                AppConfig._isCad = true;
            }
        }

        public static string GetAppSettings(string key)
        {
            if (_config == null)
                AppConfig.InitContext();

            return _config.AppSettings.Settings[key].Value;
        }

        public static ConnectionStringSettings GetConnectionStrings(string key)
        {
            if (_config == null)
                AppConfig.InitContext();

            return _config.ConnectionStrings.ConnectionStrings[key];
        }

        private AppConfig()
        { }

    }
}
