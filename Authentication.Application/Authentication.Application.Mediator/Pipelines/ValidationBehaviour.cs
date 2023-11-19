using Authentication.Application.Domain.Structure.Models;
using FluentValidation;
using FluentValidation.Results;
using Notification.Notifications.Context;
using Notification.Notifications.Enum;

namespace Authentication.Application.Mediator.Pipelines;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly Result _result;
    private readonly IServiceProvider _serviceProvider;

    public ValidationBehaviour(NotificationContext notificationContext, IServiceProvider serviceProvider)
    {
        _result = new Result(notificationContext);
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = await ValidateParameterAsync(request);

        if (failures.Any())
        {
            _result.Failure(failures.ToList());

            return (TResponse)_result;
        }

        return await next();
    }

    private async Task<IEnumerable<NotificationModel>> ValidateParameterAsync(object parameter)
    {
        var typeParam = parameter.GetType();
        if (typeParam.GetInterface(nameof(IValidationAsync)) != null)
        {
            var failures = await GetValidationErrorsAsync(parameter);

            if (failures.Any())
            {
                return GetNotifications(failures, typeParam);
            }
        }

        return new List<NotificationModel>();
    }

    private IEnumerable<NotificationModel> GetNotifications(IEnumerable<ValidationFailure> failures, Type typeParam)
    {
        foreach (var failure in failures)
        {
            yield return new NotificationModel(
                failure: new FailureModel(
                    failure.ErrorCode, failure.ErrorMessage),
                notificationInfo: new NotificationInfo(
                    new PropInfo()
                    {
                        MemberName = failure.PropertyName,
                    },
                    entityInfo: new EntityInfo
                    {
                        Name = typeParam.Name,
                        Namespace = typeParam.Namespace,
                        NotificationType = NotificationType.RequestNotification
                    }));
        }
    }

    private async Task<IEnumerable<ValidationFailure>> GetValidationErrorsAsync(object value)
    {
        if (value == null)
        {
            return new[] { new ValidationFailure(string.Empty, "instance is null") };
        }

        var genericValidatorType = typeof(IValidator<>);
        var specificValidatorType = genericValidatorType.MakeGenericType(value.GetType());

        var validatorInstance = (IValidator)_serviceProvider.GetService(specificValidatorType);

        if (validatorInstance == null)
        {
            return new List<ValidationFailure>();
        }

        var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(value));
        return validationResult.Errors ?? new List<ValidationFailure>();
    }
}