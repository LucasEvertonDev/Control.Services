using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Enuns;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Atendimentos.PutAtendimento;
public class PutAtendimentoCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutAtendimentoCommand, Result>
{
    public async Task<Result> Handle(PutAtendimentoCommand request, CancellationToken cancellationToken)
    {
        var atendimentosDia = await UnitOfWork.AtendimentoRepository.ToListAsync(
            where: (atendimento) => atendimento.Data.Date == request.Body.Data.Date
                && atendimento.Id != request.Id,
            cancellationToken: cancellationToken);

        if (atendimentosDia != null && atendimentosDia.ToList().Exists(a => Math.Abs((a.Data - request.Body.Data.Date).TotalMinutes) < 60))
        {
            return Result.Failure<PostAtendimentoCommandHandler>(AtendimentoFailures.AtendimentoExistente);
        }

        var cliente = await UnitOfWork.ClienteRepository.FirstOrDefaultTrackingAsync(
            where: cliente => cliente.Id == request.Body.ClienteId,
            cancellationToken: cancellationToken);

        var atendimento = await UnitOfWork.AtendimentoRepository.FirstOrDefaultTrackingAsync(
            where: atendimento => atendimento.Id == request.Id,
            cancellationToken: cancellationToken);

        atendimento.UpdateAtendimento(data: request.Body.Data,
            cliente: cliente,
            clienteAtrasado: request.Body.ClienteAtrasado,
            valorAtendimento: request.Body.MapAtendimentosServicos.Sum(r => r.ValorCobrado),
            valorPago: request.Body.ValorPago,
            observacaoAtendimento: request.Body.ObservacaoAtendimento,
            situacao: (SituacaoAtendimento)request.Body.Situacao);

        var servicos = await UnitOfWork.MapAtendimentoServicoRepository.ToListTrackingAsync(
                where: servico => servico.AtendimentoId == atendimento.Id,
                cancellationToken: cancellationToken);

        var servicosExcluidos = servicos.Where(mapDb =>
            !request.Body.MapAtendimentosServicos.Exists(map => map.Id == mapDb.Id)).ToArray();

        await UnitOfWork.MapAtendimentoServicoRepository.DeleteAsync(
            entidadesParaExcluir: servicosExcluidos,
            cancellationToken: cancellationToken);

        foreach (var map in request.Body.MapAtendimentosServicos)
        {
            var mapServico = await UnitOfWork.MapAtendimentoServicoRepository.FirstOrDefaultTrackingAsync(
                where: servico => servico.Id == map.Id,
                cancellationToken: cancellationToken);

            var servico = await UnitOfWork.ServicoRepository.FirstOrDefaultTrackingAsync(
                where: servico => servico.Id == map.ServicoId,
                cancellationToken: cancellationToken);

            if (mapServico == null)
            {
                mapServico = new MapAtendimentoServico(
                    servico: servico,
                    atendimento: atendimento,
                    valorCobrado: map.ValorCobrado);
            }
            else
            {
                mapServico.UpdateMapAtendimento(
                        servico: servico,
                        atendimento: atendimento,
                        valorCobrado: map.ValorCobrado);
            }

            atendimento.AddServico(mapServico);
        }

        if (atendimento.HasFailures())
        {
            return Result.Failure<PutAtendimentoCommandHandler>(atendimento);
        }

        await UnitOfWork.AtendimentoRepository.UpdateAsync(atendimento, cancellationToken);

        return Result.Ok();
    }
}
