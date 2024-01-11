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
    private const string SuperSecretAdminString = "94c165d1-7405-4795-aaa9-c2e6369b8ce8";

    public ExpertDetailsController(
        GetExpertUseCase getExpertUseCase, 
        GetCartUseCase getCartUseCase)
    {
        _getExpertUseCase = getExpertUseCase;
        _getCartUseCase = getCartUseCase;
    }

    [HttpGet, Route("{Id}")]
    public IActionResult GetReadOnlyDetails(string Id)
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

    [HttpGet, Route("Edit/{Id}")]
    public IActionResult GetEditableDetails(string Id)
    {
        //TODO properly authenticate and authorize through login
        if(!HasAdminAccess())
            return StatusCode(403);
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

        return PartialView("_editExpertModal", expertViewModel);
    }

    [HttpPost, Route("Edit/{Id}")]
    public IActionResult EditExpertDetails(string Id, EditExpertRequest request)
    {
        if(!HasAdminAccess())
            return StatusCode(403);
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
        return PartialView("_editExpertModal", expertViewModel);
    }

    private bool HasAdminAccess()
    {
        bool result = false;
        try{
            if(Request.Cookies.TryGetValue(AdminCookie, out var adminKey))
                result = adminKey == SuperSecretAdminString;
        }catch{}
        return result;
    }
}
