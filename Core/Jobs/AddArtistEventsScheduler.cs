using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Jobs
{
    public class AddArtistEventsScheduler
    {
        public static async void Start(int artistId)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();



            IJobDetail job = JobBuilder.Create<AddArtistEventsJob>()
                .UsingJobData("artistId", artistId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x
                    //.WithIntervalInSeconds(1)
                    .WithRepeatCount(0))                   
                .Build();                               
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
