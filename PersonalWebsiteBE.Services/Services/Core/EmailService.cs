using Microsoft.Extensions.Options;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Settings;
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
        private readonly ISendGridSettings sendGridSettings;
        // Helpers
        private readonly SendGridClient SendGridClient;
        private readonly string ToEmailAddress = "corbyn.greenwood.98+personal@gmail.com";
        private readonly string ToFullname = "Corbyn Greenwood";

        public EmailService(IEmailRepository emailRepository, ISendGridSettings sendGridSettings)
        {
            this.emailRepository = emailRepository;
            this.sendGridSettings = sendGridSettings;
            this.SendGridClient = new SendGridClient(sendGridSettings.ApiKey);
        }

        public async Task<bool> SendEmail(Email emailData) {
            // Create to and from objects
            var from = new EmailAddress(emailData.From, emailData.Fullname);
            var to = new EmailAddress(ToEmailAddress, ToFullname);
            // Create the email and send it
            var msg = MailHelper.CreateSingleEmail(from, to, emailData.Subject, emailData.Content, emailData.Content);
            var response = await SendGridClient.SendEmailAsync(msg);
            // Save email send attempt to database :)
            emailData.Status = response.StatusCode.ToString();
            emailData.StatusMessage = await response.Body.ReadAsStringAsync();
            await emailRepository.CreateOneAsync(emailData);
            // Check response from sendgrid to see if successful
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
