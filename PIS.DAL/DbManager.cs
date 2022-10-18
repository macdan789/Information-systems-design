using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL
{
    public class DbManager : IDbManager
    {
        private readonly ApplicationDbContext _dbContext;

        public DbManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region select

        public async Task<List<Worker>> GetWorkers() => await _dbContext.Worker.ToListAsync();
        public async Task<List<Job>> GetJobs() => await _dbContext.Job.ToListAsync();
        public async Task<List<Workplace>> GetWorkplaces() => await _dbContext.Workplace.ToListAsync();

        public async Task<Worker> GetWorker(int id) => await _dbContext.Worker.FirstOrDefaultAsync(x => x.WorkerID == id);
        public async Task<Job> GetJob(int id) => await _dbContext.Job.FirstOrDefaultAsync(x => x.JobID == id);
        public async Task<Workplace> GetWorkplace(int id) => await _dbContext.Workplace.FirstOrDefaultAsync(x => x.WorkplaceID == id);

        #endregion

        public async Task<int> InsertEntity<T>(T entity) where T : class, new()
        {
            var type = new T();

            if (type is Worker)
            {
                var worker = entity as Worker;
                await _dbContext.Worker.AddAsync(worker);
            }
            else if (type is Job)
            {
                var job = entity as Job;
                await _dbContext.Job.AddAsync(job);
            }
            else if (type is Workplace)
            {
                var workplace = entity as Workplace;
                await _dbContext.Workplace.AddAsync(workplace);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteEntity<T>(int entityId) where T : class, new()
        {
            var type = new T();

            if (type is Worker)
            {
                var entity = await GetWorker(entityId);
                _dbContext.Worker.Remove(entity);
            }
            if (type is Job)
            {
                var entity = await GetJob(entityId);
                _dbContext.Job.Remove(entity);
            }
            if (type is Workplace)
            {
                var entity = await GetWorkplace(entityId);
                _dbContext.Workplace.Remove(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateEntity<T>(T entity, int entityId) where T : class
        {
            var dbSet = _dbContext.Set<T>();

            if (entity is Worker)
            {
                dbSet = _dbContext.Worker as DbSet<T>;
            }
            if (entity is Job)
            {
                dbSet = _dbContext.Job as DbSet<T>;
            }
            if (entity is Workplace)
            {
                dbSet = _dbContext.Workplace as DbSet<T>;
            }

            if (await dbSet.FindAsync(entityId) is T found)
            {
                _dbContext.Entry(found).State = EntityState.Detached;
                dbSet.Update(entity);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
