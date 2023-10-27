using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Experts;
using WebSite.Helpers;

namespace WebSite.Controllers;

[Route("[controller]")]
public class ExpertController : Controller
{
    private readonly GetExpertUseCase _getExpertUseCase;
    private readonly GetCartUseCase _getCartUseCase;
    private const string CartCookie = "__CartId";

    public ExpertController(GetExpertUseCase getExpertUseCase, GetCartUseCase getCartUseCase)
    {
        _getCartUseCase = getCartUseCase;
        _getExpertUseCase = getExpertUseCase;
    }

    [HttpGet, Route("{Id}")]
    public IActionResult Index(string Id)
    {
        var expert = _getExpertUseCase.Execute(Id);
        List<string>? selectedExperts = null;
        try
        {
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
                selectedExperts = _getCartUseCase.Execute(result)?.ExpertIds;
        }catch(Exception ex)
        {
            Console.WriteLine("Error while removing item from cart" + ex + ex.Message);
        }
        return PartialView("_expertDetails", ExpertViewModelConverter.Convert(expert, selectedExperts));
    }
}
