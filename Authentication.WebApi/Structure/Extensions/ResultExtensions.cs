using System.Net;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Structure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Notification.Notifications.Enum;

namespace Authentication.WebApi.Structure.Extensions;

public static class ResultExtensions
{
    public static Task<IResult> GetResponse(this Result result, bool useContainer = true)
    {
        if (result.HasFailures())
        {
            var response = GetResponseError(result);
            return Task.FromResult(Results.Extensions.DynamicResponse(response, response.HttpCode));
        }
        else
        {
            if (!useContainer)
            {
                return Task.FromResult((IResult)Results.Ok(result.GetContent<dynamic>()));
            }

            if (result.GetContent<dynamic>() == null)
            {
                return Task.FromResult(Results.Ok(new ResponseDto
                {
                    Success = true
                }));
            }
            else
            {
                return Task.FromResult(Results.Ok(new ResponseDto<dynamic>()
                {
                    Content = result.GetContent<dynamic>()
                }));
            }
        }
    }

    public static RouteHandlerBuilder AllowAnonymous<TResponseSucess>(this RouteHandlerBuilder route)
    {
        route.AllowAnonymous().Produces<TResponseSucess>(StatusCodes.Status200OK)
           .Produces<ResponseError<Dictionary<object, object[]>>>(StatusCodes.Status400BadRequest);
        return route;
    }

    public static RouteHandlerBuilder Authorization<TResponseSucess>(this RouteHandlerBuilder route, string policy = null)
    {
        route.RequireAuthorization(policy).Produces<TResponseSucess>(StatusCodes.Status200OK)
           .Produces<ResponseError<Dictionary<object, object[]>>>(StatusCodes.Status400BadRequest);
        return route;
    }

    public static RouteHandlerBuilder Authorization(this RouteHandlerBuilder route, string policy = null)
    {
        if (!string.IsNullOrEmpty(policy))
        {
            route.RequireAuthorization(policy);
        }
        else
        {
            route.RequireAuthorization();
        }

        return route;
    }

    public static async Task<IResult> SendAsync(this IMediator mediator, IRequest<Result> obj, CancellationToken cancellationToken = default,  bool useContainer = true)
    {
        return await mediator.Send<Result>(obj, cancellationToken).Result.GetResponse(useContainer);
    }

    private static ResponseError<Dictionary<object, object[]>> GetResponseError(Result result)
    {
        var failures = result.GetFailures();

        // Mensagem quando a requisisção não esteve em formato válido
        var requestNotifications = failures.Where(a => a.NotificationInfo.EntityInfo.NotificationType == NotificationType.RequestNotification).ToList();

        // São validações de lógica que devem ser exibidas para o cliente
        var businessNotifications = failures.Where(a => a.NotificationInfo.EntityInfo.NotificationType == NotificationType.BusinessNotification).ToList();

        // São validações exclusivas da classe de domino tem por objetivo manter o dominio em um estado válido. São problemas excepcionais.
        // Não devem acontecer se todas as etapas anteriores forem corretas.
        var domainNotifications = failures.Where(a => a.NotificationInfo.EntityInfo.NotificationType == NotificationType.DomainNotification).ToList();

        if (requestNotifications.Any())
        {
            return new ResponseError<Dictionary<object, object[]>>(
                details: GetErros(requestNotifications),
                messages: new Messages().BadRequest);
        }
        else if (businessNotifications.Any())
        {
            return new ResponseError<Dictionary<object, object[]>>(
                details: GetErros(businessNotifications),
                messages: businessNotifications.Select(notifcation => notifcation.Error.message).ToArray());
        }
        else if (domainNotifications.Any())
        {
            return new ResponseError<Dictionary<object, object[]>>(
                details: GetErros(domainNotifications),
                httpCode: (int)HttpStatusCode.InternalServerError,
                messages: new Messages().InternalServerError);
        }

        return new ResponseError<Dictionary<object, object[]>>(
            details: null,
            messages: new Messages().InternalServerError);
    }

    private static Dictionary<object, object[]> GetErros(List<NotificationModel> notifications)
    {
        Dictionary<object, object[]> dic = new();

        var agrupados = notifications.OrderByDescending(a => (int)a.NotificationInfo.EntityInfo.NotificationType)
            .Select(a => new
            {
                key = !string.IsNullOrWhiteSpace(a.NotificationInfo.PropInfo.MemberName) ? a.NotificationInfo.PropInfo.MemberName : nameof(NotificationType.BusinessNotification),
                a.Error.message,
            })
            .GroupBy(a => a.key).Select(a => new
            {
                key = a.Key,
                messages = a.Select(a => a.message).ToArray()
            })
            .ToList();

        agrupados.ForEach(i =>
        {
            dic.Add(i.key, i.messages);
        });

        return dic;
    }

    private static IResult DynamicResponse(this IResultExtensions resultExtensions, ResponseError<Dictionary<object, object[]>> content, int statusCode)
    {
        ArgumentNullException.ThrowIfNull(content);

        return new CustomResults(content, statusCode);
    }
}

public class CustomResults(ResponseError<Dictionary<object, object[]>> content, int statusCode) : IResult
{
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(content, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }));
    }
}