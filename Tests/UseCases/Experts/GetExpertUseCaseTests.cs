using BusinessModels;
using Storage;
using Tests.TestHelpers;
using UseCases.Experts;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UseCases.Experts;

public class GetExpertUseCaseTests : ExpertTests
{
    private GetExpertUseCase _useCase;
    private ExpertsStorage _storage;
    public GetExpertUseCaseTests()
    {
        _storage = new ExpertStorageSqliteImplementation(_sqlite);
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
        _existingExperts.Add(new Expert{Id = "ID", Description = "", FirstName = "", LastName = "", Role = "", Technology = ""});
        _existingExperts.Add(new Expert{Id = "ID2", Description = "", FirstName = "", LastName = "", Role = "", Technology = ""});
        SetUpTestDB();
        
        Expert actual = _useCase.Execute("WRONGID");

        Assert.Null(actual);
    }

    [Fact]
    public void GivenTwoExpertsWhenGettingRightIdShouldGetNotNull()
    {
        string Id = "ID";
        _existingExperts.Add(new Expert{Id = Id, Description = "", FirstName = "", LastName = "", Role = "", Technology = ""});
        _existingExperts.Add(new Expert{Id = "WRONGID", Description = "", FirstName = "", LastName = "", Role = "", Technology = ""});
        SetUpTestDB();
        
        Expert actual = _useCase.Execute(Id);

        Assert.NotNull(actual);
    }
    
    [Fact]
    public void GivenTwoExpertsWhenGettingRightIdShouldGetAllFields()
    {
        string Id = "ID";
        Expert expected = new Expert
        {
            Id = Id,
            FirstName = "FirstName",
            LastName = "LastName",
            Role = "Role",
            Technology = "Technology",
            Description = "Description"
        };
        _existingExperts.Add(expected);
        _existingExperts.Add(new Expert{Id = "WRONGID", Description = "", FirstName = "", LastName = "", Role = "", Technology = ""});
        SetUpTestDB();
        
        Expert actual = _useCase.Execute(Id);

        ExpertAssertionHelper.AssertAreEqual(expected, actual);
    }
}
