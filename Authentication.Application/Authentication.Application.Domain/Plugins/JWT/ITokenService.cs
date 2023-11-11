using Authentication.Application.Domain.Contexts.Usuarios;

namespace Authentication.Application.Domain.Plugins.JWT;

public interface ITokenService
{
    Task<(string, DateTime)> GenerateToken(Usuario usuario);
}