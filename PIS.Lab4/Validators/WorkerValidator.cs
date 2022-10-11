using FluentValidation;
using PIS.Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.Lab4.Validators
{
    public class WorkerValidator : AbstractValidator<Worker>
    {
        public WorkerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is too long");
            RuleFor(x => x.IsAdmin).NotEmpty().WithMessage("IsAdmin is required");
        }
    }
}
