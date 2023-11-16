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

    private readonly ListExpertsUseCase _listExpertsUseCase;

private readonly AddExpertToCartUseCase _addExpertToCartUseCase;

public BookingController(BookExpertUseCase bookExpertUseCase, GetCartUseCase getCartUseCase, ListExpertsUseCase listExpertsUseCase, AddExpertToCartUseCase addExpertToCartUseCase)
    {
_addExpertToCartUseCase = addExpertToCartUseCase;
_listExpertsUseCase = listExpertsUseCase;
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
        return PartialView("_bookingForm");
    }
    
    [HttpPost, Route("FastCheckout/{Id}")]
    public IActionResult GetBookingForm(string Id)
    {
        if(Request.Cookies.TryGetValue(CartCookie, out var result))
        {
            Cart? cart = _getCartUseCase.Execute(result);
            List<string> expertIds = cart?.ExpertIds??new List<string>();
            if(!expertIds.Contains(Id))
            {

                string cartId = _addExpertToCartUseCase.Execute(new EditCartRequest{CartId = cart?.Id, ExpertId = Id});
                Response.Cookies.Append(CartCookie, cartId);
                Response.Headers.Add("HX-Trigger", "CartChanged");
            }
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
