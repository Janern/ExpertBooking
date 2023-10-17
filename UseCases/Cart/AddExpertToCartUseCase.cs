using Storage.Api;

namespace UseCases.Cart;

public class AddExpertToCartUseCase
{
    private CartStorage _storage;

    public AddExpertToCartUseCase(CartStorage storage)
    {
        _storage = storage;
    }
    public string Execute(AddToCartRequest request)
    {
        BusinessModels.Cart cart = string.IsNullOrEmpty(request.CartId) ? _storage.CreateCart() : _storage.GetCart(request.CartId);
        _storage.UpdateCart(new CartUpdate
        {
            CartId = cart.Id,
            ExpertIds = cart.ExpertIds.Union<string>(new List<string>(){request.ExpertId}).ToList()
        });
        return cart.Id;
    }
}
