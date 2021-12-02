using AudDApi.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudDApi
{
    public static class ConfigureAudDApiExtension
    {
        public static void ConfigureAudDApi(IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddHttpClient<IRecognitionService, RecognitionService>();
            services.Configure<AudDSettings>(configuration.GetSection("AudDSettings"));
        }
    }
}
