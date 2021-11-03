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
       // private readonly ISubscriptionService _subscriptionService;
        //private readonly INotificationService _notificationService;
        //IBackgroundJobClient client;
        public JobService()//ISubscriptionService subscriptionService, INotificationService notificationService)
        {
            //_subscriptionService = subscriptionService;
            //_notificationService = notificationService;
        }
        public static void SubscribeToArtistAndCityJob(int artistApiId, int cityApiId, int userId)
        {
            //BackgroundJob.Enqueue(() => _subscriptionService.SubscribeToArtistAndCity(artistApiId, cityApiId, userId));
            BackgroundJob.Enqueue<ISubscriptionService>(job => job.SubscribeToArtistAndCity(artistApiId, cityApiId, userId));
        }

        public static void SubscribeToArtistJob(int artistApiId, int userId)
        {
            BackgroundJob.Enqueue<ISubscriptionService>(job => job.SubscribeToArtist(artistApiId, userId));
        }

        public static void SubscribeToCityJob(int cityApiId, int userId)
        {
            BackgroundJob.Enqueue<ISubscriptionService>(job => job.SubscribeToCity(cityApiId, userId));
        }
        public static void SendEmailsAboutEvents(int cityApiId, int userId)
        {
            BackgroundJob.Enqueue<ISubscriptionService>(job => job.SubscribeToCity(cityApiId, userId));
        }

        public static void NotifyUsersAboutEventsJob()
        {
            RecurringJob.AddOrUpdate<INotificationService>(job => job.NotifyUsersAboutEvents(), Cron.Daily);
        }
    }
}
