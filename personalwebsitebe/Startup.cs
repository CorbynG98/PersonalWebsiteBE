using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Services.Repositories.Auth;
using PersonalWebsiteBE.Services.Services.Auth;
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

            // Configure appsettings data
            // services.Configure<IFireStoreSettings>(Configuration.GetSection("FireStoreSettings"));
            IFireStoreSettings fireStore = new FireStoreSettings();
            Configuration.GetSection("FireStoreSettings").Bind(fireStore);
            services.AddSingleton(fireStore);

            // Throw in auto mapper
            services.AddAutoMapper(typeof(Startup));

            // Inject repositories
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));

            // Inject services
            services.AddTransient<IUserService, UserService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
