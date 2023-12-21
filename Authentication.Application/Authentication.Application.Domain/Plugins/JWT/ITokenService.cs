using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;

namespace Authentication.Application.Domain.Plugins.JWT;

public interface ITokenService
{
    Task<(string, DateTime)> GenerateTokenAsync(Usuario usuario, CancellationToken cancellationToken = default);
}