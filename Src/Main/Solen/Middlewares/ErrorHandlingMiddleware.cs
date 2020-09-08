using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solen.Core.Application.Exceptions;

namespace Solen.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            string result = null;

            switch (exception)
            {
                case ValidationException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(ex.Failures);
                    break;
                case AppBusinessException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(ex.Failures);
                    break;
                case AppSubscriptionException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(ex.Failures);
                    break;
                case AppTechnicalException ex:
                    logger.LogError(ex, "Technical Exception");
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    result = ex.Message;
                    break;
                case NotFoundException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    result = string.IsNullOrWhiteSpace(ex.Message) ? "Not found " : ex.Message;
                    break;
                case UnauthorizedException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    result = string.IsNullOrWhiteSpace(ex.Message) ? "Unauthorized" : ex.Message;
                    break;
                case ArgumentNullException ex:
                    if (ex.Source == "MediatR")
                        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    else
                    {
                        logger.LogError(ex, "ArgumentNullException");
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    }
                    
                    result = ex.Message;
                    break;
                case LockedException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.Locked;
                    result = string.IsNullOrWhiteSpace(ex.Message) ? "Locked" : ex.Message;
                    break;
                case Exception ex:
                    logger.LogError(ex, "Unhandled Exception");
                    result = string.IsNullOrWhiteSpace(ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            if (result != null)
            {
                if (_env.IsProduction() && context.Response.StatusCode == (int) HttpStatusCode.InternalServerError)
                    result = "An error has occured, please try later.";

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}