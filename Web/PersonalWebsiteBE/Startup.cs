using AspNetCoreRateLimit;
using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalWebsiteBE.Core.Middleware;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Repository.Repositories.Core;
using PersonalWebsiteBE.Repository.Repositories.Auth;
using PersonalWebsiteBE.Services.Services.Auth;
using PersonalWebsiteBE.Services.Services.Core;
using PersonalWebsiteBE.Core.Extensions;
using System;
using System.Collections.Generic;

namespace PersonalWebsiteBE
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
            #if DEBUG
            var keyPath = "C:\\Git\\Personal website\\keys\\personal-313620.json"; // Change this if running on different computer
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            #endif

            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddInMemoryRateLimiting();

            // Throw in auto mapper
            services.AddAutoMapper(typeof(Startup));

            // Configuration based injects
            services.AddConfigurationLayer(Configuration);
            // Repositories
            services.AddRepositoryLayer();
            // Services
            services.AddServiceLayer();

            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGoogleExceptionLogging();

            app.UseIpRateLimiting();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add error handling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(t => t
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("corbyngreenwood.com", "127.0.0.1:3000", "localhost:3000")
                .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
