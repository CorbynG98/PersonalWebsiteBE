using System.Threading.Tasks;
using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.IpApi;
using RestSharp;

namespace PersonalWebsiteBE.Services.Services.Core
{
    public class MiscService : IMiscService
    {
        private readonly IMiscRepository miscRepository;
        private readonly IpApiIntegration ipApiIntegration;

        public MiscService(IMiscRepository miscRepository)
        {
            this.ipApiIntegration = new IpApiIntegration();
            this.miscRepository = miscRepository;
        }

        public async Task LogPageView(PageView data) {
            await miscRepository.CreateOneAsync(data);
        }

        public async Task<AboutYouData> GetAboutYouData(string ipAddress)
        {
            // Call integration project to get the data
            ipAddress = "174.89.46.253";
            var apiData = await ipApiIntegration.GetIpInformation(ipAddress);
            // Convert to string and back to object for returning
            var stringify = JsonConvert.SerializeObject(apiData);
            return JsonConvert.DeserializeObject<AboutYouData>(stringify);
        }
    }
}
