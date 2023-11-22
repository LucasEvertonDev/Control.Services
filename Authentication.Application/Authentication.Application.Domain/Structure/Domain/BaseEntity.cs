namespace Authentication.Application.Domain.Structure.Domain;

public partial class BaseEntity<TEntity> : Notifiable<TEntity>, IEntity
{
    public BaseEntity()
    {
    }

    public Guid Id { get; protected set; }

    public int Situacao { get; protected set; }

    public DateTime DataCriacao { get; protected set; }

    public void UpdateDate()
    {
    }
}
