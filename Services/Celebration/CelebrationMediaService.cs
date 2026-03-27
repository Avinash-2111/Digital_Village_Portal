using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Celebrations;
using MyVillageApp.Domain.Wishes;

public class CelebrationMediaService : ICelebrationMediaService
{
    private readonly IRepository<CelebrationMedia> _celebrationMediaMediaRepository;

    public CelebrationMediaService(IRepository<CelebrationMedia> celebrationMediaMediaRepository)
    {
        _celebrationMediaMediaRepository = celebrationMediaMediaRepository;
    }

    public async Task<List<CelebrationMedia>> GetByCelebrationIdAsync(int celebrationid)
    {
        return await _celebrationMediaMediaRepository.Table
            .Where(x => x.CelebrationId == celebrationid)
            .ToListAsync();
    }

    public async Task InsertAsync(CelebrationMedia entity)
    {
        await _celebrationMediaMediaRepository.InsertAsync(entity);
    }

    public async Task DeleteByCelebrationIdAsync(int celebrationid)
    {
        var items = await _celebrationMediaMediaRepository.Table.Where(x => x.CelebrationId == celebrationid).ToListAsync();

        foreach (var item in items)
            await _celebrationMediaMediaRepository.DeleteAsync(item);
    }
}