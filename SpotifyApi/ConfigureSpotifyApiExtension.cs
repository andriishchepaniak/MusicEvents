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
            services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>();
            services.AddHttpClient<ISpotifyAlbumService, SpotifyAlbumService>();
            services.AddHttpClient<ISpotifyArtistService, SpotifyArtistService>();
            services.AddHttpClient<ISpotifyTrackService, SpotifyTrackService>();
            services.Configure<ClientSettings>(configuration.GetSection("SpotifyApiSettings"));
        }
    }
}
