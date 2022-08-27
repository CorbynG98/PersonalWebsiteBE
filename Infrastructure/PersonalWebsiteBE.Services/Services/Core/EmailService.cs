using Microsoft.Extensions.Options;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Core.Static;
using PersonalWebsiteBE.SendGrid;
using PersonalWebsiteBE.Services.Helpers;
using PersonalWebsiteBE.Services.Helpers.GoogleCloud;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Services.Services.Core
{
    public class EmailService : IEmailService
    {
        // Injected stuff
        private readonly IEmailRepository emailRepository;
        private readonly IEmailTemplateRepository emailTemplateRepository;
        // Helpers
        private readonly GoogleStorageHelper cloudStorage;
        private readonly SendGridIntegration sendGridIntegration;
        private readonly string ToEmailAddress = "corbyn.greenwood.98+personal@gmail.com";
        private readonly string ToFullname = "Corbyn Greenwood";

        public EmailService(IEmailRepository emailRepository, IEmailTemplateRepository emailTemplateRepository, ISendGridSettings sendGridSettings,
            IFireStoreSettings fireStoreSettings)
        {
            this.emailRepository = emailRepository;
            this.emailTemplateRepository = emailTemplateRepository;
            this.sendGridIntegration = new SendGridIntegration(sendGridSettings);
            this.cloudStorage = new GoogleStorageHelper(fireStoreSettings);
        }

        public async Task<bool> CreateNewMessageEmail(EmailLog email)
        {
            var template = await emailTemplateRepository.GetEmailTemplateByTypeAsync(EmailTemplateTypes.NewMessage);
            // Generate subject line
            var subject = template.SubjectLine
                .Replace("{{name}}", email.FromName);
            // Generate content
            var htmlContent = template.HtmlContent
                .Replace("{{name}}", email.FromName)
                .Replace("{{content}}", email.Content);

            // Save Html content to cloud storage
            var fileName = $"new-message/{email.From}-passwordreset-{Guid.NewGuid().ToString("N")}.zip";
            // var htmlContent = GZip.Compress(htmlContent.A);
            var htmlContentStorageLink = cloudStorage.UploadFileToBucket(GZip.Zip(htmlContent), GoogleCloudConstants.EmailContentLogBucketName, fileName);

            // Create an EmailLog for this
            var newEmailLog = new EmailLog()
            {
                Content = htmlContentStorageLink,
                Subject = subject,
                To = ToEmailAddress,
                ToName = ToFullname,
                From = email.From,
                FromName = email.FromName
            };
            var newLogId = await emailRepository.CreateOneAsync(newEmailLog);

            // Send the email
            var result = await sendGridIntegration.SendEmail(newEmailLog.To, newEmailLog.ToName, newEmailLog.From, newEmailLog.FromName, htmlContent, subject);

            // Update log to show above result incase of failure
            newEmailLog.HasFailed = result.IsSuccessStatusCode;
            newEmailLog.FailReason = result.IsSuccessStatusCode ? null : await result.Body.ReadAsStringAsync();
            await emailRepository.UpdateOneAsync(newLogId, newEmailLog);

            return result.IsSuccessStatusCode;
        }
    }
}
