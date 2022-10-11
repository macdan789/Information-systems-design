using PIS.Lab4.DataAccess;
using PIS.Lab4.Models;
using System;
using System.Threading.Tasks;

namespace PIS.Lab4
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IDbManager context = new DbManager(new ApplicationDbContext());

            var workplace = await context.GetWorkplace(1);
            Console.WriteLine($"{workplace.WorkplaceID}\t{workplace.ShortName}\t{workplace.LongName}\t{workplace.City}");
        }
    }
}
