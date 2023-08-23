using Services;
using BusinessModels;

namespace UseCases;
public class BookExpertUseCase
{
    private EmailService _emailService { get; set; }

    public BookExpertUseCase(EmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<bool> Execute(Booking booking){
        return await _emailService.SendEmail(booking);
    }
}