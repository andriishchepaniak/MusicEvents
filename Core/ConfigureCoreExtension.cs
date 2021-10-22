using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ConfigureCoreExtension
    {
        public static void ConfigureCore(IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            }).CreateMapper());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IArtistSubscriptionService, ArtistSubscriptionService>();
            services.AddTransient<ICitySubscriptionService, CitySubscriptionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IArtistService, ArtistService>();
        }
    }
}
