using System;
using System.Configuration;
using Common.Logging;
using NBlock.Components.Scheduler;
using Quartz;

namespace ScheduleJob
{
    /// <summary>
    /// Quartz的Window服务类，可以利用控制台注册为Window服务
    /// </summary>
    public class QuartzService : Topshelf.ServiceControl
    {
        private static ILog log = LogManager.GetCurrentClassLogger();

        public bool Start(Topshelf.HostControl hostControl)
        {
            try
            {
                // 添加测试作业, 如果需要调试，请确保配置的这个Id没有部署成服务
                var jobDetail = JobBuilder
                    .Create<PDFViewer.PDFToSWFAndSnap>()
                   .WithIdentity("TimeoutNoticeJob")
                   .Build();

                Server.Scheduler.AddJob(jobDetail, true);
                Server.Scheduler.TriggerJob(jobDetail.Key);

                Server.Start();
            }
            catch (Exception ex)
            {
                log.Error("Quartz调度器启动报错", ex);
            }
            return true;
        }

        public bool Stop(Topshelf.HostControl hostControl)
        {
            try
            {
                Server.Shutdown();
            }
            catch (Exception ex)
            {
                log.Error("Quartz调度器关闭报错", ex);
            }
            return true;
        }

        /// <summary>
        /// 任务调度器
        /// </summary>
        public ISchedulerServer Server
        {
            get
            {
                if (_Server == null)
                {
                    var instanceId = ConfigurationManager.AppSettings["ServiceName"];
                    int port;
                    log.Info("端口号：" + ConfigurationManager.AppSettings["SchedulerPort"]);
                    if (int.TryParse(ConfigurationManager.AppSettings["SchedulerPort"], out port))
                    {
                        _Server = SchedulerManager.CreateSchedulerServer(instanceId, port);
                    }
                    else
                    {
                        log.ErrorFormat("请配置任务调度器对外提供的端口号（{0}）", port);
                    }
                }
                return _Server;
            }
        }
        private ISchedulerServer _Server = null;
    }
}
