using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Results;
public class CustosXLucroModel
{
    public string Chave { get; set; }

    public ResultadoModel Resultado { get; set; }

    public static IEnumerable<CustosXLucroModel> GetCustosPorLucro(List<Atendimento> atendimentos, List<Custo> custos)
    {
        var atendimentosAgrupados = atendimentos.Where(atendimento => atendimento.Data <= DateTime.Now.Date).GroupBy(atendimento => new { atendimento.Data.Month, atendimento.Data.Year });
        var custosAgrupados = custos.GroupBy(custo => new { custo.Data.Month, custo.Data.Year });

        return atendimentosAgrupados.Select(atendimentoAgrupado =>
        {
            var custoAgrupado = custosAgrupados.Where(custoG => custoG.Key.ToString() == atendimentoAgrupado.Key.ToString());

            return new CustosXLucroModel
            {
                Chave = $"{atendimentoAgrupado.Key.Month.ToString("00")}/{atendimentoAgrupado.Key.Year}",
                Resultado = new ResultadoModel
                {
                    Custo = custoAgrupado.Sum(g => g.Sum(custo => custo.Valor)),
                    Ganho = atendimentoAgrupado.Sum(custo => custo.ValorPago.GetValueOrDefault()),
                    Lucro = atendimentoAgrupado.Sum(custo => custo.ValorPago.GetValueOrDefault()) - custoAgrupado.Sum(g => g.Sum(custo => custo.Valor))
                }
            };
        });
    }
}

public class ResultadoModel
{
    public decimal Ganho { get; set; }

    public decimal Lucro { get; set; }

    public decimal Custo { get; set; }
}