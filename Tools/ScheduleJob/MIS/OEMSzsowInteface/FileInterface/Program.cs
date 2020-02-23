using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interface.Logic;

namespace FileInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Start();
            }
            catch (Exception e)
            {
                Formula.LogWriter.Info(string.Format("同步程序异常：{0}，错误：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), e.Message));
                Formula.LogWriter.Error(e, e.Message);
            }
        }

        private static void Start()
        {
            //new FileSynQueueHandler().TestUplaod(@"C:\Users\dell\Desktop\智源·云设计介绍.pdf");
            new FileSynUploadQueueCreator().CreateQueue();
            new FileSynQueueHandler().ExecuteQueue();

        }
    }
}
