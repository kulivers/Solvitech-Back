using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Solvintech.Contracts;
using Solvintech.Repository;
using Solvintech.Services;
using Solvintech.Services.Contracts;
using SolvintechTestProject.Extentions;

namespace SolvintechTestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMapper();
            services.ConfigureCors();
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            // services.ConfigureIisIntegration();
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true; // todo check what is it
                config.ReturnHttpNotAcceptable = true;
                config.ReturnHttpNotAcceptable = true;
            });
            services.ConfigureDbContext(Configuration.GetConnectionString("Solvintech"));
            services.AddJwtConfiguration(Configuration);
            services.ConfigureJwt(Configuration);
            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolvintechTestProject v1"));
            }
            else
                app.UseHsts();
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}