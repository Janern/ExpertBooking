using System.Text.Json;
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

        public IActionResult Index(string filter)
        {
            Expert[] experts = _listExpertsUseCase.Execute(filter);
            return PartialView("_expertTable", experts);
        }
    }
}