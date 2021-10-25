using Core.DTO;
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
    public class ArtistAndCitySubsController : BaseController<ArtistAndCitySubsController>
    {
        private readonly IArtistAndCitySubscriptionService _artistAndCitySubscriptionService;
        public ArtistAndCitySubsController
            (IArtistAndCitySubscriptionService artistAndCitySubscriptionService, 
             ILogger<ArtistAndCitySubsController> logger) : base(logger)
        {
            _artistAndCitySubscriptionService = artistAndCitySubscriptionService;
        }
        // GET: api/<ArtistAndCituSubsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscriptionService.GetAll();
            });
        }

        // GET api/<ArtistAndCituSubsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscriptionService.GetById(id);
            });
        }

        // POST api/<ArtistAndCituSubsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArtistAndCitySubscriptionDTO value)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscriptionService.Add(value);
            });
        }

        // PUT api/<ArtistAndCituSubsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArtistAndCitySubscriptionDTO value)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscriptionService.Add(value);
            });
        }

        // DELETE api/<ArtistAndCituSubsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistAndCitySubscriptionService.Delete(id);
            });
        }
    }
}
