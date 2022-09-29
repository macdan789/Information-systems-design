using Microsoft.Extensions.Configuration;
using PIS.Lab3;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();

using var dbContext = new DbContext(configuration);

ShowDatabaseTables();

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