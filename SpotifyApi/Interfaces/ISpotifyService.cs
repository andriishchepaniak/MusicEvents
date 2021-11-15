using SpotifyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Interfaces
{
    public interface ISpotifyService
    {
        Task<Album> GetAlbumById(string albumId, string token);
        Task<IEnumerable<Album>> GetAlbumsByArtistId(string artistId, string token);
        Task<List<Artist>> GetArtistsByName(string artistName, string token, int limit=10);
        Task<Artist> GetFirstArtist(string artistName, string token);
    }
}
