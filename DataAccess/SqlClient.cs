using System;
using System.Linq;

namespace DataAccess
{
    public class SqlClient
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
            var allROO = _dbContext.ResidentialOperatingOffice.ToList();
            var allWorkers = _dbContext.Worker.ToList();
            var allJobs = _dbContext.Job.ToList();

            var firstROO = allROO.First();

            var workersByROO = allWorkers.Where(x => x.ROOName == firstROO.ShortName).ToList();

            workersByROO.ForEach(worker =>
            {
                var workerJobs = worker.WorkerJob
                                       .ToList()
                                       .Select(j => j.Job.Description);

                Console.WriteLine($"\n[{worker.WorkerID}] {worker.Name}\t| {worker.ROOName}\t| {string.Join(", ", workerJobs)}");
            });
        }

        public void InsertExample()
        {
            var newROO = new ResidentialOperatingOffice
            {
                City = "Rivne",
                ShortName = "ZHEK-#111",
                LongName = "Long name for ZHEK-#111"
            };

            var newWorker = new Worker
            {
                ResidentialOperatingOffice = newROO,
                Name = "Bohdan Marko",
                ROOName = "ZHEK-#111"
            };

            _dbContext.ResidentialOperatingOffice.Add(newROO);
            _dbContext.Worker.Add(newWorker);
            _dbContext.SaveChanges();

            var allROO = _dbContext.ResidentialOperatingOffice.ToList();
            var allWorkers = _dbContext.Worker.ToList();

            Console.WriteLine($"ROOs: {allROO.Count}, workers: {allWorkers.Count}");
        }

        public void UpdateExample()
        {
            var rooNameToUpdate = "ZHEK-#111";

            var rooToUpdate = _dbContext.ResidentialOperatingOffice.Where(x => x.ShortName == rooNameToUpdate).ToList().Single();
            rooToUpdate.LongName = "Updated name for ZHEK #111";
            rooToUpdate.Worker.First().Name = "Roman Ivanstiv";

            _dbContext.SaveChanges();
        }

        public void DeleteExample()
        {
            var rooNameToDelete = "ZHEK-#111";

            var rooToDelete = _dbContext.ResidentialOperatingOffice.Where(x => x.ShortName == rooNameToDelete).ToList().Single();

            _dbContext.Worker.Remove(rooToDelete.Worker.First());
            _dbContext.ResidentialOperatingOffice.Remove(rooToDelete);

            _dbContext.SaveChanges();
        }

    }
}