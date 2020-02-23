using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Formula;
using Config;
using System.Xml;

namespace RTX_SynchDataTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过配置文件创建BaseSQLHelper
            string baseSqlHeplerConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString;
            //string startupPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";

            //string configFilePath = startupPath + "RTX_SynchServiceConfig.exe.config";
            //if (!File.Exists(configFilePath))
            //{
            //    throw new Exception("未找到该应用程序的配置文件。");
            //}
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(configFilePath);
            //XmlNodeList nodeList = xmldoc.SelectNodes("//connectionStrings//add");
            //for (int i = 0; i < nodeList.Count; i++)
            //{
            //    var node = nodeList[i];
            //    var name = node.Attributes["name"].Value;
            //    if (name == "Base")
            //    {
            //        baseSqlHeplerConnectionString = node.Attributes["connectionString"].Value;
            //        break;
            //    }
            //}
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base", baseSqlHeplerConnectionString);

            logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "RTX和金慧平台人员和部门同步程序执行开始..."
                                  , "结束时间：" + DateTime.Now, "", "", "", DateTime.Now, "", "");
            sqlHelper.ExecuteNonQuery(logSql);

            SynchDeptAndUserService.baseSqlHelper = sqlHelper;
            //同步部门信息开始
            Console.WriteLine("**********同步部门信息开始(时间：" + DateTime.Now.ToString() + ")***********");
            SynchDeptAndUserService synchDeptAndUserService = new SynchDeptAndUserService();
            synchDeptAndUserService.StartSynchDeptInfo();

            Console.WriteLine("**********同步部门信息结束(时间：" + DateTime.Now.ToString() + ")***********");

            //同步人员信息开始
            Console.WriteLine("**********同步人员信息开始(时间：" + DateTime.Now.ToString() + ")***********");
            synchDeptAndUserService.StartSynchUserInfo();
            Console.WriteLine("**********同步人员信息结束(时间：" + DateTime.Now.ToString() + ")***********");
            logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "RTX和金慧平台人员和部门同步程序执行结束"
                                   , "结束时间：" + DateTime.Now, "", "", "", DateTime.Now, "", "");
            sqlHelper.ExecuteNonQuery(logSql);

        }
    }
}
