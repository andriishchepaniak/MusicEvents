using AutoMapper;
using Core;
using Core.Interfaces;
using Core.Mappings;
using Core.Services;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SongkickApi;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using SongkickAPI.Settings;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Hangfire;
using Core.Authentication.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SpotifyApi.Interfaces;
using SpotifyApi.Services;
using System;
using SpotifyApi;
using AudDApi;

namespace MusicEvents
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicEvents", Version = "v1" });
            });
            
            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddHangfire(h => h.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();

            ConfigureDALExtension.ConfigureDAL(services, Configuration);

            ConfigureSongkickApiExtension.ConfigureSongkickApi(services, Configuration);

            ConfigureCoreExtension.ConfigureCore(services, Configuration);

            ConfigureAuthExtension.ConfigureAuth(services, Configuration);

            ConfigureSpotifyApiExtension.ConfigureSpotifyApi(services, Configuration);

            ConfigureAudDApiExtension.ConfigureAudDApi(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicEvents v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/dashboard");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
