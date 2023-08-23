using Xunit;
using UseCases;
using Services;
using System.Threading.Tasks;

namespace tests;

public class BookExpertTests
{
    private BookExpertUseCase _useCase;
    private FakeEmailService _fakeEmailService;

    public BookExpertTests()
    {
        _fakeEmailService = new FakeEmailService();
        _useCase = new BookExpertUseCase(_fakeEmailService);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendMail()
    {
        await _useCase.Execute();

        Assert.True(_fakeEmailService.EmailSent);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendMailToReceiver()
    {
        await _useCase.Execute();

        Assert.True(_fakeEmailService.EmailSent);
    }
    
    private class FakeEmailService : EmailService
    {
        public EmailRequest SentEmail = null;

        async Task<bool> EmailService.SendEmail(EmailRequest sentEmail)
        {
            SentEmail = sentEmail;
            await Task.CompletedTask;
            return true;
        }
    }
}