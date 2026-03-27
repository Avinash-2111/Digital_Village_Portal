using MyVillageApp.Domain.Pensions;

public interface IPensionService
{
    Task<List<Pension>> GetAllAsync();

    Task<Pension> GetByIdAsync(int id);

    Task InsertAsync(Pension pension);

    Task UpdateAsync(Pension pension);

    Task DeleteAsync(Pension pension);
}