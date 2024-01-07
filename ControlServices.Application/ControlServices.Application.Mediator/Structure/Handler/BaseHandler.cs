using System.Security.Principal;
using ControlServices.Application.Domain.Structure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Notification.Notifications.Context;
using Notification.Notifications.Notifiable.Notifications;

namespace ControlServices.Application.Mediator.Structure.Handler;

public class BaseHandler : Notifiable
{
    private readonly IIdentity _identity;

    private readonly IUnitOfWorkTransaction _unitOfWorkRepos;

    public BaseHandler(IServiceProvider serviceProvider)
    {
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        _unitOfWorkRepos = serviceProvider.GetService<IUnitOfWorkTransaction>();
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    public Result Result { get; private set; }

    /// <summary>
    /// Gets tem seu bind no momento da invocação do metodo on transaction.
    /// </summary>
    public IUnitOfWorkRepos UnitOfWork => _unitOfWorkRepos;

    public IIdentity Identity => _identity;
}