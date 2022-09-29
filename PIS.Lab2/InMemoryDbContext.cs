using System.Data;
using System.Text;

namespace PIS.Lab2;

public class InMemoryDbContext
{
    public DataSet DataSet { get; set; }
    public DataTable Job { get; set; }
    public DataTable Worker { get; set; }
    public DataTable WorkerJob { get; set; }
    public DataTable ResidentialOperatingOffice { get; set; }

    /// <summary>
    /// Ctor does all basic initialization:
    ///     creates DataTables, inserts initial data, creates DataSet and set up some relations of tables
    /// </summary>
    public InMemoryDbContext()
    {
        Job = CreateJob();
        Worker = CreateWorker();
        WorkerJob = CreateWorkerJob();
        ResidentialOperatingOffice = CreateResidentialOperatingOffice();

        DataSet = new DataSet()
        {
            Tables = { Job, Worker, WorkerJob, ResidentialOperatingOffice }
        };

        DataSet.Relations.Add("ROO_Worker", ResidentialOperatingOffice.Columns["ShortName"]!, Worker.Columns["ROOName"]!, true);
        DataSet.Relations.Add("Worker_WorkerJob", Worker.Columns["WorkerID"]!, WorkerJob.Columns["WorkerID"]!, true);
        DataSet.Relations.Add("Job_WorkerJob", Job.Columns["JobID"]!, WorkerJob.Columns["JobID"]!, true);

        InsertTestData();
    }

    #region Private Methods

    private DataTable CreateJob()
    {
        DataTable orderDetailTable = new("Job");

        DataColumn[] columns =
        {
            new("JobID", typeof(int)),
            new("Description", typeof(string))
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new[] { orderDetailTable.Columns["JobID"] };
        return orderDetailTable;
    }

    private DataTable CreateWorker()
    {
        DataTable orderDetailTable = new("Worker");

        DataColumn[] columns =
        {
            new("WorkerID", typeof(int)),
            new("Name", typeof(string)),
            new("ROOName", typeof(string)),
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new[] { orderDetailTable.Columns["WorkerID"] };
        return orderDetailTable;
    }

    private DataTable CreateWorkerJob()
    {
        DataTable orderDetailTable = new("WorkerJob");

        DataColumn[] columns =
        {
            new("ID", typeof(int)),
            new("WorkerID", typeof(int)),
            new("JobID", typeof(int))
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new[] { orderDetailTable.Columns["WorkerID"], orderDetailTable.Columns["JobID"] };
        return orderDetailTable;
    }

    private DataTable CreateResidentialOperatingOffice()
    {
        DataTable orderDetailTable = new("ResidentialOperatingOffice");

        DataColumn[] columns =
        {
            new("ShortName", typeof(string)),
            new("LongName", typeof(string)),
            new("City", typeof(string))
        };

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new[] { orderDetailTable.Columns["ShortName"] };
        return orderDetailTable;
    }

    private void InsertTestData()
    {
        object[] rooRows =
        {
            new object[] { "TEST 1", "Desc of TEST 1", "City1" },
            new object[] { "TEST 2", "Desc of TEST 2", "City1" },
            new object[] { "TEST 3", "Desc of TEST 3", "City2" },
            new object[] { "TEST 4", "Desc of TEST 4", "City2" },
            new object[] { "TEST 5", "Desc of TEST 5", "City3" }
        };
        foreach(object[] row in rooRows)
            ResidentialOperatingOffice.Rows.Add(row);

        object[] workerRows =
        {
            new object[] { null, "Worker 1", "TEST 1" },
            new object[] { null, "Worker 2", "TEST 1" },
            new object[] { null, "Worker 3", "TEST 2" },
            new object[] { null, "Worker 4", "TEST 2" },
            new object[] { null, "Worker 5", "TEST 5" }
        };
        foreach (object[] row in workerRows)
            Worker.Rows.Add(row);

        object[] jobRows =
        {
            new object[] { null, "Job Desc 1" },
            new object[] { null, "Job Desc 2" },
            new object[] { null, "Job Desc 3" },
            new object[] { null, "Job Desc 4" },
            new object[] { null, "Job Desc 5" }
        };
        foreach (object[] row in jobRows)
            Job.Rows.Add(row);

        object[] workerJobRows =
        {
            new object[] { null, 1, 1 },
            new object[] { null, 1, 2 },
            new object[] { null, 1, 3 },
            new object[] { null, 3, 4 },
            new object[] { null, 3, 5 },
            new object[] { null, 4, 4 },
            new object[] { null, 4, 5 },
            new object[] { null, 5, 1 },
            new object[] { null, 5, 2 },
            new object[] { null, 5, 3 }
        };
        foreach (object[] row in workerJobRows)
            WorkerJob.Rows.Add(row);
    }

    #endregion

    public void ShowTable(DataTable table)
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

    public void ShowAllTables()
    {
        foreach(var table in new[] { Job, Worker, WorkerJob, ResidentialOperatingOffice })
        {
            ShowTable(table);
        }
    }

    #region DataView using LINQ Query (also there is DataView fields: RowFilter, RowStateFilter, Sort to query data)

    public void SortByDataView<T>(DataTable table, string column)
    {
        EnumerableRowCollection<DataRow> query =
                from obj in table.AsEnumerable()
                orderby obj.Field<T>(column)
                select obj;

        DataView view = query.AsDataView();

        Console.WriteLine($"Sort by {column}:");
        ShowDataView(view);
    }

    public void FilterByDataView<T>(DataTable table, string column, T value)
    {
        EnumerableRowCollection<DataRow> query =
                from obj in table.AsEnumerable()
                where obj.Field<T>(column)!.Equals(value)
                select obj;

        DataView view = query.AsDataView();

        Console.WriteLine($"Filter by {column}:");
        ShowDataView(view);
    }

    private void ShowDataView(DataView view)
    {
        StringBuilder builder = new();
        for (int i = 0; i < view.Count; i++)
        {
            builder.AppendFormat("{0,-14}", view[i]["ShortName"]);
            builder.AppendFormat("{0,-14}", view[i]["LongName"]);
            builder.AppendFormat("{0,-14}", view[i]["City"]);
            builder.Append('\n');
        }
        Console.WriteLine(builder.ToString());
    }

    #endregion
}