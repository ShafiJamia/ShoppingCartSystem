using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.ProductManagement
{
    public interface IProductRepo
    {
        public Task AddProduct(Product newProduct);
        public Task UpdateProduct(Product product);
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProduct(int productId);
        public Task DeleteProduct(int id);
    }
}
