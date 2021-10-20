using Microsoft.AspNetCore.Mvc;
using Models.SongkickEntities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
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
    public class LocationController : ControllerBase
    {
        private readonly ILocationServiceApi _locationServiceApi;
        public LocationController(ILocationServiceApi locationServiceApi)
        {
            _locationServiceApi = locationServiceApi;
        }
        [Route("[action]/{locationName}")]
        [HttpGet()]
        public async Task<IEnumerable<LocationCity>> GetLocationsByName(string locationName)
        {
            return await _locationServiceApi.GetByName(locationName);
        }
    }
}
