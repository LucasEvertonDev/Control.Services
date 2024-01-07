using Authentication.Application.Domain.Contexts.DbAuth.Servicos.Results;
using Authentication.Application.Domain.Structure.Models;

namespace Authentication.Application.Domain.Contexts.DbAuth.MapAtendimentosServicos.Results;
public class MapAtendimentosServicosModel : BaseModel
{
    public Guid Id { get; set; }

    public decimal Valor { get; set; }

    public ServicoModel Servico { get; set; }

    public override MapAtendimentosServicosModel FromEntity(IEntity entity)
    {
        var mapServicoAtendimento = (MapAtendimentoServico)entity;

        return new MapAtendimentosServicosModel
        {
            Id = mapServicoAtendimento.Id,
            Valor = mapServicoAtendimento.ValorCobrado.GetValueOrDefault(),
            Servico = new ServicoModel().FromEntity(mapServicoAtendimento.Servico)
        };
    }
}
