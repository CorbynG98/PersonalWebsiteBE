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
    public class ActivityService : Service<AuthActivity>, IActivityService
    {
        private readonly IActivityRepository activityRepository;

        public ActivityService(IActivityRepository activityRepository) : base(activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public async Task<List<AuthActivity>> GetActivitiesByUserId(string userId, int skip, int limit)
        {
            return await activityRepository.GetAllActivitiesByUserId(userId, skip, limit);
        }
    }
}
