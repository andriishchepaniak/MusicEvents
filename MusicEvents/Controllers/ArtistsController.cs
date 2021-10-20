using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ArtistServiceApi _artistServiceApi;
        public ArtistsController(IArtistService artistService, ArtistServiceApi artistServiceApi)
        {
            _artistService = artistService;
            _artistServiceApi = artistServiceApi;
        }
        
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IEnumerable<Artist>> GetUsersArtists(int userId)
        {
            return await _artistService.GetUsersArtists(userId);

        }
        [Route("[action]/{artistId}")]
        [HttpGet()]
        public async Task<IEnumerable<Artist>> GetSimilarArtists(int artistId)
        {
            return await _artistServiceApi.GetSimilarArtists(artistId);

        }
        [Route("[action]/{artistName}")]
        [HttpGet()]
        public async Task<IEnumerable<Artist>> GetArtistsByName(string artistName)
        {
            return await _artistServiceApi.GetArtistsByName(artistName);

        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<Artist> GetArtistById(int id)
        {
            return await _artistServiceApi.GetArtistDetails(id);
        }
    }
}
