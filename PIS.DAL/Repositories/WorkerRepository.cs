using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public class WorkerRepository : IRepository<Worker>
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<int> AddAsync(Worker entity)
        {
            await _dbContext.Worker.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Worker entity)
        {
            _dbContext.Worker.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Worker>> GetAllAsync() => await _dbContext.Worker.ToListAsync();

        public async Task<Worker> GetAsync(int id) => await _dbContext.Worker.FirstOrDefaultAsync(x => x.WorkerID == id);
        
        public async Task<int> UpdateAsync(Worker entity)
        {
            var entityEntry = _dbContext.Worker.Attach(entity);
            entityEntry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
