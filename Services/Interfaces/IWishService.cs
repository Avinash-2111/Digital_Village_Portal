using MyVillageApp.Domain.Wishes;

public interface IWishService
{
    Task<List<Wish>> GetAllAsync();
    Task<Wish> GetByIdAsync(int id);
    Task InsertAsync(Wish entity);
    Task UpdateAsync(Wish entity);
    Task DeleteAsync(Wish entity);
}