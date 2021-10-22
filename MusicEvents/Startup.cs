using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using Core.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestSharp;
using SongkickAPI;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickAPI.Settings;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicEvents", Version = "v1" });
            });
            //DAL
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<SongkickApi>();
            services.AddTransient<EventServiceApi>();
            services.AddTransient<ArtistServiceApi>();
            services.AddTransient<IVenueServiceApi, VenueServiceApi>();
            services.AddTransient<ILocationServiceApi, LocationServiceApi>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IArtistSubscriptionRepository, ArtistSubscriptionRepository>();
            services.AddTransient<ICitySubscriptionRepository, CitySubscriptionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //
            //BLL
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            }).CreateMapper());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IArtistSubscriptionService, ArtistSubscriptionService>();
            services.AddTransient<ICitySubscriptionService, CitySubscriptionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IArtistService, ArtistService>();
            //

            services.AddMvc().AddNewtonsoftJson();
            services.Configure<SongkickApiSettings>(
                Configuration.GetSection("SongkickApiIntegration"));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
