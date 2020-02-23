using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace JobTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var importantCustomerLink_instance = new MarketScheduleJob.ImportantCustomerLinkCustomerWarningJob();
            importantCustomerLink_instance.Execute(new VirJobExecutionContext());
            var manageContractArchive_instance = new MarketScheduleJob.ManageContractArchiveWarningJob();
            manageContractArchive_instance.Execute(new VirJobExecutionContext());
            var manageContractExamine_instance = new MarketScheduleJob.ManageContractExamineWarningJob();
            manageContractExamine_instance.Execute(new VirJobExecutionContext());
            var marketProjectTrace_instance = new MarketScheduleJob.MarketProjectTraceAddWarningJob();
            marketProjectTrace_instance.Execute(new VirJobExecutionContext());
            var receiptPlan_instance = new MarketScheduleJob.ReceiptPlanWarningJob();
            receiptPlan_instance.Execute(new VirJobExecutionContext());
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
