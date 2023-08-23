using Services;

namespace UseCases;
public class BookExpertUseCase
{
    private EmailService _emailService { get; set; }

    public BookExpertUseCase(EmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<bool> Execute(){
        return await _emailService.SendEmail();
    }
}