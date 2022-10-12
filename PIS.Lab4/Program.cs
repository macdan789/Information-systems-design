using PIS.Lab4.DataAccess;
using PIS.Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Lab4
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IDbManager context = new DbManager(new ApplicationDbContext());

            //select one entity from DB using Entity Framework Core
            var workplace = await context.GetWorkplace(1);
            Console.WriteLine(workplace.ToString());

            SplitRow("Get all workplaces:");

            //select all entities from DB
            var workplaces = await context.GetWorkplaces();
            foreach (var entity in workplaces)
            {
                Console.WriteLine(entity.ToString());
            }

            SplitRow("Validation examples using FluentValidation:");

            await ValidationExamples(context);

            SplitRow("Audit example of Worker entity:");

            context.AuditWorker(workerId: 1);
        }

        public static async Task ValidationExamples(IDbManager context)
        {
            var workplace = await context.GetWorkplace(1);

            var validWorker = new Worker
            {
                Name = "Peter",
                EmailAddress = "peter@gmail.com",
                IsAdmin = false,
                WorkplaceID = workplace.WorkplaceID,
                Workplace = workplace
            };

            if (validWorker.Validate())
            {
                var affectedRows = await context.InsertEntity(validWorker);
                Console.WriteLine($"Inserted rows = {affectedRows}");
            }

            var invalidJob = new Job
            {
                Description = "HGldYnFdmPzbCaHPxhkc rKWxLlcFKGuNSleIQntn PdmtcKDFibikevzusmgG", //more than 50 symbols
                Priority = 11 // out of 1-10
            };

            if (invalidJob.Validate())
            {
                var affectedRows = await context.InsertEntity(invalidJob);
                Console.WriteLine($"Inserted rows = {affectedRows}");
            }

            var updateWorkplace = new Workplace
            {
                WorkplaceID = 1,
                City = "Lviv",
                ShortName = "House 1",
                LongName = "Long Name Description"
            };

            if (updateWorkplace.Validate())
            {
                var affectedRows = await context.UpdateEntity(updateWorkplace, workplace.WorkplaceID);
                Console.WriteLine($"Updated rows = {affectedRows}");
            }
        }

        public static async Task<int> InsertTestData(IDbManager context)
        {
            List<Workplace> workplaces = new()
            {
                new Workplace
                {
                    City = "Kyiv",
                    ShortName = "House 2",
                    LongName = "Long Name Description"
                },
                new Workplace
                {
                    City = "Rivne",
                    ShortName = "House 3",
                    LongName = "Long Name Description"
                },
                new Workplace
                {
                    City = "Kyiv",
                    ShortName = "House 4",
                    LongName = "Long Name Description"
                },
                new Workplace
                {
                    City = "Odessa",
                    ShortName = "House 5",
                    LongName = "Long Name Description"
                },
            };
            int affectedRows = 0;
                
            foreach (var workplace in workplaces)
            {
                affectedRows += workplace.Validate() ? await context.InsertEntity(workplace) : 0;
            }

            return affectedRows;
        }

        private static void SplitRow(string text)
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)) + "\n");
            Console.WriteLine(text + "\n");
        }
    }
}
