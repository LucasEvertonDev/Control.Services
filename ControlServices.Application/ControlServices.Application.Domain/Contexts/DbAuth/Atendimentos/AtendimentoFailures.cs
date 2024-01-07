namespace ControlServices.Application.Domain.Contexts.DbAuth.Atendimentos;

public static class AtendimentoFailures
{
    public static readonly FailureModel AtendimentoExistente = new("AtendimentoExistente", "Já existe um atendimento marcado para esse mesmo horário!");

    public static readonly FailureModel ClienteObrigatorio = new("ClienteObrigatorio", "Cliente é obrigatório!");

    public static readonly FailureModel EObrigatorioVincularServico = new("EObrigatorioVincularServico", "É obrigatório ao associar ao menos um serviço");

    public static readonly FailureModel ClienteInvalido = new("ClienteInvalido", "O cliente informado não é um cliente válido.");
}
