using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Pets;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.Pets
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> _petRepository;

        public PetService(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _petRepository.Table.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            return await _petRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Pet entity)
        {
            await _petRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Pet entity)
        {
            await _petRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Pet entity)
        {
            await _petRepository.DeleteAsync(entity);
        }
    }
}