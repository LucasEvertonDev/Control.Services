// See https://aka.ms/new-console-template for more information
using ControlServices.Application.Domain;
using ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using ControlServices.Application.Mediator.Commands.Clientes.PostCliente;
using ControlServices.Application.Mediator.Commands.Custos.PostCusto;
using ControlServices.Application.Mediator.Commands.Servicos.PostServico;
using ControlServices.Infra.IoC;
using InventoryControl.Domain.Entities;
using InventoryControl.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection()
    .AddDbContext<ApplicationDbContext>();

var appSettings = new ControlServices.Application.Domain.AppSettings
{
    ConnectionStrings = new ControlServices.Application.Domain.Connectionstrings
    {
        DefaultConnection = "server=localhost;port=3306;userid=root;password=root;database=ControlServicesDb;"
    },
};

services.AddSingleton<AppSettings>(appSettings);
services.AddInfraStructure(appSettings);

var serviceProvider = services.BuildServiceProvider();

var clientes = new List<InventoryControl.Domain.Entities.Cliente>();

var servicos = new List<Servico>();

var atendimentos = new List<Atendimento>();

var custos = new List<Custo>();

Dictionary<int, Guid> clienteKey = new Dictionary<int, Guid>();
Dictionary<int, Guid> servicoKey = new Dictionary<int, Guid>();

using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    clientes = context.Clientes.ToList();

    servicos = context.Servicos.ToList();

    atendimentos = context.Atendimentos.Include(a => a.MapServicosAtendimentos).ToList();

    custos = context.Custo.ToList();
}

foreach (var cliente in clientes)
{
    try
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(new PostClienteCommand
            {
                CPF = string.IsNullOrEmpty(cliente.Cpf) ? null : cliente.Cpf,
                DataNascimento = cliente.DataNascimento ?? DateTime.MinValue,
                Nome = cliente.Nome,
                Telefone = string.IsNullOrEmpty(cliente.Telefone) ? null : cliente.Telefone,
            });

            if (result.HasFailures())
            {
                Console.WriteLine($"Falha => {cliente.Nome} {string.Join(",", result.GetFailures().Select(a => a.Error.message))}");
            }
            else
            {
                clienteKey.Add(cliente.Id, result.GetContent<dynamic>().Id);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro => {cliente.Nome} {ex.Message}");
    }
}

foreach (var servico in servicos)
{
    try
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(new PostServicoCommand
            {
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Situacao = 1
            });

            if (result.HasFailures())
            {
                Console.WriteLine($"Falha => {servico.Nome} {string.Join(",", result.GetFailures().Select(a => a.Error.message))}");
            }
            else
            {
                servicoKey.Add(servico.Id, result.GetContent<dynamic>().Id);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro => {servico.Nome} {ex.Message}");
    }
}

foreach (var custo in custos)
{
    try
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(new PostCustoCommand
            {
                Descricao = custo.Descricao,
                Data = custo.Data,
                Valor = custo.Valor,
            });

            if (result.HasFailures())
            {
                Console.WriteLine($"Falha => {custo.Descricao} {string.Join(",", result.GetFailures().Select(a => a.Error.message))}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro => {custo.Descricao} {ex.Message}");
    }
}

foreach (var atendimento in atendimentos)
{
    try
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(new PostAtendimentoCommand
            {
               Data = atendimento.Data,
               ClienteAtrasado = atendimento.ClienteAtrasado,
               ClienteId = clienteKey[atendimento.ClienteId],
               ObservacaoAtendimento = atendimento.ObservacaoAtendimento,
               Situacao = atendimento.Situacao,
               ValorAtendimento = atendimento.ValorAtendimento.GetValueOrDefault(),
               ValorPago = atendimento.ValorPago,
               MapAtendimentosServicos = atendimento.MapServicosAtendimentos.Select(a => new MapAtendimentoServicoModel
               {
                    ServicoId = servicoKey[a.ServicoId],
                    ValorCobrado = a.ValorCobrado.GetValueOrDefault()
               }).ToList()
            });

            if (result.HasFailures())
            {
                Console.WriteLine($"Falha => {atendimento.Id} {string.Join(",", result.GetFailures().Select(a => a.Error.message))}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro => {atendimento.Id} {ex.Message}");
    }
}