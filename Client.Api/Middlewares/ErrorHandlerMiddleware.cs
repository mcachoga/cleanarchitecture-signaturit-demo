using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Signaturit.Api.Extensions;
using Signaturit.Application.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Signaturit.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception error)
        //    {
        //        var response = context.Response;

        //        var responseModel = Result<string>.Fail(error.Message);

        //        switch (error)
        //        {
        //            case ApiException e:
        //                // custom application error
        //                //response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                //responseModel.Message = e.Message;
        //                break;

        //            case KeyNotFoundException e:
        //                // not found error
        //                //response.StatusCode = (int)HttpStatusCode.NotFound;
        //                //responseModel.Data = e.Message;
        //                break;
        //            case ValidationCustomException e:
        //                response.ContentType = "application/json";
        //                response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                responseModel.Data = JsonSerializer.Serialize(e.Errors);
        //                break;

        //            default:
        //                // unhandled error
        //                //response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //                //responseModel.Message = error.Message;
        //                break;
        //        }

        //        var result = JsonSerializer.Serialize(responseModel);
        //        await response.WriteAsync(result);
        //    }
        //}

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
                
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            int httpStatusCode;
            var result = exception.Message;
            var error = exception.ToString();

            switch (exception)
            {
                case ValidationCustomException validationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Errors);
                    break;
                //case BadRequestException badRequestException:
                //    httpStatusCode = (int)HttpStatusCode.BadRequest;
                //    result = badRequestException.Message;
                //    break;
                //case NotFoundException:
                //    httpStatusCode = (int)HttpStatusCode.NotFound;
                //    break;
                case ApiException apiException:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    result = apiException.Message;
                    break;
                case JwtBearerException jwtException:
                    error = jwtException.ex != null ? jwtException.ex.ToString() : string.Empty;
                    httpStatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }


            _logger.LogError(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;

            if (result != string.Empty)
            {
                result = JsonConvert.SerializeObject(new { StatusCode = httpStatusCode, error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}