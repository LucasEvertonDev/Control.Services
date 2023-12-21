using System.Net;
using System.Text;
using System.Text.Json;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Diagnostics;
using Prometheus;
using Serilog.Context;

namespace Authentication.WebApi.Structure.Handlers;
public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    private static readonly Messages _messages = new();
    private static readonly Counter _requisicoesErroo500 = Metrics.CreateCounter("ms_exception", "erro geral");

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _requisicoesErroo500.Inc();

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ResponseError<Dictionary<object, object[]>>(
                details: new Dictionary<object, object[]> { { "Exception", [_messages.InternalServerError] } },
                httpCode: (int)HttpStatusCode.InternalServerError,
                messages: _messages.InternalServerError)));

        LogContext.PushProperty("Request", await Invoke(httpContext));
        logger.LogCritical(exception, "Exception não esperada. Erro servero urgente intervenção");

        return true;
    }

    private async Task<string> Invoke(HttpContext context)
    {
        if (context == null || context.Request == null)
        {
            return null;
        }

        string requestBodyPayload = await ReadRequestBody(context.Request);
        var httpRequestInfo = new
        {
            Host = context.Request.Host.ToString(),
            context.Request.Path,
            context.Request.Scheme,
            context.Request.Method,
            context.Request.Protocol,
            QueryString = context.Request.Query.ToDictionary((x) => x.Key, (y) => y.Value.ToString()),
            Headers = context.Request.Headers.Where((x) => x.Key != "Cookie" && x.Key != "Authorization").ToDictionary((x) => x.Key, (y) => y.Value.ToString()),
            Cookies = context.Request.Cookies.ToDictionary((x) => x.Key, (y) => y.Value.ToString()),
            Body = requestBodyPayload
        };

        return JsonSerializer.Serialize(httpRequestInfo);
    }

    private static async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        Stream body = request.Body;
        byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer);
        string requestBody = Encoding.UTF8.GetString(buffer);
        body.Seek(0L, SeekOrigin.Begin);
        request.Body = body;
        return requestBody != null ? requestBody.Replace("\n", string.Empty).Replace("\"", string.Empty) : string.Empty;
    }
}