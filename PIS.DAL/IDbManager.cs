using PIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIS.DAL
{
    public interface IDbManager : IDisposable
    {
        Task<Job> GetJob(int id);
        Task<List<Job>> GetJobs();
        Task<Worker> GetWorker(int id);
        Task<List<Worker>> GetWorkers();
        Task<Workplace> GetWorkplace(int id);
        Task<List<Workplace>> GetWorkplaces();
        Task<int> InsertEntity<T>(T entity) where T : class, new();
        Task<int> UpdateEntity<T>(T entity, int entityId) where T : class;
        Task<int> DeleteEntity<T>(int entityId) where T : class, new();
    }
}