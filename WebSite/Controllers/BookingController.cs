using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Helpers;
using BusinessModels;
using UseCases.Experts;
using UseCases.Cart;
using UseCases.Exceptions;

namespace WebSite.Controllers;

[Route("[controller]")]
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

    [Route("/")] //Default route
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet, Route("Checkout")]
    public IActionResult GetBookingForm()
    {
        if(Request.Cookies.TryGetValue(CartCookie, out var result))
        {
            Cart? cart = _getCartUseCase.Execute(result);
            
            return PartialView("_bookingForm", cart?.ExpertIds);
        }
        return PartialView("_bookingForm");
    }
    
    [HttpGet, Route("FastCheckout/{Id}")]
    public IActionResult GetBookingForm(string Id)
    {
        /*
        må få inn expert
        legges i booking
        returnerer bookingform med model
        */
        if(Request.Cookies.TryGetValue(CartCookie, out var result))
        {
            Cart? cart = _getCartUseCase.Execute(result);
            List<string> expertIds = cart?.ExpertIds??new List<string>();
            if(!expertIds.Contains(Id))
                expertIds.Add(Id);
            return PartialView("_bookingForm", expertIds);
        }
        return PartialView("_bookingForm");
    }


    [HttpPost]
    public async Task<IActionResult> Book(BookingInputModel bookingInput)
    {
        Booking booking = null;
        bool success = false;
        try{
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                Cart? cart = _getCartUseCase.Execute(result);
                bookingInput.SelectedExpertIds = cart?.ExpertIds;
            }
            booking = BookingInputModelConverter.Convert(bookingInput);
            success = await _bookExpertUseCase.Execute(booking);
            return PartialView("_bookingResult");
        }catch(InvalidBookingException ex){
            Console.WriteLine("Error while booking " + ex + ex.Message);
            return PartialView("_bookingSubmitButton", "Mangler epostadresse");
        }
    }
}
