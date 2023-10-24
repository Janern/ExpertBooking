using BusinessModels;
namespace UseCases.Email;
public interface EmailService
{
    Task<bool> SendEmail(Booking booking);
}