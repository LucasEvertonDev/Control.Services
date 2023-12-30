namespace Authentication.Application.Domain.Contexts.DbAuth.Custos;
public static class CustoFailures
{
    public static readonly FailureModel DataObrigatoria = new("DataObrigatoria", "Data é obrigatória");
    public static readonly FailureModel ValorObrigatório = new("ValorObrigatório", "Valor é obrigatório");
    public static readonly FailureModel ValorNullo = new("ValorNullo", "Valor não pode ser nullo");
}
