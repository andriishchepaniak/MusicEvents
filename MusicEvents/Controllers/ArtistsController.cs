using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ArtistsController : BaseController<ArtistsController>
    {
        private readonly IArtistService _artistService;
        private readonly IArtistServiceApi _artistServiceApi;
        public ArtistsController(
            IArtistService artistService, 
            IArtistServiceApi artistServiceApi,
            ILogger<ArtistsController> logger) : base(logger)
        {
            _artistService = artistService;
            _artistServiceApi = artistServiceApi;
        }
        
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersArtists(int userId)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistService.GetUsersArtists(userId);
            });
        }
        [Route("[action]/{artistId}")]
        [HttpGet()]
        public async Task<IActionResult> GetSimilarArtists(int artistId)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistServiceApi.GetSimilarArtists(artistId);
            });
        }
        [Route("[action]/{artistName}")]
        [HttpGet()]
        public async Task<IActionResult> GetArtistsByName(string artistName)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistServiceApi.GetArtistsByName(artistName);
            });
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistServiceApi.GetArtistDetails(id);
            });
        }
    }
}
