using SongkickEntities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistSubscriptionService
    {
        Task<ArtistApi> SubscribeToArtist(int artistApiId, int userId);
        Task<ArtistApi> UnSubscribeFromArtist(int artistApiId, int userId);
    }
}
