using BusinessModels;

namespace Storage.Implementation;
public static class ExpertFilteringHelper
{
    public static Expert[] FilterExperts(Expert[] experts, string filterString, string separationString = ",")
    {
        if(filterString.Trim() == "")
            return experts;
        List<Expert> result = new List<Expert>();
        var filterStrings = filterString.ToUpper().Split(separationString);
        foreach(var expert in experts){
            if(expert.Technology
                     .ToUpper()
                     .Split(separationString)
                     .Any(technology => filterStrings.Any(filter => filter.Trim() == technology.Trim()))){
                result.Add(expert);
            }
        }
        return result.ToArray();
    }
}