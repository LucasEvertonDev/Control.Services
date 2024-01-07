using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Enuns;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos.Results;
using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
public class AtendimentoModel : BaseModel
{
    public Guid Id { get; set; }

    public DateTime Data { get; private set; }

    public bool ClienteAtrasado { get; private set; }

    public decimal? ValorAtendimento { get; private set; }

    public decimal? ValorPago { get; private set; }

    public string ObservacaoAtendimento { get; private set; }

    public SituacaoAtendimento Situacao { get; private set; }

    public ClienteModel Cliente { get; private set; }

    public ICollection<MapAtendimentosServicosModel> MapAtendimentosServicos { get; private set; }

    public override AtendimentoModel FromEntity(IEntity entity)
    {
        var atendimento = (Atendimento)entity;

        return new AtendimentoModel
        {
            Id = atendimento.Id,
            Cliente = new ClienteModel().FromEntity(atendimento.Cliente),
            ClienteAtrasado = atendimento.ClienteAtrasado,
            Data = atendimento.Data,
            ObservacaoAtendimento = atendimento.ObservacaoAtendimento,
            Situacao = atendimento.Situacao,
            ValorAtendimento = atendimento.ValorAtendimento,
            ValorPago = atendimento.ValorPago,
            MapAtendimentosServicos = atendimento.MapAtendimentosServicos.Select(map => new MapAtendimentosServicosModel().FromEntity(map)).ToList()
        };
    }
}