using Authentication.Application.Mediator.Structure.Extensions;
using FluentValidation;

namespace Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;
public class PutUsuarioCommandValidator : AbstractValidator<PutUsuarioCommand>
{
    public PutUsuarioCommandValidator()
    {
        RuleFor(c => c.Body.Nome).NotNullOrEmpty().WithError(UsuarioFailures.NomeObrigatorio);

        RuleFor(c => c.Body.Email).NotNullOrEmpty().WithError(UsuarioFailures.EmailObrigatorio).EmailAddress().WithError(UsuarioFailures.EmailInvalido);
    }
}
