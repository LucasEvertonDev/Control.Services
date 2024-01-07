using System.Text.Json.Serialization;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios.Results;

public class TokenModel
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; private set; } = "bearer";

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("expire_date")]
    public DateTime ExpireDate { get; set; }
}
