using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Families;
using MyVillageApp.Domain.FamilyMembers;
using MyVillageApp.Models.Admin;

public class FamilyService : IFamilyService
{
    private readonly IRepository<Family> _familyRepository;

    public FamilyService(IRepository<Family> familyRepository)
    {
        _familyRepository = familyRepository;
    }

    async Task<List<Family>> IFamilyService.GetAllAsync()
    {
        return await _familyRepository.Table.ToListAsync();
    }

    async Task<Family> IFamilyService.GetByIdAsync(int id)
    {
        return await _familyRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    async Task IFamilyService.InsertAsync(Family entity)
    {
        await _familyRepository.InsertAsync(entity);
    }

    async Task IFamilyService.UpdateAsync(Family entity)
    {
        await _familyRepository.UpdateAsync(entity);
    }

    async Task IFamilyService.DeleteAsync(Family entity)
    {
        await _familyRepository.DeleteAsync(entity);
    }

    public async Task<Family> GetFamilyByRationCardNumberAsync(string rationCardNumber)
    {
        var familyalready = await _familyRepository.Table
             .FirstOrDefaultAsync(x => x.RationCardNumber == rationCardNumber);
        if (familyalready == null)
            return null;
        else return familyalready;
    }
    public async Task<List<Family>> SearchFamiliesAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return await _familyRepository.Table.ToListAsync();

        searchText = searchText.Trim();

        return await _familyRepository.Table
            .Where(x => x.OwnerName.Contains(searchText))
            .ToListAsync();
    }


}
    