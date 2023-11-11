namespace Authentication.Application.Domain.Structure.Models;

public class ResponseDto<TResult>
{
    public bool Success { get; set; } = true;

    public int HttpCode { get; set; } = 200;

    public TResult Content { get; set; }
}

public class ResponseDto
{
    public bool Success { get; set; } = true;

    public int HttpCode { get; set; } = 200;
}

public class ResponseError<TResult>
{
    public string Type { get; set; }

    public bool Success { get; set; } = false;

    public int HttpCode { get; set; }

    public TResult Errors { get; set; }
}