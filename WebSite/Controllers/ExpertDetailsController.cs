using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Experts;
using WebSite.Helpers;

namespace WebSite.Controllers;

[Route("[controller]")]
public class ExpertDetailsController : Controller
{
    private GetExpertUseCase _getExpertUseCase;
    private GetCartUseCase _getCartUseCase;
    private const string CartCookie = "__CartId";
    private const string AdminCookie = "__SuperSecretAdminKey";

    public ExpertDetailsController(
        GetExpertUseCase getExpertUseCase, 
        GetCartUseCase getCartUseCase)
    {
        _getExpertUseCase = getExpertUseCase;
        _getCartUseCase = getCartUseCase;
    }

    [HttpGet, Route("{Id}")]
    public IActionResult GetDetails(string Id)
    {
        var expert = _getExpertUseCase.Execute(Id);
        if(expert == null)
            return StatusCode(404);
        List<string> expertIds = null;
        try{
            if(Request.Cookies.TryGetValue(CartCookie, out var result) && _getCartUseCase.Execute(result) != null)
            {
                expertIds = _getCartUseCase.Execute(result)?.ExpertIds;
            }
        }catch{}
        var expertViewModel = ExpertViewModelConverter.Convert(expert, expertIds);

        return PartialView("_expertModal", expertViewModel);
    }
}
