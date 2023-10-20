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
        Response.Headers.Add("HX-Trigger", "CartChanged");
        return PartialView("_removeFromCartCheckmark", request.ExpertId);
    }

    [HttpPost, Route("Remove")]
    public IActionResult RemoveFromCart(EditCartRequest request)
    {
        try
        {
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                request.CartId = result;
                _removeExpertFromCartUseCase.Execute(request);
                Response.Headers.Add("HX-Trigger", "CartChanged");
                return PartialView("_addToCartCheckmark", request.ExpertId);
            }
        }catch(Exception ex){
            Console.WriteLine("Error while removing item from cart" + ex + ex.Message);
        }
        return PartialView("_removeFromCartCheckmark", request.ExpertId);
    }

    [HttpGet, Route("MenuButton")]
    public IActionResult GetCartMenuButton(EditCartRequest request)
    {
        try
        {
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                request.CartId = result;
                var cart = _getCartUseCase.Execute(result);
                if(cart != null)
                    return PartialView("_cartMenuButton", cart.ExpertIds.Count);
            }
        }catch(Exception ex){
            Console.WriteLine("Error while getting cart menu button" + ex + ex.Message);
        }
        return PartialView("_cartMenuButton", 0);
    }
}
