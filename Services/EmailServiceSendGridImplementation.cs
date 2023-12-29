using System.Text.Json;
using BusinessModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using UseCases.Email;
using UseCases.Experts;

namespace Services;
public class EmailServiceSendGridImplementation : EmailService
{
    private string _apiKey { get; set; }
    private string _bookingReceiver { get; set; }

    private readonly ExpertsStorage _expertsStorage;

    public EmailServiceSendGridImplementation(EmailServiceConfiguration config, ExpertsStorage expertsStorage)
    {
        _expertsStorage = expertsStorage;
        _apiKey = config.ApiKey;
        _bookingReceiver = config.ReceiverAddress;
    }   
    public async Task<bool> SendEmail(Booking booking)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_bookingReceiver);
        var subject = "Ny bestilling fra "+booking.BookerEmailAddress;
        var to = new EmailAddress(_bookingReceiver);
        string expertsText = GenerateExpertsText(booking.ExpertIds);
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

    private string GenerateExpertsText(string[] expertIds){
        if(expertIds == null || expertIds.Length == 0)
            return "Ingen valgt";
        var experts = _expertsStorage.GetExperts("").Where(e => expertIds.Contains(e.Id));
        string result = "";
        foreach(var expert in experts )
        {
            result += $"Navn: {expert.FirstName} {expert.LastName}\r\n";
        }
        return result;
    }
}