using Authentication.Application.Domain;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Auth.PostFlowLogin;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.WebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(
    IMediator mediator,
    AppSettings appSettings) : BaseController(appSettings)
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] PostLoginCommand request)
    {
        return Result(await mediator.Send<Result>(request));
    }

    [Authorize]
    [HttpPost("refreshtoken")]
    [ProducesResponseType(typeof(ResponseDto<TokenModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] PostRefreshTokenCommand request)
    {
        return Result(await mediator.Send<Result>(request));
    }

    [HttpPost("flowlogin")]
    [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> FlowLogin([FromForm] PostFlowLoginCommand request)
    {
        return Result(await mediator.Send<Result>(request), false);
    }
}
