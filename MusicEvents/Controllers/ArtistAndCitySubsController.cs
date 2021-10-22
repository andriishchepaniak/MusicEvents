using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistAndCitySubsController : ControllerBase
    {
        private readonly IArtistAndCitySubscriptionService _artistAndCitySubscriptionService;
        public ArtistAndCitySubsController
            (IArtistAndCitySubscriptionService artistAndCitySubscriptionService)
        {
            _artistAndCitySubscriptionService = artistAndCitySubscriptionService;
        }
        // GET: api/<ArtistAndCituSubsController>
        [HttpGet]
        public async Task<IEnumerable<ArtistAndCitySubscriptionDTO>> Get()
        {
            return await _artistAndCitySubscriptionService.GetAll();
        }

        // GET api/<ArtistAndCituSubsController>/5
        [HttpGet("{id}")]
        public async Task<ArtistAndCitySubscriptionDTO> Get(int id)
        {
            return await _artistAndCitySubscriptionService.GetById(id);
        }

        // POST api/<ArtistAndCituSubsController>
        [HttpPost]
        public async Task<ArtistAndCitySubscriptionDTO> Post([FromBody] ArtistAndCitySubscriptionDTO value)
        {
            return await _artistAndCitySubscriptionService.Add(value);
        }

        // PUT api/<ArtistAndCituSubsController>/5
        [HttpPut("{id}")]
        public async Task<ArtistAndCitySubscriptionDTO> Put(int id, [FromBody] ArtistAndCitySubscriptionDTO value)
        {
            return await _artistAndCitySubscriptionService.Add(value);

        }

        // DELETE api/<ArtistAndCituSubsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _artistAndCitySubscriptionService.Delete(id);
        }
    }
}
