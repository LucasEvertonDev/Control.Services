using Authentication.Application.Domain;
using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;
using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;
using Authentication.Tests.Structure;
using Authentication.Tests.Structure.Apis;
using Authentication.Tests.Structure.Attribute;
using Authentication.Tests.Structure.Factorys;
using Authentication.WebApi;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Notification.Notifications.Enum;

namespace Authentication.Tests.Tests.UsuarioTests;
public class UsuarioControllerTest : BaseTestInMemoryDb
{
    private readonly IUsuarioApi _usuarioApi;

    private readonly IAuthApi _authApi;
    private readonly AppSettings _appSettings;
    private readonly (string email, string senha) _usuario;

    public UsuarioControllerTest(IntegrationTestInMemoryDbFactory<Program> integrationTestFactory)
    : base(integrationTestFactory)
    {
        _usuarioApi = InstanceApi<IUsuarioApi>();
        _authApi = InstanceApi<IAuthApi>();
        _appSettings = ServiceProvider.GetService<AppSettings>();
        _usuario = CriaUsuario();
    }

    [Fact]
    [TestPriority(0)]
    public async Task Create_User_Sucesso()
    {
        var response = await _usuarioApi.Post(new PostUsuarioCommand
        {
            Email = _usuario.email,
            Senha = _usuario.senha
        });

        response.Success.Should()
            .BeTrue();

        response.Error.Should()
            .BeNull();
    }

    [Fact]
    [TestPriority(1)]
    public async Task Login_Sucesso()
    {
        await _usuarioApi.Post(new PostUsuarioCommand
        {
            Email = _usuario.email,
            Senha = _usuario.senha
        });

        var response = await _authApi.Login(new PostLoginCommand
        {
            Email = _usuario.email,
            Senha = _usuario.senha
        });

        response.Success.Should()
            .BeTrue();

        response.Error.Should()
            .BeNull();
    }

    [Fact]
    [TestPriority(2)]
    public async Task Login_Erro_Requisicao()
    {
        var response = await _authApi.Login(new PostLoginCommand());

        response.Success.Should()
            .BeFalse();

        response.Error.Should()
            .NotBeNull();

        response.Error.Messages.Should()
            .Contain(_appSettings.Messages.BadRequest);

        response.Error.Details.Should()
            .NotBeEmpty()
            .And.ContainKey(MemberName<PostLoginCommand>(login => login.Email))
            .And.ContainKey(MemberName<PostLoginCommand>(login => login.Senha));

        response.Error.Details[MemberName<PostLoginCommand>(login => login.Email)].Should()
            .Contain(UsuarioFailures.EmailObrigatorio.message);

        response.Error.Details[MemberName<PostLoginCommand>(login => login.Senha)].Should()
            .Contain(UsuarioFailures.SenhaObrigatoria.message);
    }

    [Fact]
    [TestPriority(3)]
    public async Task Login_Erro_Usuario_Senha_Invalidos()
    {
        var response = await _authApi.Login(new Faker<PostLoginCommand>()
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Senha, f => f.Internet.Password(8)));

        response.Success.Should()
            .BeFalse();

        response.Error.Should()
            .NotBeNull();

        response.Error.Messages.Should()
            .Contain(UsuarioFailures.EmailSenhaInvalidos.message);

        response.Error.Details[nameof(NotificationType.BusinessNotification)].Should()
            .NotBeEmpty()
            .And.Contain(UsuarioFailures.EmailSenhaInvalidos.message);
    }

    [Fact]
    [TestPriority(4)]
    public async Task Refresh_Token_Sucesso()
    {
        await _usuarioApi.Post(new PostUsuarioCommand
        {
            Email = _usuario.email,
            Senha = _usuario.senha
        });

        var responseToken = await _authApi.Login(new PostLoginCommand
        {
            Email = _usuario.email,
            Senha = _usuario.senha
        });

        var response = await _authApi.RefreshToken(
            bearerToken: $"Bearer {responseToken.Content.AccessToken}",
            request: new PostRefreshTokenCommand());

        response.Success.Should()
            .BeTrue();
    }

    [Fact]
    [TestPriority(5)]
    public async Task Refresh_Token_Erro()
    {
        var response = await _authApi.RefreshToken(
            bearerToken: $"Bearer {new Faker().Lorem.Text}",
            request: new PostRefreshTokenCommand());

        response.Success.Should()
            .BeFalse();

        response.Error.Messages.Should()
            .NotBeNull()
            .And.Contain(_appSettings.Messages.Unauthorized);

        response.Error.Details[nameof(_appSettings.Messages.Unauthorized)].Should()
            .NotBeEmpty()
            .And.Contain(_appSettings.Messages.Unauthorized);
    }
}