using PIS.DAL;
using PIS.DAL.Models;
using PIS.DAL.Repositories;

using ApplicationDbContext dbContext = new();

var jobRepository = new BaseRepository<Job>(dbContext);

var job = await jobRepository.GetAsync(1);
Console.WriteLine($"Job: {job.Description} \t ID: {job.JobID}");

var jobs = await jobRepository.GetAllAsync();
jobs.ForEach(job => Console.WriteLine($"Job: {job.Description} \t ID: {job.JobID}"));

var workplaceRepository = new BaseRepository<Workplace>(dbContext);

var affectedRows = await workplaceRepository.InsertAsync(new Workplace
{
    ShortName = "Test",
    LongName = "Test Test",
    City = "Lviv"
});
Console.WriteLine($"Affected rows: {affectedRows}");