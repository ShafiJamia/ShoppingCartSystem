using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingCartSystem.DataAccess.ProductManagement;
using ShoppingCartSystem.Identity;
using ShoppingCartSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ShoppingCartSystem.DataAccess.AccountManagement
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public AccountRepo(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                var users = await appDbContext.Users.ToListAsync();
                if(users == null)
                {
                    throw new Exception("No users found");
                }
                return users;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SignIn(SignInRequest credential)
        {
            try
            {
                if (credential == null)
                {
                    throw new Exception("Request body is empty");
                }
                var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == credential.Email && u.Password == credential.Password);
                if (user == null)
                {
                    throw new InvalidCredentialException("Invalid Credentials");
                }

                var secretKey = configuration["JwtConfig:Key"]!;


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SignUp(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("Request body is empty");
                }

                var existingUser = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (existingUser != null)
                {
                    throw new Exception("This mail id is already in use");
                }

                var userList = await appDbContext.Users.ToListAsync();               

                if(userList.Count == 0) //The first user will be made admin, perks of being a beta user
                {
                    user.IsAdmin = true;
                }
                else
                {
                    user.IsAdmin = false;
                }

                await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpgradeUserToAdmin(int userId)
        {
            try
            {
                var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if(user == null)
                {
                    throw new Exception("No user found");
                }
                user.IsAdmin = true;
                appDbContext.Users.Update(user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
