using FluentValidation.Results;
using PIS.Lab4.Validators;
using System;
using System.Collections.Generic;

namespace PIS.Lab4.Models
{
    public class Workplace
    {
        public int WorkplaceID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string City { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }

    public static class WorkplaceExtension
    {
        public static bool Validate(this Workplace workplace)
        {
            var validator = new WorkplaceValidator();

            ValidationResult results = validator.Validate(workplace);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            return results.IsValid;
        }

        public static string ToString(this Workplace workplace) 
            => $"[{workplace.WorkplaceID}]\t{workplace.ShortName}\t{workplace.LongName}\t{workplace.City}";
    }
}
