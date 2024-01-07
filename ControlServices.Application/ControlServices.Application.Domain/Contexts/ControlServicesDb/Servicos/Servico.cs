using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
public class Servico : BaseEntity<Servico>
{
    public Servico(string nome, string descricao, Situacao situacao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        Nome = nome;
        Descricao = descricao;
        Situacao = situacao;
    }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<MapAtendimentoServico> MapAtendimentoServicos { get; private set; }

    public Servico UpdateServico(string nome, string descricao, Situacao situacao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        Nome = nome;
        Descricao = descricao;
        Situacao = situacao;
        return this;
    }
}
