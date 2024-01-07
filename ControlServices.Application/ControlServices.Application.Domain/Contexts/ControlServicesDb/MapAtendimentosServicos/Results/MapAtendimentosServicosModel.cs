using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Results;
using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos.Results;
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
