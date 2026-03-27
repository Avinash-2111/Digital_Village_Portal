using MyVillageApp.Domain.VehicleTypes;

namespace MyVillageApp.Services.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<List<VehicleType>> GetAllAsync();
        Task<VehicleType> GetByIdAsync(int id);
        Task InsertAsync(VehicleType entity);
        Task UpdateAsync(VehicleType entity);
        Task DeleteAsync(VehicleType entity);
        Task<VehicleType> GetVechileByTypeAsync(string Vechiletype);
    }
}