namespace ControlServices.Application.Domain.Contexts.DbAuth.Usuarios;
public class Usuario : BaseEntity<Usuario>
{
    public Usuario(string nome, string email, string senha, string chave)
    {
        Ensure(email).ForContext(u => u.Email)
            .NotNullOrEmpty(UsuarioFailures.EmailObrigatorio)
            .EmailAddress(UsuarioFailures.EmailInvalido);

        Ensure(senha).ForContext(u => u.Senha)
            .NotNullOrEmpty(UsuarioFailures.SenhaObrigatoria);

        Ensure(chave).ForContext(u => u.Chave)
            .NotNullOrEmpty(UsuarioFailures.ChaveHashObrigatoria);

        Ensure(nome).ForContext(u => u.Nome)
            .NotNullOrEmpty(UsuarioFailures.NomeObrigatorio);

        Nome = nome;
        Email = email;
        Senha = senha;
        Chave = chave;
        UltimoAcesso = DateTime.Now;
    }

    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string Senha { get; private set; }

    public string Chave { get; private set; }

    public DateTime UltimoAcesso { get; private set; }

    public Usuario SetUltimoAcesso()
    {
        UltimoAcesso = DateTime.Now;

        return this;
    }

    public void AtualizaUsuario(string nome, string email)
    {
        Ensure(email).ForContext(u => u.Email)
           .NotNullOrEmpty(UsuarioFailures.EmailObrigatorio)
           .EmailAddress(UsuarioFailures.EmailInvalido);

        Ensure(nome).ForContext(u => u.Nome)
            .NotNullOrEmpty(UsuarioFailures.NomeObrigatorio);

        Nome = nome;
        Email = email;
    }
}
