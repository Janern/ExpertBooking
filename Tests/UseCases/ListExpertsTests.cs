using Xunit;
using BusinessModels;
using UseCases;

namespace Tests;
public class ListExpertsTests
{
    private ListExpertsUseCase _useCase;

    public ListExpertsTests()
    {
        _useCase = new ListExpertsUseCase();
    }

    [Fact]
    public void GivenNoExpertsWhenListingExpertsShouldReturnNoExperts()
    {
        Expert[] existingExperts = new Expert[]
        {
        };

        Expert[] actualExperts = _useCase.Execute();

        Assert.Equal(actualExperts.Length, existingExperts.Length);
    }
}