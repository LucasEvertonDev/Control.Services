using ControlServices.Application.Domain.Contexts.DbAuth.Atendimentos;
using ControlServices.Application.Domain.Contexts.DbAuth.Clientes;
using ControlServices.Application.Domain.Contexts.DbAuth.Custos;
using ControlServices.Application.Domain.Contexts.DbAuth.MapAtendimentosServicos;
using ControlServices.Application.Domain.Contexts.DbAuth.Servicos;
using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios;
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
