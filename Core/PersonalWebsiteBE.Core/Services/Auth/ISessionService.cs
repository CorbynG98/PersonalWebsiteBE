using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Auth
{
    public interface ISessionService : IService<Session>
    {
        Task<List<Session>> GetSessionsForUser(string userId, int skip, int limit);
        Task<bool> VerifyUserSession(string sessionToken);
        Task RevokeSessionById(string sessionId, string userId, string requestingSessionId);
        Task<User> GetUserBySessionToken(string sessionToken);
        Task<Session> GetSessionByToken(string sessionToken);
    }
}
