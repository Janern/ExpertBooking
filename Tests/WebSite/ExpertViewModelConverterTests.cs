using System.Collections.Generic;
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
        
        public class ConvertListTests : ExpertViewModelConverterTests
        {
            [Fact]
            public void GivenEmptyListOfExpertsWhenConvertingShouldReturnEmptyListOfExpertViewModels()
            {
                List<string> _ = new List<string>();
                Expert[] emptyList = new Expert[0];

                ExpertViewModel[] result = ExpertViewModelConverter.Convert(emptyList, _);

                Assert.Empty(result);
            }

            [Fact]
            public void GivenListOfExpertsWhenConvertingShouldReturnListOfExpertViewModels()
            {
                List<string> _ = new List<string>();
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

                ExpertViewModel[] actual = ExpertViewModelConverter.Convert(experts, new List<string>());

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
                List<string> selectedExperts = new List<string>{
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
        }
        
        public class ConvertSingleTests : ExpertViewModelConverterTests
        {
            [Fact]
            public void GivenNullSelectedExpertsWhenConvertingShouldSetIsSelectedToFalse()
            {
                Expert inputExpert = new Expert
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription"
                };
                ExpertViewModel expectedViewModel = new ExpertViewModel
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription",
                    IsSelected = false
                };

                ExpertViewModel actual = ExpertViewModelConverter.Convert(inputExpert, null);

                AssertAreEqual(expectedViewModel, actual);
            }
            
            [Fact]
            public void GivenNotSameSelectedExpertsWhenConvertingShouldSetIsSelectedToFalse()
            {
                Expert inputExpert = new Expert
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription"
                };
                ExpertViewModel expectedViewModel = new ExpertViewModel
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription",
                    IsSelected = false
                };

                ExpertViewModel actual = ExpertViewModelConverter.Convert(inputExpert, new List<string>{"OTHERSTRING"});

                AssertAreEqual(expectedViewModel, actual);
            }

            [Fact]
            public void GivenSameSelectedExpertsWhenConvertingShouldSetIsSelectedToFalse()
            {
                Expert inputExpert = new Expert
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription"
                };
                ExpertViewModel expectedViewModel = new ExpertViewModel
                {
                    Id = "ID",
                    FirstName = "SomeFirstName",
                    LastName = "SomeLastName",
                    Role = "SomeRole",
                    Technology = "SomeTechnology",
                    Description = "SomeDescription",
                    IsSelected = true
                };

                ExpertViewModel actual = ExpertViewModelConverter.Convert(inputExpert, new List<string>{"OTHERSTRING", "ALSO OTHER STRING", inputExpert.Id});

                AssertAreEqual(expectedViewModel, actual);
            }
        }
        private void AssertAreEqual(ExpertViewModel[] expected, ExpertViewModel[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            foreach(ExpertViewModel expectedItem in expected)
            {
                ExpertViewModel? actualItem = actual.FirstOrDefault(x => x.Id == expectedItem.Id);
                Assert.NotNull(actualItem);
                AssertAreEqual(expectedItem, actualItem);
            }
        }

        private void AssertAreEqual(ExpertViewModel expected, ExpertViewModel actual)
        {
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.Role, actual.Role);
                Assert.Equal(expected.Technology, actual.Technology);
                Assert.Equal(expected.IsSelected, actual.IsSelected);
        }
    }
}