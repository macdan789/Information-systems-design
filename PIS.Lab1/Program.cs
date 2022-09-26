using DataAccess;

namespace PIS.Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new SqlClient(new pis_lab1Entities());

            client.SelectExample();
            client.InsertExample();
            client.UpdateExample();
            client.DeleteExample();
        }
    }
}
