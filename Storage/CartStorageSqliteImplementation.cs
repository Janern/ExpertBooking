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

        _sqlite.InsertRow(DatabaseTableName.Cart, new DatabaseColumnName[]{DatabaseColumnName.Id}, new string[]{newCart.Id});
        return newCart;
    }

    public void DeleteCart(string cartId)
    {
        _sqlite.DeleteRow(DatabaseTableName.Cart, cartId);
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
