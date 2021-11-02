using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public SubscribesController(
            ISubscriptionService subscriptionService, 
            ILogger<SubscribesController> logger) : base(logger)
        {
            _subscriptionService = subscriptionService;
        }
        // GET: api/<SubscribesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public async Task<IActionResult> SubscribeToArtist(int artistApiId, int userId)
        {
            return Ok(await _subscriptionService.SubscribeToArtist(artistApiId, userId));
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
