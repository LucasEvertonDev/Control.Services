using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios;
using ControlServices.Application.Domain.Plugins.Cryptography;
using ControlServices.Tests.Structure;
using ControlServices.Tests.Structure.Attribute;
using ControlServices.Tests.Structure.Factorys;
using ControlServices.WebApi;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Notification.Extensions;

namespace ControlServices.Tests.Tests.UsuarioTests;
public class UsuarioDomainTest : BaseTestInMemoryDb
{
    private readonly IPasswordHash _passwordHash;

    public UsuarioDomainTest(IntegrationTestInMemoryDbFactory<Program> integrationTestFactory)
    : base(integrationTestFactory)
    {
        _passwordHash = ServiceProvider.GetRequiredService<IPasswordHash>();
    }

    [Fact]
    [TestPriority(0)]
    public void Create_User_Sucesso()
    {
        var chaveHash = _passwordHash.GeneratePasswordHash();
        var usuario = new Usuario(
            nome: new Faker().Person.FullName,
            email: new Faker().Person.Email,
            senha: _passwordHash.EncryptPassword(new Faker().Internet.Password(), chaveHash),
            chave: chaveHash);

        usuario.HasFailures().Should()
            .BeFalse();
    }

    [Fact]
    [TestPriority(2)]
    public void Create_User_Error()
    {
        var usuario = new Usuario(
            nome: null,
            email: null,
            senha: string.Empty,
            chave: string.Empty);

        usuario.GetFailures().Should()
            .NotBeNullOrEmpty()
            .And.Contain(UsuarioFailures.NomeObrigatorio)
            .And.Contain(UsuarioFailures.EmailObrigatorio)
            .And.Contain(UsuarioFailures.ChaveHashObrigatoria)
            .And.Contain(UsuarioFailures.SenhaObrigatoria);
    }
}