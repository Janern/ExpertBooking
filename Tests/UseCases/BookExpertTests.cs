using Xunit;
using UseCases;
using Services;

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
    public void WhenBookingExpertShouldSendMail()
    {
        _useCase.Execute();

        Assert.True(_fakeEmailService.EmailSent);
    }
    
    private class FakeEmailService : EmailService
    {
        public bool EmailSent = false;

        public void SendEmail()
        {
            EmailSent = true;
        }
    }
}
