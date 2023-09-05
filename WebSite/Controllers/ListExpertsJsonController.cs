using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessModels;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("ListExperts")]
    public class ListExpertsJsonController : Controller
    {
        public ListExpertsJsonController()
        {
        }

        [Route("JsonData")]
        public IActionResult GetAllExperts()
        {
            List<Expert>? jsonData = new List<Expert>
            {
                new Expert {  },
                new Expert {  },
                new Expert {  }
            };
            string jsonString = JsonSerializer.Serialize(jsonData);
            return new JsonResult(jsonString);
        }
    }
}