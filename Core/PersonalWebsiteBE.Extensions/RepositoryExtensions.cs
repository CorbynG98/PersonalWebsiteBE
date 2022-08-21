using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Repository.Repositories.Auth;
using PersonalWebsiteBE.Repository.Repositories.Core;

namespace PersonalWebsiteBE.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));
            services.AddScoped(typeof(IMiscRepository), typeof(MiscRepository));
            services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));
            services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));
            services.AddScoped(typeof(IEmailTemplateRepository), typeof(EmailTemplateRepository));
        }
    }
}
