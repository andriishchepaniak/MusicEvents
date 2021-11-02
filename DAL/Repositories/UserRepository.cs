using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users
                .Include(u => u.Artists)
                .Include(u => u.Cities)
                .Include(u => u.ArtistAndCitySubscriptions)
                .Include(u => u.Events)
                .ToListAsync();
        }
        public override async Task<User> GetById(int id)
        {
            return await _db.Users
                .Include(u => u.Artists)
                .Include(u => u.Cities)
                .Include(u => u.ArtistAndCitySubscriptions)
                .Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
    }
}
