using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Experts;
using WebSite.Helpers;

namespace WebSite.Controllers;

[Route("[controller]")]
public class EditExpertController : Controller
{
    private readonly GetExpertUseCase _getExpertUseCase;

    public EditExpertController(GetExpertUseCase getExpertUseCase)
    {
        _getExpertUseCase = getExpertUseCase;
    }

    [HttpGet, Route("{Id}")]
    public IActionResult Index(string Id)
    {
        Expert expert = _getExpertUseCase.Execute(Id);
        if(expert == null)
            return StatusCode(404);
        return PartialView("_editExpert", ExpertViewModelConverter.Convert(expert, null));
    }
}
