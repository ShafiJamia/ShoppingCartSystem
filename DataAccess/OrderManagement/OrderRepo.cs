using Microsoft.EntityFrameworkCore;
using ShoppingCartSystem.DataAccess.TransactionManagement;
using ShoppingCartSystem.Models;
using System;
using System.Formats.Tar;

namespace ShoppingCartSystem.DataAccess.OrderManagement
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext appDbContext;

        public OrderRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddToCart(Cart item)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("Request contain no data");
                }

                var product = await appDbContext.Products.FindAsync(item.ProductId);

                if(product == null)
                {
                    throw new Exception("No such product exist");
                }
                if (item.Quantity > product.Stock)
                {
                    throw new Exception("Not enough in stock"); 
                }
                else
                {
                    await appDbContext.Carts.AddAsync(item);

                    product.Stock = product.Stock - item.Quantity;

                    appDbContext.Products.Update(product);

                    await appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task CheckoutCart(Order order)
        {
            try
            {
                if (order == null)
                {
                    throw new Exception("Request contains no data");
                }

                var cartEntries = await appDbContext.Carts.Where(c => c.UserId == order.UserId).ToListAsync();

                await appDbContext.Orders.AddAsync(order);

                appDbContext.Carts.RemoveRange(cartEntries);

                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCartItem(int cartId)
        {
            try
            {
                var cartEntry = await appDbContext.Carts.FindAsync(cartId);

                if (cartEntry != null)
                {
                    appDbContext.Carts.Remove(cartEntry);

                    await appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Cart>> GetCartItems(User user)
        {
            try
            {
                var cartEntries = await appDbContext.Carts.Where(c => c.UserId == user.UserId).ToListAsync();

                if (cartEntries.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }

                return cartEntries;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetCartItems");
            }
        }

        public async Task UpdateCartItem(Cart updatedItem)
        {
            try
            {
                var cartEntry = await appDbContext.Carts.FirstOrDefaultAsync(c => c.UserId == updatedItem.UserId && c.ProductId==updatedItem.ProductId);
                var product = await appDbContext.Products.FindAsync(updatedItem.ProductId);
                if(updatedItem.Quantity < 0)
                {
                    throw new Exception("Quantity cannot be less than zero");
                }
                if(product == null)
                {
                    throw new Exception("No such exception exists");
                }

                if (cartEntry != null)
                {
                    int change = Math.Abs(cartEntry.Quantity - updatedItem.Quantity);
                    if(updatedItem.Quantity == 0)
                    {
                        await DeleteCartItem(cartEntry.CartId);
                    }
                    if (cartEntry.Quantity < updatedItem.Quantity)//increasing the quantity
                    {
                        if(product.Stock < change)//no more stock are left
                        {
                            throw new Exception("Cannot cater this request stock is less than request");
                        }
                        else
                        {
                            product.Stock = product.Stock - change;
                        }
                    }
                    else//decreasing the quantity
                    {
                        product.Stock = product.Stock + change;
                    }

                    appDbContext.Products.Update(product);
                    appDbContext.Products.Update(product);
                    await appDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("No entry for such a cart with this user name and product exist");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
