using MyVillageApp.Models.Account;

namespace MyVillageApp.Factories.Interfaces
{
    public interface IUserModelFactory
    {
        RegisterModel PrepareRegisterModel();

        LoginModel PrepareLoginModel();
    }
}