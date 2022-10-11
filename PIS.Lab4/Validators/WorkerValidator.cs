using FluentValidation;
using PIS.Lab4.Models;

namespace PIS.Lab4.Validators
{
    public class WorkerValidator : AbstractValidator<Worker>
    {
        public WorkerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is too long");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.IsAdmin).NotNull().WithMessage("IsAdmin is required");
        }
    }
}
