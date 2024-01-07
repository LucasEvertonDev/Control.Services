using System.Text;
using Newtonsoft.Json;
using Serilog.Context;

namespace ControlServices.WebApi.Structure.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
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
        LogContext.PushProperty("Request", JsonConvert.SerializeObject(httpRequestInfo));
        await next(context);
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
