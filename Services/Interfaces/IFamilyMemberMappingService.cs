namespace MyVillageApp.Services.Interfaces
{
    public interface IFamilyMemberMappingService
    {
        Task SaveProfessionsAsync(int familyMemberId, List<int> professionIds);
        Task SaveSchemesAsync(int familyMemberId, List<int> schemeIds);
        Task SavePensionsAsync(int familyMemberId, List<int> pensionIds);
        Task SaveVehicleTypesAsync(int familyMemberId, List<int> vehicleTypeIds);

        Task<List<int>> GetProfessionIdsAsync(int familyMemberId);
        Task<List<int>> GetSchemeIdsAsync(int familyMemberId);
        Task<List<int>> GetPensionIdsAsync(int familyMemberId);
        Task<List<int>> GetVehicleTypeIdsAsync(int familyMemberId);
        Task<List<string>> GetProfessionNamesAsync(int familyMemberId);
        Task<List<string>> GetSchemeNamesAsync(int familyMemberId);
        Task<List<string>> GetPensionNamesAsync(int familyMemberId);
        Task<List<string>> GetVehicleTypeNamesAsync(int familyMemberId);
        Task DeleteAllMappingsAsync(int familyMemberId);
    }
}