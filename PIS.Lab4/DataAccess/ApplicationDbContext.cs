using Microsoft.EntityFrameworkCore;
using PIS.Lab4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.Lab4.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Job> Job { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Workplace> Workplace { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=.;Database=PIS_Lab4_EFCore;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }

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

        #region insert

        public async Task<int> InsertWorkplace(Workplace workplace)
        {
            await _dbContext.Workplace.AddAsync(workplace);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertWorker(Worker worker)
        {
            await _dbContext.Worker.AddAsync(worker);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertJob(Job job)
        {
            await _dbContext.Job.AddAsync(job);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region delete

        public async Task<int> DeleteWorker(int id)
        {
            var worker = await GetWorker(id);

            if (worker != null)
            {
                _dbContext.Worker.Remove(worker);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteJob(int id)
        {
            var job = await GetJob(id);

            if (job != null)
            {
                _dbContext.Job.Remove(job);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteWorkplace(int id)
        {
            var workplace = await GetWorkplace(id);

            if (workplace != null)
            {
                _dbContext.Workplace.Remove(workplace);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        #endregion

        #region update

        public async Task<int> UpdateWorker(Worker worker)
        {
            _dbContext.Worker.Update(worker);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateJob(Job job)
        {
            _dbContext.Job.Update(job);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateWorkplace(Workplace workplace)
        {
            _dbContext.Workplace.Update(workplace);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        public void Dispose() => _dbContext.Dispose();
    }
}
