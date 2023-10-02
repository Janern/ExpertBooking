using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Route("[controller]")]
    public class BookExpertResultController : Controller
    {
        public BookExpertResultController()
        {
        }

        public IActionResult Index(BookingResultModel viewModel)
        {
            return View(viewModel);
        }
    }
}