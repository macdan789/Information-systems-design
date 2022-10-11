﻿using PIS.Lab4.DataAccess;
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
            Console.WriteLine($"[{workplace.WorkplaceID}]\t{workplace.ShortName}\t{workplace.LongName}\t{workplace.City}");

            //select all entities from DB
            var workplaces = await context.GetWorkplaces();
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
            foreach (var entity in workplaces)
            {
                Console.WriteLine($"[{entity.WorkplaceID}]\t{entity.ShortName}\t{entity.LongName}\t{entity.City}");
            }
            
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
            Console.WriteLine("Validation examples using FluentValidation:\n");

            ValidationExamples(context);

            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
            Console.WriteLine("Audit example of Worker entity:\n");

            context.AuditWorker(workerId: 1);
        }

        public static async void ValidationExamples(IDbManager context)
        {
            var workplace = await context.GetWorkplace(1);

            var validWorker = new Worker
            {
                Name = "Peter",
                IsAdmin = false,
                WorkplaceID = workplace.WorkplaceID,
                Workplace = workplace
            };

            if (validWorker.Validate())
            {
                var affectedRows = await context.InsertWorker(validWorker);
                Console.WriteLine(affectedRows);
            }

            var invalidJob = new Job
            {
                Description = "HGldYnFdmPzbCaHPxhkc rKWxLlcFKGuNSleIQntn PdmtcKDFibikevzusmgG",
                Priority = 11
            };

            if (invalidJob.Validate())
            {
                var affectedRows = await context.InsertJob(invalidJob);
                Console.WriteLine(affectedRows);
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
                var affectedRows = await context.UpdateWorkplace(updateWorkplace);
                Console.WriteLine(affectedRows);
            }
        }

        public static async Task<int> InsertTestData(IDbManager context)
        {
            List<Workplace> workplaces = new List<Workplace>
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
                if(workplace.Validate())
                {
                    affectedRows += await context.InsertWorkplace(workplace);
                }
            }

            return affectedRows;
        }
    }
}