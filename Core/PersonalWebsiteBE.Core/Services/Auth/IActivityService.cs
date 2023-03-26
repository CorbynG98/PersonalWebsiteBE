using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Auth
{
    public interface IActivityService : IService<AuthActivity>
    {
        Task<List<AuthActivity>> GetActivitiesByUserId(string userId, int skip, int limit);
    }
}
