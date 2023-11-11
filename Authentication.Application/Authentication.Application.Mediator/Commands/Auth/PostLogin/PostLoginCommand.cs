using Authentication.Application.Domain.Structure.Models;

namespace Authentication.Application.Mediator.Commands.Auth.PostLogin;

public class PostLoginCommand : IRequest<Result>, IHandler<PostLoginCommandHandler>, IValidationAsync
{
    public string Email { get; set; }

    public string Senha { get; set; }
}
