using BusinessModels;
using ExternalApi;
using Storage;
using UseCases.Experts;
using Xunit;

namespace Tests.UseCases.Experts;

public class GetExpertFromExternalStorageUseCaseTests
{
    private GetExpertUseCase _useCase { get; set; }
    private ExpertsStorage _storage { get; set; }
    private ApiClientFake _fakeApi { get; set; }
    private ExpertApiConverterFake _fakeConverter { get; set; }

    public GetExpertFromExternalStorageUseCaseTests()
    {
        _fakeApi = new ApiClientFake();
        _fakeConverter = new ExpertApiConverterFake();
        _storage = new ExpertStorageApiImplementation(_fakeApi, _fakeConverter);
        _useCase = new GetExpertUseCase(_storage);
    }

    [Fact]
    public void 
    GivenExistingExpertWhenGettingDescriptionShouldCallApi()
    {
        string Id = "EXTERNALEXPERTID";
        
        _useCase.Execute(Id);

        Assert.True(_fakeApi.WasCalled);
    }

    [Fact]
    public void 
    GivenExistingExpertWhenGettingDescriptionShouldSendIdToApi()
    {
        string Id = "EXTERNALEXPERTID";
        
        _useCase.Execute(Id);

        Assert.Equal(Id, _fakeApi.IdCalled);
    }

    [Fact]
    public void 
    GivenValidJsonReturnedWhenGettingExpertFromApiShouldCallConverter()
    {
        string Id = "EXTERNALEXPERTID";

        _useCase.Execute(Id);

        Assert.True(_fakeConverter.WasCalled);
    }

    [Fact]
    public void 
    GivenExpertObjectFromConverterWhenGettingExpertShouldReturnConvertedExpert()
    {
        string Id = "EXTERNALEXPERTID";
        Expert expectedExpert = new Expert
        {
            Id = "ID",
            Description = "DESCRIPTION",
            FirstName = "FIRSTNAME",
            LastName = "LASTNAME",
            Role = "ROLE",
            Technology = "TECHNOLOGY"
        };
        _fakeConverter.ReturnedExpert = expectedExpert;

        var actual = _useCase.Execute(Id);

        TestHelpers.ExpertAssertionHelper.AssertAreEqual(expectedExpert, actual);
    }

    private class ApiClientFake : ApiClient
    {
        public bool WasCalled = false;
        public string IdCalled = string.Empty;

        public string GetExpertJson(string externalId)
        {
            WasCalled = true;
            IdCalled = externalId;
            return "";
        }
    }

    private class ExpertApiConverterFake : ExpertApiConverter
    {
        public bool WasCalled {get; set;}
        public Expert ReturnedExpert {get; set;}
        public Expert Convert(string expertJson)
        {
            WasCalled = true;
            return ReturnedExpert;
        }
    }
}