using System.Threading.Tasks;
using SendGrid;

namespace PersonalWebsiteBE.Core.Integrations
{
    public interface ISendGridIntegration
    {
        Task<Response> SendEmail(string to, string toName, string from, string fromName, string htmlContent, string subject);
    }
}
