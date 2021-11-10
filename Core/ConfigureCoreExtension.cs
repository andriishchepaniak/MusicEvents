using AutoMapper;
using Core.Authentication.Service;
using Core.EmailService;
using Core.Interfaces;
using Core.Jobs;
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
            services.AddTransient<IArtistAndCitySubscriptionService, ArtistAndCitySubscriptionService>();
            services.AddTransient<IArtistSubscriptionService, ArtistSubscriptionService>();
            services.AddTransient<ICitySubscriptionService, CitySubscriptionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<JobService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IMailService, MailService>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }
    }
}
