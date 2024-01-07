namespace ControlServices.Infra.Data.Structure.Exceptions;
public class AuditLogException(string message) : System.Exception($"Não foi possível registrar o log de auditoria. => {message}")
{
}
