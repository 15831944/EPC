using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Formula;

namespace JobTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetConfig.Configure();
            init();

            var timeoutAlarm_instance = new WorkflowScheduleJob.TimeoutAlarmJob();
            timeoutAlarm_instance.Execute(new VirJobExecutionContext());
            var timeoutAutoPass_instance = new WorkflowScheduleJob.TimeoutAutoPassJob();
            timeoutAutoPass_instance.Execute(new VirJobExecutionContext());
            var timeoutDeadline_instance = new WorkflowScheduleJob.TimeoutDeadlineJob();
            timeoutDeadline_instance.Execute(new VirJobExecutionContext());
            var timeoutNotice_instance = new WorkflowScheduleJob.TimeoutNoticeJob();
            timeoutNotice_instance.Execute(new VirJobExecutionContext());

            var calendarService = FormulaHelper.GetService<ICalendarService>();
            calendarService.SetHolidayNULL();
        }

        private static void init()
        {
            System.IO.Stream sm = System.Reflection.Assembly.GetAssembly(typeof(Formula.AppStart)).GetManifestResourceStream("Formula.Resources.Service.bin");
            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);

            System.Reflection.Assembly asm = System.Reflection.Assembly.Load(bs);
            FormulaHelper.RegisterService(typeof(IUserService), asm.GetType("Config.Service.UserService"));
            FormulaHelper.RegisterService(typeof(IOrgService), asm.GetType("Config.Service.OrgService"));
            FormulaHelper.RegisterService(typeof(IRoleService), asm.GetType("Config.Service.RoleService"));
            FormulaHelper.RegisterService(typeof(IEnumService), asm.GetType("Config.Service.EnumService"));
            FormulaHelper.RegisterService(typeof(IResService), asm.GetType("Config.Service.ResService"));
            FormulaHelper.RegisterService(typeof(IMessageService), asm.GetType("Config.Service.MessageService"));
            FormulaHelper.RegisterService(typeof(IWorkflowService), asm.GetType("Config.Service.WorkflowService"));
            FormulaHelper.RegisterService(typeof(IDataLogService), asm.GetType("Config.Service.DataLogService"));
            FormulaHelper.RegisterService(typeof(ICalendarService), asm.GetType("Config.Service.CalendarService"));
            FormulaHelper.RegisterService(typeof(ISSOService), asm.GetType("Config.Service.SSOService"));
            FormulaHelper.RegisterService(typeof(IAlarmService), asm.GetType("Config.Service.AlarmService"));
        }
    }

    class VirJobExecutionContext : IJobExecutionContext
    {
        public ICalendar Calendar
        {
            get { throw new NotImplementedException(); }
        }

        public string FireInstanceId
        {
            get { throw new NotImplementedException(); }
        }

        public DateTimeOffset? FireTimeUtc
        {
            get { throw new NotImplementedException(); }
        }

        public object Get(object key)
        {
            throw new NotImplementedException();
        }

        public IJobDetail JobDetail
        {
            get { throw new NotImplementedException(); }
        }

        public IJob JobInstance
        {
            get { throw new NotImplementedException(); }
        }

        public TimeSpan JobRunTime
        {
            get { throw new NotImplementedException(); }
        }

        public JobDataMap MergedJobDataMap
        {
            get { throw new NotImplementedException(); }
        }

        public DateTimeOffset? NextFireTimeUtc
        {
            get { throw new NotImplementedException(); }
        }

        public DateTimeOffset? PreviousFireTimeUtc
        {
            get { throw new NotImplementedException(); }
        }

        public void Put(object key, object objectValue)
        {
            throw new NotImplementedException();
        }

        public bool Recovering
        {
            get { throw new NotImplementedException(); }
        }

        public int RefireCount
        {
            get { throw new NotImplementedException(); }
        }

        public object Result
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTimeOffset? ScheduledFireTimeUtc
        {
            get { throw new NotImplementedException(); }
        }

        public IScheduler Scheduler
        {
            get { throw new NotImplementedException(); }
        }

        public ITrigger Trigger
        {
            get { return TriggerBuilder.Create().Build(); }
        }
    }
}
