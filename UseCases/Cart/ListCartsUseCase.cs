using Storage.Api;

namespace UseCases.Cart;

public class ListCartsUseCase
{
    private CartStorage _storage { get; set; }    
    public ListCartsUseCase(CartStorage storage)
    {
        _storage = storage;
    }

    public List<BusinessModels.Cart> Execute()
    {
        return _storage.ListCarts();
    }
}
