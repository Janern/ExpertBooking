using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using UseCases;
using WebSite.Helpers;
using BusinessModels;

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
        Booking booking = null;
        bool success = false;
        try{
            booking = BookingInputModelConverter.Convert(bookingInput);
            success = await _useCase.Execute(booking);
        }catch(Exception ex){
        }
        return RedirectToAction("Index", "BookExpertResult", 
            new BookingResultModel{
                Booking = booking, 
                Success = success});
        
    }
}
