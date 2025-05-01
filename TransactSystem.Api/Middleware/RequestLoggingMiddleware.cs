
using FluentValidation;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Transact.Api.Exceptions;

namespace Transact.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;


    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            _logger.LogInformation($"Request {httpContext.Request?.Method}  -  {httpContext.Request?.Path.Value} => {httpContext.Response?.StatusCode}   invoked");

            var originalBodyStream = httpContext.Response.Body;
            var ReqText = await FormatRequest(httpContext.Request);
            var RspText = "";

            using (var responseBody = new MemoryStream())
            {

                httpContext.Response.Body = responseBody;
                await _next.Invoke(httpContext);
                RspText = await FormatResponse(httpContext?.Response);
                await responseBody.CopyToAsync(originalBodyStream);

                // log 
                _logger.LogInformation($"ResponseCode= {httpContext.Response.StatusCode} ||  Request= {ReqText}  ||  Response= {RspText} ");
            }
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }

    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
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

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var bodyAsText = "";

        using (var bodyReader = new StreamReader(request.Body))
        {
            bodyAsText = await bodyReader.ReadToEndAsync();
            request.Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyAsText));
        }


        return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString} {bodyAsText}";
    }
    private async Task<string> FormatResponse(HttpResponse response)
    {
        var sr = new StreamReader(response.Body);
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await sr.ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return $"Response {text}";
    }

}