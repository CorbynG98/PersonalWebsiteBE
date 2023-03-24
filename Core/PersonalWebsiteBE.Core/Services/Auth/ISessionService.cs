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
        Task<List<Session>> GetSessionsForUser(string userId);
        Task RevokeSessionById(string sessionId, string userId, string requestingSessionId);
    }
}
