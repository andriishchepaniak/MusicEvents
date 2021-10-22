using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SongkickAPI;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickAPI.Settings;

namespace SongkickApi
{
    public static class ConfigureSongkickApiExtension
    {
        public static void ConfigureSongkickApi(IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<SongkickServiceApi>();
            services.AddTransient<EventServiceApi>();
            services.AddTransient<ArtistServiceApi>();
            services.AddTransient<IVenueServiceApi, VenueServiceApi>();
            services.AddTransient<ILocationServiceApi, LocationServiceApi>();

            services.Configure<SongkickApiSettings>
                (configuration.GetSection("SongkickApiIntegration"));
        }
    }
}
