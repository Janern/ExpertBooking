using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases;
using UseCases.Cart;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    [Route("ListExperts")]
    public class ListExpertsController : Controller
    {
        private ListExpertsUseCase _listExpertsUseCase;
        private GetCartUseCase _getCartUseCase;
        private const string CartCookie = "__CartId";

        public ListExpertsController(
            ListExpertsUseCase listExpertsUseCase, 
            GetCartUseCase getCartUseCase)
        {
            _listExpertsUseCase = listExpertsUseCase;
            _getCartUseCase = getCartUseCase;
        }

        public IActionResult Index(string technologyFilter)
        {
            Expert[] experts = _listExpertsUseCase.Execute(technologyFilter);
            List<string>? expertIds = null;
            try{
                if(Request.Cookies.TryGetValue(CartCookie, out var result) && _getCartUseCase.Execute(result) != null)
                {
                    expertIds = _getCartUseCase.Execute(result)?.ExpertIds;
                }
            }catch{}
            return PartialView("_expertTable", ExpertViewModelConverter.Convert(experts, expertIds?.ToArray()));
        }
    }
}