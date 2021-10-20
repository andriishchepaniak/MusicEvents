using Microsoft.AspNetCore.Mvc;
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
    public class VenuesController : ControllerBase
    {
        private readonly IVenueServiceApi _venueServiceApi;
        public VenuesController(IVenueServiceApi venueServiceApi)
        {
            _venueServiceApi = venueServiceApi;
        }
        // GET: api/<VenueController>
        [HttpGet]
        public async Task<IEnumerable<Venue>> GetVenuesByName(string venueName)
        {
            return await _venueServiceApi.GetVenuesByName(venueName);
        }
        [HttpGet("{venueId}")]
        public async Task<Venue> GetById(int venueId)
        {
            return await _venueServiceApi.GetVenueDetails(venueId);
        }
    }
}
