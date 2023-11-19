using System.Net;

namespace Authentication.Application.Domain.Structure.Models;

public class ResponseDto<TResult>
{
    public bool Success { get; set; } = true;

    public int HttpCode { get; set; } = (int)HttpStatusCode.OK;

    public TResult Content { get; set; }
}

public class ResponseDto
{
    public bool Success { get; set; } = true;

    public int HttpCode { get; set; } = (int)HttpStatusCode.OK;
}

public class ResponseError<TResult>
{
    public ResponseError(TResult details, int httpCode = (int)HttpStatusCode.BadRequest, params string[] messages)
    {
        HttpCode = httpCode;
        Error = new (
            details: details,
            messages: messages);
    }

    public bool Success { get; set; } = false;

    public int HttpCode { get; set; }

    public Error<TResult> Error { get; set; }
}

public class Error<TResult>
{
    public Error(TResult details, params string[] messages)
    {
        Messages = messages;
        Details = details;
    }

    public string[] Messages { get; set; }

    public TResult Details { get; set; }
}