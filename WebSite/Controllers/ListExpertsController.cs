using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using UseCases.Cart;
using UseCases.Experts;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    [Route("ListExperts")]
    public class ListExpertsController : Controller
    {
        private ListExpertsUseCase _listExpertsUseCase;
        private GetCartUseCase _getCartUseCase;
        private const string CartCookie = "__CartId";
        private const string AdminKeyCookie = "__SuperSecretAdminKey";

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
            
            List<string>? selectedExpertIds = null;
            bool isAdmin = false;
            try{
                if(Request.Cookies.TryGetValue(CartCookie, out var result) && _getCartUseCase.Execute(result) != null)
                {
                    selectedExpertIds = _getCartUseCase.Execute(result)?.ExpertIds;
                }
                if(Request.Cookies.TryGetValue(AdminKeyCookie, out var AdminCookieValue))
                {
                    isAdmin = AdminCookieValue == "94c165d1-7405-4795-aaa9-c2e6369b8ce8";
                }
            }catch{}
            return PartialView("_expertTable", ExpertViewModelConverter.Convert(experts, selectedExpertIds, isAdmin));
        }
    }
}