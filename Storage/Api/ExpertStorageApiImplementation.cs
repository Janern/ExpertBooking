using BusinessModels;
using UseCases.Experts;
using ExternalApi;

namespace Storage;

public class ExpertStorageApiImplementation : ExpertsStorage
{
    public ApiClient _api { get; set; }
    public ExpertStorageApiImplementation(ApiClient api)
    {
        _api = api;
    }
    public void EditExpert(EditExpertRequest request)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string id)
    {
        throw new NotImplementedException();
    }

    public Expert GetExpert(string id)
    {
        string json = _api.GetExpertJson(id);
        return null;
    }

    public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
    {
        throw new NotImplementedException();
    }
}
