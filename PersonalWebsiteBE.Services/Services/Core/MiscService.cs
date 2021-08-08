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
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;

namespace PersonalWebsiteBE.Services.Services
{
    public class MiscService : IMiscService
    {
        private readonly IMiscRepository miscRepository;

        public MiscService(IMiscRepository miscRepository)
        {
            this.miscRepository = miscRepository;
        }

        public async Task LogPageView(PageView data) {
            await miscRepository.CreateOneAsync(data);
        }
    }
}
