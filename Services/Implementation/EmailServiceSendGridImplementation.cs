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
        private string _bookingReceiver { get; set; }
        public EmailServiceSendGridImplementation(string ApiKey, string bookingReceiver)
        {
            _apiKey = ApiKey;
            _bookingReceiver = bookingReceiver;
        }
        public async Task<bool> SendEmail(Booking booking)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_bookingReceiver);
            var subject = "Ny bestilling fra "+booking.BookerEmailAddress;
            var to = new EmailAddress(_bookingReceiver);
            var plainTextContent = $@"Epost: {booking.BookerEmailAddress}
                                              Type ressurs: {booking.ExpertType}
                                              Rolle: {booking.ExpertRole}
                                              Antall {booking.Quantity}
                                              Forventet periode: {booking.TimePeriod}";
                                            //   Beskrivelse av prosjektet: {booking.Description}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
            var response = await client.SendEmailAsync(msg);
            Dictionary<string, dynamic>? responseContent = await response.DeserializeResponseBodyAsync();
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email sent");
            }
            else
            {
                Console.WriteLine("Noe gikk galt: "+ JsonSerializer.Serialize(response));
                foreach(var key in responseContent.Keys)
                {
                    Console.WriteLine(key + " " + responseContent[key]);
                }
            }
            return response.IsSuccessStatusCode;
        }
    }
}