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
using PersonalWebsiteBE.Services.Repositories.Auth;
using PersonalWebsiteBE.Services.Services.Auth;
using PersonalWebsiteBE.Services.Services.Core;
using System;

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
            var keyPath = "C:\\Git\\Personal website\\keys\\personal-313620-455b9a56117a.json"; // Change this if running on different computer
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            #endif

            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddInMemoryRateLimiting();

            // Configure appsettings data
            ISendGridSettings sendGrid = new SendGridSettings();
            Configuration.GetSection("Sendgrid").Bind(sendGrid);
            services.AddSingleton(sendGrid);

            IFireStoreSettings fireStore = new FireStoreSettings();
            Configuration.GetSection("FireStoreSettings").Bind(fireStore);
            services.AddSingleton(fireStore);

            // Throw in auto mapper
            services.AddAutoMapper(typeof(Startup));

            // Inject repositories
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));
            services.AddScoped(typeof(IMiscRepository), typeof(MiscRepository));
            services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));

            // Inject services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMiscService, MiscService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = fireStore.ProjectId;
                options.ServiceName = "personalwebsitebe";
                options.Version = "1.1";
            });

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add error handling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
