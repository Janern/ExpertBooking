using BusinessModels;
using UseCases.Email;

namespace Services;

public class EmailServiceConsoleLogImplementation : EmailService
{
    public async Task<bool> SendEmail(Booking booking)
    {
        Console.WriteLine("Email sent to " + booking.BookerEmailAddress);
        await Task.CompletedTask;
        return true;
    }
}