using Core.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SongkickAPI.Interfaces;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : BaseController<VenuesController>
    {
        private readonly IVenueServiceApi _venueServiceApi;
        public VenuesController(IVenueServiceApi venueServiceApi,
            ILogger<VenuesController> logger) : base(logger)
        {
            _venueServiceApi = venueServiceApi;
        }
        // GET: api/<VenueController>
        [HttpGet]
        public async Task<IActionResult> GetVenuesByName(string name)
        {
            return await ExecuteAction(async () => await _venueServiceApi.GetVenuesByName(name));
        }
        [HttpGet("{venueId}")]
        public async Task<IActionResult> GetById(int venueId)
        {
            return await ExecuteAction(async () => await _venueServiceApi.GetVenueDetails(venueId));
        }
        [Route("testJob")]
        [HttpGet]
        public IActionResult NotifyJob()
        {
            JobService.SendAlbumsJob();
            return Ok();
        }
    }
}
