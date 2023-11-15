using BusinessModels;

namespace UseCases.Cart;

public interface ExpertsStorage
{
    Expert[] GetExperts(string technologyFilter, string[] expertIds = null);
    Expert GetExpert(string id);
}