using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi
{
    public static class ConfigureSpotifyApiExtensions
    {
        public static void ConfigureSpotifyApi(IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure();
            //services.Configure<ClientSettings>(configuration.GetSection("SpotifyApiSettings"));
            //services.Configure<MailSettings>(configuration.GetSection("MailSettingsProduction"));
        }
    }
}
