using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using ControlServices.Application.Domain.Structure.Enuns;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Servicos.PutServico;

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
