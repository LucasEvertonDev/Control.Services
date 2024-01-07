using System.Net;
using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Tests.Structure.Response;
public class ResponseClient<TResult>
{
    public ResponseClient()
    {
    }

    public ResponseClient(Dictionary<string, string[]> details, int httpCode = (int)HttpStatusCode.BadRequest, params string[] messages)
    {
        HttpCode = httpCode;
        Error = new(
            details: details,
            messages: messages);
    }

    public TResult Content { get; set; }

    public bool Success { get; set; } = false;

    public int HttpCode { get; set; }

    public Error<Dictionary<string, string[]>> Error { get; set; }
}

public class ResponseClient
{
    public ResponseClient()
    {
    }

    public ResponseClient(Dictionary<string, string[]> details, int httpCode = (int)HttpStatusCode.BadRequest, params string[] messages)
    {
        HttpCode = httpCode;
        Error = new(
            details: details,
            messages: messages);
    }

    public bool Success { get; set; } = false;

    public int HttpCode { get; set; }

    public Error<Dictionary<string, string[]>> Error { get; set; }
}