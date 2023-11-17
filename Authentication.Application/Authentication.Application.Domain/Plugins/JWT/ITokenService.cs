using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;

namespace Authentication.Application.Domain.Plugins.JWT;

public interface ITokenService
{
    Task<(string, DateTime)> GenerateToken(Usuario usuario);
}