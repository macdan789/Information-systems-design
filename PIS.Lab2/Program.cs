using PIS.Lab2;
using System.Data;

InMemoryDbContext dbContext = new();

dbContext.ShowTable(dbContext.Job);
dbContext.ShowAllTables();

dbContext.FilterByDataView(dbContext.ResidentialOperatingOffice, "City", "City1");
dbContext.SortByDataView<string>(dbContext.ResidentialOperatingOffice, "City");

//Change Row State to 'Unchanged' to all table rows
dbContext.Worker.AcceptChanges();
dbContext.WorkerJob.AcceptChanges();

//Delete row
dbContext.Worker.Rows[0].Delete();

//Add row
dbContext.Worker.Rows.Add(null, "NEW Worker", "TEST 1");

//Add row or update if already exists
dbContext.Worker.LoadDataRow(new object[] { null, "LoadDataRow method", "TEST 1" }, LoadOption.Upsert);

//Update row
var rowToUpdate = dbContext.Worker.Select("Name = 'TEST 1'").FirstOrDefault();
rowToUpdate!["Name"] = "New Worker Name";
rowToUpdate!["ROOName"] = "TEST 2";

Console.WriteLine("Table that represents changes of table 'Worker':"); //4 changed rows
dbContext.ShowTable(dbContext.Worker.GetChanges());
Console.WriteLine("Table that represents changes of table 'WorkerJob':"); //should be 3 deleted rows for deleted 'Worker 1' from Worker table
dbContext.ShowTable(dbContext.WorkerJob.GetChanges());