using BusinessModels;
using WebSite.Models;
using WebSite.Helpers;
using Xunit;
using Tests.TestHelpers;

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
        string expert1JsonString = "";
        string expert2JsonString = "";
        public BookingInputModelConverterTests()
        {
            expert1JsonString = "{\"Id\": \""+Expert1.Id+"\","+
                                "\"FirstName\": \""+Expert1.FirstName+"\","+
                                "\"LastName\": \""+Expert1.LastName+"\","+
                                "\"Role\": \""+Expert1.Role+"\","+
                                "\"Technology\": \""+Expert1.Technology+"\","+
                                "\"Description\": \""+Expert1.Description+"\"}";
            expert2JsonString = "{\"Id\": \""+Expert2.Id+"\","+
                                "\"FirstName\": \""+Expert2.FirstName+"\","+
                                "\"LastName\": \""+Expert2.LastName+"\","+
                                "\"Role\": \""+Expert2.Role+"\","+
                                "\"Technology\": \""+Expert2.Technology+"\","+
                                "\"Description\": \""+Expert2.Description+"\"}";
        }
        [Fact]
        public void GivenEmptyExpertsJsonShouldConvertToEmptyArray()
        {
            string expertsJson = "[]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Empty(result.Experts);
        }

        
        [Fact]
        public void GivenOneExpertInExpertsJsonShouldConvertToOneExpertInArray()
        {
            string expertsJson = $"[{expert1JsonString}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Single(result.Experts);
        }
        
        [Fact]
        public void GivenTwoExpertsInExpertsJsonShouldConvertToTwoExpertsInArray()
        {
            string expertsJson = $"[{expert1JsonString}, {expert2JsonString}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Equal(2, result.Experts.Length);
        }

        [Fact]
        public void GivenOneExpertInExpertsJsonShouldConvertFieldsOnExpert()
        {
            string expertsJson = $"[{expert1JsonString}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Equal(Expert1.Id, result.Experts[0].Id);
            Assert.Equal(Expert1.FirstName, result.Experts[0].FirstName);
            Assert.Equal(Expert1.LastName, result.Experts[0].LastName);
            Assert.Equal(Expert1.Technology, result.Experts[0].Technology);
            Assert.Equal(Expert1.Role, result.Experts[0].Role);
            Assert.Equal(Expert1.Description, result.Experts[0].Description);
        }

        [Fact]
        public void GivenTwoExpertsInExpertsJsonShouldConvertFieldsOnAllExperts()
        {
            string expertsJson = $"[{expert1JsonString}, {expert2JsonString}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            ExpertAssertionHelper.AssertContainsExpert(Expert1, result.Experts);
            ExpertAssertionHelper.AssertContainsExpert(Expert2, result.Experts);
        }
    }
}