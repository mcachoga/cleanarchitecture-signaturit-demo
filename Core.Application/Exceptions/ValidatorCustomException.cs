using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Signaturit.Application.Exceptions
{
    // Ya existe un ValidationException que está en la libreria de FluentValidator, para evitar movidas,
    // le damos otro nombre.

    public class ValidationCustomException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationCustomException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationCustomException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        
    }
}
