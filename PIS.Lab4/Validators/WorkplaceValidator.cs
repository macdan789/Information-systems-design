using FluentValidation;
using PIS.Lab4.Models;

namespace PIS.Lab4.Validators
{
    public class WorkplaceValidator : AbstractValidator<Workplace>
    {
        public WorkplaceValidator()
        {
            RuleFor(x => x.ShortName).NotEmpty().WithMessage("ShortName is required");
            RuleFor(x => x.ShortName).MaximumLength(10).WithMessage("ShortName is too long");
            RuleFor(x => x.LongName).NotEmpty().WithMessage("LongName is required");
            RuleFor(x => x.LongName).MaximumLength(50).WithMessage("LongName is too long");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
        }
    }
}
