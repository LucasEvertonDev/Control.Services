using System.Text.Json.Serialization;
using Authentication.Application.Mediator.Commands.PostRefreshToken;

namespace Authentication.Application.Mediator.Commands.PostFlowLogin;

public class PostFlowLoginCommand : IRequest<Result>, IHandler<PostRefreshTokenCommandHandler>
{
    [JsonPropertyName("grant_type")]
    public string GrandType { get; set; }

    [JsonPropertyName("username")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Senha { get; set; }
}
