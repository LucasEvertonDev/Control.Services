using ControlServices.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Http;

namespace ControlServices.Application.Mediator.Commands.Auth.PostLogin;

public class PostLoginCommand : IRequest<Result>, IHandler<PostLoginCommandHandler>, IValidationAsync<PostLoginCommandValidator>
{
    public string Email { get; set; }

    public string Senha { get; set; }

    public static async Task<PostLoginCommand> ConvertForm(HttpRequest request, CancellationToken cancellationToken = default)
    {
        var form = await request.ReadFormAsync(cancellationToken);

        return new PostLoginCommand()
        {
            Email = form["username"],
            Senha = form["password"]
        };
    }
}
