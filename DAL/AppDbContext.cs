using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<City> Cities { get; set; }
        
        public DbSet<ArtistAndCitySubscription> ArtistAndCitySubscriptions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
    }
}
