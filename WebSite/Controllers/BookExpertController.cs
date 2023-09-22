using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using UseCases;
using WebSite.Helpers;

namespace WebSite.Controllers;

public class BookExpertController : Controller
{
    private BookExpertUseCase _useCase;

    public BookExpertController(BookExpertUseCase useCase)
    {
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
