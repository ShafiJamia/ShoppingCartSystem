using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartSystem.DataAccess.AccountManagement;
using ShoppingCartSystem.DataAccess.ProductManagement;
using ShoppingCartSystem.Identity;
using ShoppingCartSystem.Models;
using System.Security.Authentication;

namespace ShoppingCartSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<Response>> SignUp(User user)
        {
            Response response;
            try
            {
                await accountRepo.SignUp(user);
                response = new Response()
                {
                    IsSuccess = true,
                    Message = "Sign up successfull"
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

        [HttpPost("signin")]
        public async Task<ActionResult<JwtResponse>> SignIn(SignInRequest credentials)
        {
            JwtResponse response;
            try
            {
                var jwtToken = await accountRepo.SignIn(credentials);

                response = new JwtResponse()
                {
                    Jwt = jwtToken.ToString(),
                    Message = "Jwt Token generated successfully"
                };

                return response;

            }
            catch (Exception ex)
            {
                response = new JwtResponse()
                {
                    Jwt = null,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }
        [HttpPost("upgradeUser")]
        public async Task<ActionResult<Response>> UpgradeUserToAdmin(int userId)
        {
            Response response;
            try
            {
                await accountRepo.UpgradeUserToAdmin(userId);

                response = new Response()
                {
                    IsSuccess = true,
                    Message = "User successfully upgraded to admin"
                };

                return response;
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
        public async Task<ActionResult<User>> GetUsers()
        {
            try
            {
                var users = await accountRepo.GetUsers();
                return Ok(users);
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
