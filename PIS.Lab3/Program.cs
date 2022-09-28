using Microsoft.Extensions.Configuration;
using PIS.Lab3;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();

using var dbContext = new DbContext(configuration);

ShowDatabaseTables();

var firstRowDescriptionValue = dbContext.Select(Table.Job, "Description");

dbContext.Update(Table.Worker, "SET Name = 'New Name' WHERE WorkerID = 2");
dbContext.Update(Table.Job, "SET Description = 'New Description' WHERE JobID = 2");

//dbContext.Insert(Table.Job, new List<Job> { new() { Description = "New Added Job" } });

//dbContext.Delete(Table.Job, "WHERE Description = 'New Added Job'");

void ShowDatabaseTables()
{
    dbContext.SelectWorkers()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine();
    
    dbContext.SelectJobs()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine();
    
    dbContext.SelectWorkerJobs()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine();
    
    dbContext.SelectResidentialOperatingOffices()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine();
}