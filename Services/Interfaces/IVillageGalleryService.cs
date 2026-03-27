using MyVillageApp.Domain.Gallery;

namespace MyVillageApp.Services.Interfaces
{
    public interface IVillageGalleryService
    {
        Task<List<VillageGallery>> GetAllAsync();
        Task<VillageGallery> GetByIdAsync(int id);
        Task InsertAsync(VillageGallery entity);
        Task UpdateAsync(VillageGallery entity);
        Task DeleteAsync(VillageGallery entity);
    }
}