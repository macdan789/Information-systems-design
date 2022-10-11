using FluentValidation.Results;
using PIS.Lab4.Validators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PIS.Lab4.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }

    public static class JobExtension
    {
        public static bool Validate(this Job job)
        {
            var validator = new JobValidator();
            
            ValidationResult results = validator.Validate(job);

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
