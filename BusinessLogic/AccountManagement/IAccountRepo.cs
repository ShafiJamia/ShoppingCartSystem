using ShoppingCartSystem.Models;

namespace ShoppingCartSystem.DataAccess.AccountManagement
{
    public interface IAccountRepo
    {
        public Task SignUp(User user);
        public Task<string> SignIn(SignInRequest credential);
        public Task UpgradeUserToAdmin(int userId);
        public Task<List<User>> GetUsers();
    }
}
