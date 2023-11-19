using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Tests.Structure.Base;

namespace Authentication.Tests.Tests.Commands;

public class LoginCommandTest : BaseTest
{
    private readonly IMediator _mediator;

    public LoginCommandTest()
    {
        _mediator = ServiceProvider.GetService<IMediator>();
    }

    [Fact]
    [TestPriority(1)]
    public async Task PostLoginCommandInvalidoRequest()
    {
        var result = await _mediator.Send<Result>(new PostLoginCommand() { });

        var failures = result.GetFailures().Select(a => a.Error);

        failures.Should().Contain(UsuarioFailures.EmailObrigatorio)
            .And.Contain(UsuarioFailures.SenhaObrigatoria);
    }

    [Fact]
    [TestPriority(2)]
    public async Task PostLoginCommandUsuarioInvalidoRequest()
    {
        var result = await _mediator.Send<Result>(new PostLoginCommand()
        {
            Email = "Teste",
            Senha = "Teste"
        });

        var failures = result.GetFailures().Select(a => a.Error);

        failures.Should().NotBeNullOrEmpty()
            .And.Contain(UsuarioFailures.EmailSenhaInvalidos);
    }
}