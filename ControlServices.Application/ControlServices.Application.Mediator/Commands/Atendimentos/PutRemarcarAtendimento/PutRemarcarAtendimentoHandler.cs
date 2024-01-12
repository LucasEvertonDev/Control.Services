using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using ControlServices.Application.Mediator.Commands.Atendimentos.PutDataAtendimento;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Atendimentos.PutRemarcarAtendimento;
public class PutRemarcarAtendimentoHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutRemarcarAtendimentoCommand, Result>
{
    public async Task<Result> Handle(PutRemarcarAtendimentoCommand request, CancellationToken cancellationToken)
    {
        var atendimentosDia = await UnitOfWork.AtendimentoRepository.ToListAsync(
         where: (atendimento) => atendimento.Data.Date == request.Body.Data.Date
             && atendimento.Id != request.Id,
         cancellationToken: cancellationToken);

        if (atendimentosDia != null && atendimentosDia.ToList().Exists(a => Math.Abs((a.Data - request.Body.Data.Date).TotalMinutes) < 60))
        {
            return Result.Failure<PostAtendimentoCommandHandler>(AtendimentoFailures.AtendimentoExistente);
        }

        var atendimento = await UnitOfWork.AtendimentoRepository.FirstOrDefaultTrackingAsync(
           where: atendimento => atendimento.Id == request.Id,
           cancellationToken: cancellationToken);

        atendimento.RemarcarAgendamento(
            dataAgendamento: request.Body.Data);

        if (atendimento.HasFailures())
        {
            return Result.Failure<PutRemarcarAtendimentoHandler>(atendimento);
        }

        await UnitOfWork.AtendimentoRepository.UpdateAsync(
            domain: atendimento);

        return Result.Ok();
    }
}
