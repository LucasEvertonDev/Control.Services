namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
public static class ClienteFailures
{
    public static readonly FailureModel NomeObrigatorio = new("NomeObrigatorio", "Nome é obrigatório");

    public static readonly FailureModel ClienteNaoEncontrado = new("ClienteNaoEncontrado", "Cliente não encontrado");

    public static readonly FailureModel ClienteJaCadastrado = new("ClienteJaCadastrado", "Já existe um cliente cdastrado com o nome informado.");
}
