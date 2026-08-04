using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftPlus_ToDo.DTOs;

namespace SoftPlus_ToDo.Middleware
{
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> _logger
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken
        )
        {
            var error = MapException(exception);

            if (error.StatusCode >= StatusCodes.Status500InternalServerError)
                _logger.LogError(
                    exception,
                    "Unhandled exception occured. TraceId: {TraceId}",
                    httpContext.TraceIdentifier
                );
            else
                _logger.LogWarning(
                    "Request failed with status {StatusCode}: {Message}. TraceId: {TraceId}",
                    error.StatusCode,
                    exception.Message,
                    httpContext.TraceIdentifier
                );

            var problemDetails = new ProblemDetails
            {
                Status = error.StatusCode,
                Title = error.Title,
                Detail = error.Detail,
                Instance = httpContext.Request.Path  
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
            httpContext.Response.StatusCode = error.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static ExceptionResponseDto MapException(
            Exception exception
        )
        {
            return exception switch
            {
                KeyNotFoundException => new ExceptionResponseDto
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = exception.Message  
                },
                InvalidOperationException => new ExceptionResponseDto
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Invalid request",
                    Detail = exception.Message
                },
                _ => new ExceptionResponseDto
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Internal server error",
                    Detail = "An unexpected error accurred"
                }
            };
        }
    }
}