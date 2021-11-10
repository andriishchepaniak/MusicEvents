using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWorkService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository UserRepository { get; }

        public IArtistAndCitySubscriptionRepository ArtistAndCitySubscription { get; }
        public IEventRepository EventRepository { get; }
        public IArtistRepository ArtistRepository { get; }
        public ICityRepository CityRepository { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository userRepository,
            IArtistAndCitySubscriptionRepository artistAndCitySubscription,
            IEventRepository eventRepository,
            IArtistRepository artistRepository,
            ICityRepository cityRepository)
        {
            _context = context;
            UserRepository = userRepository;
            ArtistAndCitySubscription = artistAndCitySubscription;
            EventRepository = eventRepository;
            ArtistRepository = artistRepository;
            CityRepository = cityRepository;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
