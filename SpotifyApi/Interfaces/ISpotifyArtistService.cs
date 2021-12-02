using Models.SpotifyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Interfaces
{
    public interface ISpotifyArtistService
    {
        Task<IEnumerable<ArtistSpotify>> GetArtistsByName(string artistName, int limit = 10);
        Task<ArtistSpotify> GetFirstArtist(string artistName);
    }
}
