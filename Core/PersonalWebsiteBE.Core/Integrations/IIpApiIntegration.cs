using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using SendGrid;

namespace PersonalWebsiteBE.Core.Integrations
{
    public interface IIpApiIntegration
    {
        Task<IpApiData> GetIpInformation(string ip);
    }
}
