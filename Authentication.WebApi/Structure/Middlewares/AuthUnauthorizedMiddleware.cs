using System.Net;
using System.Text.Json;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Structure.Models;

namespace Authentication.WebApi.Structure.Middlewares;

public class AuthUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    private readonly AppSettings _appSettings;

    private readonly IEnumerable<HttpStatusCode> _vallowedStatusCodes = new HttpStatusCode[2] { HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden };

    public AuthUnauthorizedMiddleware(RequestDelegate next, AppSettings appSettings)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
        await WriteUnauthorizedResponseAsync(httpContext);
    }

    public async Task WriteUnauthorizedResponseAsync(HttpContext httpContext)
    {
        if (_vallowedStatusCodes.ToList().Contains((HttpStatusCode)httpContext.Response.StatusCode))
        {
            int statusCode = httpContext.Response.StatusCode;

            ResponseError<Dictionary<object, object[]>> errormodel = statusCode switch
            {
                (int)HttpStatusCode.Unauthorized => new ResponseError<Dictionary<object, object[]>>(
                        details: new Dictionary<object, object[]> { { "Unauthorized", [_appSettings.Messages.Unauthorized] } },
                        httpCode: (int)HttpStatusCode.Unauthorized,
                        messages: _appSettings.Messages.Unauthorized),
                (int)HttpStatusCode.Forbidden => new ResponseError<Dictionary<object, object[]>>(
                        details: new Dictionary<object, object[]> { { "Forbidden", [_appSettings.Messages.Forbidden] } },
                        httpCode: (int)HttpStatusCode.Forbidden,
                        messages: _appSettings.Messages.Forbidden),
                _ => new ResponseError<Dictionary<object, object[]>>(
                        details: new Dictionary<object, object[]> { { "Exception", [_appSettings.Messages.InternalServerError] } },
                        httpCode: (int)HttpStatusCode.InternalServerError,
                        messages: _appSettings.Messages.InternalServerError),
            };

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errormodel));
        }
    }
}
