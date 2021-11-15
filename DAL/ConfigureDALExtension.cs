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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IArtistAndCitySubscriptionRepository, 
                                  ArtistAndCitySubscriptionRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
