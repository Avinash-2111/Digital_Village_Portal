using MyVillageApp.Domain.FamilyMembers;

namespace MyVillageApp.Services.Interfaces
{
    public interface IFamilyMemberService
    {
        Task<List<FamilyMember>> GetByFamilyIdAsync(int familyId);
        Task<FamilyMember> GetByIdAsync(int id);
        Task InsertAsync(FamilyMember entity);
        Task UpdateAsync(FamilyMember entity);
        Task DeleteAsync(FamilyMember entity);
        Task<FamilyMember> GetFamilyMemberByNameAsync(string Name);
        Task<FamilyMember> GetFamilyMemberByAadhaarNumberAsync(string Adharnumber);
        Task<List<FamilyMember>> SearchFamilyMembersAsync(int familyId, string searchText);
        Task<List<FamilyMember>> GetAllAsync();
        Task<List<FamilyMember>> SearchAllFamilyMembersAsync(string searchText);

    }
}