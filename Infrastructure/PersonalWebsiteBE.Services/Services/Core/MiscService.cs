using System.Threading.Tasks;
using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using RestSharp;

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

        public async Task<AboutYouData> GetAboutYouData(string ipAddress)
        {
            var client = new RestClient(@"http://ip-api.com/");
            var request = new RestRequest($"json/{ipAddress}");
            var response = await client.GetAsync(request);
            return JsonConvert.DeserializeObject<AboutYouData>(response.Content);
        }
    }
}
