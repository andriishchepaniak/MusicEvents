using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ISpotifyTrackService _trackService;
        public TracksController(ISpotifyTrackService trackService)
        {
            _trackService = trackService;
        }
        [HttpGet("{trackName}")]
        public async Task<IActionResult> GetTracksByName(string trackName)
        {
            return Ok(await _trackService.GetTracksByName(trackName));
        }
        
    }
}
