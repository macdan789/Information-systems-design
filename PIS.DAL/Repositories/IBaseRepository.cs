using PIS.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
