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

            ResponseError<Dictionary<object, object[]>> errormodel = new ();
            switch (statusCode)
            {
                case (int)HttpStatusCode.Unauthorized:
                    errormodel = new ResponseError<Dictionary<object, object[]>>
                    {
                        HttpCode = (int)HttpStatusCode.Unauthorized,
                        Success = false,
                        Type = "Authorization",
                        Errors = new Dictionary<object, object[]>
                        {
                            { "Authorization", new object[] { _appSettings.Messages.Unauthorized } }
                        }
                    };
                    break;
                case (int)HttpStatusCode.Forbidden:
                    errormodel = new ResponseError<Dictionary<object, object[]>>
                    {
                        HttpCode = (int)HttpStatusCode.Forbidden,
                        Success = false,
                        Type = "Forbidden",
                        Errors = new Dictionary<object, object[]>
                        {
                            { "Forbidden", new object[] { _appSettings.Messages.Forbidden } }
                        }
                    };
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errormodel));
        }
    }
}
