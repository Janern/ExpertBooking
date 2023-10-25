using BusinessModels;
using WebSite.Models;
using WebSite.Helpers;
using Xunit;
using System.Collections.Generic;

namespace Tests.WebSite
{
    public class BookingInputModelConverterTests
    {
        Expert Expert1 = new Expert
        {
            Id="Jan Erik Dehli",
            FirstName="Jan Erik",
            LastName="Dehli",
            Role="Seniorkonsulent",
            Technology=".Net",
            Description="Jan Erik er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden."
        };
        Expert Expert2 = new Expert
        {
            Id="Jan Erik Dehli2",
            FirstName="Jan Erik2",
            LastName="Dehli2",
            Role="Seniorkonsulent2",
            Technology=".Net2",
            Description="Jan Erik 2er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden."
        };
        public BookingInputModelConverterTests()
        {
        }
        [Fact]
        public void GivenEmptyExpertIdsShouldConvertToEmptyArray()
        {
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{SelectedExpertIds=new System.Collections.Generic.List<string>()});

            Assert.Empty(result.ExpertIds);
        }

        
        [Fact]
        public void GivenOneIdInExpertIdsShouldConvertToOneExpertInArray()
        {
            List<string> expertIds = new List<string>{Expert1.Id};
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{SelectedExpertIds=expertIds});

            Assert.Single(result.ExpertIds);
        }
        
        [Fact]
        public void GivenTwoIdsInExpertIdsShouldConvertToTwoExpertsInArray()
        {
            List<string> expertIds = new List<string>{Expert1.Id, Expert2.Id};
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{SelectedExpertIds=expertIds});

            Assert.Equal(2, result.ExpertIds.Length);
        }

        [Fact]
        public void GivenOneExpertIdInExpertIdsShouldAddExpertIdToBooking()
        {
            List<string> expertIds = new List<string>{Expert1.Id};
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{SelectedExpertIds=expertIds});

            Assert.Equal(Expert1.Id, result.ExpertIds[0]);
        }

        [Fact]
        public void GivenTwoExpertIdsInExpertIdsShouldConvertFieldsOnExperts()
        {
            List<string> expertIds = new List<string>{Expert1.Id, Expert2.Id};
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{SelectedExpertIds=expertIds});

            Assert.Contains(Expert1.Id, result.ExpertIds);
            Assert.Contains(Expert2.Id, result.ExpertIds);
        }
        [Fact]
        public void ShouldConvertFieldsOnBooking()
        {
            List<string> expertIds = new List<string>();
            Booking expectedBooking = new Booking{
                BookerEmailAddress = "EMAIL@EMAil.com",
                TimePeriod = "2020-10-8 20:20:20",
                Description = "DESCRIPTION"
            };
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel
            {
                SelectedExpertIds = expertIds, 
                BookerEmailAddress=expectedBooking.BookerEmailAddress, 
                Description=expectedBooking.Description, 
                TimePeriod = expectedBooking.TimePeriod
            });

            Assert.Equal(expectedBooking.BookerEmailAddress, result.BookerEmailAddress);
            Assert.Equal(expectedBooking.TimePeriod, result.TimePeriod);
            Assert.Equal(expectedBooking.Description, result.Description);
        }
    }
}