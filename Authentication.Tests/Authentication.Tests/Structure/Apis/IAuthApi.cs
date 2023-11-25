using Authentication.Application.Domain.Contexts.Usuarios.Models;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;
using Authentication.Tests.Structure.Response;
using Refit;

namespace Authentication.Tests.Structure.Apis;
public interface IAuthApi
{
    [Post("/api/v1/auth/login")]
    Task<ResponseClient<TokenModel>> Login([Body] PostLoginCommand request);

    [Post("/api/v1/auth/refreshtoken")]
    Task<ResponseClient<TokenModel>> RefreshToken([Header("Authorization")] string bearerToken, [Body] PostRefreshTokenCommand request);
}
