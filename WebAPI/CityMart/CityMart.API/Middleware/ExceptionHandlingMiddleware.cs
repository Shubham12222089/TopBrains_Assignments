using System.Net;
using System.Text.Json;

namespace CityMart.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = exception.Message,
                statusCode = context.Response.StatusCode
            };

            switch (exception)
            {
                case ArgumentNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        success = false,
                        message = "Invalid request data",
                        statusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new
                    {
                        success = false,
                        message = "Resource not found",
                        statusCode = (int)HttpStatusCode.NotFound
                    };
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        success = false,
                        message = "Unauthorized access",
                        statusCode = (int)HttpStatusCode.Unauthorized
                    };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        success = false,
                        message = "An internal server error occurred",
                        statusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
