using MyVillageApp.Domain.Schemes;
using MyVillageApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class SchemeService : ISchemeService
{
    private readonly IRepository<Scheme> _schemeRepository;

    public SchemeService(IRepository<Scheme> schemeRepository)
    {
        _schemeRepository = schemeRepository;
    }

    public async Task<List<Scheme>> GetAllSchemesAsync()
    {
        var query= await _schemeRepository.Table.ToListAsync();
        if (query != null)
            return query;
        else
            return null;
    }

    public async Task<Scheme> GetSchemeByIdAsync(int id)
    {
        return await _schemeRepository.Table.FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task InsertSchemeAsync(Scheme scheme)
    {
        await _schemeRepository.InsertAsync(scheme);
    }

    public async Task UpdateSchemeAsync(Scheme scheme)
    {
        await _schemeRepository.UpdateAsync(scheme);
    }

    public async Task DeleteSchemeAsync(Scheme scheme)
    {
        await _schemeRepository.DeleteAsync(scheme);
    }
}