using Xunit;
using UseCases;
using UseCases.Exceptions;
using Services;
using System.Threading.Tasks;
using BusinessModels;
using Tests.TestHelpers;

namespace Tests.UseCases;
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
        Expert[] experts = new Expert[]{
             new Expert{
                Id = expertId
            }
        };
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { Experts = experts, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expected: expertId, _fakeEmailService.SentBooking.Experts[0].Id);
    }

    [Fact]
    public async Task WhenBookingSingleExpertShouldSendEmailWithExpertName()
    {
        string expertId = "EXPERT1";
        string expertFirstName = "Firstname";
        string expertLastName = "LASTNAME";
        Expert[] experts = new Expert[]{
             new Expert{
                Id = expertId,
                FirstName = expertFirstName,
                LastName = expertLastName
            }
        };
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { Experts = experts, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(expected: expertFirstName, _fakeEmailService.SentBooking.Experts[0].FirstName);
        Assert.Equal(expected: expertLastName, _fakeEmailService.SentBooking.Experts[0].LastName);
    }

    [Fact]
    public async Task WhenBookingTwoExpertsShouldSendEmailWithExpertIds()
    {
        string expertId1 = "EXPERT1";
        string expertId2 = "EXPERT2";
        string expertFirstName1 = "Firstname1";
        string expertFirstName2 = "Firstname2";
        string expertLastName1 = "LASTNAME1";
        string expertLastName2 = "LASTNAME2";
        Expert[] experts = new Expert[]{
            new Expert
            {
                Id = expertId1,
                FirstName = expertFirstName1,
                LastName = expertLastName1
            },
            new Expert
            {
                Id = expertId2,
                FirstName = expertFirstName2,
                LastName = expertLastName2
            }
        };
        string bookerEmailAddress = "booker@example.com";
        Booking booking = new Booking { Experts = experts, BookerEmailAddress = bookerEmailAddress };

        await _useCase.Execute(booking);

        Assert.Equal(experts.Length, _fakeEmailService.SentBooking.Experts.Length);
        ExpertAssertionHelper.AssertContainsExpert(experts[0], _fakeEmailService.SentBooking.Experts);
        ExpertAssertionHelper.AssertContainsExpert(experts[1], _fakeEmailService.SentBooking.Experts);
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