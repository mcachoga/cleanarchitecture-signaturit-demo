using Signaturit.Application.Features.Trials.Commands.Create;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace Signaturit.Application.Validators
{
    public static class CreateTrialValidatorExtensions
    {
        public static IRuleBuilderOptions<T, CreateTrialCommand> IsValidSignature<T>(this IRuleBuilder<T, CreateTrialCommand> ruleBuilder)
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
                int count = 0;
                if (string.IsNullOrEmpty(cadena))
                    return count;

                char[] charAllowed = "#".ToCharArray();
                char[] data = cadena.ToCharArray();
                
                foreach (var ch in data)
                {
                    if (charAllowed.Contains(ch)) count++;
                }

                return count;
            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                CreateTrialCommand current = (CreateTrialCommand)context.PropertyValue;

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