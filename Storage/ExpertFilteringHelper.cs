using BusinessModels;

namespace Storage;
public static class ExpertFilteringHelper
{
    public static Expert[] FilterExperts(Expert[] experts, string filterString = null, string separationString = ",", string[] expertIds = null)
    {
        if (string.IsNullOrEmpty(filterString.Trim()) &&
            expertIds == null)
            return experts;
        List<Expert> result = new List<Expert>();
        foreach (var expert in experts)
        {
            if (FilterTechnology(expert, separationString, filterString) && 
                FilterId(expert, expertIds))
            {
                result.Add(expert);
            }
        }
        return result.ToArray();
    }

    private static bool FilterTechnology(Expert expert, string separationString, string filterString)
    {
        if(string.IsNullOrEmpty(filterString?.Trim()))
            return true;
        return expert.Technology
                     .ToUpper()
                     .Split(separationString)
                     .Any(technology => technology == filterString.Trim().ToUpper());
    }

    private static bool FilterId(Expert expert, string[] expertIds)
    {
        if(expertIds == null || expertIds.Length == 0)
            return true;
        return expertIds.Any(id => id == expert.Id);
    }
}