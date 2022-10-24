using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL.Repositories
{
    public class JobRepository : IRepository<Job>
    {
        private readonly ApplicationDbContext _dbContext;

        public JobRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<int> AddAsync(Job entity)
        {
            await _dbContext.Job.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Job entity)
        {
            _dbContext.Job.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Job>> GetAllAsync() => await _dbContext.Job.ToListAsync();

        public async Task<Job> GetAsync(int id) => await _dbContext.Job.FirstOrDefaultAsync(x => x.JobID == id);

        public async Task<int> UpdateAsync(Job entity)
        {
            var entityEntry = _dbContext.Job.Attach(entity);
            entityEntry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
