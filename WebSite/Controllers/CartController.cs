using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
namespace WebSite.Controllers;

[Route("Cart")]
public class CartController : Controller
{
    private AddExpertToCartUseCase _addExpertToCartUseCase;
    private GetCartUseCase _getCartUseCase;

    public CartController(
        AddExpertToCartUseCase addExpertToCartUseCase,
        GetCartUseCase getCartUseCase)
    {
        _addExpertToCartUseCase = addExpertToCartUseCase;
        _getCartUseCase = getCartUseCase;
    }

    [Route("/Cart/{Id}")]
    public IActionResult Index(string Id)
    {
        var cart = _getCartUseCase.Execute(Id);
        if(cart != null)
            return View();
        return StatusCode(404);
    }

    [HttpPost]
    [Route("/Cart/AddToCart")]
    public IActionResult AddToCart(AddToCartRequest request)
    {
        string cartId = _addExpertToCartUseCase.Execute(request);
        return PartialView("_addedToCartCheckmark", cartId);
    }
}
