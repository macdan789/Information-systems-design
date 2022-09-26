
using PIS.Lab2;
using System.Data;

InMemoryDbContext dbContext = new();

dbContext.ShowTable(dbContext.Job);
dbContext.ShowAllTables();

dbContext.FilterByDataView();
dbContext.SortByDataView();

//Change Row State to 'Unchanged' to all table rows
dbContext.Worker.AcceptChanges();
dbContext.WorkerJob.AcceptChanges();

//Delete row
dbContext.Worker.Rows[0].Delete();
//Add row
dbContext.Worker.Rows.Add(null, "New Added Worker", "JEK 4");
//Add row or update if already exists
dbContext.Worker.LoadDataRow(new object[] { null, "LoadDataRow method", "JEK 4" }, LoadOption.Upsert);
//Update row
var rowToUpdate = dbContext.Worker.Select("Name = 'Worker 2'").FirstOrDefault();
rowToUpdate["Name"] = "Bohdan Marko";
rowToUpdate["ROOName"] = "JEK 3";

Console.WriteLine("Table that represents changes of table 'Worker':"); //4 changed rows
dbContext.ShowTable(dbContext.Worker.GetChanges());
Console.WriteLine("Table that represents changes of table 'WorkerJob':"); //should be 3 deleted rows for deleted 'Worker 1' from Worker table
dbContext.ShowTable(dbContext.WorkerJob.GetChanges());