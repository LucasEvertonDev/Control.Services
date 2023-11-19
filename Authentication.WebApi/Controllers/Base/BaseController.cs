using System.Net;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Structure.Models;
using Notification.Notifications.Enum;

namespace Authentication.WebApi.Controllers.Base;

public class BaseController(AppSettings appSettings) : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Result(Result result, bool useContainer = true)
    {
        if (result.HasFailures())
        {
            var response = GetResponseError(result);
            return StatusCode(response.HttpCode, response);
        }
        else
        {
            if (!useContainer)
            {
                return Ok(result.GetContent<dynamic>());
            }

            if (result.GetContent<dynamic>() == null)
            {
                return Ok(new ResponseDto
                {
                    Success = true
                });
            }
            else
            {
                return Ok(new ResponseDto<dynamic>()
                {
                    Content = result.GetContent<dynamic>()
                });
            }
        }
    }

    protected ResponseError<Dictionary<object, object[]>> GetResponseError(Result result)
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
                messages: appSettings.Messages.BadRequest);
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
                messages: appSettings.Messages.InternalServerError);
        }

        return new ResponseError<Dictionary<object, object[]>>(
            details: null,
            messages: appSettings.Messages.InternalServerError);
    }

    private Dictionary<object, object[]> GetErros(List<NotificationModel> notifications)
    {
        Dictionary<object, object[]> dic = new ();

        var agrupados = notifications.OrderByDescending(a => (int)a.NotificationInfo.EntityInfo.NotificationType)
            .Select(a => new
            {
                key = a.NotificationInfo.PropInfo.MemberName ?? nameof(NotificationType.BusinessNotification),
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
}
