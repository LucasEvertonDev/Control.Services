using ControlServices.Application.Mediator.Structure.Extensions;
using FluentValidation;

namespace ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommandValidator : AbstractValidator<PostUsuarioCommand>
{
    public PostUsuarioCommandValidator()
    {
        RuleFor(c => c.Nome).NotNullOrEmpty().WithError(UsuarioFailures.NomeObrigatorio);

        RuleFor(c => c.Email).NotNullOrEmpty().WithError(UsuarioFailures.EmailObrigatorio).EmailAddress().WithError(UsuarioFailures.EmailInvalido);

        RuleFor(c => c.Senha).NotNullOrEmpty().WithError(UsuarioFailures.SenhaObrigatoria);

        When(c => !string.IsNullOrWhiteSpace(c.Senha), () =>
        {
            RuleFor(c => c.Senha.Length).GreaterThanOrEqualTo(8).WithError(UsuarioFailures.SenhaDeveTer8Caracteres).OverridePropertyName(c => c.Senha);
        });
    }
}
