using Signaturit.Web.Areas.Catalog.Models;
using FluentValidation;

namespace Signaturit.Web.Areas.Catalog.Validators
{
    public class TrialViewModelValidator : AbstractValidator<TrialViewModel>
    {
        public TrialViewModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Defense)
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Prosecutor)
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
        }
    }
}