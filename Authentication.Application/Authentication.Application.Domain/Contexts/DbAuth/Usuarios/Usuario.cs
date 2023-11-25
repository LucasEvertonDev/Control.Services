namespace Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
public class Usuario : BaseEntity<Usuario>
{
    public Usuario(string email, string senha, string chave)
    {
        Ensure(email).ForContext(u => u.Email)
            .NotNullOrEmpty(UsuarioFailures.EmailObrigatorio)
            .EmailAddress(UsuarioFailures.EmailInvalido);

        Ensure(senha).ForContext(u => u.Senha)
            .NotNullOrEmpty(UsuarioFailures.SenhaObrigatoria);

        Ensure(chave).ForContext(u => u.Chave)
            .NotNullOrEmpty(UsuarioFailures.ChaveHashObrigatoria);

        Email = email;
        Senha = senha;
        Chave = chave;
        UltimoAcesso = DateTime.Now;
    }

    public string Email { get; private set; }

    public string Senha { get; private set; }

    public string Chave { get; private set; }

    public DateTime UltimoAcesso { get; private set; }

    public Usuario SetUltimoAcesso()
    {
        UltimoAcesso = DateTime.Now;

        return this;
    }
}
