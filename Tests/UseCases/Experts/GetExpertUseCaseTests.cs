using BusinessModels;
using Storage.Api;
using Storage.Implementation;
using Tests.TestHelpers;
using UseCases.Experts;
using Xunit;

namespace Tests.UseCases.Experts;

public class GetExpertUseCaseTests
{
    private GetExpertUseCase _useCase;
    private ExpertsStorage _storage;
    private Expert[] _experts {get;set;}
    public GetExpertUseCaseTests()
    {
        _experts = new Expert[]{};
        SetupUseCase();
    }

    private void SetupUseCase()
    {
        _storage = new ExpertsStorageInMemoryImplementation(_experts);
        _useCase = new GetExpertUseCase(_storage);
    }
    
    [Fact]
    public void GivenNoIdShouldGetNull()
    {
        Expert actual = _useCase.Execute(null);

        Assert.Null(actual);
    }

    [Fact]
    public void GivenNoExpertsShouldGetNull()
    {
        Expert actual = _useCase.Execute("ID");

        Assert.Null(actual);
    }

    
    [Fact]
    public void GivenTwoExpertsWhenGettingWrongIdShouldGetNull()
    {
        _experts = new Expert[]{new Expert{Id = "ID"}, new Expert{Id = "ID2"}};
        SetupUseCase();
        
        Expert actual = _useCase.Execute("WRONGID");

        Assert.Null(actual);
    }

    [Fact]
    public void GivenTwoExpertsWhenGettingRightIdShouldGetNotNull()
    {
        string Id = "ID";
        _experts = new Expert[]{new Expert{Id = Id}, new Expert{Id = "WRONGID"}};
        SetupUseCase();
        
        Expert actual = _useCase.Execute(Id);

        Assert.NotNull(actual);
    }
    
    [Fact]
    public void GivenTwoExpertsWhenGettingRightIdShouldGetAllFields()
    {
        string Id = "ID";
        Expert expected = new Expert
        {
            Id = "ID",
            FirstName = "FirstName",
            LastName = "LastName",
            Role = "Role",
            Technology = "Technology",
            Description = "Description"
        };
        _experts = new Expert[]{expected, new Expert{Id = "WRONGID"}};
        SetupUseCase();
        
        Expert actual = _useCase.Execute(Id);

        ExpertAssertionHelper.AssertAreEqual(expected, actual);
    }
}
