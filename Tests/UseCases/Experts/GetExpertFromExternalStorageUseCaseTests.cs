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

    public GetExpertFromExternalStorageUseCaseTests()
    {
        _fakeApi = new ApiClientFake();
        _storage = new ExpertStorageApiImplementation(_fakeApi);
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
}
