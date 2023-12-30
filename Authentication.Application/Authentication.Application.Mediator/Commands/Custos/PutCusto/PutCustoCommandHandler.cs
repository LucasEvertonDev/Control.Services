using Authentication.Application.Mediator.Commands.Custos.PostCusto;
using Notification.Extensions;

namespace Authentication.Application.Mediator.Commands.Custos.PutCusto;
public class PutCustoCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutCustoCommand, Result>
{
    public async Task<Result> Handle(PutCustoCommand request, CancellationToken cancellationToken)
    {
        var custo = await UnitOfWork.CustoRepository.FirstOrDefaultAsync(
            where: custo => custo.Id == request.Id,
            cancellationToken: cancellationToken);

        custo.UpdateCusto(
            data: request.Body.Data,
            valor: request.Body.Valor,
            descricao: request.Body.Descricao);
        if (custo.HasFailures())
        {
            return Result.Failure<PostCustoCommandHandler>(custo);
        }

        await UnitOfWork.CustoRepository.UpdateAsync(
            domain: custo,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
