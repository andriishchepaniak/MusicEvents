using Models.SpotifyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpotifyApi.Interfaces
{
    public interface ISpotifyAlbumService
    {
        Task<Album> GetAlbumById(string albumId);
        Task<IEnumerable<Album>> GetAlbumsByArtistId(string artistId);
        
    }
}
