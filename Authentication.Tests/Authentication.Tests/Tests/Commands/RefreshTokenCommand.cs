using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;
using Authentication.Tests.Structure.Base;

namespace Authentication.Tests.Tests.Commands;
public class RefreshTokenCommandTest : BaseTest
{
    private readonly IMediator _mediator;

    public RefreshTokenCommandTest()
    {
        _mediator = ServiceProviderAuthenticated.GetService<IMediator>();
    }

    [Fact]
    [TestPriority(2)]
    public async Task PostLoginCommandUsuarioInvalidoRequest()
    {
        var result = await _mediator.Send<Result>(new PostRefreshTokenCommand() { });

        var failures = result.GetFailures().Select(a => a.Error);

        failures.Should().NotBeNullOrEmpty()
            .And.Contain(UsuarioFailures.EmailSenhaInvalidos);
    }
}
