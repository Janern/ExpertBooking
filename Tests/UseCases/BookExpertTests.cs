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

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithTypeOfExpert()
    {
        string expertType = ".Net";
        Booking booking = new Booking{ExpertType=expertType};

        await _useCase.Execute(booking);

        Assert.Equal(expertType, _fakeEmailService.SentEmail.ExpertType);
    }
    
    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithRoleOfExpert()
    {
        string expertRole = "Prosjektleder";
        Booking booking = new Booking{ExpertRole=expertRole};

        await _useCase.Execute(booking);

        Assert.Equal(expected: expertRole, _fakeEmailService.SentEmail.ExpertRole);
    }
    
    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithTimePeriod()
    {
        string timePeriod = "2023-09-01 2023-09-30";
        Booking booking = new Booking{TimePeriod=timePeriod};

        await _useCase.Execute(booking);

        Assert.Equal(expected: timePeriod, _fakeEmailService.SentEmail.TimePeriod);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithQuantity()
    {
        int quantity = 2;
        Booking booking = new Booking{Quantity=quantity};

        await _useCase.Execute(booking);

        Assert.Equal(expected: quantity, _fakeEmailService.SentEmail.Quantity);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithDescription()
    {
        string description = "Beskrivelse av problemet";
        Booking booking = new Booking{Description=description};

        await _useCase.Execute(booking);

        Assert.Equal(expected: description, _fakeEmailService.SentEmail.Description);
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