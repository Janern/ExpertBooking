using BusinessModels;
using Storage.Api;

namespace UseCases;
public class ListExpertsUseCase
{
    private ExpertsStorage _storage { get; set; }
    public ListExpertsUseCase(ExpertsStorage storage)
    {
        _storage = storage;
    }

    public Expert[] Execute(string technologyFilter = "")
    {
        return _storage.GetExperts(technologyFilter);
    }
}