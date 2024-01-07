using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Domain.Structure.Domain;

public partial class BaseEntity<TEntity> : BasicEntity<TEntity>
{
    public BaseEntity()
    {
        Situacao = Situacao.Ativo;
    }

    public Situacao Situacao { get; protected set; }
}

public partial class BasicEntity<TEntity> : Notifiable<TEntity>, IEntity
{
    public BasicEntity()
    {
    }

    public Guid Id { get; protected set; }

    public DateTime DataCriacao { get; private set; }

    public DateTime? DataAtualizacao { get; private set; }

    public virtual void AtualizarDataDeEstados(EntityState entityState)
    {
        if (entityState == EntityState.Added)
        {
            DataCriacao = DateTime.Now;
        }
        else if (entityState == EntityState.Modified)
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
