using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
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

    [HttpGet, Route("/Carts")]
    public IActionResult Index()
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

    [HttpPost, Route("Remove/{Id}")]
    public IActionResult RemoveFromCart(string Id)
    {
        try
        {
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                _removeExpertFromCartUseCase.Execute(new EditCartRequest{CartId = result, ExpertId = Id});
                Response.Headers.Add("HX-Trigger", "CartChanged");
                return PartialView("_addToCartCheckmark", Id);
            }
        }catch(Exception ex){
            Console.WriteLine("Error while removing item from cart" + ex + ex.Message);
        }
        return PartialView("_removeFromCartCheckmark", Id);
    }

    [HttpGet]
    public IActionResult GetCart()
    {
        try
        {
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
            {
                BusinessModels.Cart cart = _getCartUseCase.Execute(result);
                if(cart != null)
                    return PartialView("_cartDetails", cart);
            }
        }catch(Exception ex){
            Console.WriteLine("Error while getting cart details" + ex + ex.Message);
        }
        return PartialView("_cartDetails", null);
    }
}
