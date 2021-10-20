using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Jobs
{
    public class JobScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<MyJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInMinutes(60)
                    .RepeatForever())                   
                .Build();                               
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
