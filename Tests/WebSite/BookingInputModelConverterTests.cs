using BusinessModels;
using WebSite.Models;
using WebSite.Helpers;
using Xunit;

namespace Tests.WebSite
{
    public class BookingInputModelConverterTests
    {
        
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
            string ExpertJsonString1 = "{\"Id\": \"User1\",\"FirstName\": \"Jan Erik\",\"LastName\": \"Dehli\",\"Role\": \"Seniorkonsulent\",\"Technology\": \".Net\",\"Description\": \"Jan Erik er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden.\"}";
            string expertsJson = $"[{ExpertJsonString1}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Single(result.Experts);
        }
        
        [Fact]
        public void GivenTwoExpertsInExpertsJsonShouldConvertToTwoExpertsInArray()
        {
            string ExpertJsonString1 = "{\"Id\": \"User1\",\"FirstName\": \"Jan Erik\",\"LastName\": \"Dehli\",\"Role\": \"Seniorkonsulent\",\"Technology\": \".Net\",\"Description\": \"Jan Erik er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden.\"}";
            string ExpertJsonString2 = "{\"Id\": \"User2\",\"FirstName\": \"Jan Erik\",\"LastName\": \"Dehli\",\"Role\": \"Seniorkonsulent\",\"Technology\": \".Net\",\"Description\": \"Jan Erik er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden.\"}";
            string expertsJson = $"[{ExpertJsonString1}, {ExpertJsonString2}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Equal(2, result.Experts.Length);
        }
        [Fact]
        public void GivenOneExpertInExpertsJsonShouldConvertIdOfExpert()
        {
            string expertID = "EXPERTID";
            string ExpertJsonString1 = "{\"Id\": \""+expertID+"\",\"FirstName\": \"Jan Erik\",\"LastName\": \"Dehli\",\"Role\": \"Seniorkonsulent\",\"Technology\": \".Net\",\"Description\": \"Jan Erik er en senior .Net-utvikler med erfaring fra hele stacken, og har over ti \u00E5rs erfaring med C#. Han har jobbet med databaseteknologier som MSSQL, PostgreSQL og Mongo, og frontendteknologier som HTML, JavaScript, CSS og Vue.Han arbeider alltid for \u00E5 ha en god dialog med kunde og tar ansvar for at arkitekturen st\u00F8tter behovene kunden har n\u00E5 og i fremtiden.\"}";
            string expertsJson = $"[{ExpertJsonString1}]";
            
            Booking result = BookingInputModelConverter.Convert(new BookingInputModel{ExpertsJson=expertsJson});

            Assert.Equal(expertID, result.Experts[0].Id);
        }
    }
}