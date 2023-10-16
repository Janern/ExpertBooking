using System.Linq;
using BusinessModels;
using WebSite.Helpers;
using WebSite.Models;
using Xunit;

namespace Tests.WebSite
{
    public class ExpertViewModelConverterTests
    {        
        public ExpertViewModelConverterTests()
        {
        }

        [Fact]
        public void GivenEmptyListOfExpertsWhenConvertingShouldReturnEmptyListOfExpertViewModels()
        {
            string[] _ = new string[0];
            Expert[] emptyList = new Expert[0];

            ExpertViewModel[] result = ExpertViewModelConverter.Convert(emptyList, _);

            Assert.Empty(result);
        }

        [Fact]
        public void GivenListOfExpertsWhenConvertingShouldReturnListOfExpertViewModels()
        {
            string[] _ = new string[0];
            Expert[] experts = new Expert[]
            {
                    new Expert { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology" },
                    new Expert { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2" },
                    new Expert { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3" },
                    new Expert { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4" }
            };
            ExpertViewModel[] expected = new ExpertViewModel[]
            {
                new ExpertViewModel { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology" },
                new ExpertViewModel { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2" },
                new ExpertViewModel { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3" },
                new ExpertViewModel { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4" }
            };

            ExpertViewModel[]? actual = ExpertViewModelConverter.Convert(experts, _);

            AssertAreEqual(expected, actual);
        }

        [Fact]
        public void GivenNullSelectedExpertsListWhenConvertingShouldSetSelectedFalse()
        {
            Expert[] experts = new Expert[]
            {
                    new Expert { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology" },
                    new Expert { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2" },
                    new Expert { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3" },
                    new Expert { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4" }
            };
            ExpertViewModel[] expected = new ExpertViewModel[]
            {
                new ExpertViewModel { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology", IsSelected = false },
                new ExpertViewModel { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2", IsSelected = false },
                new ExpertViewModel { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3", IsSelected = false },
                new ExpertViewModel { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4", IsSelected = false }
            };

            ExpertViewModel[] actual = ExpertViewModelConverter.Convert(experts, null);

            AssertAreEqual(expected, actual);
        }

        [Fact]
        public void GivenEmptySelectedExpertsListWhenConvertingShouldSetSelectedFalse()
        {
            Expert[] experts = new Expert[]
            {
                    new Expert { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology" },
                    new Expert { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2" },
                    new Expert { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3" },
                    new Expert { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4" }
            };
            ExpertViewModel[] expected = new ExpertViewModel[]
            {
                new ExpertViewModel { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology", IsSelected = false },
                new ExpertViewModel { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2", IsSelected = false },
                new ExpertViewModel { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3", IsSelected = false },
                new ExpertViewModel { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4", IsSelected = false }
            };

            ExpertViewModel[] actual = ExpertViewModelConverter.Convert(experts, new string[0]);

            AssertAreEqual(expected, actual);
        }
        
        [Fact]
        public void GivenTwoSelectedExpertsWhenConvertingShouldSetSelectedTrue()
        {
            Expert[] experts = new Expert[]
            {
                    new Expert { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology" },
                    new Expert { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2" },
                    new Expert { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3" },
                    new Expert { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4" }
            };
            string[] selectedExperts = new string[]{
                "2", "3"
            };
            ExpertViewModel[] expected = new ExpertViewModel[]
            {
                new ExpertViewModel { Id = "1", FirstName = "FirstName", LastName = "LastName", Description = "Description", Role = "Role", Technology = "Technology", IsSelected = false },
                new ExpertViewModel { Id = "2", FirstName = "FirstName2", LastName = "LastName2", Description = "Description2", Role = "Role2", Technology = "Technology2", IsSelected = true },
                new ExpertViewModel { Id = "3", FirstName = "FirstName3", LastName = "LastName3", Description = "Description3", Role = "Role3", Technology = "Technology3", IsSelected = true },
                new ExpertViewModel { Id = "4", FirstName = "FirstName4", LastName = "LastName4", Description = "Description4", Role = "Role4", Technology = "Technology4", IsSelected = false }
            };

            ExpertViewModel[] actual = ExpertViewModelConverter.Convert(experts, selectedExperts);

            AssertAreEqual(expected, actual);
        }

        private void AssertAreEqual(ExpertViewModel[] expected, ExpertViewModel[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            foreach(ExpertViewModel expectedItem in expected)
            {
                ExpertViewModel? actualItem = actual.FirstOrDefault(x => x.Id == expectedItem.Id);
                Assert.NotNull(actualItem);
                Assert.Equal(expectedItem.Id, actualItem.Id);
                Assert.Equal(expectedItem.FirstName, actualItem.FirstName);
                Assert.Equal(expectedItem.LastName, actualItem.LastName);
                Assert.Equal(expectedItem.Description, actualItem.Description);
                Assert.Equal(expectedItem.Role, actualItem.Role);
                Assert.Equal(expectedItem.Technology, actualItem.Technology);
                Assert.Equal(expectedItem.IsSelected, actualItem.IsSelected);
            }
        }
    }
}