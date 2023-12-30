using Authentication.Application.Domain.Structure.Models;

namespace Authentication.Application.Domain.Contexts.DbAuth.Servicos.Results;
public class ServicoModel : BaseModel
{
    public Guid Id { get; protected set; }

    public string Nome { get; protected set; }

    public string Descricao { get; protected set; }

    public override ServicoModel FromEntity(IEntity entity)
    {
        var servico = (Servico)entity;

        return new ServicoModel
        {
            Descricao = servico.Descricao,
            Id = servico.Id,
            Nome = servico.Nome,
        };
    }
}