using Xunit;
using BusinessModels;
using Storage.Api;
using Storage.Implementation;
using System.Linq;
using Tests.TestHelpers;
using UseCases.Experts;

namespace Tests.UseCases.Experts;
public class ListExpertsTests
{
    private ListExpertsUseCase _useCase;
    private ExpertsStorage _storage;
    private BusinessModels.Expert[] _existingExperts;

    public ListExpertsTests()
    {
        _existingExperts = new BusinessModels.Expert[0];
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
            new Expert{Id="ID"},
            new Expert{Id="ID2"}
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
            Id = "ID1",
            FirstName = "FirstName1",
            LastName = "LastName1",
            Role = "Role1",
            Technology = "Technology1",
            Description = "Description1"
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

    [Fact]
    public void GivenExistingExpertsWhenFilteringExpertsOnTechnologyShouldReturnMatchingExperts()
    {
        string expectedTechnology = ".NET";
        Expert[] expectedExperts = new Expert[]{
            new Expert
            {
                Id="ID1",
                FirstName="FirstName1",
                LastName="LastName1",
                Role="Role1",
                Technology=expectedTechnology+", HTML",
                Description="Description1"
            },
            new Expert
            {
                Id="ID2",
                FirstName="FirstName2",
                LastName="LastName2",
                Role="Role2",
                Technology=expectedTechnology+", JavaScript",
                Description="Description2"
            },
            new Expert
            {
                Id="ID3",
                FirstName="FirstName3",
                LastName="LastName3",
                Role="Role3",
                Technology=expectedTechnology+", Java",
                Description="Description3"
            }
        };
        Expert[] otherExperts = new Expert[]
        {
            new Expert
            {
                Id="ID4",
                FirstName="FirstName4",
                LastName="LastName4",
                Role="Role4",
                Technology="Java, Javascript",
                Description="Description4"
            },
            new Expert
            {
                Id="ID5",
                FirstName="FirstName5",
                LastName="LastName5",
                Role="Role5",
                Technology="Python, HTML",
                Description="Description5"
            },
            new Expert
            {
                Id="ID6",
                FirstName="FirstName6",
                LastName="LastName6",
                Role="Role6",
                Technology="Javascript, CSS",
                Description="Description6"
            }
        };
        _existingExperts = expectedExperts.Concat(otherExperts).ToArray();
        SetUpUseCase();

        Expert[] actualExperts = _useCase.Execute(expectedTechnology);

        Assert.Equal(expectedExperts.Length, actualExperts.Length);
        foreach (var expert in expectedExperts)
        {
            ExpertAssertionHelper.AssertContainsExpert(expert, actualExperts);
        };
    }
}