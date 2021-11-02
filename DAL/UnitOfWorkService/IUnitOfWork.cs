using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWorkService
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IArtistSubscriptionRepository ArtistSubscription { get; }
        ICitySubscriptionRepository CitySubscription { get; }
        IArtistAndCitySubscriptionRepository ArtistAndCitySubscription { get; }
        IEventRepository EventRepository { get; }
        IArtistRepository ArtistRepository { get; }
        ICityRepository CityRepository { get; }
        Task<int> SaveAsync();
    }
}
