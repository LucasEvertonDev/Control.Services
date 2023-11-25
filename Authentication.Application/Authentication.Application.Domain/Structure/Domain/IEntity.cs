using Authentication.Application.Domain.Structure.Enuns;

namespace Authentication.Application.Domain.Structure.Domain;

public interface IEntity
{
    void AtualizarDataDeEstados(EntityState entityState);
}
