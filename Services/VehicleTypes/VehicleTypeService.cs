using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.Families;
using MyVillageApp.Domain.VehicleTypes;
using MyVillageApp.Services.Interfaces;

namespace MyVillageApp.Services.VehicleTypes
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleTypeService(IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<List<VehicleType>> GetAllAsync()
        {
            return await _vehicleTypeRepository.Table.ToListAsync();
        }

        public async Task<VehicleType> GetByIdAsync(int id)
        {
            return await _vehicleTypeRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task InsertAsync(VehicleType entity)
        {
            await _vehicleTypeRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(VehicleType entity)
        {
            await _vehicleTypeRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(VehicleType entity)
        {
            await _vehicleTypeRepository.DeleteAsync(entity);
        }
        public async Task<VehicleType> GetVechileByTypeAsync(string Vechiletype)
        {
            var Vechilealready = await _vehicleTypeRepository.Table
                 .FirstOrDefaultAsync(x => x.VehicleTypeName == Vechiletype);
            if (Vechilealready == null)
                return null;
            else return Vechilealready;
        }
    }
}