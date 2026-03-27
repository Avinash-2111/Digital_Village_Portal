using Microsoft.EntityFrameworkCore;
using MyVillageApp.Domain.Professions;
using MyVillageApp.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class ProfessionService : IProfessionService
{
    private readonly IRepository<Profession> _professionRepository;

    public ProfessionService(IRepository<Profession> professionRepository)
    {
        _professionRepository = professionRepository;
    }

    public async Task<List<Profession>> GetAllProfessionsAsync()
    {
        return await _professionRepository.Table.ToListAsync();
    }

    public async Task<Profession> GetProfessionByIdAsync(int id)
    {
        return await _professionRepository. Table.FirstOrDefaultAsync(x => x.Id == id); 
    }

    public async Task InsertProfessionAsync(Profession profession)
    {
        await _professionRepository.InsertAsync(profession);
    }
   public async Task<Profession> GetProfeesionByNameAsync(string professionName)
    {
       return await _professionRepository.Table.FirstOrDefaultAsync(x => x.ProfessionName == professionName);
       
    }

    public async Task UpdateProfessionAsync(Profession profession)
    {
        await _professionRepository.UpdateAsync(profession);
    }

    public async Task DeleteProfessionAsync(Profession profession)
    {
        await _professionRepository.DeleteAsync(profession);
    }
}