using BusinessModels;
using UseCases.Experts;

namespace Storage;

public class ExpertStorageApiImplementation : ExpertsStorage
{
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
        return null;
    }

    public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
    {
        throw new NotImplementedException();
    }
}
