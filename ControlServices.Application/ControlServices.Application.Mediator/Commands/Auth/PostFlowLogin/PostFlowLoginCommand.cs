using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Auth.PostFlowLogin;

public class PostFlowLoginCommand : IRequest<Result>, IHandler<PostFlowLoginCommandHandler>
{
    [ModelBinder(Name = "grant_type")]
    public string GrandType { get; set; }

    [ModelBinder(Name = "username")]
    public string Email { get; set; }

    [ModelBinder(Name = "password")]
    public string Senha { get; set; }
}
