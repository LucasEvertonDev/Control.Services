namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
public static class ServicoFailures
{
    public static readonly FailureModel ServicoNaoEncontrado = new("ServicoNaoEncontrado", "Não foi possível recuperar o serviço informado.");

    public static readonly FailureModel ServicoDuplicado = new("ServicoDuplicado", "Serviço duplicado");

    public static readonly FailureModel NomeObrigatorio = new("NomeObrigatorio", "Nome é obrigatório.");
}
