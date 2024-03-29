﻿using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
public class ClienteModel : BaseModel
{
    public Guid Id { get; protected set; }

    public string Cpf { get; protected set; }

    public string Nome { get; protected set; }

    public DateTime? DataNascimento { get; protected set; }

    public string Telefone { get; protected set; }

    public int Situacao { get; protected set; }

    public int NumeroAtendimentos { get; protected set; }

    public override ClienteModel FromEntity(IEntity entity)
    {
        var cliente = (Cliente)entity;

        var clienteModel = new ClienteModel()
        {
            Id = cliente.Id,
            Cpf = cliente.Cpf,
            Telefone = cliente.Telefone,
            DataNascimento = cliente.DataNascimento,
            Nome = cliente.Nome,
            Situacao = (int)cliente.Situacao,
            NumeroAtendimentos = cliente.Atendimentos?.Count(atendimento => atendimento.Situacao == Atendimentos.Enuns.SituacaoAtendimento.Concluido) ?? 0
        };

        return clienteModel;
    }
}
