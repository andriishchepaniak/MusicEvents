using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISubscriptionService
    {
        //Task<Artist> SubscribeToArtist(int artistApiId, int userId);
        Task<Artist> SubscribeToArtist(int artistApiId, int userId);
        Task<City> SubscribeToCity(int cityApiId, int userId);
        Task<ArtistAndCitySubscription> SubscribeToArtistAndCity(
            int artistApiId, 
            int cityApiId,
            int userId);
    }
}
