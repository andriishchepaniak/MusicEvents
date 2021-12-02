using AudDApi;
using AudDApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : BaseController<AlbumsController>
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly ISpotifyAlbumService _albumService;
        private readonly ISpotifyArtistService _artistService;
        private readonly ISpotifyTrackService _trackService;
        public AlbumsController(
            ISpotifyAccountService spotifyAccountService, 
            ISpotifyAlbumService albumService,
            ISpotifyArtistService artistService,
            ISpotifyTrackService trackService,
            ILogger<AlbumsController> logger) : base(logger)
        {
            _spotifyAccountService = spotifyAccountService;
            _albumService = albumService;
            _artistService = artistService;
            _trackService = trackService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await ExecuteAction(async () =>
            {
                return await _albumService.GetAlbumById(id);
            });
        }
        [Route("token")]
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var token = await _spotifyAccountService.GetAccessToken();
            return Ok(token);
        }
        [Route("getArtistAlbums")]
        [HttpGet]
        public async Task<IActionResult> GetArtistAlbums(string artistId)
        {
            var albums = await _albumService.GetAlbumsByArtistId(artistId);
            return Ok(albums);
        }
        [HttpGet("{albumId}/tracks")]
        public async Task<IActionResult> GetAlbumTracks(string albumId)
        {
            return Ok(await _trackService.GetAlbumTracks(albumId));
        }
    }
}
