namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;

public static class MapAtendimentoServicoFailures
{
    public static readonly FailureModel ServicoObrigatorio = new FailureModel("ServicoObrigatorio", "Serviço é obrigatório");

    public static readonly FailureModel AtendimentoObrigatorio = new FailureModel("AtendimentoObrigatorio", "Atendimento é obrigatório");
}
