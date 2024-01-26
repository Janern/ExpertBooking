using BusinessModels;
using UseCases.Experts;
using ExternalApi;

namespace Storage;

public class ExpertStorageApiImplementation : ExpertsStorage
{
    private ApiClient _api { get; set; }
    private ExpertApiConverter _converter { get; set; }
    public ExpertStorageApiImplementation(ApiClient api, ExpertApiConverter converter)
    {
        _api = api;
        _converter = converter;
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
        return _converter.Convert(json);
    }

    public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
    {
        throw new NotImplementedException();
    }
}
