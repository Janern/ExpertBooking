using BusinessModels;

namespace Storage.Api
{
    public interface ExpertsStorage
    {
        Expert[] GetExperts(string technologyFilter);
    }
}