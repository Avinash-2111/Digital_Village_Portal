using Microsoft.EntityFrameworkCore;
using MyVillageApp.Domain.Leaders;
using MyVillageApp.Data.Repositories;

public class LeaderService : ILeaderService
{
    private readonly IRepository<Leader> _leaderRepository;

    public LeaderService(IRepository<Leader> leaderRepository)
    {
        _leaderRepository = leaderRepository;
    }

    public async Task<List<Leader>> GetAllLeadersAsync()
    {
        return await _leaderRepository.Table.ToListAsync();
    }

    public async Task<Leader> GetLeaderByIdAsync(int id)
    {
        return await _leaderRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task InsertLeaderAsync(Leader leader)
    {
        await _leaderRepository.InsertAsync(leader);
    }

    public async Task UpdateLeaderAsync(Leader leader)
    {
        await _leaderRepository.UpdateAsync(leader);
    }

    public async Task DeleteLeaderAsync(Leader leader)
    {
        await _leaderRepository.DeleteAsync(leader);
    }
}
