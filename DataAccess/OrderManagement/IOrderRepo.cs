using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.TransactionManagement
{
    public interface IOrderRepo
    {
        public Task AddToCart(Cart item);
        public Task<List<Cart>> GetCartItems(User user);
        public Task UpdateCartItem(Cart updatedItem);
        public Task DeleteCartItem(int itemId);
        public Task CheckoutCart(Order order);
    }
}
