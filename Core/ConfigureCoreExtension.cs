using AutoMapper;
using Core.EmailService;
using Core.Interfaces;
using Core.Mappings;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ConfigureCoreExtension
    {
        public static void ConfigureCore(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            }).CreateMapper());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IArtistSubscriptionService, ArtistSubscriptionService>();
            services.AddTransient<ICitySubscriptionService, CitySubscriptionService>();
            services.AddTransient<IArtistAndCitySubscriptionService, ArtistAndCitySubscriptionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();

            services.AddTransient<IMailService, MailService>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }
    }
}
