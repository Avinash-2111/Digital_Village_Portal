using MyVillageApp.Models.Admin;
using System.Linq;

namespace MyVillageApp.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
       
    }
}