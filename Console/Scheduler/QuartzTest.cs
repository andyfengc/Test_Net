using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Console.Scheduler
{
    public class QuartzTest
    {
        public static void SchedulePrintJob()
        {
            //StdSchedulerFactory factory = new StdSchedulerFactory();
            //// get a scheduler
            //IScheduler scheduler = factory.GetScheduler();
            // create scheduler
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            // start scheduler
            scheduler.Start();

            // create job
            IJobDetail job = JobBuilder.Create<PrintJob>().WithIdentity("job1", "group1").Build();

            // trigger the job to run now, and then repeat every x seconds
            ITrigger trigger =
                TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                    .Build();

            // schedule the job using trigger
            scheduler.ScheduleJob(job, trigger);

            // running for a while
            Thread.Sleep(TimeSpan.FromSeconds(60));// keep main thread alive for a while

            // shutdown scheduler
            scheduler.Shutdown();

        }
    }

    public class PrintJob : IJob
    {
        private static int count = 0;
        public void Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine($"Hello Quartz! - {count++} - {DateTime.Now}");
        }
    }
}
