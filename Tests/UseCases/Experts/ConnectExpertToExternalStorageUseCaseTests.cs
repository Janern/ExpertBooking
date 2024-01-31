using UseCases;
using UseCases.Experts;
using Xunit;
namespace Tests.UseCases.Experts;

public class ConnectExpertToExternalStorageUseCaseTests
{
    ExpertExternalConnectionsStorage _storage { get; set; }
    private ConnectExpertToExternalStorageUseCase _useCase { get; set; }
    public ConnectExpertToExternalStorageUseCaseTests()
    {
        _useCase = new ConnectExpertToExternalStorageUseCase();
    }

    [Fact]
    public void GivenValidExternalExpertIdWhenConnectingToExternalStorageShouldStoreExternalExpertId()
    {
        string existingId = "EXISTING";
        
        _useCase.Execute();

        // var connection = _storage.FindConnection(existingId);
        // Assert.NotNull(connection);
    }
}