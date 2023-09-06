using Xunit;
using BusinessModels;
using UseCases;
using Storage;
using Storage.Api;
using Storage.Implementation;

namespace Tests;
public class ListExpertsTests
{
    private ListExpertsUseCase _useCase;
    private ExpertsStorage _storage;
    private Expert[] _existingExperts;

    public ListExpertsTests()
    {
        _existingExperts = new Expert[0];
        SetUpUseCase();
    }

    private void SetUpUseCase()
    {
        _storage = new ExpertsStorageInMemoryImplementation(_existingExperts);
        _useCase = new ListExpertsUseCase(_storage);
    }

    [Fact]
    public void GivenNoExpertsWhenListingExpertsShouldReturnNoExperts()
    {
        Expert[] actualExperts = _useCase.Execute();

        Assert.Equal(_existingExperts.Length, actualExperts.Length);
    }

    [Fact]
    public void GivenExistingExpertsWhenListingAllExpertsShouldReturnExperts()
    {
        _existingExperts = new Expert[]
        {
            new Expert(),
            new Expert()
        };
        SetUpUseCase();

        Expert[] actualExperts = _useCase.Execute();

        Assert.Equal(_existingExperts.Length, actualExperts.Length);
    }

    [Fact]
    public void GivenExistingExpertsWhenListingAllExpertsShouldReturnExpertsWithFields()
    {
        Expert existingExpert = new Expert
            {
                Id="ID1",
                FirstName="FirstName1",
                LastName="LastName1",
                Role="Role1",
                Technology="Technology1",
                Description="Description1"
            };
        _existingExperts = new Expert[]
        {
            existingExpert
        };
        SetUpUseCase();

        Expert actualExpert = _useCase.Execute()[0];

        Assert.Equal(existingExpert.Id, actualExpert.Id);
        Assert.Equal(existingExpert.FirstName, actualExpert.FirstName);
        Assert.Equal(existingExpert.LastName, actualExpert.LastName);
        Assert.Equal(existingExpert.Role, actualExpert.Role);
        Assert.Equal(existingExpert.Technology, actualExpert.Technology);
        Assert.Equal(existingExpert.Description, actualExpert.Description);
    }
}