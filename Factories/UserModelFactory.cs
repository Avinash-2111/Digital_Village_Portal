using MyVillageApp.Factories.Interfaces;
using MyVillageApp.Models.Account;

namespace MyVillageApp.Factories
{
    public class UserModelFactory : IUserModelFactory
    {
        public RegisterModel PrepareRegisterModel()
        {
            return new RegisterModel();
        }

        public LoginModel PrepareLoginModel()
        {
            return new LoginModel();
        }
    }
}