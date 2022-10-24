using PIS.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
