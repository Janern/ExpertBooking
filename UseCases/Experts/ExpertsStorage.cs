using BusinessModels;

namespace UseCases.Experts;

public interface ExpertsStorage
{
    Expert[] GetExperts(string technologyFilter, string[] expertIds = null);
    Expert GetExpert(string id);
}