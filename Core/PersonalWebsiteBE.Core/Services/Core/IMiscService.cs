using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Core
{
    public interface IMiscService : IService<PageView>
    {
        Task LogPageView(PageView data);
        Task<AboutYouData> GetAboutYouData(string ipAddress);
    }
}
