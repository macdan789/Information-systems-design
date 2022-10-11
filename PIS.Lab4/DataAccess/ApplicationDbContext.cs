using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PIS.Lab4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Text;
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
            if (await _dbContext.Worker.FindAsync(worker.WorkerID) is Worker found)
            {
                _dbContext.Entry(found).State = EntityState.Detached;
                _dbContext.Worker.Update(worker);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateJob(Job job)
        {
            if (await _dbContext.Job.FindAsync(job.JobID) is Job found)
            {
                _dbContext.Entry(found).State = EntityState.Detached;
                _dbContext.Job.Update(job);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateWorkplace(Workplace workplace)
        {
            if (await _dbContext.Workplace.FindAsync(workplace.WorkplaceID) is Workplace found)
            {
                _dbContext.Entry(found).State = EntityState.Detached;
                _dbContext.Workplace.Update(workplace);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        #endregion
        
        public void Dispose() => _dbContext.Dispose();

        public void AuditWorker(int workerId)
        {
            var manager = _dbContext.Worker.Find(workerId);

            if (manager is null) return;
            
            // Change value directly in the DB
            using var contextDB = new ApplicationDbContext();
            contextDB.Database.ExecuteSqlRawAsync(
                "UPDATE dbo.Worker SET Name += '_DB' WHERE WorkerID = {0}", workerId);

            // Change the current value in memory
            manager.Name += "_Memory";

            string originalValue = _dbContext.Entry(manager).Property(m => m.Name).OriginalValue;
            Console.WriteLine(string.Format("Original Value : {0}", originalValue));

            string currentValue = _dbContext.Entry(manager).Property(m => m.Name).CurrentValue;
            Console.WriteLine(string.Format("Current Value : {0}", currentValue));

            string dbValue = _dbContext.Entry(manager).GetDatabaseValues().GetValue<string>("Name");
            Console.WriteLine(string.Format("DB Value : {0}", dbValue));

            //return old value back
            contextDB.Database.ExecuteSqlRawAsync(
                "UPDATE dbo.Worker SET Name = 'John' WHERE WorkerID = {0}", workerId);
        }
    }
}
