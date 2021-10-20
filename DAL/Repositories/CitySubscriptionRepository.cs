using DAL.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class CitySubscriptionRepository : 
        BaseRepository<CitySubscription>, ICitySubscriptionRepository
    {
        public CitySubscriptionRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
