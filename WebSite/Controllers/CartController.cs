using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Exceptions;
namespace WebSite.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private AddExpertToCartUseCase _addExpertToCartUseCase;
    private RemoveExpertFromCartUseCase _removeExpertFromCartUseCase;
    private GetCartUseCase _getCartUseCase;
    private ListCartsUseCase _listCartsUseCase;
    
    private const string CartCookie = "__CartId";

    public CartController(
        AddExpertToCartUseCase addExpertToCartUseCase,
        GetCartUseCase getCartUseCase,
        ListCartsUseCase listCartsUseCase,
        RemoveExpertFromCartUseCase removeExpertFromCartUseCase)
    {
        _addExpertToCartUseCase = addExpertToCartUseCase;
        _getCartUseCase = getCartUseCase;
        _listCartsUseCase = listCartsUseCase;
        _removeExpertFromCartUseCase = removeExpertFromCartUseCase;
    }

    [HttpGet, Route("Cart/{Id}")]
    public IActionResult GetCart(string Id)
    {
        var cart = _getCartUseCase.Execute(Id);
        if(cart != null)
            return View();
        return StatusCode(404);
    }

    [HttpGet, Route("/Carts")]
    public IActionResult Index(string Id)
    {
        var carts = _listCartsUseCase.Execute();
        return View("Index", carts);
    }

    [HttpPost, Route("Add")]
    public IActionResult AddToCart(EditCartRequest request)
    {
        try{
            if(Request.Cookies.TryGetValue(CartCookie, out var result) && _getCartUseCase.Execute(result) != null)
                request.CartId = result;
        }catch{}
        string cartId = _addExpertToCartUseCase.Execute(request);
        Response.Cookies.Append(CartCookie, cartId);
        return PartialView("_removeFromCartCheckmark", request.ExpertId);
    }

    [HttpPost, Route("Remove")]
    public IActionResult RemoveFromCart(EditCartRequest request)
    {
        try{
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                request.CartId = result;
                _removeExpertFromCartUseCase.Execute(request);
                Console.WriteLine("Item removed from cart successfully.");
                return PartialView("_addToCartCheckmark", request.ExpertId);
            }
        }catch(InvalidEditCartRequestException ex){
            Console.WriteLine("Error while removing item from cart" + ex + ex.Message);
        }catch(Exception ex){
            Console.WriteLine("Error while removing item from cart" + ex + ex.Message);
        }
        return PartialView("_removeFromCartCheckmark", request.ExpertId);
    }
}
