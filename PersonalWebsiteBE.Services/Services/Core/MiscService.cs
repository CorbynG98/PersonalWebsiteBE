using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;

namespace PersonalWebsiteBE.Services.Services.Core
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
