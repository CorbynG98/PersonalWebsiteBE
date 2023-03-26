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
        private readonly IActivityRepository activityRepository;
        private readonly IUserRepository userRepository;

        public SessionService(ISessionRepository sessionRepository, IUserRepository userRepository, IActivityRepository activityRepository) : base(sessionRepository)
        {
            this.sessionRepository = sessionRepository;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<Session>> GetSessionsForUser(string userId, int skip, int limit)
        {
            return await sessionRepository.GetAllSessionsByUserId(userId, skip, limit);
        }

        public async Task<bool> VerifyUserSession(string sessionToken)
        {
            if (string.IsNullOrWhiteSpace(sessionToken)) return false;
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return false;
            return true;
        }

        public async Task RevokeSessionById(string sessionId, string userId, string requestingSessionId) { 
            await sessionRepository.DeleteOneAsync(sessionId);
            // Create auth activity for this
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, ExpireAt = DateTime.UtcNow.AddDays(30), UserId = userId, SessionId = requestingSessionId, Type = AuthActivityType.SessionRevoked };
            await activityRepository.CreateAuthActivity(activity);
        }

        public async Task<Session> GetSessionByToken(string sessionToken)
        {
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return null;
            return session;
        }

        public async Task<User> GetUserBySessionToken(string sessionToken)
        {
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return null;
            var user = await userRepository.GetOneAsync(session.UserId);
            if (user == null) return null;
            return user;
        }
    }
}
