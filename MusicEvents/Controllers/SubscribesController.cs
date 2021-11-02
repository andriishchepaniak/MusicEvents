using Core.Interfaces;
using Core.Jobs;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : BaseController<SubscribesController>
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly INotificationService _notificationService;
        private readonly IJobService _jobService;
        public SubscribesController(
            ISubscriptionService subscriptionService,
            IJobService jobService,
            INotificationService notificationService,
            ILogger<SubscribesController> logger) : base(logger)
        {
            _subscriptionService = subscriptionService;
            _notificationService = notificationService;
            _jobService = jobService;
        }
        // GET: api/<SubscribesController>
        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
        {
            return await _notificationService.NotifyUsersAboutEvents();
        }

        // GET api/<SubscribesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SubscribesController>
        [Route("artistsubscribe")]
        [HttpPost]
        public IActionResult SubscribeToArtist(int artistApiId, int userId)
        {
            _jobService.SubscribeToArtistJob(artistApiId, userId);
            return Ok();
            //return Ok(await _subscriptionService.SubscribeToArtist(artistApiId, userId));
            //return await ExecuteAction(async () =>
            //{
            //    return await _subscriptionService.SubscribeToArtist(artistApiId, userId);
            //});
        }
        [Route("citysubscribe")]
        [HttpPost]
        public async Task<IActionResult> SubscribeToCity(int cityApiId, int userId)
        {
            return Ok(await _subscriptionService.SubscribeToCity(cityApiId, userId));
            //return await ExecuteAction(async () =>
            //{
            //    return await _subscriptionService.SubscribeToArtist(artistApiId, userId);
            //});
        }
        [Route("artistandcitysubscribe")]
        [HttpPost]
        public async Task<IActionResult> SubscribeToArtistAndCity(int artistApiId, int cityApiId, int userId)
        {
            return Ok(await _subscriptionService.SubscribeToArtistAndCity(artistApiId, cityApiId, userId));
            //return await ExecuteAction(async () =>
            //{
            //    return await _subscriptionService.SubscribeToArtist(artistApiId, userId);
            //});
        }

        // PUT api/<SubscribesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubscribesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
