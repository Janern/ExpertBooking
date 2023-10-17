using Storage.Api;

namespace UseCases.Cart;

public class GetCartUseCase
{
    private CartStorage _storage { get; set; }    
    public GetCartUseCase(CartStorage storage)
    {
        _storage = storage;
    }

    public BusinessModels.Cart Execute(string cartId)
    {
        return _storage.GetCart(cartId);
    }
}
