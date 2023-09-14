using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using UseCases;
using BusinessModels;
using WebSite.Helpers;

namespace WebSite.Controllers;

public class 
BookExpertController : Controller
{
    private readonly ILogger<BookExpertController> _logger;
    private BookExpertUseCase _useCase;

    public BookExpertController(
        ILogger<BookExpertController> logger, 
        BookExpertUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Book(BookingInputModel bookingInput)
    {
        var booking = BookingInputModelConverter.Convert(bookingInput);
        bool success = await _useCase.Execute(booking);
        return RedirectToAction("Index", "BookExpertResult", new BookingResultModel{Booking = booking, Success = success});
    }
}
