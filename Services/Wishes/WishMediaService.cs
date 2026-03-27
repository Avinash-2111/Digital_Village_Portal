using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Wishes;

public class WishMediaService : IWishMediaService
{
    private readonly IRepository<WishMedia> _wishMediaRepository;

    public WishMediaService(IRepository<WishMedia> wishMediaRepository)
    {
        _wishMediaRepository = wishMediaRepository;
    }

    public async Task<List<WishMedia>> GetByWishIdAsync(int wishId)
    {
        return await _wishMediaRepository.Table
            .Where(x => x.WishId == wishId)
            .ToListAsync();
    }

    public async Task InsertAsync(WishMedia entity)
    {
        await _wishMediaRepository.InsertAsync(entity);
    }

    public async Task DeleteByWishIdAsync(int wishId)
    {
        var items = await _wishMediaRepository.Table.Where(x => x.WishId == wishId).ToListAsync();

        foreach (var item in items)
            await _wishMediaRepository.DeleteAsync(item);
    }
}