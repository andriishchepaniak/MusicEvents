using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class LocationController : BaseController<LocationController>
    {
        private readonly ILocationServiceApi _locationServiceApi;
        public LocationController(ILocationServiceApi locationServiceApi, 
            ILogger<LocationController> logger) : base(logger)
        {
            _locationServiceApi = locationServiceApi;
        }
        [Route("[action]/{locationName}")]
        [HttpGet()]
        public async Task<IActionResult> GetLocationsByName(string locationName)
        {
            return await ExecuteAction(async () => await _locationServiceApi.GetByName(locationName));
        }
    }
}
