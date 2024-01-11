using BusinessModels;
using WebSite.Models;

namespace WebSite.Helpers;

public static class ExpertViewModelConverter
{
    public static ExpertViewModel[] Convert(Expert[] experts, List<string>? selectedExperts, bool editable = false)
    {
        ExpertViewModel[] expertViewModels = new ExpertViewModel[experts.Length];
        for(int i = 0; i < expertViewModels.Length; i++)
        {
            expertViewModels[i] = new ExpertViewModel
            {
                Id = experts[i].Id,
                FirstName = experts[i].FirstName,
                LastName = experts[i].LastName,
                Description = experts[i].Description,
                Role = experts[i].Role,
                Technology = experts[i].Technology,
                IsSelected = selectedExperts?.Any(e => e == experts[i].Id)??false,
                IsEditable = editable
            };
        }
        return expertViewModels;
    }

    public static ExpertViewModel Convert(Expert expert, List<string>? selectedExperts)
    {
        return CreateViewModel(expert, selectedExperts?.Any(e => e == expert.Id)??false);
    }

    private static ExpertViewModel CreateViewModel(Expert expert, bool isSelected, bool editable = false)
    {
        return new ExpertViewModel
            {
                Id = expert.Id,
                FirstName = expert.FirstName,
                LastName = expert.LastName,
                Description = expert.Description,
                Role = expert.Role,
                Technology = expert.Technology,
                IsSelected = isSelected,
                IsEditable = editable
            };
    }
}
