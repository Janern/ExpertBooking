using Xunit;
using UseCases;
using Services;
using System.Threading.Tasks;
using BusinessModels;

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
    public async Task WhenBookingExpertShouldSendEmail()
    {
        await _useCase.Execute(new Booking());

        Assert.NotNull(_fakeEmailService.SentEmail);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailToBookerEmailAddress()
    {
        string bookerEmailAddress = "test@example.com";
        Booking booking = new Booking{BookerEmailAddress = bookerEmailAddress};

        await _useCase.Execute(booking);

        Assert.Equal(bookerEmailAddress, _fakeEmailService.SentEmail.BookerEmailAddress);
    }
    
    private class FakeEmailService : EmailService
    {
        public Booking SentEmail = null;

        public async Task<bool> SendEmail(Booking booking)
        {
            SentEmail = booking;
            await Task.CompletedTask;
            return true;
        }
    }
}

public class EmailRequest
{
}