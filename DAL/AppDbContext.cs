using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ArtistSubscription> ArtistSubscriptions { get; set; }
        public DbSet<CitySubscription> CitySubscriptions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
    }
}
