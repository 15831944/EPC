using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interface.Logic;

namespace DataInterface
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
            //创建数据队列：枚举、人员、项目、校审意见库
            new DataSynQueueCreator().CreateBaseInfoQueue();
            //处理数据队列
            new DataSynQueueHandler().ExecuteQueue();

            //创建数据队列：项目相关
            new DataSynQueueCreator().CreateProjectInfoQueue();
            //处理数据队列
            new DataSynQueueHandler().ExecuteQueue();
        }
    }
}
