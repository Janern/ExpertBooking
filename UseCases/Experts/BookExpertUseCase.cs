using BusinessModels;
using UseCases.Email;
using UseCases.Exceptions;

namespace UseCases.Experts;
public class BookExpertUseCase
{
    private EmailService _emailService { get; set; }

    public BookExpertUseCase(EmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<bool> Execute(Booking booking)
    {
        if (string.IsNullOrWhiteSpace(booking.BookerEmailAddress))
            throw new InvalidBookingException("BookerEmailAddress is null or whitespace");
        return await _emailService.SendEmail(booking);
    }
}