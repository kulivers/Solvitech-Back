using System;
using System.Text;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecretsProvider;
using Solvintech.Contracts;
using Solvintech.Repository;
using Solvintech.Services;
using Solvintech.Services.Contracts;

namespace SolvintechTestProject.Extentions
{
    public static class ServiceExtentions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod() //todo origins
                        .AllowCredentials().WithExposedHeaders("X-Pagination");
                });
            });

        public static void ConfigureIisIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(_ => { });

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("SolvintechTestProject")));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration) =>
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));


        public static void ConfigureJwt(this IServiceCollection services, IConfiguration
            configuration)
        {
            configuration.GetSection("JwtSettings");
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = JwtSecretsProvider.GetJwtSecretKey();
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //• The issuer must be actual server that created the token 
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        //• The receiver must be a valid recipient
                        //The token can be expired
                        //The signing key must be valid and trusted by the server
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.ValidIssuer,
                        ValidAudience = jwtConfiguration.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ??
                            throw new InvalidOperationException(
                                "secretKey is null")))
                    };
                });
        }
    }
}