using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Domain.Structure.Domain;

public interface IEntity
{
    void AtualizarDataDeEstados(EntityState entityState);
}
