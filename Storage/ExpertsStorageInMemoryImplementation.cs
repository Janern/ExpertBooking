using BusinessModels;
using UseCases.Experts;

namespace Storage
{
    public class ExpertsStorageInMemoryImplementation : ExpertsStorage
    {

        private IDictionary<string, Expert> _experts { get; set; }

        public ExpertsStorageInMemoryImplementation(Expert[] experts)
        {
            _experts = new Dictionary<string, Expert>();
            foreach (var expert in experts)
            {
                if (expert != null && !string.IsNullOrEmpty(expert.Id))
                    _experts.Add(expert.Id, expert);
            }

        }

        public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
        {
            return ExpertFilteringHelper.FilterExperts(_experts.Values.ToArray(), filterString: technologyFilter, expertIds: expertIds);
        }

        public Expert GetExpert(string id)
        {
            try
            {
                return _experts[id];
            }
            catch (KeyNotFoundException)
            {

            }
            return null;
        }

        public bool Exists(string id)
        {
            return _experts.ContainsKey(id);
        }

        public void EditExpert(EditExpertRequest request)
        {
            if(request.Description != null)
                _experts[request.Id].Description = request.Description;
            if(request.FirstName != null)
                _experts[request.Id].FirstName = request.FirstName;
        }
    }
}