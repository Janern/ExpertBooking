using Xunit;
using BusinessModels;
using UseCases;
using Storage;
using Storage.Api;
using Storage.Implementation;
using System.Runtime.CompilerServices;

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
}