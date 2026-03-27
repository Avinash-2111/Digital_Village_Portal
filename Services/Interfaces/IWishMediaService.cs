using MyVillageApp.Domain.Wishes;

public interface IWishMediaService
{
    Task<List<WishMedia>> GetByWishIdAsync(int wishId);
    Task InsertAsync(WishMedia entity);
    Task DeleteByWishIdAsync(int wishId);
}