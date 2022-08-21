using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Repositories.Core
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
        Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateTypes type);
    }
}
