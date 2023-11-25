using Authentication.Application.Domain.Structure.Enuns;

namespace Authentication.Application.Domain.Structure.Domain;

public partial class BaseEntity<TEntity> : Notifiable<TEntity>, IEntity
{
    public BaseEntity()
    {
        Situacao = Situacao.Ativo;
    }

    public Guid Id { get; protected set; }

    public Situacao Situacao { get; protected set; }

    public DateTime DataCriacao { get; private set; }

    public DateTime? DataAtualizacao { get; private set; }

    public virtual void AtualizarDataDeEstados(EntityState entityState)
    {
        if (entityState == EntityState.Added)
        {
            DataCriacao = DateTime.Now;
        }
        else if(entityState == EntityState.Modified)
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
