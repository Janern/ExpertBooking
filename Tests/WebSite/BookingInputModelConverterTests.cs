using BusinessModels;
using WebSite.Models;
using WebSite.Helpers;
using Xunit;

namespace Tests.WebSite
{
    public class BookingInputModelConverterTests
    {
        [Fact]
        public void ShouldConvertExpertsJson()
        {
            string expertsJson = "[]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.NotNull(result.Experts);
        }
    }
}