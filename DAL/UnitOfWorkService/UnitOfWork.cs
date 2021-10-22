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

        public IArtistSubscriptionRepository ArtistSubscription { get; }

        public ICitySubscriptionRepository CitySubscription { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository userRepository,
            IArtistSubscriptionRepository artistSubscription,
            ICitySubscriptionRepository citySubscription)
        {
            _context = context;
            UserRepository = userRepository;
            ArtistSubscription = artistSubscription;
            CitySubscription = citySubscription;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
