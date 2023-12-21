namespace Authentication.Application.Mediator.Commands.Clientes.PostCliente;
public class PostClienteCommand : IRequest<Result>, IHandler<PostClienteCommandHandler>
{
    public string CPF { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public string Telefone { get; set; }
}
