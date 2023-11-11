using System.Net;
using Authentication.Application.Domain.Contexts.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Auth.PostFlowLogin;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;
using Microsoft.AspNetCore.Authorization;
using Notification.Notifications.Helpers;

namespace Authentication.WebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IMediator mediator) : BaseController
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
    public async Task<ActionResult> FlowLogin([FromForm] PostFlowLoginCommand request)
    {
        var result = await mediator.Send<Result>(request);

        if (result.HasFailures())
        {
            return BadRequest(new ResponseError<Dictionary<object, object[]>>
            {
                Type = "Business rules",
                HttpCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Errors = ResultServiceHelpers.GetFailures(result)
            });
        }

        return Ok(result.GetContent<TokenModel>());
    }
}
