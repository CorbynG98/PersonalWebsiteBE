using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Constants;

public static class TemplateScripts
{
    public static async Task ManuallyUpdateHtmlContentForEmailTemplate(this IServiceProvider serviceProvider, string htmlContent, EmailTemplateTypes type)
    {
        var templateRepository = serviceProvider.GetService<IEmailTemplateRepository>();
        var template = await templateRepository.GetEmailTemplateByTypeAsync(type);
        template.HtmlContent = htmlContent;
        await templateRepository.UpdateOneAsync(template.Id, template);
    }
}
