using MyVillageApp.Domain.Celebrations;

public interface ICelebrationMediaService
{
    Task<List<CelebrationMedia>> GetByCelebrationIdAsync(int celebrationId);
    Task InsertAsync(CelebrationMedia entity);
    Task DeleteByCelebrationIdAsync(int celebrationId);
}