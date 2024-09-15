using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learn_core_mvc.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var dashedLine = new string('-', 100); // Adjust the length as needed
                var errorMessage = $"An unhandled exception occurred:\n{ex}\n{dashedLine}";
                _logger.LogError(errorMessage);

                //await HandleExceptionAsync(context, ex);
            }
        }
        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    HttpStatusCode status;
        //    var stackTrace = String.Empty;
        //    string message;
        //    var exceptionType = exception.GetType();
        //    if (exceptionType == typeof(BadRequestException))
        //    {
        //        message = exception.Message;
        //        status = HttpStatusCode.BadRequest;
        //        stackTrace = exception.StackTrace;
        //    }
        //    else if (exceptionType == typeof(NotFoundException))
        //    {
        //        message = exception.Message;
        //        status = HttpStatusCode.NotFound;
        //        stackTrace = exception.StackTrace;
        //    }
        //    else if (exceptionType == typeof(NotImplementedException))
        //    {
        //        status = HttpStatusCode.NotImplemented;
        //        message = exception.Message;
        //        stackTrace = exception.StackTrace;
        //    }
        //    else if (exceptionType == typeof(UnauthorizedAccessException))
        //    {
        //        status = HttpStatusCode.Unauthorized;
        //        message = exception.Message;
        //        stackTrace = exception.StackTrace;
        //    }
        //    else if (exceptionType == typeof(KeyNotFoundException))
        //    {
        //        status = HttpStatusCode.Unauthorized;
        //        message = exception.Message;
        //        stackTrace = exception.StackTrace;
        //    }
        //    else
        //    {
        //        status = HttpStatusCode.InternalServerError;
        //        message = exception.Message;
        //        stackTrace = exception.StackTrace;
        //    }
        //    var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)status;
        //    return context.Response.WriteAsync(exceptionResult);
        //}
    }
}
