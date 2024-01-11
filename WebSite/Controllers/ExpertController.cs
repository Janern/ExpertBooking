using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Experts;
using WebSite.Helpers;

namespace WebSite.Controllers;
/*
    NOT IN USE?
*/
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
        Expert? expert = null;
        List<string>? selectedExperts = null;
        try
        {
            expert = _getExpertUseCase.Execute(Id);
            if(Request.Cookies.TryGetValue(CartCookie, out var result))
                selectedExperts = _getCartUseCase.Execute(result)?.ExpertIds;
        }catch(Exception ex)
        {
            Console.WriteLine("Error while getting expert " + ex + " " + ex.Message);
        }
        if(expert==null)
            return NotFound();
        return PartialView("_expertDetails", ExpertViewModelConverter.Convert(expert, selectedExperts));
    }
}
