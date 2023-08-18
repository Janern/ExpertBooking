using Xunit;
using UseCases;

namespace tests;

public class BookExpertTests
{
    private BookExpertUseCase _useCase;
    private FakeEmailService _fakeEmailService;
    [Fact]
    public void WhenBookingExpertShouldSendMail()
    {
        _useCase.Execute();

        Assert.True(_fakeEmailService.EmailSent);
    }
    
    private class FakeEmailService
    {
        public bool EmailSent = false;
    }
}
