using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace WebApi.Schedulers
{
    public class PrintScheduler
    {
        public async Task Start()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();
            var job = JobBuilder.Create<PrintJob>()
                .WithIdentity("printJob", "group1")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("printTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(2)
                        .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}