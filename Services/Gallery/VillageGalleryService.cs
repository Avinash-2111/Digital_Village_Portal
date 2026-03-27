using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Gallery;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.Gallery
{
    public class VillageGalleryService : IVillageGalleryService
    {
        private readonly IRepository<VillageGallery> _galleryRepository;

        public VillageGalleryService(IRepository<VillageGallery> galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<List<VillageGallery>> GetAllAsync()
        {
            return await _galleryRepository.Table.ToListAsync();
        }

        public async Task<VillageGallery> GetByIdAsync(int id)
        {
            return await _galleryRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task InsertAsync(VillageGallery entity)
        {
            await _galleryRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(VillageGallery entity)
        {
            await _galleryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(VillageGallery entity)
        {
            await _galleryRepository.DeleteAsync(entity);
        }
    }
}