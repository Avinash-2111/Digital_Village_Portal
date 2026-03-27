using MyVillageApp.Domain.Announcements;

public interface IAnnouncementService
{
    Task<List<Announcement>> GetAllAsync();
    Task<Announcement> GetByIdAsync(int id);
    Task InsertAsync(Announcement entity);
    Task UpdateAsync(Announcement entity);
    Task DeleteAsync(Announcement entity);
}