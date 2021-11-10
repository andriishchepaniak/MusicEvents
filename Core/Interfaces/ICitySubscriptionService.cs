using Models.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICitySubscriptionService
    {
        Task<City> SubscribeToCity(int cityApiId, int userId);
    }
}
