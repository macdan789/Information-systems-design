using System.Data;
using PIS.Lab3.Models;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PIS.Lab3;

public enum Table
{
    Worker = 1,
    Job,
    WorkerJob,
    ResidentialOperatingOffice
}

public class DbContext : IDisposable
{
    private readonly SqlConnection _connection;
    private const string ConnectionString = "SqlServerConnectionString";

    public DbContext(IConfiguration configuration)
    {
        _connection = new SqlConnection(connectionString: configuration.GetConnectionString(ConnectionString));
        _connection.Open();
    }

    #region SELECT

    public List<Worker> SelectWorkers()
    {
        var command = CreateCommand($"SELECT * FROM dbo.{Table.Worker};");

        using DbDataReader reader = command.ExecuteReader();

        List<Worker> workers = new();
        while (reader.Read())
        {
            workers.Add(new Worker
            {
                Name = reader["Name"].ToString(),
                RooName = reader["ROOName"].ToString(),
                WorkerId = (int)reader["WorkerID"]
            });
        }

        return workers;
    }

    public List<Job> SelectJobs()
    {
        var command = CreateCommand($"SELECT * FROM dbo.{Table.Job};");

        using DbDataReader reader = command.ExecuteReader();

        List<Job> jobs = new();
        while (reader.Read())
        {
            jobs.Add(new Job
            {
                JobId = (int)reader["JobId"],
                Description = reader["Description"].ToString(),
            });
        }

        return jobs;
    }

    public List<WorkerJob> SelectWorkerJobs()
    {
        var command = CreateCommand($"SELECT * FROM dbo.{Table.WorkerJob};");

        using DbDataReader reader = command.ExecuteReader();

        List<WorkerJob> workerJobs = new();
        while (reader.Read())
        {
            workerJobs.Add(new WorkerJob
            {
                Id = (int)reader["ID"],
                WorkerId = (int)reader["WorkerID"],
                JobId = (int)reader["JobId"],
            });
        }

        return workerJobs;
    }

    public List<ResidentialOperatingOffice> SelectResidentialOperatingOffices()
    {
        var command = CreateCommand($"SELECT * FROM dbo.{Table.ResidentialOperatingOffice};");

        using DbDataReader reader = command.ExecuteReader();

        List<ResidentialOperatingOffice> residentialOperatingOffices = new List<ResidentialOperatingOffice>();
        while (reader.Read())
        {
            residentialOperatingOffices.Add(new ResidentialOperatingOffice
            {
                ShortName = reader["ShortName"].ToString(),
                LongName = reader["LongName"].ToString(),
                City = reader["City"].ToString(),
            });
        }

        return residentialOperatingOffices;
    }

    #endregion

    public int Update(Table table, string query)
    {
        var command = CreateCommand($"UPDATE dbo.{table} {query};");

        return command.ExecuteNonQuery();
    }

    public int Delete(Table table, string query)
    {
        var command = CreateCommand($"DELETE FROM dbo.{table} {query};");

        return command.ExecuteNonQuery();
    }

    public int Insert<T>(Table table, List<T> objects) where T : IEntity
    {
        ArgumentNullException.ThrowIfNull(objects);

        (string fields, string values) = (string.Empty, string.Empty);

        //Find out the type of objects
        switch (objects.Single())
        {
            case Worker:
            {
                var dataList = (objects as List<Worker>)!.Select(item => $"('{item.Name}', '{item.RooName}')").ToList();

                fields = "Name, ROOName";
                values = string.Join(',', dataList);
                break;
            }
            case Job:
            {
                var dataList = (objects as List<Job>)!.Select(item => $"('{item.Description}')").ToList();

                fields = "Description";
                values = string.Join(',', dataList);
                break;
            }
            case WorkerJob:
            {
                var dataList = (objects as List<WorkerJob>)!.Select(item => $"({item.WorkerId}, {item.JobId})").ToList();

                fields = "WorkerID, JobID";
                values = string.Join(',', dataList);
                break;
            }
            case ResidentialOperatingOffice:
            {
                var dataList = (objects as List<ResidentialOperatingOffice>)!.Select(item => $"('{item.ShortName}', '{item.LongName}', '{item.City}')").ToList();

                fields = "ShortName, LongName, City";
                values = string.Join(',', dataList);
                break;
            }
        }

        var command = CreateCommand($"INSERT INTO dbo.{table}({fields}) VALUES {values};");

        return command.ExecuteNonQuery();
    }

    public object Select(Table table, string columnName)
    {
        var command = CreateCommand($"SELECT TOP 1 {columnName} FROM dbo.{table};");
        
        return command.ExecuteScalar();
    }

    private DbCommand CreateCommand(string query)
    {
        return new SqlCommand
        {
            Connection = _connection,
            CommandType = CommandType.Text,
            CommandText = query
        };
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}