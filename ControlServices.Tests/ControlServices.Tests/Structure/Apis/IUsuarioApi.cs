using ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;
using ControlServices.Tests.Structure.Response;
using Refit;

namespace ControlServices.Tests.Structure.Apis;

public interface IUsuarioApi
{
    [Post("/api/v1/usuarios")]
    Task<ResponseClient> Post([Body] PostUsuarioCommand request);
}
