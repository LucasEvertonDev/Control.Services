using Authentication.Application.Domain.Structure.Enuns;

namespace Authentication.Application.Domain.Contexts.DbAuth.Servicos;
public class Servico : BaseEntity<Servico>
{
    public Servico(string nome, string descricao, int situacao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        this.Nome = nome;
        this.Descricao = descricao;
        this.Situacao = (Situacao)situacao;
    }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public Servico UpdateServico(string nome, string descricao, int situacao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        this.Nome = nome;
        this.Descricao = descricao;
        this.Situacao = (Situacao)situacao;
        return this;
    }
}
