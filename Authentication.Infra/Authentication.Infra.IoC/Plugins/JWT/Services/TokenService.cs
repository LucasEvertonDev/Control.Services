using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Plugins.JWT;
using Authentication.Application.Domain.Plugins.JWT.Contants;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infra.IoC.Plugins.JWT.Services;

public class TokenService(AppSettings appSettings) : ITokenService
{
    public Task<(string, DateTime)> GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.Jwt.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (JwtUserClaims.Email, usuario.Email),
                new (JwtUserClaims.Id, usuario.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(appSettings.Jwt.ExpireInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult((tokenHandler.WriteToken(token), DateTime.Now.AddMinutes(appSettings.Jwt.ExpireInMinutes)));
    }
}
