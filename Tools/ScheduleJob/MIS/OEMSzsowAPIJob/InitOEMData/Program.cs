using Config.Logic;
using InitOEMData.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitOEMData
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetConfig.Configure();
            var init = new SzsowData();
            string step =string.Format("初始化用户开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            LogWriter.Info(step);
            Console.WriteLine(step);
            init.InitUser();
            step = string.Format("初始化用户结束：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            LogWriter.Info(step);
            Console.WriteLine(step);
            Console.ReadKey();
            return;
            step = string.Format("初始化项目开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            LogWriter.Info(step);
            Console.WriteLine(step);
            init.InitProject();
            step = string.Format("初始化项目结束：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            LogWriter.Info(step);
            Console.WriteLine(step);
            Console.ReadKey();
        }
    }
}
