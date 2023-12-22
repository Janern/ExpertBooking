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
            Id = Guid.NewGuid().ToString(),
            ExpertIds = new List<string>()
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
        List<IDictionary<string, object>> rows = _sqlite.SelectRows(DatabaseTableName.Cart);
        IDictionary<string, object>? row = rows.FirstOrDefault(r => ((string) r["Id"]) == cartId);
        if(row != null)
        {
            return new Cart{
                Id = (string) row["Id"],
                ExpertIds = new List<string>() //TODO
            };
        }
        return null;
    }

    public List<Cart> ListCarts()
    {
        List<IDictionary<string, object>>? rows = _sqlite.SelectRows(DatabaseTableName.Cart);
        return rows.Select(r => new Cart
        {
            Id = (string) r["Id"]
        }).ToList();
    }

    public void UpdateCart(CartUpdate update)
    {
        Cart existingCart = GetCart(update.CartId);
        if (existingCart != null)
            UpdateExperts(update);
    }

    private void UpdateExperts(CartUpdate update){
        Console.WriteLine("experts are updated...");
    }
}
