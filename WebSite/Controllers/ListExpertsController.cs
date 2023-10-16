using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases;

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

        public IActionResult Index(string technologyFilter)
        {
            Expert[] experts = _listExpertsUseCase.Execute(technologyFilter);
            return PartialView("_expertTable", experts);
        }
    }
}