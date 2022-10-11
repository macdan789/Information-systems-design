using FluentValidation.Results;
using PIS.Lab4.Validators;
using System;
using System.Collections.Generic;

namespace PIS.Lab4.Models
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public int WorkplaceID { get; set; }
        public Workplace Workplace { get; set; }
        public List<Job> Jobs { get; set; } = new();
    }

    public static class WorkerExtension
    {
        public static bool Validate(this Worker worker)
        {
            var validator = new WorkerValidator();

            ValidationResult results = validator.Validate(worker);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            return results.IsValid;
        }
    }
}
