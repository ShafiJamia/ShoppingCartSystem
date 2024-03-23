using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartSystem.DataAccess.ProductManagement;
using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo productRepo;

        public ProductController(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddProduct(Product newProduct)
        {
            Response response;
            try
            {
                await productRepo.AddProduct(newProduct);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Product Added successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct(Product product)
        {
            Response response;
            try
            {
                await productRepo.UpdateProduct(product);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Grocery updated successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            try
            {
                var groceries = await productRepo.GetProducts();
                return Ok(groceries);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await productRepo.GetProduct(id);
                return Ok(product);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> DeleteProduct(int id)
        {
            Response response;
            try
            {
                await productRepo.DeleteProduct(id);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Product deleted successfully"
                };
                return Ok(response);
            }
            catch (Exception rex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = rex.Message
                };
                return BadRequest(response);
            }
        }
    }
}
