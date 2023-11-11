using Authentication.Application.Domain.Contexts.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.PostLogin;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.WebApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsuarioController(IMediator mediator) : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] PostLoginCommand request)
    {
        return Result(await mediator.Send<Result>(request));
    }
}
