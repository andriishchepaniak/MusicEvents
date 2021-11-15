using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApi.Interfaces;
using SpotifyApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi
{
    public static class ConfigureSpotifyApiExtension
    {
        public static void ConfigureSpotifyApi(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(c =>
            {
                c.BaseAddress = new Uri("https://accounts.spotify.com/api/");
            });
            services.AddHttpClient<ISpotifyService, SpotifyService>(c =>
            {
                c.BaseAddress = new Uri("https://api.spotify.com/v1/");
            });
            
            services.Configure<ClientSettings>(configuration.GetSection("SpotifyApiSettings"));
        }
    }
}
