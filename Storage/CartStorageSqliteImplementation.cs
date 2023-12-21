using BusinessModels;
using UseCases.Cart;

namespace Storage;

public class CartStorageSqliteImplementation : CartStorage
{
    private SqlController _sqlite;
    public CartStorageSqliteImplementation(SqlController sqlite)
    {
        _sqlite = sqlite;    
    }

    public Cart CreateCart()
    {
        Cart newCart = new Cart
        {
            Id = Guid.NewGuid().ToString()
        };
        
        return newCart;
    }

    public void DeleteCart(string cartId)
    {
        throw new NotImplementedException();
    }

    public Cart GetCart(string cartId)
    {
        throw new NotImplementedException();
    }

    public List<Cart> ListCarts()
    {
        throw new NotImplementedException();
    }

    public void UpdateCart(CartUpdate update)
    {
        throw new NotImplementedException();
    }
}
