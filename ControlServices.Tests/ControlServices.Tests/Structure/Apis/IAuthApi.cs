using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using ControlServices.Application.Mediator.Commands.Auth.PostLogin;
using ControlServices.Application.Mediator.Commands.Auth.PostRefreshToken;
using ControlServices.Tests.Structure.Response;
using Refit;

namespace ControlServices.Tests.Structure.Apis;
public interface IAuthApi
{
    [Post("/api/v1/auth/login")]
    Task<ResponseClient<TokenModel>> Login([Body] PostLoginCommand request);

    [Post("/api/v1/auth/refreshtoken")]
    Task<ResponseClient<TokenModel>> RefreshToken([Header("Authorization")] string bearerToken, [Body] PostRefreshTokenCommand request);
}
