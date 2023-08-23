using BusinessModels;
namespace Services;
public interface EmailService
{
    Task<bool> SendEmail(Booking booking);
}