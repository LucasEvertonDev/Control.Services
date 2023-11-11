using System.Net;
using Authentication.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Prometheus;
using Metrics = Prometheus.Metrics;

namespace Authentication.WebApi.Structure.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;
    private static readonly Counter _requisicoesErroo500 = Metrics.CreateCounter("ms_exception", "erro geral");

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _requisicoesErroo500.Inc();
        HandleUnknownError(context);
        _logger.LogCritical(context.Exception, "Exception não esperada. Erro servero urgente internção. -> " + context.Exception.Message);
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        var errors = new Dictionary<object, object[]>
        {
            { "Erro_Desconhecido", new List<object> { "Algo inesperado aconteceu por favor contate o administrador do sistema." }.ToArray() }
        };

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError<Dictionary<object, object[]>>
        {
            Type = "Erro Desconhecido",
            HttpCode = (int)HttpStatusCode.InternalServerError,
            Success = false,
            Errors = errors
        });
    }
}
