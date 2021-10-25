using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicEvents.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistSubsController : BaseController<ArtistSubsController>
    {
        private readonly IArtistSubscriptionService _artistSubService;
        public ArtistSubsController(IArtistSubscriptionService artistSubService,
            ILogger<ArtistSubsController> logger) : base(logger)
        {
            _artistSubService = artistSubService;
        }
        // GET: api/<SubscriptionsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAction(async () =>
            {
                return await _artistSubService.GetAll();
            });
        }

        // POST api/<SubscriptionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArtistSubscriptionDTO artistSubscription)
        {
            return await ExecuteAction(async () =>
            {
                await _artistSubService.Add(artistSubscription);
                return artistSubscription;
            });
        }

        // PUT api/<SubscriptionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArtistSubscriptionDTO artistSubscription)
        {
            return await ExecuteAction(async () =>
            {
                await _artistSubService.Update(artistSubscription);
                return artistSubscription;
            });
        }

        // DELETE api/<SubscriptionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistSubService.Delete(id);
            });
        }
    }
}
