using BusinessModels;

namespace Storage.Api;

public interface CartStorage
{
    Cart CreateCart();
    Cart GetCart(string cartId);
    void UpdateCart(CartUpdate update);
    void DeleteCart(string cartId);
}
