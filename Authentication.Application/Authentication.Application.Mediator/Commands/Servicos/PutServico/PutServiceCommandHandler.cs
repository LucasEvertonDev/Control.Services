using Authentication.Application.Domain.Contexts.DbAuth.Servicos;
using Authentication.Application.Domain.Structure.Enuns;
using Authentication.Application.Mediator.Commands.Servicos.PutServico;
using Notification.Extensions;

namespace Authentication.Application.Mediator.Commands.Servicos.PostServico;

public class PutServiceCommandHandler(
    IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutServiceCommand, Result>
{
    public async Task<Result> Handle(PutServiceCommand request, CancellationToken cancellationToken)
    {
        var servico = await UnitOfWork.ServicoRepository.FirstOrDefaultTrackingAsync(
            where: servico => servico.Id == request.Id,
            cancellationToken: cancellationToken);

        if (servico == null)
        {
            return Result.Failure<PutServiceCommandHandler>(ServicoFailures.ServicoNaoEncontrado);
        }

        servico.UpdateServico(
            nome: request.Body.Nome,
            descricao: request.Body.Descricao,
            situacao: (Situacao)request.Body.Situacao);

        if (servico.HasFailures())
        {
            return Result.Failure<PutServiceCommandHandler>(servico);
        }

        await UnitOfWork.ServicoRepository.UpdateAsync(
            domain: servico,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
