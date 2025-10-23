using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PosSystem.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Main function that runs on each HTTP request
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se capturó una excepción inesperada.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // Function to constructs an HTTP response in JSON format when an error occurs
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Gets the path where the error occurred
            var path = context.Request.Path;
            context.Response.ContentType = "application/json";

            // Asign HTTP status code by exception type
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,                           // 404: resource not found
                ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,   // 400: invalid data
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,                // 401: Not found
                DbUpdateException => (int)HttpStatusCode.InternalServerError,                   // 500: Database error
                _ => (int)HttpStatusCode.InternalServerError                                    // 500: Generic error
            };

            // Build custom error message
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = context.Response.StatusCode == 500 ? "Ocurrió un error interno en el servidor." : exception.Message,
                Detail = context.Response.StatusCode == 500 && !context.Request.Host.Host.Contains("localhost") ? "contacte al administrador del sistema." : exception.Message,
                Path = path,
                Timespan = DateTime.UtcNow
            };

            // Serialize JSON in camelCase
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var errorJson = JsonSerializer.Serialize(errorResponse, options);

            // Write the response to the HTTP response body
            await context.Response.WriteAsync(errorJson);
        }
    }
}
