using Microsoft.Extensions.Configuration;
using PIS.Lab3;
using PIS.Lab3.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();

SqlDataAdapter rooDataAdapter;
SqlDataAdapter workerDataAdapter;
SqlDataAdapter jobDataAdapter;
SqlDataAdapter workerJobDataAdapter;

using var dbContext = new DbContext(configuration);

ShowDatabaseTables();

#region Point 4

dbContext.Insert(Table.ResidentialOperatingOffice, new List<ResidentialOperatingOffice>
{
    new ResidentialOperatingOffice{ ShortName = "New ROO object", LongName = "New ROO object", City = "Lviv" }
});

dbContext.Insert(Table.Worker, new List<Worker>
{
    new Worker { Name = "New Added Worker 1", RooName = "New ROO object" },
    new Worker { Name = "New Added Worker 2", RooName = "New ROO object" }
});

dbContext.Update(Table.Worker, query: "SET Name = 'Updated Name for Added Worker' WHERE ROOName = 'New ROO object'");

dbContext.Delete(Table.WorkerJob, query: "WHERE WorkerID IN ('3')");

dbContext.Delete(Table.Worker, query: "WHERE WorkerID IN ('3')");

Console.WriteLine($"After manipulation with table {Table.Worker}:");
ShowDatabaseTables();

#endregion

#region Point 5

var dataset = CreateAndFillDataSet("pis_lab3");

ShowDataSetTables();

#endregion

#region Point 6

ChangeDataSetWorkerTable();

workerDataAdapter.UpdateCommand = new SqlCommandBuilder(workerDataAdapter).GetUpdateCommand();
workerDataAdapter.Update(dataset, Table.Worker.ToString());

ShowDatabaseTables();

#endregion

void ShowDatabaseTables()
{
    Console.WriteLine($"Table: {Table.Worker}");
    dbContext.SelectWorkers()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

    Console.WriteLine($"Table: {Table.Job}");
    dbContext.SelectJobs()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

    Console.WriteLine($"Table: {Table.WorkerJob}");
    dbContext.SelectWorkerJobs()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

    Console.WriteLine($"Table: {Table.ResidentialOperatingOffice}");
    dbContext.SelectResidentialOperatingOffices()
        .ForEach(item => Console.WriteLine(item.ToString()));
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
}

void ShowDataSetTables()
{
    Console.WriteLine("Print tables from DataSet object:");
    foreach (DataTable table in dataset.Tables)
    {
        ShowTable(table);
    }
}

void ShowTable(DataTable table)
{
    ArgumentNullException.ThrowIfNull(table);

    StringBuilder builder = new();

    builder.AppendLine($"Table: [{table.TableName}]");
    foreach (DataColumn col in table.Columns)
    {
        builder.AppendFormat("{0,-14}", col.ColumnName);
    }
    builder.Append('\n');

    foreach (DataRow row in table.Rows)
    {
        if (row.RowState != DataRowState.Deleted)
        {
            foreach (DataColumn col in table.Columns)
            {
                builder.AppendFormat("{0,-14}", row[col]);
            }
            builder.Append('\t');
        }
        builder.Append($"{row.RowState}\n");
    }

    Console.WriteLine(builder.ToString());
}

DataSet CreateAndFillDataSet(string name)
{
    var dataset = new DataSet(name);

    rooDataAdapter = new SqlDataAdapter(dbContext.SelectCommand(Table.ResidentialOperatingOffice));
    workerDataAdapter = new SqlDataAdapter(dbContext.SelectCommand(Table.Worker));
    jobDataAdapter = new SqlDataAdapter(dbContext.SelectCommand(Table.Job));
    workerJobDataAdapter = new SqlDataAdapter(dbContext.SelectCommand(Table.WorkerJob));

    rooDataAdapter.Fill(dataset, Table.ResidentialOperatingOffice.ToString());
    workerDataAdapter.Fill(dataset, Table.Worker.ToString());
    jobDataAdapter.Fill(dataset, Table.Job.ToString());
    workerJobDataAdapter.Fill(dataset, Table.WorkerJob.ToString());

    return dataset;
}

void ChangeDataSetWorkerTable()
{
    var workerTable = dataset.Tables[Table.Worker.ToString()];

    //Delete row
    var rowsToDelete = workerTable.Select("Name = 'Updated Name for Added Worker'").ToList();
    rowsToDelete.ForEach(row => row.Delete());

    //Add row
    workerTable.Rows.Add(new Random().Next(20, 100), "New Added Worker via DataSet", "LKPZ");

    //Update row
    var rowToUpdate = workerTable.Select("Name = 'New Added Worker via DataSet'").FirstOrDefault();
    rowToUpdate!["Name"] = "Updated Worker Name via DataSet";
}