using System.Text.Json;
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
            string expertsText = GenerateExpertsText(booking.Experts);
            var plainTextContent = $"Epost: {booking.BookerEmailAddress}\r\n\r\n" +
                                            "Experter: \r\n"+
                                           $"{expertsText}\r\n"+
                                           $"Forventet periode: {booking.TimePeriod}\r\n\r\n"+
                                           $"Beskrivelse av prosjektet: \r\n{booking.Description}";
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

        private string GenerateExpertsText(Expert[] experts){
            string result = "";
            if(experts == null) return result;
            
            foreach(var expert in experts )
            {
                result += $"Navn: {expert.FirstName} {expert.LastName}\r\n";
            }
            return result;
        }
    }
}