namespace Authentication.Infra.Data.Structure.Exceptions;
public class DomainWithFailuresException : System.Exception
{
    public DomainWithFailuresException(string message)
        : base(message)
    {
    }
}
