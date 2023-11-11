using Authentication.Application.Mediator.Commands.PostLogin;
using Authentication.Tests.Structure.Base;

namespace Authentication.Tests.Tests.Commands;

public class LiberacaoAuthenticationCommandTest : BaseTest
{
    private readonly IMediator _mediator;

    public LiberacaoAuthenticationCommandTest()
    {
        _mediator = ServiceProvider.GetService<IMediator>();
    }

    [Fact]
    [TestPriority(1)]
    public void PostLoginCommandInvalidoRequest()
    {
        var postLoginCommand = new PostLoginCommand();

        var validations = ValidationService.Validate(postLoginCommand).Select(v => v.ErrorMessage);

        validations.Should().Contain(Failures.PostLoginCommand.LoginObrigatorio);
        validations.Should().Contain(Failures.PostLoginCommand.PasswordObrigatorio);
    }

    [Fact]
    [TestPriority(2)]
    public async Task PostLoginCommandUsuarioInvalidoRequest()
    {
        var result = await _mediator.Send<Result>(new PostLoginCommand()
        {
            CodigoFilial = 1,
            CodigoEmpresa = 2,
            Login = "Teste",
            Password = "Teste"
        });

        var failures = result.GetFailures().Select(a => a.Error);

        failures.Should().NotBeNullOrEmpty();
        failures.Should().Contain(Failures.Authentication.LoginSenhaInvalido);
    }
}