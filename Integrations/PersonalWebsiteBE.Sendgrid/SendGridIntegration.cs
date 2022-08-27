using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Core.Integrations;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PersonalWebsiteBE.SendGrid
{
    public class SendGridIntegration : ISendGridIntegration
    {
        private ISendGridSettings sendGridConfig;
        private string PlainTextFallback = "This message could not be viewed at this time. Try again later.";

        public SendGridIntegration(ISendGridSettings sendGridConfig)
        {
            this.sendGridConfig = sendGridConfig;
        }

        public async Task<Response> SendEmail(string to, string toName, string from, string fromName, string htmlContent, string subject)
        {
            // Make client
            SendGridClient client = new SendGridClient(sendGridConfig.ApiKey);
            // Make To and From objects
            var fromAddress = new EmailAddress("no-reply@corbyngreenwood.com", fromName);
            var toAddress = new EmailAddress(to, toName);
            // Create the message
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, PlainTextFallback, htmlContent);
            message.ReplyTo = new EmailAddress(from, fromName);
            return await client.SendEmailAsync(message);
        }
    }
}