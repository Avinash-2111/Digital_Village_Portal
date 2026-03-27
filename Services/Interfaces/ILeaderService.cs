using MyVillageApp.Domain.Leaders;

public interface ILeaderService
{
    Task<List<Leader>> GetAllLeadersAsync();

    Task<Leader> GetLeaderByIdAsync(int id);

    Task InsertLeaderAsync(Leader leader);

    Task UpdateLeaderAsync(Leader leader);

    Task DeleteLeaderAsync(Leader leader);
}