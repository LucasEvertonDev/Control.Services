using Authentication.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Mediator.Commands.Auth.PostLogin;

public class PostLoginCommand : IRequest<Result>, IHandler<PostLoginCommandHandler>, IValidationAsync<PostLoginCommandValidator>
{
    public string Email { get; set; }

    public string Senha { get; set; }

    public static async Task<PostLoginCommand> ConvertForm(HttpRequest request)
    {
        var form = await request.ReadFormAsync();

        return new PostLoginCommand()
        {
            Email = form["username"],
            Senha = form["password"]
        };
    }
}
