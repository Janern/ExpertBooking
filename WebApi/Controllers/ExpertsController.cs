using System.Text.Json;
using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpertsController : ControllerBase
    {
        private ListExpertsUseCase _listUseCase { get; set; }
        public ExpertsController(ListExpertsUseCase listUseCase)
        {
            _listUseCase = listUseCase;
        }

        [HttpGet]
        public ActionResult Get()
        {
            Expert[] experts = _listUseCase.Execute();
            string jsonResult = JsonSerializer.Serialize(experts);
            return new JsonResult(jsonResult);
        }
    }
}