using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.ProductManagement
{
    public class ProductRepo : IProductRepo
    {
        public Task AddProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
