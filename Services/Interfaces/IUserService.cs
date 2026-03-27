using MyVillageApp.Domain.Users;

namespace MyVillageApp.Services.Interfaces
{
    public interface IUserService
    {
        Task InsertUserAsync(User user);

        Task<User> GetUserByEmailPasswordAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneExistsAsync(string phone);
        Task<User?> GetUserByPhoneAsync(string phone);
        Task UpdateUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int id);



    }
}