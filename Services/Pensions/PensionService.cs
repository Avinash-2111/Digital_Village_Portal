using Microsoft.EntityFrameworkCore;
using MyVillageApp.Domain.Pensions;
using MyVillageApp.Data.Repositories;

public class PensionService : IPensionService
{
    private readonly IRepository<Pension> _pensionRepository;

    public PensionService(IRepository<Pension> pensionRepository)
    {
        _pensionRepository = pensionRepository;
    }

    public async Task<List<Pension>> GetAllAsync()
    {
        return await _pensionRepository.Table.ToListAsync();
    }

    public async Task<Pension> GetByIdAsync(int id)
    {
        return await _pensionRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task InsertAsync(Pension pension)
    {
        await _pensionRepository.InsertAsync(pension);
    }

    public async Task UpdateAsync(Pension pension)
    {
        await _pensionRepository.UpdateAsync(pension);
    }

    public async Task DeleteAsync(Pension pension)
    {
        await _pensionRepository.DeleteAsync(pension);
    }
}