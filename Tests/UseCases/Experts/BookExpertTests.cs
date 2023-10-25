using Xunit;
using UseCases.Exceptions;
using System.Threading.Tasks;
using BusinessModels;
using Tests.TestHelpers;
using UseCases.Experts;
using UseCases.Email;

namespace Tests.UseCases.Experts;
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
        string bookerEmailAddress = "test@example.com";
        Booking booking = new Booking { BookerEmailAddress = bookerEmailAddress };
        await _useCase.Execute(booking);

        Assert.NotNull(_fakeEmailService.SentBooking);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailToBookerEmailAddress()
    {
        string bookerEmailAddress = "test@example.com";
        Booking booking = new Booking { BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(bookerEmailAddress, _fakeEmailService.SentBooking.BookerEmailAddress);
    }

    [Fact]
    public async Task WhenBookingSingleExpertShouldSendEmailWithExpertId()
    {
        string expertId = "EXPERT1";
        string[] expertIds = new string[]{expertId};
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { ExpertIds = expertIds, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expected: expertId, _fakeEmailService.SentBooking.ExpertIds[0]);
    }

    [Fact]
    public async Task WhenBookingTwoExpertsShouldSendEmailWithExpertIds()
    {
        string expertId1 = "EXPERT1";
        string expertId2 = "EXPERT2";
        string[] expertIds = new string[]{expertId1, expertId2};
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { ExpertIds = expertIds, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expertIds.Length, _fakeEmailService.SentBooking.ExpertIds.Length);
        Assert.Contains(expertId1, _fakeEmailService.SentBooking.ExpertIds);
        Assert.Contains(expertId2, _fakeEmailService.SentBooking.ExpertIds);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithTimePeriod()
    {
        string timePeriod = "2023-09-01 2023-09-30";
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { TimePeriod = timePeriod, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expected: timePeriod, _fakeEmailService.SentBooking.TimePeriod);
    }

    [Fact]
    public async Task WhenBookingExpertShouldSendEmailWithDescription()
    {
        string description = "Beskrivelse av problemet";
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { Description = description, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expected: description, _fakeEmailService.SentBooking.Description);
    }

    [Fact]
    public async Task GivenNoEmailWhenBookingExpertShouldThrowException()
    {
        string description = "Beskrivelse av problemet";
        Booking booking = new Booking { Description = description };

        await Assert.ThrowsAsync<InvalidBookingException>(() => _useCase.Execute(booking));
    }

    private class FakeEmailService : EmailService
    {
        public Booking SentBooking = null;

        public async Task<bool> SendEmail(Booking booking)
        {
            SentBooking = booking;
            await Task.CompletedTask;
            return true;
        }
    }
}