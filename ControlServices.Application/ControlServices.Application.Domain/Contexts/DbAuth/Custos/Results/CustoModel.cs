using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Application.Domain.Contexts.DbAuth.Custos.Results;
public class CustoModel : BaseModel
{
    public Guid Id { get; protected set; }

    public DateTime? Data { get; protected set; }

    public decimal Valor { get; protected set; }

    public string Descricao { get; protected set; }

    public override BaseModel FromEntity(IEntity entity)
    {
        var custo = (Custo)entity;
        var custoModel = new CustoModel()
        {
            Id = custo.Id,
            Data = custo.Data,
            Valor = custo.Valor,
            Descricao = custo.Descricao
        };
        return custoModel;
    }
}
