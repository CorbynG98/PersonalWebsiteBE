using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Integrations;
using RestSharp;
using SendGrid;

namespace PersonalWebsiteBE.IpApi
{
    public class IpApiIntegration : IIpApiIntegration
    {
        public async Task<IpApiData?> GetIpInformation(string ip)
        {
            var client = new RestClient(@"http://ip-api.com/");
            var request = new RestRequest($"json/{ip}");
            var response = await client.GetAsync(request);
            if (response.IsSuccessful && response.Content != null)
                return JsonConvert.DeserializeObject<IpApiData>(response.Content);
            return null;
        }
    }
}