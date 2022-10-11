using PIS.Lab4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.Lab4.DataAccess
{
    public interface IDbManager : IDisposable
    {
        Task<int> DeleteJob(int id);
        Task<int> DeleteWorker(int id);
        Task<int> DeleteWorkplace(int id);
        Task<Job> GetJob(int id);
        Task<List<Job>> GetJobs();
        Task<Worker> GetWorker(int id);
        Task<List<Worker>> GetWorkers();
        Task<Workplace> GetWorkplace(int id);
        Task<List<Workplace>> GetWorkplaces();
        Task<int> InsertJob(Job job);
        Task<int> InsertWorker(Worker worker);
        Task<int> InsertWorkplace(Workplace workplace);
        Task<int> UpdateJob(Job job);
        Task<int> UpdateWorker(Worker worker);
        Task<int> UpdateWorkplace(Workplace workplace);
        public void AuditWorker(int workerId);
    }
}