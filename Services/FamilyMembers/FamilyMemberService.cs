using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Families;
using MyVillageApp.Domain.FamilyMembers;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.FamilyMembers
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly IRepository<FamilyMember> _familyMemberRepository;

        public FamilyMemberService(IRepository<FamilyMember> familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<List<FamilyMember>> GetByFamilyIdAsync(int familyId)
        {
            return await _familyMemberRepository.Table
                .Where(x => x.FamilyId == familyId)
                .ToListAsync();
        }

        public async Task<FamilyMember> GetByIdAsync(int id)
        {
            return await _familyMemberRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task InsertAsync(FamilyMember entity)
        {
            await _familyMemberRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(FamilyMember entity)
        {
            await _familyMemberRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FamilyMember entity)
        {
            await _familyMemberRepository.DeleteAsync(entity);
        }
        public async Task<FamilyMember> GetFamilyMemberByNameAsync(string Name)
        {
            var familymemberalready = await _familyMemberRepository.Table
                 .FirstOrDefaultAsync(x => x.Name == Name);
            if (familymemberalready == null)
                return null;
            else return familymemberalready;
        }
        public async Task<FamilyMember> GetFamilyMemberByAadhaarNumberAsync(string Adharnumber)
        {
            var familymemberalready = await _familyMemberRepository.Table
                 .FirstOrDefaultAsync(x => x.AadhaarNumber == Adharnumber);
            if (familymemberalready == null)
                return null;
            else return familymemberalready;
        }
        public async Task<List<FamilyMember>> SearchFamilyMembersAsync(int familyId, string searchText)
        {
            searchText = searchText?.Trim();

            return await _familyMemberRepository.Table
                .Where(x => x.FamilyId == familyId &&
                            x.Name.Contains(searchText))
                .ToListAsync();
        }
        public async Task<List<FamilyMember>> GetAllAsync()
        {
            return await _familyMemberRepository.Table.ToListAsync();
        }
        public async Task<List<FamilyMember>> SearchAllFamilyMembersAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return await _familyMemberRepository.Table.ToListAsync();

            searchText = searchText.Trim();

            return await _familyMemberRepository.Table
                .Where(x => x.Name.Contains(searchText))
                .ToListAsync();
        }
    }
}