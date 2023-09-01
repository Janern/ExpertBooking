using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("[controller]")]
    public class ListExpertsJsonController : Controller
    {
        public ListExpertsJsonController()
        {
        }

        public IActionResult GetAllExperts()
        {
            string jsonString = "tegds";
            return new JsonResult(jsonString);
        }
    }
}