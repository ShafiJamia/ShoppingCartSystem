using Microsoft.EntityFrameworkCore;
using ShoppingCartSystem.Models;
using System;

namespace ShoppingCartSystem.DataAccess.ProductManagement
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext appDbContext;
        public ProductRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddProduct(Product newProduct)
        {
            try
            {
                if (newProduct == null)
                {
                    throw new Exception("Request body is empty");
                }
                
                await appDbContext.Products.AddAsync(newProduct);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("exception upon adding product");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var result = await appDbContext.Products.FindAsync(id);

                if (result == null)
                {
                    throw new Exception("No such product available");
                }

                appDbContext.Products.Remove(result);
                await appDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProduct(int productId)
        {
            try
            {
                var result = await appDbContext.Products.FindAsync(productId);
                if (result == null)
                {
                    throw new Exception($"No Product with id:{productId} present");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var result = await appDbContext.Products.ToListAsync();
                if (result.Count == 0)
                {
                    throw new Exception("No products available");
                }

                return result;
            }
            catch (Exception)
            {
                throw new Exception ("Exception at get products");
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new Exception("Request body is empty");
                }

                var existingProduct = await appDbContext.Products.FindAsync(product.ProductId);

                if (existingProduct == null)
                {
                    throw new Exception($"No Product with id:{product.ProductId} present");
                }
                existingProduct.Name = product.Name;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
