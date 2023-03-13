using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Integrations;
using RestSharp;

namespace PersonalWebsiteBE.IpApi
{
    public class IpApiIntegration : IIpApiIntegration
    {
        public async Task<IpApiData?> GetIpInformation(string ip)
        {
            if (ip == "0.0.0.1" || ip == "127.0.0.1" || ip == "localhost")
            {
                return new IpApiData()
                {
                    CountryCode = "localhost",
                    Country = "localhost",
                    RegionName = "localhost",
                    Lat = 0,
                    Lon = 0,
                    City = "localhost",
                };
            }
            var client = new RestClient(@"http://ip-api.com/");
            var request = new RestRequest($"json/{ip}");
            var response = await client.GetAsync(request);
            if (response.IsSuccessful && response.Content != null)
                return JsonConvert.DeserializeObject<IpApiData>(response.Content);
            return null;
        }
    }
}