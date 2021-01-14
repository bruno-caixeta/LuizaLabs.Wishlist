using System.IO;
using System.Text;
using LuizaLabs.Wishlist.App.Interfaces.Services;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.App.Services;
using LuizaLabs.Wishlist.App.Wrappers;
using LuizaLabs.Wishlist.Domain.Database;
using LuizaLabs.Wishlist.Domain.Repositories.Implementation;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace LuizaLabs.Wishlist.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("resources.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("fiver",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers();

            services.AddSingleton(LoadClientResponseMessages());
            services.AddSingleton(LoadFavoriteResponseMessages());
            services.AddTransient(typeof(ILoggerWrapper<>), typeof(LoggerWrapper<>));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IFavoriteService, FavoriteService>();

            services.AddDbContext<PostgresContext>() ;

            var key = Encoding.ASCII.GetBytes("ashmutesceneryhiddensummer");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private ClientResponseMessages LoadClientResponseMessages()
        {
            return Configuration.GetSection(nameof(ClientResponseMessages)).Get<ClientResponseMessages>();
        }

        private FavoriteResponseMessages LoadFavoriteResponseMessages()
        {
            return Configuration.GetSection(nameof(FavoriteResponseMessages)).Get<FavoriteResponseMessages>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("fiver");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var context = new PostgresContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
