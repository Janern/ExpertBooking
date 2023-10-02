using BusinessModels;

namespace Storage.Implementation;
public static class ExpertFilteringHelper
{
    public static Expert[] FilterExperts(Expert[] experts, string filterString = null, string separationString = ",")
    {
        if(filterString == null || filterString.Trim() == "")
            return experts;
        List<Expert> result = new List<Expert>();
        var filterStrings = filterString.ToUpper();
        foreach(var expert in experts){
            if(expert.Technology
                     .ToUpper()
                     .Split(separationString)
                     .Any(technology => technology == filterString.Trim().ToUpper())){
                result.Add(expert);
            }
        }
        return result.ToArray();
    }
}