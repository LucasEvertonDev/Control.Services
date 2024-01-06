using Authentication.Application.Domain.Contexts.DbAuth.Atendimentos;
using Authentication.Application.Domain.Contexts.DbAuth.Clientes;
using Authentication.Application.Domain.Contexts.DbAuth.Custos;
using Authentication.Application.Domain.Contexts.DbAuth.MapAtendimentosServicos;
using Authentication.Application.Domain.Contexts.DbAuth.Servicos;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Structure.Repositories;

namespace Authentication.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkRepos
{
    IUsuarioRepository UsuarioRepository { get; }

    IRepository<Cliente> ClienteRepository { get; }

    IRepository<Servico> ServicoRepository { get; }

    IRepository<Custo> CustoRepository { get; }

    IRepository<Atendimento> AtendimentoRepository { get; }

    IRepository<MapAtendimentoServico> MapAtendimentoServicoRepository { get; }
}
