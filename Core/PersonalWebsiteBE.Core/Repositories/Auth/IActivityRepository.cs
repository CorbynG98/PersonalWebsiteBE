using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Repositories.Auth
{
    public interface IActivityRepository : IRepository<AuthActivity>
    {
        Task<List<AuthActivity>> GetAllActivitiesByUserId(string userId, int skip, int limit);
        Task CreateAuthActivity(AuthActivity activity);
    }
}
