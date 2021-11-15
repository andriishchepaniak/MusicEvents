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
        private readonly ISpotifyService _albumService;
        public AlbumsController(
            ISpotifyAccountService spotifyAccountService, 
            ISpotifyService albumService, 
            ILogger<AlbumsController> logger) : base(logger)
        {
            _spotifyAccountService = spotifyAccountService;
            _albumService = albumService;
        }
        [Route("album")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var token = await _spotifyAccountService.GetAccessToken();
            var album = await _albumService.GetAlbumById(id, token);
            return Ok(album);
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
            var token = await _spotifyAccountService.GetAccessToken();
            var albums = await _albumService.GetAlbumsByArtistId(artistId, token);
            return Ok(albums);
        }
        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> Search(string artistName)
        {
            var token = await _spotifyAccountService.GetAccessToken();
            var artists = await _albumService.GetArtistsByName(artistName, token);
            return Ok(artists);
        }
    }
}
