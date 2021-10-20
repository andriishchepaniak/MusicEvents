using DAL.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ArtistSubscriptionRepository : 
        BaseRepository<ArtistSubscription>, IArtistSubscriptionRepository
    {
        public ArtistSubscriptionRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
