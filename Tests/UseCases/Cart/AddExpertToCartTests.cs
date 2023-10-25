using BusinessModels;
using Storage;
using UseCases.Cart;
using Xunit;

namespace Tests.UseCases.Cart;

public class AddExpertToCartTests
{
    private AddExpertToCartUseCase _useCase;
    private CartStorage _storage;

    public AddExpertToCartTests()
    {
        _storage = new CartStorageInMemoryImplementation();
        _useCase = new AddExpertToCartUseCase(_storage);
    }

    [Fact]
    public void GivenNoCartWhenAddingExpertToCartShouldCreateNewCartId()
    {
        string createdCartId = _useCase.Execute(new EditCartRequest());

        Assert.False(string.IsNullOrEmpty(createdCartId));
    }

    [Fact]
    public void GivenExistingCartWhenAddingExpertToCartWithoutProvidingCartIdShouldCreateNewCartId()
    {
        string existingCartId = _useCase.Execute(new EditCartRequest());
        string newCartId = _useCase.Execute(new EditCartRequest());

        Assert.NotEqual(existingCartId, newCartId);
    }

    [Fact]
    public void GivenExistingCartWhenAddingExpertToCartProvidingCartIdShouldNotCreateNewCartId()
    {
        string existingCartId = _useCase.Execute(new EditCartRequest());

        string newCartId = _useCase.Execute(new EditCartRequest{CartId = existingCartId});

        Assert.Equal(existingCartId, newCartId);
    }

    [Fact]
    public void GivenEmptyCartWhenAddingExpertToCartShouldAddExpertToCart()
    {
        Expert existingExpert = new Expert { Id = "ID" };

        string cartId = _useCase.Execute(new EditCartRequest{ExpertId = existingExpert.Id});

        Assert.Single(_storage.GetCart(cartId).ExpertIds);
        Assert.Equal(existingExpert.Id, _storage.GetCart(cartId).ExpertIds[0]);
    }

    [Fact]
    public void GivenNonEmptyCartWhenAddingExpertToCartShouldAddExpertToCart()
    {
        Expert existingExpert = new Expert { Id = "ID" };
        Expert existingExpert2 = new Expert { Id = "ID2" };
        string cartId = _useCase.Execute(new EditCartRequest{ExpertId = existingExpert.Id});

        _useCase.Execute(new EditCartRequest{CartId = cartId, ExpertId = existingExpert2.Id});

        var experts = _storage.GetCart(cartId).ExpertIds;
        Assert.Equal(2, experts.Count);
        Assert.Contains(existingExpert.Id, experts);
        Assert.Contains(existingExpert2.Id, experts);
    }
}
