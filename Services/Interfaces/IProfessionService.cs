using MyVillageApp.Domain.Professions;

public interface IProfessionService
{
    Task<List<Profession>> GetAllProfessionsAsync();

    Task<Profession> GetProfessionByIdAsync(int id);

    Task InsertProfessionAsync(Profession profession);

    Task UpdateProfessionAsync(Profession profession);

    Task DeleteProfessionAsync(Profession profession);
    Task<Profession> GetProfeesionByNameAsync(string professionName);
}