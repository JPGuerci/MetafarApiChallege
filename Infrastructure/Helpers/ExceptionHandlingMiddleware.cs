using System.Net;
using System.Text.Json;

namespace MetafarApiChallege.Infrastructure.Helpers
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;

            // Verifica si la excepción es de tipo CustomException
            if (ex is CustomException customEx)
            {
                statusCode = customEx.StatusCode;
            }
            else
            {
               
                statusCode = HttpStatusCode.InternalServerError;
            }

            return HandleExceptionResponse(context, ex, statusCode);
        }

        private Task HandleExceptionResponse(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                message = ex.Message,
                statusCode = context.Response.StatusCode
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            _logger.LogError(ex, ex.Message);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
