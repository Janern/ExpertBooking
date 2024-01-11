using UseCases.Exceptions;

namespace UseCases.Cart;

 
public class RemoveExpertFromCartUseCase
{
    private CartStorage _storage;

    public RemoveExpertFromCartUseCase(CartStorage storage)
    {
        _storage = storage;
    }
    public void Execute(EditCartRequest request)
    {
        if(request == null)
            throw new InvalidEditCartRequestException("request is null");
        if(string.IsNullOrEmpty(request.CartId))
            throw new InvalidEditCartRequestException("missing cartid");
        if(string.IsNullOrEmpty(request.ExpertId))
            throw new InvalidEditCartRequestException("missing expertid");
        var cart = _storage.GetCart(request.CartId);
        if(cart == null)
            throw new InvalidEditCartRequestException("cart not found");
        if(cart.ExpertIds.Contains(request.ExpertId))
        {
            _storage.UpdateCart(new CartUpdate
            {
                CartId = cart.Id,
                ExpertIds = cart.ExpertIds.Where(id => id != request.ExpertId).ToList()
            });
        }
    }
}
