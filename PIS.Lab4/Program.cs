using PIS.Lab4.DataAccess;
using PIS.Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIS.Lab4
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new DbManager(new ApplicationDbContext());

            var workplaces = context.GetWorkplaces();
            foreach (var workplace in workplaces)
            {
                Console.WriteLine($"[{workplace.WorkplaceID}]\t{workplace.ShortName}\t{workplace.LongName}\t{workplace.City}");
            }
        }
    }

    public class DbManager : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public DbManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Worker> GetWorkers() => _dbContext.Worker.ToList();
        public List<Job> GetJobs() => _dbContext.Job.ToList();
        public List<Workplace> GetWorkplaces() => _dbContext.Workplace.ToList();

        public int InsertWorkplace(Workplace workplace)
        {
            _dbContext.Workplace.Add(workplace);
            return _dbContext.SaveChanges();
        }

        public int InsertWorker(Worker worker)
        {
            _dbContext.Worker.Add(worker);
            return _dbContext.SaveChanges();
        }

        public int InsertJob(Job job)
        {
            _dbContext.Job.Add(job);
            return _dbContext.SaveChanges();
        }

        public void Dispose() => _dbContext.Dispose();
        
    }
}
