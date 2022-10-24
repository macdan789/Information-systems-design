using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class SqlClient : IDisposable
    {
        private readonly pis_lab1Entities _dbContext;

        public SqlClient(pis_lab1Entities dbContext)
        {
            //Main class to interact with your database will automatically be generated as: YOUR_DATABASE_NAME + Entities.cs

            //.tt file extension: a T4 text template is a mixture of text blocks and control logic that can generate a text file.
            //  If you take a look inside the file, you'll probably notice a lot of logic injecting things. This is because this kind of files are used to generate other files.

            _dbContext = dbContext;
        }

        public void SelectExample()
        {
            var allRoo = _dbContext.ResidentialOperatingOffice.ToList();
            var allWorkers = _dbContext.Worker.ToList();

            var workersByRoo = allWorkers.Where(x => x.ROOName == allRoo.First().ShortName).ToList();

            workersByRoo.ForEach(worker =>
            {
                var workerJobs = worker.WorkerJob
                                       .ToList()
                                       .Select(j => j.Job.Description);

                Console.WriteLine($"\n[{worker.WorkerID}] {worker.Name}\t| {worker.ROOName}\t| {string.Join(", ", workerJobs)}");
            });
        }

        public void InsertExample()
        {
            var newRoo = new ResidentialOperatingOffice
            {
                City = "TEST",
                ShortName = "TEST",
                LongName = "TEST"
            };

            var newWorker = new Worker
            {
                ResidentialOperatingOffice = newRoo,
                Name = "TEST",
                ROOName = newRoo.ShortName
            };

            _dbContext.ResidentialOperatingOffice.Add(newRoo);
            _dbContext.Worker.Add(newWorker);
            _dbContext.SaveChanges();

            List<ResidentialOperatingOffice> allRoo = _dbContext.ResidentialOperatingOffice.ToList();
            List<Worker> allWorkers = _dbContext.Worker.ToList();

            Console.WriteLine($"Roos: {allRoo.Count}, Workers: {allWorkers.Count}");
        }

        public void UpdateExample()
        {
            var rooNameToUpdate = "TEST";

            var rooToUpdate = _dbContext.ResidentialOperatingOffice
                .Where(x => x.ShortName == rooNameToUpdate)
                .ToList()
                .Single();

            rooToUpdate.LongName = "NEW TEST VALUE";
            rooToUpdate.Worker?.ToList().ForEach(worker => worker.Name = "NEW TEST VALUE");

            _dbContext.SaveChanges();
        }

        public void DeleteExample()
        {
            var rooNameToDelete = "TEST";

            var rooToDelete = _dbContext.ResidentialOperatingOffice
                .Where(x => x.ShortName == rooNameToDelete)
                .ToList()
                .Single();

            rooToDelete.Worker.ToList()
                .ForEach(worker => _dbContext.Worker.Remove(worker));
            _dbContext.ResidentialOperatingOffice.Remove(rooToDelete);

            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}