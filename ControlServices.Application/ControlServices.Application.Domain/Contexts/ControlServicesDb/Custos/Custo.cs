namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
public class Custo : BaseEntity<Custo>
{
    public Custo(DateTime data, decimal valor, string descricao)
    {
        Ensure(data).ForContext(c => c.Data)
            .NotNull(CustoFailures.DataObrigatoria);
        Ensure(valor).ForContext(c => c.Valor)
           .NotNull(CustoFailures.ValorObrigatório);

        Data = data;
        Valor = valor;
        Descricao = descricao;
    }

    public DateTime Data { get; private set; }

    public decimal Valor { get; private set; }

    public string Descricao { get; private set; }

    public void UpdateCusto(DateTime data, decimal valor, string descricao)
    {
        Ensure(data).ForContext(c => c.Data)
            .NotNull(CustoFailures.DataObrigatoria);
        Ensure(valor).ForContext(c => c.Valor)
          .NotNull(CustoFailures.ValorObrigatório);
        Data = data;
        Valor = valor;
        Descricao = descricao;
    }
}
