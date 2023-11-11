using Authentication.Application.Mediator.Structure.Extensions;
using FluentValidation;

namespace Authentication.Application.Mediator.Commands.PostLogin;

public class PostLoginCommandValidator : AbstractValidator<PostLoginCommand>
{
    public PostLoginCommandValidator()
    {
        RuleFor(c => c.Email).NotNullOrEmpty().WithError(UsuarioFailures.EmailObrigatorio).EmailAddress().WithError(UsuarioFailures.EmailInvalido);

        RuleFor(c => c.Senha).NotNullOrEmpty().WithError(UsuarioFailures.SenhaObrigatoria);

        When(c => !string.IsNullOrWhiteSpace(c.Senha), () =>
        {
            RuleFor(c => c.Senha.Length).GreaterThanOrEqualTo(8).WithError(UsuarioFailures.SenhaDeveTer8Caracteres);
        });
    }
}
