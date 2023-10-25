using BusinessModels;
using UseCases.Cart;

namespace Storage;

public class CartStorageInMemoryImplementation : CartStorage
{
    private List<Cart> _carts { get; set; }
    public CartStorageInMemoryImplementation()
    {
        _carts = new List<Cart>();
    }
    public Cart CreateCart()
    {
        Cart newCart = new Cart
        {
            Id = Guid.NewGuid().ToString(),
            ExpertIds = new List<string>()
        };
        _carts.Add(newCart);
        return newCart;
    }

    public void DeleteCart(string cartId)
    {
        Cart existingCart = _carts.FirstOrDefault(c => c.Id == cartId);
        if (existingCart != null)
            _carts.Remove(existingCart);
    }

    public Cart GetCart(string cartId)
    {
        return _carts.FirstOrDefault(c => c.Id == cartId);
    }

    public void UpdateCart(CartUpdate update)
    {
        Cart existingCart = _carts.FirstOrDefault(c => c.Id == update.CartId);
        if (existingCart != null && existingCart.ExpertIds != null)
            existingCart.ExpertIds = update.ExpertIds;
    }

    public List<Cart> ListCarts()
    {
        return _carts;
    }
}
