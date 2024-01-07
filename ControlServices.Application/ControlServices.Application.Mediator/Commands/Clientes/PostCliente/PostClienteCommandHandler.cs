using ControlServices.Application.Domain.Contexts.DbAuth.Clientes;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Clientes.PostCliente;
public class PostClienteCommandHandler(
    IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PostClienteCommand, Result>
{
    public async Task<Result> Handle(PostClienteCommand request, CancellationToken cancellationToken)
    {
        var clienteDb = await UnitOfWork.ClienteRepository.FirstOrDefaultAsync(
            where: cliente => cliente.Nome == request.Nome,
            cancellationToken: cancellationToken);

        if (clienteDb != null)
        {
            return Result.Failure<PostClienteCommandHandler>(ClienteFailures.ClienteJaCadastrado);
        }

        var cliente = new Cliente(
            cpf: request.CPF,
            nome: request.Nome,
            dataNascimento: request.DataNascimento,
            telefone: request.Telefone);

        if (cliente.HasFailures())
        {
            return Result.Failure<PostClienteCommandHandler>(cliente);
        }

        await UnitOfWork.ClienteRepository.CreateAsync(
            domain: cliente,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
