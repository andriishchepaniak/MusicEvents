using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistSubsController : ControllerBase
    {
        private readonly IArtistSubscriptionService _artistSubService;
        public ArtistSubsController(IArtistSubscriptionService artistSubService)
        {
            _artistSubService = artistSubService;
        }
        // GET: api/<SubscriptionsController>
        [HttpGet]
        public async Task<IEnumerable<ArtistSubscriptionDTO>> Get(int artistId)
        {
            return await _artistSubService.GetAll();
        }

        // POST api/<SubscriptionsController>
        [HttpPost]
        public async Task<ArtistSubscriptionDTO> Post([FromBody] ArtistSubscriptionDTO artistSubscription)
        {
            await _artistSubService.Add(artistSubscription);
            return artistSubscription;
        }

        // PUT api/<SubscriptionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubscriptionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _artistSubService.Delete(id);
        }
    }
}
