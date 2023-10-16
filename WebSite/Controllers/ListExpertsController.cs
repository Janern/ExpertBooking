using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    [Route("ListExperts")]
    public class ListExpertsController : Controller
    {
        private ListExpertsUseCase _listExpertsUseCase;

        public ListExpertsController(ListExpertsUseCase listExpertsUseCase)
        {
            _listExpertsUseCase = listExpertsUseCase;
        }

        public IActionResult Index(string technologyFilter, string[]? selectedExperts = null)
        {
            Expert[] experts = _listExpertsUseCase.Execute(technologyFilter);
            return PartialView("_expertTable", ExpertViewModelConverter.Convert(experts, selectedExperts));
        }
    }
}