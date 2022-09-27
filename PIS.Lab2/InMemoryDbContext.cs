using System.Data;

namespace PIS.Lab2;

public class InMemoryDbContext
{
    public DataSet DataSet { get; set; }
    public DataTable Job { get; set; }
    public DataTable Worker { get; set; }
    public DataTable WorkerJob { get; set; }
    public DataTable ResidentialOperatingOffice { get; set; }

    /// <summary>
    /// Ctor does all basic initialization: creates tables, inserts test data, creates data set and set up some relations of tables
    /// </summary>
    public InMemoryDbContext()
    {
        Job = CreateTableJob();
        Worker = CreateTableWorker();
        WorkerJob = CreateTableWorkerJob();
        ResidentialOperatingOffice = CreateTableResidentialOperatingOffice();

        DataSet = new DataSet();
        DataSet.Tables.AddRange(new DataTable[] { Job, Worker, WorkerJob, ResidentialOperatingOffice });

        DataSet.Relations.Add("ROO_Worker", ResidentialOperatingOffice.Columns["ShortName"], Worker.Columns["ROOName"], true);
        DataSet.Relations.Add("Worker_WorkerJob", Worker.Columns["WorkerID"], WorkerJob.Columns["WorkerID"], true);
        DataSet.Relations.Add("Job_WorkerJob", Job.Columns["JobID"], WorkerJob.Columns["JobID"], true);

        InsertTestData();
    }

    #region Private Methods

    private DataTable CreateTableJob()
    {
        DataTable orderDetailTable = new("Job");

        DataColumn[] columns =
        {
            new DataColumn("JobID", typeof(int)),
            new DataColumn("Description", typeof(string))
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["JobID"] };
        return orderDetailTable;
    }

    private DataTable CreateTableWorker()
    {
        DataTable orderDetailTable = new("Worker");

        DataColumn[] columns =
        {
            new DataColumn("WorkerID", typeof(int)),
            new DataColumn("Name", typeof(string)),
            new DataColumn("ROOName", typeof(string)),
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["WorkerID"] };
        return orderDetailTable;
    }

    private DataTable CreateTableWorkerJob()
    {
        DataTable orderDetailTable = new("WorkerJob");

        DataColumn[] columns =
        {
            new DataColumn("ID", typeof(int)),
            new DataColumn("WorkerID", typeof(int)),
            new DataColumn("JobID", typeof(int))
        };

        columns[0].AutoIncrement = true;
        columns[0].AutoIncrementSeed = 1;
        columns[0].AutoIncrementStep = 1;

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["WorkerID"], orderDetailTable.Columns["JobID"] };
        return orderDetailTable;
    }

    private DataTable CreateTableResidentialOperatingOffice()
    {
        DataTable orderDetailTable = new("ResidentialOperatingOffice");

        DataColumn[] columns =
        {
            new DataColumn("ShortName", typeof(string)),
            new DataColumn("LongName", typeof(string)),
            new DataColumn("City", typeof(string))
        };

        orderDetailTable.Columns.AddRange(columns);
        orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["ShortName"] };
        return orderDetailTable;
    }

    private void InsertTestData()
    {
        object[] rooRows =
        {
            new object[] { "JEK 1", "Desc of JEK 1", "Lviv" },
            new object[] { "JEK 2", "Desc of JEK 2", "Lviv" },
            new object[] { "JEK 3", "Desc of JEK 3", "Kyiv" },
            new object[] { "JEK 4", "Desc of JEK 4", "Kyiv" },
            new object[] { "JEK 5", "Desc of JEK 5", "Vinnytsia" }
        };
        foreach(object[] row in rooRows)
            ResidentialOperatingOffice.Rows.Add(row);

        object[] workerRows =
        {
            new object[] { null, "Worker 1", "JEK 1" },
            new object[] { null, "Worker 2", "JEK 1" },
            new object[] { null, "Worker 3", "JEK 2" },
            new object[] { null, "Worker 4", "JEK 2" },
            new object[] { null, "Worker 5", "JEK 5" }
        };
        foreach (object[] row in workerRows)
            Worker.Rows.Add(row);

        object[] jobRows =
        {
            new object[] { null, "Job 1" },
            new object[] { null, "Job 2" },
            new object[] { null, "Job 3" },
            new object[] { null, "Job 4" },
            new object[] { null, "Job 5" }
        };
        foreach (object[] row in jobRows)
            Job.Rows.Add(row);

        object[] workerJobRows =
        {
            new object[] { null, 1, 1 },
            new object[] { null, 1, 2 },
            new object[] { null, 1, 3 },
            new object[] { null, 3, 4 },
            new object[] { null, 3, 5 }
        };
        foreach (object[] row in workerJobRows)
            WorkerJob.Rows.Add(row);
    }

    #endregion

    public void ShowTable(DataTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        foreach (DataColumn col in table.Columns)
        {
            Console.Write("{0,-14}", col.ColumnName);
        }
        Console.WriteLine();

        foreach (DataRow row in table.Rows)
        { 
            if (row.RowState != DataRowState.Deleted)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("{0,-14}", row[col]);
                }
                Console.Write('\t');
            }
            Console.Write(row.RowState);
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void ShowAllTables()
    {
        foreach(var table in new DataTable[] { Job, Worker, WorkerJob, ResidentialOperatingOffice })
        {
            ShowTable(table);
        }
    }

    public void SortByDataView()
    {
        EnumerableRowCollection<DataRow> query =
                from obj in ResidentialOperatingOffice.AsEnumerable()
                orderby obj.Field<string>("City")
                select obj;

        DataView view = query.AsDataView();

        Console.WriteLine("Sort by City column:");
        ShowDataView(view);
    }

    public void FilterByDataView()
    {
        EnumerableRowCollection<DataRow> query =
                from obj in ResidentialOperatingOffice.AsEnumerable()
                where obj.Field<string>("City") == "Lviv"
                select obj;

        DataView view = query.AsDataView();

        Console.WriteLine("Filter by City column:");
        ShowDataView(view);
    }

    private void ShowDataView(DataView view)
    {
        for (int i = 0; i < view.Count; i++)
        {
            Console.Write("{0,-14}", view[i]["ShortName"]);
            Console.Write("{0,-14}", view[i]["LongName"]);
            Console.Write("{0,-14}", view[i]["City"]);
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

