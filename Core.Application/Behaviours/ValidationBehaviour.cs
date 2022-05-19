using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Signaturit.Application.Behaviours
{
    //public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class where TRequest : IValidateable
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    //throw new ValidationCustomException(failures);

                    var responseType = typeof(TResponse);

                    List<string> errors = new List<string>();

                    foreach (var message in failures)
                    {
                        errors.Add(message.ErrorMessage);
                    }

                    if (responseType.IsGenericType)
                    {
                        var resultType = responseType.GetGenericArguments()[0];
                        var inValidResponse = typeof(ValidateableResponse<>).MakeGenericType(resultType);

                        var invalidResponse = Activator.CreateInstance(inValidResponse, null, errors);

                        return invalidResponse as TResponse; 
                    }
                }
            }

            return await next();
        }
    }
}