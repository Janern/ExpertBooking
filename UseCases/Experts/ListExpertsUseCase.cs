using UseCases.Cart;

namespace UseCases.Experts;
public class ListExpertsUseCase
{
    private ExpertsStorage _storage { get; set; }
    public ListExpertsUseCase(ExpertsStorage storage)
    {
        _storage = storage;
    }

    public BusinessModels.Expert[] Execute(string technologyFilter = "", string[] expertIds = null)
    {
        return _storage.GetExperts(technologyFilter, expertIds);
    }
}