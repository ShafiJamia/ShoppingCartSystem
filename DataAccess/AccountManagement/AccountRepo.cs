using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.AccountManagement
{
    public class AccountRepo : IAccountRepo
    {
        public Task<User> SignIn(SignInRequest credential)
        {
            throw new NotImplementedException();
        }

        public Task SignUp(User user)
        {
            throw new NotImplementedException();
        }
    }
}
