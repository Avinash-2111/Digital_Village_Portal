using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Celebrations;
using MyVillageApp.Domain.Wishes;

public class CelebrationService : ICelebrationService
{
    private readonly IRepository<Celebration> _celebrationRepository;

    public CelebrationService(IRepository<Celebration> celebrationRepository)
    {
        _celebrationRepository = celebrationRepository;
    }

    public async Task<List<Celebration>> GetAllAsync()
    {
        return await _celebrationRepository.Table.OrderByDescending(x => x.CelebrationName).ToListAsync();
    }

    public async Task<Celebration> GetByIdAsync(int id)
    {
        return await _celebrationRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Celebration entity)
    {
        await _celebrationRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(Celebration entity)
    {
        await _celebrationRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Celebration entity)
    {
        await _celebrationRepository.DeleteAsync(entity);
    }
}