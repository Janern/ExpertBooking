using BusinessModels;

namespace UseCases.Cart;

public interface ExpertsStorage
{
    Expert[] GetExperts(string technologyFilter);
    Expert GetExpert(string id);
}