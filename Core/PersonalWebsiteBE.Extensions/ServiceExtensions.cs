using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Services.Services.Auth;
using PersonalWebsiteBE.Services.Services.Core;

namespace PersonalWebsiteBE.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IMiscService, MiscService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProjectService, ProjectService>();
        }
    }
}
