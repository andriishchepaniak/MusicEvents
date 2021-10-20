using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface IArtistServiceApi
    {
        Task<IEnumerable<Artist>> GetArtistsByName(string artistName);
        Task<IEnumerable<Artist>> GetSimilarArtists(int artistId);
        Task<Artist> GetArtistDetails(int artistId);
    }
}
