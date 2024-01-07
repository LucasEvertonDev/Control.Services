using ControlServices.Application.Domain;
using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios;
using ControlServices.Application.Mediator.Commands.Auth.PostLogin;
using ControlServices.Application.Mediator.Commands.Auth.PostRefreshToken;
using ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;
using ControlServices.Tests.Structure;
using ControlServices.Tests.Structure.Apis;
using ControlServices.Tests.Structure.Attribute;
using ControlServices.Tests.Structure.Factorys;
using ControlServices.WebApi;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Notification.Notifications.Enum;

namespace ControlServices.Tests.Tests.UsuarioTests;
public class UsuarioControllerTest : BaseTestInDatabase
{
    private readonly IUsuarioApi _usuarioApi;

    private readonly IAuthApi _authApi;
    private readonly AppSettings _appSettings;
    private readonly (string nome, string email, string senha) _usuario;

    public UsuarioControllerTest(IntegrationTestInDatabaseFactory<Program> integrationTestFactory)
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
            Nome = _usuario.nome,
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
            Nome = _usuario.nome,
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
            Nome = _usuario.nome,
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