using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Enuns;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
public class PostAtendimentoCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PostAtendimentoCommand, Result>
{
    public async Task<Result> Handle(PostAtendimentoCommand request, CancellationToken cancellationToken)
    {
        ////var atendimentosDia = await UnitOfWork.AtendimentoRepository.ToListAsync(
        ////        where: (atendimento) => atendimento.Data.Date == request.Data.Date,
        ////        cancellationToken: cancellationToken);

        ////if (atendimentosDia != null && atendimentosDia.ToList().Exists(a => Math.Abs((a.Data - request.Data).TotalMinutes) < 60))
        ////{
        ////    return Result.Failure<PostAtendimentoCommandHandler>(AtendimentoFailures.AtendimentoExistente);
        ////}

        var cliente = await UnitOfWork.ClienteRepository.FirstOrDefaultTrackingAsync(
            where: cliente => cliente.Id == request.ClienteId,
            cancellationToken: cancellationToken);

        var atendimento = new Atendimento(
            data: request.Data,
            cliente: cliente,
            clienteAtrasado: request.ClienteAtrasado,
            valorAtendimento: request.ValorAtendimento,
            valorPago: request.ValorPago,
            observacaoAtendimento: request.ObservacaoAtendimento,
            situacao: (SituacaoAtendimento)request.Situacao);

        foreach(var map in request.MapAtendimentosServicos)
        {
            var servico = await UnitOfWork.ServicoRepository.FirstOrDefaultTrackingAsync(
               where: servico => servico.Id == map.ServicoId,
               cancellationToken: cancellationToken);

            var mapServicoAtendimento = new MapAtendimentoServico(
                servico: servico,
                atendimento: atendimento,
                valorCobrado: map.ValorCobrado);

            atendimento.AddServico(mapServicoAtendimento);
        }

        if (atendimento.HasFailures())
        {
            return Result.Failure<PostAtendimentoCommandHandler>(atendimento);
        }

        await UnitOfWork.AtendimentoRepository.CreateAsync(
            domain: atendimento,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
