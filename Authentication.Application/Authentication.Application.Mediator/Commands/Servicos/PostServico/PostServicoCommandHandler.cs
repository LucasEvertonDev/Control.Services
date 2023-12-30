using Authentication.Application.Domain.Contexts.DbAuth.Servicos;
using Notification.Extensions;

namespace Authentication.Application.Mediator.Commands.Servicos.PostServico;

public class PostServicoCommandHandler(
    IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PostServicoCommand, Result>
{
    public async Task<Result> Handle(PostServicoCommand request, CancellationToken cancellationToken)
    {
        var servicodb = await UnitOfWork.ClienteRepository.FirstOrDefaultAsync(
            where: servico => servico.Nome == request.Nome,
            cancellationToken: cancellationToken);

        if (servicodb != null)
        {
            return Result.Failure<PostServicoCommandHandler>(ServicoFailures.ServicoNaoEncontrado);
        }

        var servico = new Servico(
            nome: request.Nome,
            descricao: request.Descricao);

        if (servico.HasFailures())
        {
            return Result.Failure<PostServicoCommandHandler>(servico);
        }

        await UnitOfWork.ServicoRepository.CreateAsync(
            domain: servico,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
