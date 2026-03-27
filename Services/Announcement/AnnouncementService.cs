using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Announcements;
using MyVillageApp.Domain.Wishes;

public class AnnouncementService : IAnnouncementService
{
    private readonly IRepository<Announcement> _anouncementRepository;

    public AnnouncementService(IRepository<Announcement> anouncementRepository)
    {
        _anouncementRepository= anouncementRepository;
    }

    public async Task<List<Announcement>> GetAllAsync()
    {
        return await _anouncementRepository.Table.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<Announcement> GetByIdAsync(int id)
    {
        return await _anouncementRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Announcement entity)
    {
        await _anouncementRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(Announcement entity)
    {
        await _anouncementRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Announcement entity)
    {
        await _anouncementRepository.DeleteAsync(entity);
    }
}