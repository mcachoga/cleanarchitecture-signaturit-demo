using Signaturit.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signaturit.Application.Results;

namespace Signaturit.Web.Helpers.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                //{ typeof(ValidationCustomException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ExistingRecordException), HandleExistingRecordException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ///context.Result = new ApiResult<int>(-1, 500, new string[] { "an error occurred", context.Exception.Message });

            //context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationCustomException;
            List<string> errorList = new List<string>();
            foreach (var error in exception.Errors.Values)
            {
                errorList.AddRange(error);
            }

            //context.Result = new ApiResult<int>(-1, 400, errorList.ToArray());
            context.Result =  (Microsoft.AspNetCore.Mvc.IActionResult)Result<Microsoft.AspNetCore.Mvc.IActionResult>.Fail("AAAAAAA");

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            context.Result = new ApiResult<int>(-1, 404, new string[] { "your resource not found", exception.Message });

            context.ExceptionHandled = true;
        }
        private void HandleExistingRecordException(ExceptionContext context)
        {
            var exception = context.Exception as ExistingRecordException;

            context.Result = new ApiResult<int>(-1, 500, new string[] { exception.Message });

            context.ExceptionHandled = true;
        }
    }
}
