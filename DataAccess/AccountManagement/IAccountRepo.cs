using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.AccountManagement
{
    public interface IAccountRepo
    {
        public Task SignUp(User user);
        public Task<User> SignIn(SignInRequest credential);
    }
}
