using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartSystem.DataAccess.TransactionManagement;
using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepo orderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddToCart(Cart item)
        {
            Response response;
            try
            {
                await orderRepo.AddToCart(item);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Added successfully"
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

        [HttpPost("checkout")]
        public async Task<ActionResult<Response>> CheckoutCart(Order order)
        {
            Response response;
            try
            {
                await orderRepo.CheckoutCart(order);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Order successfully placed"
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
        public async Task<ActionResult<Response>> UpdateCartItem(Cart updatedItem)
        {
            Response response;
            try
            {
                await orderRepo.UpdateCartItem(updatedItem);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Cart updated successfully"
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


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cart>> GetCartItems(int id)
        {
            try
            {
                var cartItems = await orderRepo.GetCartItems(id);
                return Ok(cartItems);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> DeleteCartItem(int id)
        {
            Response response;
            try
            {
                await orderRepo.DeleteCartItem(id);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Item removed from cart successfully"
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
    }
}
