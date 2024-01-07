using ControlServices.Application.Domain.Structure.UnitOfWork;

namespace ControlServices.Application.Mediator.Pipelines;

public class TransactionBehaviour<TRequest, TResponse>(IUnitOfWorkTransaction unitWorkTransaction) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await unitWorkTransaction.OnTransactionAsync(async () =>
            {
                return await next();
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}
