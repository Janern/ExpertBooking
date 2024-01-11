using BusinessModels;

namespace UseCases.Experts;

public interface ExpertsStorage
{
    Expert[] GetExperts(string technologyFilter, string[] expertIds = null);
    Expert GetExpert(string id);
    bool Exists(string id);
    void EditExpert(EditExpertRequest request);
}