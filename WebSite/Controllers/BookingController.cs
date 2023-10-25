using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Helpers;
using BusinessModels;
using UseCases.Experts;
using UseCases.Cart;

namespace WebSite.Controllers;

[Route("Booking")]
public class BookingController : Controller
{
    private BookExpertUseCase _bookExpertUseCase;
    private GetCartUseCase _getCartUseCase;
    private const string CartCookie = "__CartId";

    public BookingController(BookExpertUseCase bookExpertUseCase, GetCartUseCase getCartUseCase)
    {
        _bookExpertUseCase = bookExpertUseCase;
        _getCartUseCase = getCartUseCase;
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
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                var cart = _getCartUseCase.Execute(result);
                bookingInput.SelectedExpertIds = cart.ExpertIds;
            }
            booking = BookingInputModelConverter.Convert(bookingInput);
            success = await _bookExpertUseCase.Execute(booking);
        }catch(Exception ex){
        }
        return PartialView("_bookingResult", 
            new BookingResultModel{
                Booking = booking, 
                Success = success});
        
    }
}
