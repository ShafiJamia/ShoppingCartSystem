using Microsoft.EntityFrameworkCore;
using ShoppingCartSystem.Models;
using System.Net;
using System.Security.Authentication;

namespace ShoppingCartSystem.DataAccess.AccountManagement
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext appDbContext;

        public AccountRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<User> SignIn(SignInRequest credential)
        {
            try
            {
                if (credential == null)
                {
                    throw new Exception("Request body is empty");
                }
                var user = await appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == credential.Email && u.Password == credential.Password);
                if (user == null)
                {
                    throw new InvalidCredentialException("Invalid Credentials");
                }
                return user;
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

                if (user.Role != null)
                {
                    user.Role = null;
                }

                if(userList.Count == 0)
                {
                    user.RoleId = 1;
                }
                else
                {
                    user.RoleId = 2;
                }

                await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
