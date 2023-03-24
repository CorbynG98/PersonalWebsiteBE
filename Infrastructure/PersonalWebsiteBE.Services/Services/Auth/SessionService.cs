using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.IpApi;
using PersonalWebsiteBE.Repository.Repositories.Auth;

namespace PersonalWebsiteBE.Services.Services.Auth
{
    public class SessionService : Service<Session>, ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IUserRepository userRepository;

        public SessionService(ISessionRepository sessionRepository, IUserRepository userRepository) : base(sessionRepository)
        {
            this.sessionRepository = sessionRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<Session>> GetSessionsForUser(string userId)
        {
            return await sessionRepository.GetAllSessionsByUserId(userId);
        }

        public async Task RevokeSessionById(string sessionId, string userId, string requestingSessionId) { 
            await sessionRepository.DeleteOneAsync(sessionId);
            // Create auth activity for this
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, UserId = userId, SessionId = requestingSessionId, Type = AuthActivityType.SessionRevoked };
            await userRepository.CreateLoginActivityAsync(userId, activity);
        }
    }
}
