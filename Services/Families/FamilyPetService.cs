using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Families;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.Families
{
    public class FamilyPetService : IFamilyPetService
    {
        private readonly IRepository<FamilyPetMap> _familyPetRepository;

        public FamilyPetService(IRepository<FamilyPetMap> familyPetRepository)
        {
            _familyPetRepository = familyPetRepository;
        }

        public async Task<List<FamilyPetMap>> GetByFamilyIdAsync(int familyId)
        {
            return await _familyPetRepository.Table
                .Where(x => x.FamilyId == familyId)
                .ToListAsync();
        }

        public async Task SaveFamilyPetsAsync(int familyId, List<FamilyPetMap> pets)
        {
            var oldPets = await _familyPetRepository.Table
                .Where(x => x.FamilyId == familyId)
                .ToListAsync();

            foreach (var item in oldPets)
                await _familyPetRepository.DeleteAsync(item);

            if (pets != null)
            {
                foreach (var item in pets.Where(x => x.PetId > 0 && x.PetCount > 0))
                {
                    item.FamilyId = familyId;
                    await _familyPetRepository.InsertAsync(item);
                }
            }
        }

        public async Task DeleteByFamilyIdAsync(int familyId)
        {
            var oldPets = await _familyPetRepository.Table
                .Where(x => x.FamilyId == familyId)
                .ToListAsync();

            foreach (var item in oldPets)
                await _familyPetRepository.DeleteAsync(item);
        }
    }
}