using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;
using ControlServices.Application.Domain.Structure.Repositories;

namespace ControlServices.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkRepos
{
    IUsuarioRepository UsuarioRepository { get; }

    IRepository<Cliente> ClienteRepository { get; }

    IRepository<Servico> ServicoRepository { get; }

    IRepository<Custo> CustoRepository { get; }

    IAtendimentoRepository AtendimentoRepository { get; }

    IRepository<MapAtendimentoServico> MapAtendimentoServicoRepository { get; }
}
