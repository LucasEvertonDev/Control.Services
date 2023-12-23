namespace Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;

public class TokenModel
{
    public string TokenType { get; private set; } = "bearer";

    public string AccessToken { get; set; }

    public int ExpiresIn { get; set; }

    public DateTime ExpireDate { get; set; }
}
