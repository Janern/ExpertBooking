namespace UseCases.Cart;

public interface CartStorage
{
    BusinessModels.Cart CreateCart();
    BusinessModels.Cart GetCart(string cartId);
    List<BusinessModels.Cart> ListCarts();
    void UpdateCart(CartUpdate update);
    void DeleteCart(string cartId);
}
