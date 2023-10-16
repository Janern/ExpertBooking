using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessModels;
using Microsoft.Extensions.Azure;
using WebSite.Models;

namespace WebSite.Helpers;

public static class ExpertViewModelConverter
{
    public static ExpertViewModel[] Convert(Expert[] experts, string[]? selectedExperts)
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
                IsSelected = selectedExperts?.Any(e => e == experts[i].Id)??false
            };
        }
        return expertViewModels;
    }
}
