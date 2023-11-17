using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Plugins.JWT;
using Authentication.Application.Domain.Plugins.JWT.Conts;
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
                new Claim(JwtUserClaims.Email, usuario.Email),
                new Claim(JwtUserClaims.Id, usuario.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(appSettings.Jwt.ExpireInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult((tokenHandler.WriteToken(token), DateTime.Now.AddMinutes(appSettings.Jwt.ExpireInMinutes)));
    }
}
