using ShoppingCartSystem.DataAccess.TransactionManagement;
using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.OrderManagement
{
    public class OrderRepo : IOrderRepo
    {
        public Task AddToCart(Cart item)
        {
            throw new NotImplementedException();
        }

        public Task CheckoutCart(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cart>> GetCartItems(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItem(Cart updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
