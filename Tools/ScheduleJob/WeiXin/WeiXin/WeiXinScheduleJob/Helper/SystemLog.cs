﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WeiXinScheduleJob
{
    public class SystemLog
    {
        /// <summary>
        /// 日志部分
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLogs(string content)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "-->" + content);
                    sw.Close();
                }
            }
        }
    }
}
