using MyVillageApp.Domain.Pets;

namespace MyVillageApp.Services.Interfaces
{
    public interface IPetService
    {
        Task<List<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(int id);
        Task InsertAsync(Pet entity);
        Task UpdateAsync(Pet entity);
        Task DeleteAsync(Pet entity);
    }
}