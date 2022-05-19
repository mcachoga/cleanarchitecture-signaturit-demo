using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Signaturit.Application.Results
{
    public class ApiResult : ApiResult<object>
    {
        public ApiResult(object data, int statusCode = 200, string[] errors = null) : base(data, statusCode, errors)
        {

        }
    }

    public class ApiResult<T> : IActionResult, IDisposable, IStatusCodeActionResult
    {
        public string[] Errors { get; set; }

        public T Data { get; set; }

        public int? StatusCode { get; set; }

        public ApiResult(T data, int statusCode = 200, string[] errors = null)
        {
            Data = data;
            StatusCode = statusCode;
            Errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new OkObjectResult(this).ExecuteResultAsync(context);
        }

        public void Dispose()
        {
            if (Data != null && typeof(T).GetInterfaces().Contains(typeof(IDisposable)))
            {
                ((IDisposable)Data).Dispose();
            }
        }
    }
}
