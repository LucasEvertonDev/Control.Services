﻿using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
public class MapAtendimentoServico() : BaseEntity<MapAtendimentoServico>
{
    // Toda vez que se recebe uma entidade deve ter um construtor vazio para o entity entender
    public MapAtendimentoServico(Servico servico, Atendimento atendimento, decimal? valorCobrado)
        : this()
    {
        Ensure(servico).ForContext(map => map.Servico)
            .NotNull(MapAtendimentoServicoFailures.ServicoObrigatorio);

        Ensure(atendimento).ForContext(map => map.Atendimento)
            .NotNull(MapAtendimentoServicoFailures.AtendimentoObrigatorio)
            .NoFailures();

        Servico = servico;
        Atendimento = atendimento;
        ValorCobrado = valorCobrado;
    }

    public Guid ServicoId { get; set; }

    public Guid AtendimentoId { get; set; }

    public decimal? ValorCobrado { get; set; }

    public virtual Servico Servico { get; set; }

    public virtual Atendimento Atendimento { get; set; }

    public void UpdateMapAtendimento(Servico servico, Atendimento atendimento, decimal? valorCobrado)
    {
        Ensure(servico).ForContext(map => map.Servico)
            .NotNull(MapAtendimentoServicoFailures.ServicoObrigatorio);

        Ensure(atendimento).ForContext(map => map.Atendimento)
            .NotNull(MapAtendimentoServicoFailures.AtendimentoObrigatorio)
            .NoFailures();

        Servico = servico;
        Atendimento = atendimento;
        ValorCobrado = valorCobrado;
    }
}
