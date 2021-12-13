using Core.Interfaces;
using Hangfire;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class JobService //: IJobService
    {
        public JobService()//ISubscriptionService subscriptionService, INotificationService notificationService)
        {
            
        }
        
        public static void SendEventsJob()
        {
            RecurringJob.AddOrUpdate<INotificationService>(job => job.SendEvents(), Cron.Daily);
        }
        public static void SendAlbumsJob()
        {
            RecurringJob.AddOrUpdate<INotificationService>(job => job.SendAlbums(), Cron.Weekly(DayOfWeek.Saturday));
        }
        public static void RefreshEventsJob()
        {
            RecurringJob.AddOrUpdate<IEventService>(job => job.RefreshEvents(), Cron.Daily(4));
        }
        public static void RefreshUserEventsJob()
        {
            RecurringJob.AddOrUpdate<IEventService>(job => job.RefreshUserEvents(), Cron.Daily(5));
        }
    }
}
