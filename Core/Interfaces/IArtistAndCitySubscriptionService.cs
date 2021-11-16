using Models.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistAndCitySubscriptionService
    {
        Task<ArtistAndCitySubscription> SubscribeToArtistAndCity(int artistApiId, int cityApiId, int userId);
        Task<int> UnSubscribeFromArtistAndCity(int artistApiId, int cityApiId, int userId);
    }
}
