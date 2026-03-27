using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Users;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task InsertUserAsync(User user)
        {
            await _userRepository.InsertAsync(user);
        }

        public async Task<User> GetUserByEmailPasswordAsync(string email, string password)
        {
            return await _userRepository.Table
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.Table.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> PhoneExistsAsync(string phone)
        {
            return await _userRepository.Table.AnyAsync(x => x.PhoneNumber == phone);
        }
        public async Task<User?> GetUserByPhoneAsync(string phone)
        {
            return await _userRepository.Table.FirstOrDefaultAsync(x => x.PhoneNumber == phone);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.Table.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.Table
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    } 
}