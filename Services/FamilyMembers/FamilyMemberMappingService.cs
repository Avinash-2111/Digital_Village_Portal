using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Domain.FamilyMembers;
using MyVillageApp.Domain.Pensions;
using MyVillageApp.Domain.Professions;
using MyVillageApp.Domain.Schemes;
using MyVillageApp.Domain.VehicleTypes;
using MyVillageApp.Services.Interfaces;


namespace MyVillageApp.Services.FamilyMembers
{
    public class FamilyMemberMappingService : IFamilyMemberMappingService
    {
        private readonly IRepository<FamilyMemberProfessionMap> _professionMapRepository;
        private readonly IRepository<FamilyMemberSchemeMap> _schemeMapRepository;
        private readonly IRepository<FamilyMemberPensionMap> _pensionMapRepository;
        private readonly IRepository<FamilyMemberVehicleTypeMap> _vehicleTypeMapRepository;
        private readonly IRepository<Profession> _professionRepository;
        private readonly IRepository<Scheme> _schemeRepository;
        private readonly IRepository<Pension> _pensionRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;
        public FamilyMemberMappingService(
            IRepository<FamilyMemberProfessionMap> professionMapRepository,
            IRepository<FamilyMemberSchemeMap> schemeMapRepository,
            IRepository<FamilyMemberPensionMap> pensionMapRepository,
            IRepository<FamilyMemberVehicleTypeMap> vehicleTypeMapRepository, IRepository<Profession> professionRepository, IRepository<Scheme> schemeRepository, IRepository<Pension> pensionRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _professionMapRepository = professionMapRepository;
            _schemeMapRepository = schemeMapRepository;
            _pensionMapRepository = pensionMapRepository;
            _vehicleTypeMapRepository = vehicleTypeMapRepository;
            _professionRepository = professionRepository;
            _schemeRepository = schemeRepository;
            _vehicleTypeRepository = vehicleTypeRepository; 
            _pensionRepository = pensionRepository;
        }

        public async Task SaveProfessionsAsync(int familyMemberId, List<int> professionIds)
        {
            var oldMaps = await _professionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in oldMaps)
                await _professionMapRepository.DeleteAsync(item);

            if (professionIds != null)
            {
                foreach (var id in professionIds.Distinct())
                {
                    await _professionMapRepository.InsertAsync(new FamilyMemberProfessionMap
                    {
                        FamilyMemberId = familyMemberId,
                        ProfessionId = id
                    });
                }
            }
        }

        public async Task SaveSchemesAsync(int familyMemberId, List<int> schemeIds)
        {
            var oldMaps = await _schemeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in oldMaps)
                await _schemeMapRepository.DeleteAsync(item);

            if (schemeIds != null)
            {
                foreach (var id in schemeIds.Distinct())
                {
                    await _schemeMapRepository.InsertAsync(new FamilyMemberSchemeMap
                    {
                        FamilyMemberId = familyMemberId,
                        SchemeId = id
                    });
                }
            }
        }

        public async Task SavePensionsAsync(int familyMemberId, List<int> pensionIds)
        {
            var oldMaps = await _pensionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in oldMaps)
                await _pensionMapRepository.DeleteAsync(item);

            if (pensionIds != null)
            {
                foreach (var id in pensionIds.Distinct())
                {
                    await _pensionMapRepository.InsertAsync(new FamilyMemberPensionMap
                    {
                        FamilyMemberId = familyMemberId,
                        PensionId = id
                    });
                }
            }
        }

        public async Task SaveVehicleTypesAsync(int familyMemberId, List<int> vehicleTypeIds)
        {
            var oldMaps = await _vehicleTypeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in oldMaps)
                await _vehicleTypeMapRepository.DeleteAsync(item);

            if (vehicleTypeIds != null)
            {
                foreach (var id in vehicleTypeIds.Distinct())
                {
                    await _vehicleTypeMapRepository.InsertAsync(new FamilyMemberVehicleTypeMap
                    {
                        FamilyMemberId = familyMemberId,
                        VehicleTypeId = id
                    });
                }
            }
        }

        public async Task<List<int>> GetProfessionIdsAsync(int familyMemberId)
        {
            return await _professionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.ProfessionId)
                .ToListAsync();
        }

        public async Task<List<int>> GetSchemeIdsAsync(int familyMemberId)
        {
            return await _schemeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.SchemeId)
                .ToListAsync();
        }

        public async Task<List<int>> GetPensionIdsAsync(int familyMemberId)
        {
            return await _pensionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.PensionId)
                .ToListAsync();
        }

        public async Task<List<int>> GetVehicleTypeIdsAsync(int familyMemberId)
        {
            return await _vehicleTypeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.VehicleTypeId)
                .ToListAsync();
        }
        public async Task<List<string>> GetProfessionNamesAsync(int familyMemberId)
        {
            var professionIds = await _professionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.ProfessionId)
                .ToListAsync();

            return await _professionRepository.Table
                .Where(x => professionIds.Contains(x.Id))
                .Select(x => x.ProfessionName)
                .ToListAsync();
        }

        public async Task<List<string>> GetSchemeNamesAsync(int familyMemberId)
        {
            var schemeIds = await _schemeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.SchemeId)
                .ToListAsync();

            return await _schemeRepository.Table
                .Where(x => schemeIds.Contains(x.Id))
                .Select(x => x.SchemeName)
                .ToListAsync();
        }

        public async Task<List<string>> GetPensionNamesAsync(int familyMemberId)
        {
            var pensionIds = await _pensionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.PensionId)
                .ToListAsync();

            return await _pensionRepository.Table
                .Where(x => pensionIds.Contains(x.Id))
                .Select(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<string>> GetVehicleTypeNamesAsync(int familyMemberId)
        {
            var vehicleTypeIds = await _vehicleTypeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .Select(x => x.VehicleTypeId)
                .ToListAsync();

            return await _vehicleTypeRepository.Table
                .Where(x => vehicleTypeIds.Contains(x.Id))
                .Select(x => x.VehicleTypeName)
                .ToListAsync();
        }
        public async Task DeleteAllMappingsAsync(int familyMemberId)
        {
            var professionMaps = await _professionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in professionMaps)
                await _professionMapRepository.DeleteAsync(item);

            var schemeMaps = await _schemeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in schemeMaps)
                await _schemeMapRepository.DeleteAsync(item);

            var pensionMaps = await _pensionMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in pensionMaps)
                await _pensionMapRepository.DeleteAsync(item);

            var vehicleTypeMaps = await _vehicleTypeMapRepository.Table
                .Where(x => x.FamilyMemberId == familyMemberId)
                .ToListAsync();

            foreach (var item in vehicleTypeMaps)
                await _vehicleTypeMapRepository.DeleteAsync(item);
        }
    }
}