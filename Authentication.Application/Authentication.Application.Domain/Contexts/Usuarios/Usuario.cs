namespace Authentication.Application.Domain.Contexts.Usuarios;
public class Usuario : BaseEntity<Usuario>
{
    public Usuario(string email, string senha, string chave)
    {
        Email = email;
        Senha = senha;
        Chave = chave;
        UltimoAcesso = DateTime.Now;
    }

    public string Email { get; private set; }

    public string Senha { get; private set; }

    public string Chave { get; private set; }

    public DateTime UltimoAcesso { get; private set; }
}
