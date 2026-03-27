using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Wishes;

public class WishService : IWishService
{
    private readonly IRepository<Wish> _wishRepository;

    public WishService(IRepository<Wish> wishRepository)
    {
        _wishRepository = wishRepository;
    }

    public async Task<List<Wish>> GetAllAsync()
    {
        return await _wishRepository.Table.OrderByDescending(x => x.DateOfCreate).ToListAsync();
    }

    public async Task<Wish> GetByIdAsync(int id)
    {
        return await _wishRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Wish entity)
    {
        await _wishRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(Wish entity)
    {
        await _wishRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Wish entity)
    {
        await _wishRepository.DeleteAsync(entity);
    }
}