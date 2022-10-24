using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public class WorkplaceRepository : IRepository<Workplace>
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkplaceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Workplace entity)
        {
            await _dbContext.Workplace.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Workplace entity)
        {
            _dbContext.Workplace.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Workplace>> GetAllAsync() => await _dbContext.Workplace.ToListAsync();

        public async Task<Workplace> GetAsync(int id) => await _dbContext.Workplace.FirstOrDefaultAsync(x => x.WorkplaceID == id);

        public async Task<int> UpdateAsync(Workplace entity)
        {
            var entityEntry = _dbContext.Workplace.Attach(entity);
            entityEntry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
