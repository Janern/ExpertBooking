using System.Text.Json;
using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace WebSite.Controllers
{
    [Route("ListExperts")]
    public class ListExpertsJsonController : Controller
    {
        private ListExpertsUseCase _listExpertsUseCase;

        public ListExpertsJsonController(ListExpertsUseCase listExpertsUseCase)
        {
            _listExpertsUseCase = listExpertsUseCase;
        }

        [Route("JsonData")]
        public IActionResult GetAllExperts()
        {
            Expert[] experts = _listExpertsUseCase.Execute("");
            string jsonString = JsonSerializer.Serialize(experts);
            return new JsonResult(jsonString);
        }
    }
}