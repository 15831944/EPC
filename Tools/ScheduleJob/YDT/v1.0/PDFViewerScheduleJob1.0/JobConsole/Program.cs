using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Topshelf;
using NBlock.Core.Logging;
using Common.Logging;
using ScheduleJob;

namespace ScheduleJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new Ninject.StandardKernel();
            // 加载所有定义在dll中的注册模块
            var files = "NBlock*.dll,*Models.dll,*Services.dll,";
            var configLoadFiles = ConfigurationManager.AppSettings["RegisterModules"] ?? "";
            kernel.Load((files + configLoadFiles).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            // 注册服务
            kernel.Bind<ILogService>().To<Log4NetService>().InSingletonScope();

            // 设置依赖注入
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));

            // 配置Log服务
            ServiceLocator.Current.GetInstance<ILogService>().Configure();

            // 启动服务
            var host = HostFactory.New(x =>
            {
                x.Service<QuartzService>();
                x.SetDescription(ConfigurationManager.AppSettings["ServiceDescription"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["ServiceDisplayName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.AfterUninstall(() =>
                {
                    // TODO 删除调度器表记录

                    LogManager.GetCurrentClassLogger().Info("服务卸载成功");
                });
            });

            host.Run();

            System.Console.ReadLine();
        }
    }

    /// <summary>
    /// 依赖注入定位类
    /// </summary>
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        public IKernel Kernel { get; set; }

        public NinjectServiceLocator(IKernel kernel)
        {
            this.Kernel = kernel;
        }
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return Kernel.Get(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
    }
}
