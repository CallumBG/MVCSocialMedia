using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace MVCSocialMedia.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        public ConfigSettings Settings { get; } //Set with Secret Manager.

        public EmailSender(IOptions<ConfigSettings> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Settings = optionsAccessor.Value;
            _logger = logger;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Settings.ApiKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Settings.ApiKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("callumbusuttilgoodfellow@gmail.com", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
