using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services.Implementation
{
    public class EmailServiceSendGridImplementation : EmailService
    {
        private string _apiKey { get; set; }
        public EmailServiceSendGridImplementation(string ApiKey)
        {
            _apiKey = ApiKey;
        }
        public async Task<bool> SendEmail()
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("", "");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("", "");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}