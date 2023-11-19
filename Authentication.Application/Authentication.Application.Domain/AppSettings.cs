namespace Authentication.Application.Domain;

public class AppSettings
{
    public Logging Logging { get; set; }

    public Connectionstrings ConnectionStrings { get; set; }

    public Jwt Jwt { get; set; }

    public Messages Messages { get; set; } = new Messages();

    public Swagger Swagger { get; set; }

    public bool FilterExceptions { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string System { get; set; }

    public string Default { get; set; }

    public string MicrosoftAspNetCore { get; set; }

    public string SerilogDb { get; set; }
}

public class Connectionstrings
{
    public string DefaultConnection { get; set; }
}

public class Jwt
{
    public string Key { get; set; }

    public int ExpireInMinutes { get; set; }
}

public class Swagger
{
    public string FlowLogin { get; set; }
}

public class Messages
{
    public string Forbidden { get; set; } = "Não autorizado. Credenciais fornecidas ausentes, inválidas ou expiradas";

    public string Unauthorized { get; set; } = "Acesso negado. Você não tem permissões suficientes para acessar esta API";

    public string InternalServerError { get; set; } = "Algo inesperado aconteceu. Por favor contate o administrador do sistema.";

    public string BadRequest { get; set; } = "Não foi possível processar a requisição. A mesma se encontra em um formato inválido";
}