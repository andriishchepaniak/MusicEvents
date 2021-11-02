using DAL.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
