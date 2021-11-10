using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistSubscriptionService
    {
        Task<int> SubscribeToArtist(int artistApiId, int userId);
    }
}
