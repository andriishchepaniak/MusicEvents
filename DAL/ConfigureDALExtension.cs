using DAL.Interfaces;
using DAL.Repositories;
using DAL.UnitOfWorkService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ConfigureDALExtension
    {
        public static void ConfigureDAL(IServiceCollection services,
                                        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IArtistSubscriptionRepository, ArtistSubscriptionRepository>();
            services.AddTransient<ICitySubscriptionRepository, CitySubscriptionRepository>();
            services.AddTransient<IArtistAndCitySubscriptionRepository, 
                                  ArtistAndCitySubscriptionRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
