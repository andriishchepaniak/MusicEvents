using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface IArtistServiceApi
    {
        Task<IEnumerable<ArtistApi>> GetArtistsByName(string artistName);
        Task<IEnumerable<ArtistApi>> GetSimilarArtists(int artistId);
        Task<ArtistApi> GetArtistDetails(int artistId);
    }
}
