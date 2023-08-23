using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessModels;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services.Implementation
{
    public class EmailServiceSendGridImplementation : EmailService
    {
        private string _apiKey { get; set; }
        private string _fromAddress { get; set; }
        public EmailServiceSendGridImplementation(string ApiKey, string fromAddress)
        {
            _apiKey = ApiKey;
            _fromAddress = fromAddress;
        }
        public async Task<bool> SendEmail(Booking booking)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromAddress);
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(booking.BookerEmailAddress);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Dictionary<string, dynamic>? responseContent = await response.DeserializeResponseBodyAsync();
            if(responseContent?.Keys != null)
            {
                foreach(var key in responseContent.Keys)
                {
                    Console.WriteLine(key + " " + responseContent[key]);
                }
                Console.WriteLine("Email sent");
            }
            else
            {
                Console.WriteLine("Noe gikk galt: "+ JsonSerializer.Serialize(response));
            }
            return response.IsSuccessStatusCode;
        }
    }
}