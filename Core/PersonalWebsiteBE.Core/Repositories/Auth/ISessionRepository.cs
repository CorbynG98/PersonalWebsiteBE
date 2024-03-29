﻿using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Repositories.Auth
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session> GetSessionByTokenAsync(string sessionToken);
        Task<List<Session>> GetAllSessionsByUserId(string userId, int skip, int limit);
    }
}
