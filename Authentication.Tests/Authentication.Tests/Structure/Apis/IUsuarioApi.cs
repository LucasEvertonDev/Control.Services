using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;
using Authentication.Tests.Structure.Response;
using Refit;

namespace Authentication.Tests.Structure.Apis;

public interface IUsuarioApi
{
    [Post("/api/v1/usuarios")]
    Task<ResponseClient> Post([Body] PostUsuarioCommand request);
}
