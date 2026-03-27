using MyVillageApp.Domain.Families;

namespace MyVillageApp.Services.Interfaces
{
    public interface IFamilyPetService
    {
        Task<List<FamilyPetMap>> GetByFamilyIdAsync(int familyId);
        Task SaveFamilyPetsAsync(int familyId, List<FamilyPetMap> pets);
        Task DeleteByFamilyIdAsync(int familyId);
    }
}