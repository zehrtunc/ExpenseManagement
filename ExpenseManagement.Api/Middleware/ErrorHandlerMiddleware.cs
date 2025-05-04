using FluentValidation;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using Transact.Api.Exceptions;

namespace Transact.Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;


    public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();

    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            _logger.LogInformation($"Request {httpContext.Request?.Method}  -  {httpContext.Request?.Path.Value}  invoked");
            await _next.Invoke(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
            _logger.LogError($"Error = {ex.Message}");
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
            _logger.LogError($"Error = {ex.Message}");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
            _logger.LogError($"Error = {ex.Message}");
        }

    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        // Log issues and handle ex response

        if (ex.GetType() == typeof(ValidationException))
        {
            var code = HttpStatusCode.BadRequest;
            var _e = (ValidationException)ex;

            var validationErrors = _e.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            });

            var result = JsonConvert.SerializeObject(new
            {
                isSuccess = false,
                statusCode = (int)HttpStatusCode.BadRequest,
                message = "Validation failed",
                errors = validationErrors
            });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsync(result);

        }
        else if (ex.GetType() == typeof(NotFoundException))
        {
            var code = HttpStatusCode.NotFound;
            var result = JsonConvert.SerializeObject(new { isSuccess = false, error = ex.Message });
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsync(result);
        }
        else
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { isSuccess = false, error = ex.Message });
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsync(result);
        }
    }
}
