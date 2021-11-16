using Core.Interfaces;
using Core.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEvents.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : BaseController<SubscribesController>
    {
        private readonly IArtistAndCitySubscriptionService _artistAndCitySubscription;
        private readonly IArtistSubscriptionService _artistSubscription;
        private readonly ICitySubscriptionService _citySubscription;
        private readonly INotificationService _notificationService;
        //private readonly IJobService _jobService;
        public SubscribesController(
            IArtistAndCitySubscriptionService artistAndCitySubscription,
            IArtistSubscriptionService artistSubscription,
            ICitySubscriptionService citySubscription,
            INotificationService notificationService,
            ILogger<SubscribesController> logger) : base(logger)
        {
            _notificationService = notificationService;
            _artistAndCitySubscription = artistAndCitySubscription;
            _artistSubscription = artistSubscription;
            _citySubscription = citySubscription;
        }
        //[Authorize]
        [Route("artistsubscribe")]
        [HttpPost]
        public async Task<IActionResult> SubscribeToArtist(int artistApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _artistSubscription.SubscribeToArtist(artistApiId, userId);
            });
        }
        [Route("artistunsubscribe")]
        [HttpPost]
        public async Task<IActionResult> UnSubscribeFromArtist(int artistApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _artistSubscription.UnSubscribeFromArtist(artistApiId, userId);
            });
        }
        [Route("citysubscribe")]
        [HttpPost]
        public async Task<IActionResult> SubscribeToCity(int cityApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _citySubscription.SubscribeToCity(cityApiId, userId);
            });
        }
        [Route("cityunsubscribe")]
        [HttpPost]
        public async Task<IActionResult> UnSubscribeFromCity(int cityApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _citySubscription.UnSubscribeFromCity(cityApiId, userId);
            });
        }
        [Route("artistandcitysubscribe")]
        [HttpPost]
        public async Task<IActionResult> SubscribeToArtistAndCity(int artistApiId, int cityApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscription.SubscribeToArtistAndCity(artistApiId, cityApiId, userId);
            });
        }
        [Route("artistandcityunsubscribe")]
        [HttpPost]
        public async Task<IActionResult> UnSubscribeFromArtistAndCity(int artistApiId, int cityApiId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscription.UnSubscribeFromArtistAndCity(artistApiId, cityApiId, userId);
            });
        }
    }
}
