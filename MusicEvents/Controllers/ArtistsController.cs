using AutoMapper;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using SongkickAPI.Interfaces;
using SpotifyApi.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEvents.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : BaseController<ArtistsController>
    {
        private readonly IArtistService _artistService;
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly ISpotifyArtistService _spotifyArtistService;
        public ArtistsController(
            IArtistService artistService, 
            IArtistServiceApi artistServiceApi,
            ISpotifyArtistService spotifyArtistService,
            ILogger<ArtistsController> logger) : base(logger)
        {
            _artistService = artistService;
            _artistServiceApi = artistServiceApi;
            _spotifyArtistService = spotifyArtistService;
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
        [AllowAnonymous]
        [Route("[action]/{artistName}")]
        [HttpGet()]
        public async Task<IActionResult> GetArtistsByName(string artistName)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistServiceApi.GetArtistsByName(artistName);
            });
        }
        [AllowAnonymous]
        [Route("[action]/{artistName}")]
        [HttpGet()]
        public async Task<IActionResult> GetSpotifyArtistsByName(string artistName)
        {
            return await ExecuteAction(async () =>
            {
                return await _spotifyArtistService.GetArtistsByName(artistName);
            });
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistServiceApi.GetArtistDetails(id);
            });
        }
        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await ExecuteAction(async () => await _artistService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Artist artist)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistService.Add(artist);
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Artist artist)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistService.Update(artist);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _artistService.Delete(id);
            });
        }
        [AllowAnonymous]
        [Route("deleteAll")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            return await ExecuteAction(async () =>
            {
                return await _artistService.DeleteAll();
            });
        }
    }
}
