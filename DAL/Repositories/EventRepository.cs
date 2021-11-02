using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public async Task<IEnumerable<Event>> GetAllWithUsers(Expression<Func<Event, bool>> predicate)
        {
            return await _db.Events
                .Where(predicate)
                .Include(e => e.Users)
                .ToListAsync();
        }
    }
}
