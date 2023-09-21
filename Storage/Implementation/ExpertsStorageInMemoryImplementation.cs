using Storage.Api;
using BusinessModels;

namespace Storage.Implementation
{
    public class ExpertsStorageInMemoryImplementation : ExpertsStorage
    {
        private Expert[] _experts { get; set; }

        public ExpertsStorageInMemoryImplementation(Expert[] experts)
        {
            _experts = experts;
        }

        public Expert[] GetExperts(string technologyFilter)
        {
            if(technologyFilter.Trim() == ""){
                return _experts;
            }
            return _experts.Where(e => e.Technology.Trim().ToUpper() == technologyFilter.Trim().ToUpper()).ToArray();
        }
    }
}