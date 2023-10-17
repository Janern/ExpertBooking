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

        public ListExpertsController(
            ListExpertsUseCase listExpertsUseCase, 
            GetCartUseCase getCartUseCase)
        {
            _listExpertsUseCase = listExpertsUseCase;
            _getCartUseCase = getCartUseCase;
        }

        public IActionResult Index(string technologyFilter, string cartId)
        {
            Expert[] experts = _listExpertsUseCase.Execute(technologyFilter);
            Cart cart = _getCartUseCase.Execute(cartId);
            return PartialView("_expertTable", ExpertViewModelConverter.Convert(experts, cart?.ExpertIds?.ToArray()));
        }
    }
}