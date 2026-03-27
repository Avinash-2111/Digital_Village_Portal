using MyVillageApp.Domain.Families;
using MyVillageApp.Models.Admin;

public interface IFamilyService
{
    Task<List<Family>> GetAllAsync();

    Task<Family> GetByIdAsync(int id);

    Task InsertAsync(Family entity);

    Task UpdateAsync(Family entity);

    Task DeleteAsync(Family entity);
    Task<Family> GetFamilyByRationCardNumberAsync(string rationCardNumber);
   
    Task<List<Family>> SearchFamiliesAsync(string searchText);
    
}