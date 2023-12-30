namespace Authentication.Application.Domain.Contexts.DbAuth.Servicos;
public class Servico : BaseEntity<Servico>
{
    public Servico(string nome, string descricao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        this.Nome = nome;
        this.Descricao = descricao;
    }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public Servico UpdateServico(string nome, string descricao)
    {
        Ensure(nome).ForContext(servico => servico.Nome)
            .NotNullOrEmpty(ServicoFailures.NomeObrigatorio);

        this.Nome = nome;
        this.Descricao = descricao;
        return this;
    }
}
