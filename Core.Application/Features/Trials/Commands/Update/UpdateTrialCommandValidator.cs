using Signaturit.Application.Validators;
using FluentValidation;
using System.Linq;

namespace Signaturit.Application.Features.Trials.Commands.Update
{
    public class UpdateTrialCommandValidator : AbstractValidator<UpdateTrialCommand>
    {
        public UpdateTrialCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Defense)
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed {MaxLength} characters.")
                .Must(IsPartySignatures).WithMessage("{PropertyName} should be all charactes as: KNV#")
                    .When(q => q.Defense != null && q.Defense.Length > 0);

            RuleFor(p => p.Prosecutor)
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed {MaxLength} characters.")
                .Must(IsPartySignatures).WithMessage("{PropertyName} should be all charactes as: KNV#")
                    .When(q => q.Prosecutor != null && q.Prosecutor.Length > 0);

            RuleFor(x => x)
                .IsValidSignature()
                    .When(x => x.Defense != null && x.Defense.Length > 0)
                    .WithMessage("Only one of them can have the (#) char. More or 1 (#) char can not be in same sign");

        }

        private bool IsPartySignatures(string cadena)
        {
            char[] charAllowed = "KNV#".ToCharArray();
            char[] data = cadena.ToCharArray();

            var isValidChars = true;
            foreach (var ch in data)
            {
                if (!charAllowed.Contains(ch))
                {
                    isValidChars = false;
                    break;
                }
            }

            return isValidChars;
        }
    }
}
