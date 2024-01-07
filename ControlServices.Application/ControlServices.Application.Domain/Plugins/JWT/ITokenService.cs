using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;

namespace ControlServices.Application.Domain.Plugins.JWT;

public interface ITokenService
{
    Task<(string, DateTime)> GenerateTokenAsync(Usuario usuario, CancellationToken cancellationToken = default);
}