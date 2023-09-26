using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}