using Core.Interfaces;
using Models.SpotifyEntities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlbumsService : IAlbumService
    {
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly ISpotifyArtistService _spotifyArtistsService;
        private readonly ISpotifyAlbumService _spotifyAlbumService;
        public AlbumsService(IArtistServiceApi artistServiceApi, ISpotifyArtistService spotifyArtistsService, ISpotifyAlbumService spotifyAlbumService)
        {
            _artistServiceApi = artistServiceApi;
            _spotifyAlbumService = spotifyAlbumService;
            _spotifyArtistsService = spotifyArtistsService;
        }
        public async Task<IEnumerable<Album>> GetAlbumsBySongkickArtistId(int songkickArtistId)
        {
            var songkickArtist = await _artistServiceApi.GetArtistDetails(songkickArtistId);
            var spotifyArtists = (await _spotifyArtistsService.GetArtistsByName(songkickArtist.displayName)).ToList();
            var albums = await _spotifyAlbumService.GetAlbumsByArtistId(spotifyArtists[0].id);
            return albums;
        }
    }
}
