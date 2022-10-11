using FluentValidation;
using PIS.Lab4.Models;

namespace PIS.Lab4.Validators
{
    public class JobValidator : AbstractValidator<Job>
    {
        public JobValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Description is too long");
            RuleFor(x => x.Priority).InclusiveBetween(1, 10).WithMessage("Priority must be between 1 and 10");
        }
    }
}
