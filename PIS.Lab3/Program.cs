// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();

using var connection = new SqlConnection(
    connectionString: configuration.GetConnectionString("SqlServerConnectionString"));

connection.Open();

Select(connection);

connection.Close();



void Select(SqlConnection connection)
{
    var command = connection.CreateCommand();
    command.CommandType = System.Data.CommandType.Text;
    command.CommandText = "SELECT * FROM dbo.Worker;";

    using DbDataReader reader = command.ExecuteReader();

    while (reader.Read())
    {
        Console.WriteLine($"{reader["WorkerID"]}\t\t{reader["Name"]}\t\t{reader["ROOName"]}");
    }
}