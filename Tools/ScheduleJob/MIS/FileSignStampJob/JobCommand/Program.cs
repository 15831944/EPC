using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using FileSignStamp;
using Common.Logic;

namespace JobTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance = new PDFSignStampAndSnap();
            if (AppConfig.IsUseCAMachine)
                instance.CAExecute(new VirJobExecutionContext());
            else
                instance.Execute(new VirJobExecutionContext());
            //instance.TestExecute(new VirJobExecutionContext());
        }
        public static void pdfTest()
        {

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
