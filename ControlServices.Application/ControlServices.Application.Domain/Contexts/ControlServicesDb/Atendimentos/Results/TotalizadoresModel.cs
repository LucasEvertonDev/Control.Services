namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
public class TotalizadoresModel
{
    public int Agendados { get; set; }

    public int Concluidos { get; set; }

    public decimal Lucro { get; set; }

    public decimal Receber { get; set; }
}
