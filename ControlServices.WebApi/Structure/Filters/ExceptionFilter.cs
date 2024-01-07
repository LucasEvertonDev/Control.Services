using System.Net;
using ControlServices.Application.Domain;
using ControlServices.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Prometheus;
using Metrics = Prometheus.Metrics;

namespace ControlServices.WebApi.Structure.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    private static readonly Messages _messages = new ();
    private static readonly Counter _requisicoesErroo500 = Metrics.CreateCounter("ms_exception", "erro geral");

    public void OnException(ExceptionContext context)
    {
        _requisicoesErroo500.Inc();
        HandleUnknownError(context);
        logger.LogCritical(context.Exception, "Exception não esperada. Erro servero urgente intervenção");
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError<Dictionary<object, object[]>>(
                details: new Dictionary<object, object[]> { { "Exception", [_messages.InternalServerError] } },
                httpCode: (int)HttpStatusCode.InternalServerError,
                messages: _messages.InternalServerError));
    }
}
