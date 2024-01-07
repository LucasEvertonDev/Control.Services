using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Results;
using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Mediator.Queries.Servicos.GetServicos;
public class GetServicosQueryHandler(IServiceProvider serviceProvider)
    : BaseHandler(serviceProvider), IRequestHandler<GetServicosQuery, Result>
{
    public async Task<Result> Handle(GetServicosQuery request, CancellationToken cancellationToken)
    {
        var servicos = await UnitOfWork.ServicoRepository
          .ToListAsync<ServicoModel>(
              pageNumber: request.PageNumber,
              pageSize: request.PageSize,
              where: servico =>
                  (string.IsNullOrWhiteSpace(request.Nome) || servico.Nome.Contains(request.Nome))
                  && (string.IsNullOrWhiteSpace(request.Descricao) || servico.Descricao.Contains(request.Descricao))
                  && (request.Situacao.GetValueOrDefault() == 0 || servico.Situacao == (Situacao)request.Situacao.GetValueOrDefault()),
              cancellationToken: cancellationToken);

        return Result.Ok(servicos);
    }
}
