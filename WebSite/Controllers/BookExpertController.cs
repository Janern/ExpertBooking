using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
// using UseCases;

namespace WebSite.Controllers;

public class BookExpertController : Controller
{
    private readonly ILogger<BookExpertController> _logger;
    // private BookExpertUseCase _useCase;

    public BookExpertController(
        ILogger<BookExpertController> logger
        // BookExpertUseCase useCase
        )
    {
        _logger = logger;
        // _useCase = useCase;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Book()
    {
        //_useCase.Execute();
        return new JsonResult("success");
    }
}
