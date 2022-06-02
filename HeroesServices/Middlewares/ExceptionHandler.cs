using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace HeroesServices.Middlewares
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger _logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
     
        }
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.RouteValues.First().Value;

            var exceptionType = context.Exception.GetType();

            context.ExceptionHandled = true;


            var statusCode = (int)HttpStatusCode.InternalServerError;
            string msg = context.Exception.Message.Length == 0 ?
                    "Internal Server Error" : context.Exception.Message;


            if (exceptionType.FullName.Contains("NotFound"))
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exceptionType.FullName.Contains("Sql") || exceptionType.FullName.Contains("DbUpdateException"))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                msg = "שגיאת נתונים, יש לפנות לתמיכה";
            }
            else if (exceptionType.FullName.Contains("System.UnauthorizedAccessException"))
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exceptionType.FullName.Contains("System.Exception"))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exceptionType.FullName.Contains("Exception"))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }


            try
            {
                _logger.LogError($"Dev: {msg}  ,StatusCode: {statusCode}",
                                 $",Path: {actionName} ,Error handler api exception, StackTrace: { context.Exception.StackTrace}"
                              );
            }
            catch { }

            context.Result = new ObjectResult(msg) { StatusCode = statusCode };
        }


   }
    public class ErrorDetails
    {
        public string title { get; set; }
        public int status { get; set; }
        public string detail { get; set; }

    }
}
