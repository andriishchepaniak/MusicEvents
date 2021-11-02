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
    public class JobService : IJobService
    {
        private readonly ISubscriptionService _subscriptionService;
        IBackgroundJobClient client;
        public JobService(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        public void SubscribeToArtistAndCityJob(int artistApiId, int cityApiId, int userId)
        {
            //BackgroundJob.Enqueue(() => _subscriptionService.SubscribeToArtistAndCity(artistApiId, cityApiId, userId));
            BackgroundJob.Enqueue<ISubscriptionService>(job => job.SubscribeToArtistAndCity(artistApiId, cityApiId, userId));
        }

        public void SubscribeToArtistJob(int artistApiId, int userId)
        {
            BackgroundJob.Enqueue(() => _subscriptionService.SubscribeToArtist(artistApiId, userId));
        }

        public void SubscribeToCityJob(int cityApiId, int userId)
        {
            BackgroundJob.Enqueue(() => _subscriptionService.SubscribeToCity(cityApiId, userId));
        }
        public void SendEmailsAboutEvents(int cityApiId, int userId)
        {
            BackgroundJob.Enqueue(() => _subscriptionService.SubscribeToCity(cityApiId, userId));
        }
    }
}
