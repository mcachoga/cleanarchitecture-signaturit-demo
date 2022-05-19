using Signaturit.Application.Features.Trials.Commands.Update;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace Signaturit.Application.Validators
{
    public static class UpdateTrialValidatorExtensions
    {
        public static IRuleBuilderOptions<T, UpdateTrialCommand> IsValidSignature<T>(this IRuleBuilder<T, UpdateTrialCommand> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ValidSignaturePropertyValidator());
        }

        public class ValidSignaturePropertyValidator : PropertyValidator
        {
            public ValidSignaturePropertyValidator() : base("Only one of them can have the (#). More or 2 (#) char can be in same sign")
            {
            }

            private static int NumOfSpecialChar(string cadena)
            {
                char[] charAllowed = "#".ToCharArray();
                char[] data = cadena.ToCharArray();

                int count = 0;
                foreach (var ch in data)
                {
                    if (charAllowed.Contains(ch)) count++;
                }

                return count;
            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                UpdateTrialCommand current = (UpdateTrialCommand)context.PropertyValue;

                int numD = NumOfSpecialChar(current.Defense);
                int numP = NumOfSpecialChar(current.Prosecutor);

                if (numD == 0 && numP == 0)
                    return true;

                if ((numD == 0 && numP == 1) || numP == 0 && numD == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}