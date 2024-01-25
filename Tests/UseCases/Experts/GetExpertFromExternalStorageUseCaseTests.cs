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
        _storage = new ExpertStorageApiImplementation();
        _useCase = new GetExpertUseCase(_storage);
    }

    [Fact]
    public void GivenExistingExpertWhenGettingDescriptionShouldCallApi()
    {
        string Id = "EXTERNALEXPERTID";
        
        _useCase.Execute(Id);

        Assert.True(_fakeApi.WasCalled);
    }

    private class ApiClientFake : ApiClient
    {
        public bool WasCalled = false;
    }
}
