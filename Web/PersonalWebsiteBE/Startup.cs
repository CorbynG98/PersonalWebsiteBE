using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalWebsiteBE.Core.Middleware;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Core.Extensions;
using System;
using Google.Cloud.Diagnostics.AspNetCore3;

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


            IFireStoreSettings googleCloudSettings = new FireStoreSettings();
            Configuration.GetSection("GoogleCloud").Bind(googleCloudSettings);

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

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddGoogleErrorReportingForAspNetCore(new Google.Cloud.Diagnostics.Common.ErrorReportingServiceOptions
            {
                ProjectId = googleCloudSettings.ProjectId,
                ServiceName = "personalwebsitebe",
                Version = "1.1"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
