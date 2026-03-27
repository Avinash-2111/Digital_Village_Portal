using MyVillageApp.Domain.Schemes;

public interface ISchemeService
{
    Task<List<Scheme>> GetAllSchemesAsync();

    Task<Scheme> GetSchemeByIdAsync(int id);

    Task InsertSchemeAsync(Scheme scheme);

    Task UpdateSchemeAsync(Scheme scheme);

    Task DeleteSchemeAsync(Scheme scheme);
}