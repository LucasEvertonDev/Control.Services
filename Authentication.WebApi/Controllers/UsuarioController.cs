using Authentication.Application.Domain;
using Authentication.Application.Domain.Contexts.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.WebApi.Controllers;

[ApiController]
[Route("api/v1/usuarios")]
public class UsuarioController(
    IMediator mediator,
    AppSettings appSettings) : BaseController(appSettings)
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] PostUsuarioCommand request)
    {
        return Result(await mediator.Send<Result>(request));
    }
}
