using PIS.DAL;
using PIS.DAL.Models;

using IDbManager dbManager = new DbManager(new ApplicationDbContext());

List<Job> jobs = await dbManager.GetJobs();

Console.WriteLine("Jobs count: " + jobs.Count);

int insertedRows = await dbManager.InsertEntity(new Job { Description = "New Job via Code", Priority = 10 });

Console.WriteLine("Inserted rows: " + insertedRows);

List<Job> jobsAfterInsert = await dbManager.GetJobs();

Console.WriteLine("Jobs count after insert: " + jobsAfterInsert.Count);
Console.WriteLine("Last job:" + "\t" + jobsAfterInsert.Last().Description + " - " + jobsAfterInsert.Last().Priority);