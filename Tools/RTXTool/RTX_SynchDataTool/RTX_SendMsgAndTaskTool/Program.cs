using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using Formula;
using System.Xml;
using System.IO;

namespace RTX_SendMsgAndTaskTool
{
    class Program
    {
        static void Main(string[] args)
        {
            InitBaseFrameService();
            string baseSqlHeplerConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString;
            string workFlowSqlHeplerConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WorkFlow"].ConnectionString;
            string delayTime = System.Configuration.ConfigurationManager.AppSettings["DelayTime"];
            string webAddress = System.Configuration.ConfigurationManager.AppSettings["WebAddress"];

            //当前程序的执行目录
            //string startupPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string configFilePath = startupPath + "RTX_SynchServiceConfig.exe.config";
            //if (!File.Exists(configFilePath))
            //    throw new Exception("未找到该应用程序的配置文件。");

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(configFilePath);
            //XmlNodeList nodeList = xmldoc.SelectNodes("//connectionStrings//add");
            //for (int i = 0; i < nodeList.Count; i++)
            //{
            //    var node = nodeList[i];
            //    var name = node.Attributes["name"].Value;
            //    if (name == "Base")
            //        baseSqlHeplerConnectionString = node.Attributes["connectionString"].Value;
            //    else if (name == "WorkFlow")
            //        workFlowSqlHeplerConnectionString = node.Attributes["connectionString"].Value;
            //}
            SQLHelper baseSqlHelper = SQLHelper.CreateSqlHelper("Base", baseSqlHeplerConnectionString);
            SQLHelper workFlowHelper = SQLHelper.CreateSqlHelper("WorkFlow", workFlowSqlHeplerConnectionString);
            SendMsgAndTaskService.baseSqlHelper = baseSqlHelper;
            SendMsgAndTaskService.workFlowHelper = workFlowHelper;
            //
            //nodeList = xmldoc.SelectNodes("//appSettings//add");
            //for (int i = 0; i < nodeList.Count; i++)
            //{
            //    var node = nodeList[i];
            //    var name = node.Attributes["key"].Value;
            //    if (name == "DelayTime")
            //        delayTime = node.Attributes["value"].Value;
            //    else if (name == "WebAddress")
            //        webAddress = node.Attributes["value"].Value;
            //}

            if (string.IsNullOrEmpty(webAddress))
                throw new Exception("未读取到WebAddress配置项。");
            if (string.IsNullOrEmpty(delayTime)) delayTime = "3600000";
            SendMsgAndTaskService.strdelayTime = delayTime;
            SendMsgAndTaskService.webAddress = webAddress;

            string logSQL = string.Empty;

            logSQL = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            logSQL = string.Format(logSQL, FormulaHelper.CreateGuid(), "WorkFlow", "", "Added", "向RTX发送提醒开始"
                 , "开始时间：" + DateTime.Now, "", "", "", DateTime.Now, "", "");
            baseSqlHelper.ExecuteNonQuery(logSQL);

            Console.WriteLine("向RTX发送提醒开始...");
            SendMsgAndTaskService sendMsgAndTaskService = new SendMsgAndTaskService();
            sendMsgAndTaskService.StartSendNotify();
            Console.WriteLine("向RTX发送提醒结束...");

            logSQL = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            logSQL = string.Format(logSQL, FormulaHelper.CreateGuid(), "WorkFlow", "", "Added", "向RTX发送提醒结束"
                 , "结束时间：" + DateTime.Now, "", "", "", DateTime.Now, "", "");
            baseSqlHelper.ExecuteNonQuery(logSQL);

        }

        private static void InitBaseFrameService()
        {
            System.IO.Stream sm = System.Reflection.Assembly.GetAssembly(typeof(Formula.AppStart)).GetManifestResourceStream("Formula.Resources.Service.bin");
            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);

            System.Reflection.Assembly asm = System.Reflection.Assembly.Load(bs);
            FormulaHelper.RegisterService(typeof(IUserService), asm.GetType("Config.Service.UserService"));
        }
    }
}
