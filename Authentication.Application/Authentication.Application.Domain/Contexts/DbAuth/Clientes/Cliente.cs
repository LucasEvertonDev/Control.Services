namespace Authentication.Application.Domain.Contexts.DbAuth.Clientes;
public class Cliente : BaseEntity<Cliente>
{
    public Cliente(string cpf, string nome, DateTime? dataNascimento, string telefone)
    {
        Cpf = cpf;
        Nome = nome;
        DataNascimento = dataNascimento;
        Telefone = telefone;
    }

    public string Cpf { get; private set; }

    public string Nome { get; private set; }

    public DateTime? DataNascimento { get; private set; }

    public string Telefone { get; private set; }
}
