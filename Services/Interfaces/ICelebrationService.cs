using MyVillageApp.Domain.Celebrations;

public interface ICelebrationService
{
    Task<List<Celebration>> GetAllAsync();
    Task<Celebration> GetByIdAsync(int id);
    Task InsertAsync(Celebration entity);
    Task UpdateAsync(Celebration entity);
    Task DeleteAsync(Celebration entity);
}