using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
public class Cliente : BaseEntity<Cliente>
{
    public Cliente(string cpf, string nome, DateTime? dataNascimento, string telefone)
    {
        Ensure(nome).ForContext(c => c.Nome)
            .NotNullOrEmpty(ClienteFailures.NomeObrigatorio);

        Cpf = cpf;
        Nome = nome;
        DataNascimento = dataNascimento;
        Telefone = telefone;
    }

    public string Cpf { get; private set; }

    public string Nome { get; private set; }

    public DateTime? DataNascimento { get; private set; }

    public string Telefone { get; private set; }

    public virtual ICollection<Atendimento> Atendimentos { get; private set; }

    public void UpdateUsuario(string cpf, string nome, DateTime? dataNascimento, string telefone, int situacao)
    {
        Ensure(nome).ForContext(c => c.Nome)
            .NotNullOrEmpty(ClienteFailures.NomeObrigatorio);

        Cpf = cpf;
        Nome = nome;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Situacao = (Situacao)situacao;
    }
}
