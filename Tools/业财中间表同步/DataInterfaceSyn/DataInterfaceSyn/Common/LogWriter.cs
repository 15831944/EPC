namespace DataInterfaceSyn.Common
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 日志记录器
    /// </summary>
    public class LogWriter
    {
        public static Action<string> onLogger;
         /// <summary>
        /// 记录普通信息日志
        /// </summary>
        /// <param name="info">建议信息格式：方法名-内容-开始/结束</param>
        public static void Info(string msg)
        {
            if (onLogger != null) onLogger(msg);

            //EnableLogger
            var setting = System.Configuration.ConfigurationManager.AppSettings["EnableLogger"];
            if (setting != null && setting.ToLower() != "true") return;

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        /// <summary>
        /// 记录错误异常日志
        /// </summary>
        /// <param name="info">附加信息</param>
        /// <param name="ex">错误</param>
        public static void Error(Exception ex)
        {
            Info(BeautyErrorMsg(ex));
        }

        /// <summary>
        /// 美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            var exName = ex.GetType().Name;
            var className = ex.TargetSite.DeclaringType.FullName;
            var methodName = ex.TargetSite.Name;

            var sb = new StringBuilder();
            sb.AppendFormat("【异常信息】：{0} <br>", ex.Message.Replace("\r\n", " "));
            sb.AppendFormat("【异常方法】：{0}（{1}）<br>", ex.TargetSite.Name, ex.TargetSite.DeclaringType.FullName);
            sb.AppendFormat("【异常类型】：{0} <br>", ex.GetType().Name);
            sb.AppendFormat("【堆栈调用】：{0} <br>", ex.StackTrace.Trim());
            if (ex.InnerException != null)
            {
                sb.Append(BeautyErrorMsg(ex.InnerException));
            }

            var errorMsg = sb.ToString();
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            errorMsg = errorMsg.Replace("行号", "　<strong style=\"color:red\">行号</strong>");
            return errorMsg;
        }
    }
}
