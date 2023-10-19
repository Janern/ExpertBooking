
using Storage.Api;

namespace UseCases.Experts;

public class GetExpertUseCase
{
    private ExpertsStorage _storage;
    public GetExpertUseCase(ExpertsStorage storage)
    {
        _storage = storage;
    }    

    public BusinessModels.Expert Execute(string Id)
    {
        if(Id == null)
            return null;
        return _storage.GetExpert(Id);
    }
}
